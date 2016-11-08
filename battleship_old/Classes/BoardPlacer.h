#pragma once

#include "Board.h"

class BoardPlacer
{
private:

	Board *_board;
	BoardItem *_currentItem;
	bool _placing;

private:

	void onMouseMove(Event *event)
	{
		if (!_placing)
			return;

		auto mouseEvent = (EventMouse*)event;
		auto position = Vec2(mouseEvent->getCursorX(), mouseEvent->getCursorY());

		position = _board->convertToNodeSpace(position);

		_currentItem->setPosition(position);
	}

public:

	BoardPlacer(Board *board)
	{
		_board = board;
		_currentItem = 0;
		_placing = false;

		auto eventDispatcher = _board->getEventDispatcher();

		auto mouseListener = EventListenerMouse::create();
		mouseListener->onMouseMove = CC_CALLBACK_1(BoardPlacer::onMouseMove, this);

		eventDispatcher->addEventListenerWithSceneGraphPriority(mouseListener, _board);
	}

	static BoardPlacer *create(Board *board)
	{
		return new BoardPlacer(board);
	}

	void beginPlacement(BoardItem *item)
	{
		if (_placing)
			cancelPlacement();

		_placing = true;

		_currentItem = item;
		
		_board->addChild(item);
	}

	void cancelPlacement()
	{
		if (!_placing)
			return;

		_placing = false;

		_currentItem->removeFromParentAndCleanup(false);
		_currentItem = 0;
	}
};
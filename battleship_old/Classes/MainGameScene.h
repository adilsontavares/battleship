#pragma once

#include "Board.h"
#include "BoardPlacer.h"
#include "BoardItems.h"

class MainGame : public Layer
{
private:

	Board *_board;
	BoardPlacer *_boardPlacer;

	CREATE_FUNC(MainGame)

public:

	static Scene *createScene()
	{
		auto scene = Scene::create();
		auto layer = MainGame::create();
		scene->addChild(layer);
		return scene;
	}

	virtual bool init()
	{
		if (!Layer::init())
			return false;

		auto director = Director::getInstance();
		auto winSize = director->getVisibleSize();

		_board = Board::create(10);
		_board->setPosition(winSize * 0.5);
		addChild(_board);

		auto item = BoardItemSubmarine::create();

		_boardPlacer = BoardPlacer::create(_board);
		_boardPlacer->beginPlacement(item);

		return true;
	}
};
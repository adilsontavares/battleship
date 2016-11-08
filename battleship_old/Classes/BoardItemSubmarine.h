#pragma once

#include "BoardItem.h"

class BoardItemSubmarine : public BoardItem
{
public:

	BoardItemSubmarine()
		: BoardItem(BoardItem::Type::Ship, Direction::Horizontal, 1)
	{
	}

	static BoardItemSubmarine *create()
	{
		return new BoardItemSubmarine();
	}
};
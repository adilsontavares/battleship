#pragma once

#include "BoardItem.h"

class BoardItemWater : public BoardItem
{
public:

	BoardItemWater()
		: BoardItem(BoardItem::Type::Water, Direction::Horizontal, 1)
	{
		/*_normalSprite->setSpriteFrame("water-normal.png");
		_hitSprite->setSpriteFrame("water-hit.png");*/
	}

	static BoardItemWater *create()
	{
		return new BoardItemWater();
	} 
};
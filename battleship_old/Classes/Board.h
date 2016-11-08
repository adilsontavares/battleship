#pragma once

#include "BoardItem.h"
#include "BoarditemWater.h"

#include <cocos2d.h>
#include <vector>

USING_NS_CC;

class Board : public Node
{
private:

	BoardItem **_items;
	UINT _size;
	UINT _slotSize;
	UINT _spacing;

	Index getItemsIndex(Index2 index) { return index.x + index.y * _size; }

	void updateItemPosition(BoardItem *item)
	{
		const float offsetX = (_slotSize * 0.5) - (_size * _slotSize + (_size - 1) * _spacing) * 0.5;
		const float offsetY = (_slotSize * 0.5) - (_size * _slotSize + (_size - 1) * _spacing) * 0.5;

		auto index = item->getIndex();
		auto position = Vec2(offsetX + index.x * _slotSize + index.x * _spacing, offsetY + index.y * _slotSize + index.y * _spacing);
		
		item->setPosition(position);
	}

public:

	Board(UINT size)
	{
		_size = size;
		_slotSize = 34;
		_spacing = 4;

		_items = new BoardItem*[_size * _size];

		for (Index i = 0; i < _size * _size; ++i)
			_items[i] = 0;

		for (Index i = 0; i < _size; ++i)
		{
			for (Index j = 0; j < _size; ++j)
			{
				auto item = BoardItemWater::create();
				item->setIndex(Index2(i, j));
				addItem(item);
			}
		}
	}

	static Board *create(UINT size)
	{
		return new Board(size);
	}
	
	~Board()
	{
		delete _items;
	}

	BoardItem *getItemAt(const Index2& index)
	{
		const Index max = _size * _size;
		BoardItem *item;

		for (Index i = 0; i < max; ++i)
		{
			item = _items[i];
			
			if (item && item->intersects(index))
				return item;
		}

		return 0;
	}

	bool containsItem(BoardItem *item)
	{
		if (!item)
			return false;

		auto existingItem = getItemAt(item->getIndex());
		return existingItem == item;
	}

	bool containsItemAt(Index2 index)
	{
		return getItemAt(index) != 0;
	}

	bool moveItem(BoardItem *item, Index2 index)
	{
		if (!removeItem(item))
			return false;

		item->setIndex(index);
		
		return addItem(item);
	}

	bool removeItem(BoardItem *item)
	{
		if (!containsItem(item))
			return false;

		auto index = getItemsIndex(item->getIndex());
		_items[index] = 0;

		item->removeFromParentAndCleanup(false);

		return true;
	}

	bool addItem(BoardItem *item)
	{
		if (containsItem(item))
			return false;

		auto indexes = item->getSlotIndexes();
		for (auto index : indexes)
			if (containsItemAt(index))
				return false;

		auto index = getItemsIndex(item->getIndex());
		_items[index] = item;

		addChild(item);
		updateItemPosition(item);

		return true;
	}
};


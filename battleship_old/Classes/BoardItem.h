#pragma once

#include <cocos2d.h>

#include "Index.h"

USING_NS_CC;

class BoardItem : public Node
{
private:

	static void initialize()
	{
		static bool initialized = false;

		if (!initialized) 
		{
			initialized = true;

			auto cache = SpriteFrameCache::getInstance();
			cache->addSpriteFramesWithFile("boarditems.plist");
		}
	}

public:

	enum Direction
	{
		Horizontal,
		Vertical
	};

	enum Type
	{
		Water,
		Ship
	};

	CC_SYNTHESIZE(Index2, _index, Index);
	CC_SYNTHESIZE(Type, _type, Type);
	CC_SYNTHESIZE(Direction, _direction, Direction);

protected:

	Sprite *_normalSprite;
	Sprite *_hitSprite;

	UINT _size;

	BoardItem(Type type, Direction direction, UINT size)
		: Node()
	{
		assert(size > 0);

		initialize();

		_index = Index2::zero();
		_type = type;
		_direction = direction;
		_size = size;

		_normalSprite = Sprite::createWithSpriteFrameName("boarditems-none-normal.png");
		addChild(_normalSprite);

		_hitSprite = Sprite::createWithSpriteFrameName("boarditems-none-hit.png");
		addChild(_hitSprite);
	}

	BoardItem(Index2 index, Type type, Direction direction, UINT size)
		: BoardItem(type, direction, size)
	{
		_index = index;
	}

public:

	UINT getSize() { return _size; }
	
	void setSize(UINT size) 
	{
		assert(size > 0);
		_size = size;
	}

	std::vector<Index2> getSlotIndexes()
	{
		std::vector<Index2> indexes;

		if (_direction == Direction::Horizontal)
		{
			for (Index i = 0; i < _size; ++i)
				indexes.push_back(Index2(_index.x + i, _index.y));
		}
		else if (_direction == Direction::Vertical)
		{
			for (Index i = 0; i < _size; ++i)
				indexes.push_back(Index2(_index.x, _index.y + i));
		}

		return indexes;
	}

	bool intersects(Index2 index)
	{
		if (index == _index)
			return true;

		if (_direction == Direction::Horizontal)
			return index.y == _index.y && index.x >= _index.x && index.x < (_index.x + _size);

		if (_direction == Direction::Vertical)
			return index.x == _index.x && index.y >= _index.y && index.y < (_index.y + _size);

		return false;
	}
};


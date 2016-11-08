#pragma once

typedef unsigned int Index;

class Index2
{
public:

	Index x;
	Index y;

	Index2()
	{
		x = 0;
		y = 0;
	}

	Index2(Index x, Index y)
	{
		this->x = x;
		this->y = y;
	}

	bool operator==(const Index2& index)
	{
		return index.x == x && index.y == y;
	}

	bool operator!=(const Index2& index)
	{
		return index.x != x || index.y != y;
	}

	static Index2 zero()
	{
		return Index2(0, 0);
	}
};
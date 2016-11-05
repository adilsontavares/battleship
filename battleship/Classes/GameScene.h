#pragma once

#include <cocos2d.h>

USING_NS_CC;

class GameScene : public Layer
{
	CREATE_FUNC(GameScene)

public:

	static Scene *createScene()
	{
		auto scene = Scene::create();
		auto layer = GameScene::create();
		scene->addChild(layer);
		return scene;
	}

	virtual bool init()
	{
		if (!Layer::init())
			return false;

		auto director = Director::getInstance();
		auto winSize = director->getVisibleSize();

		const int lines = 10;
		const int columns = 10;

		const float deltaX = 40;
		const float deltaY = 40;

		const float offsetX = winSize.width * 0.5 - (deltaX * (columns - 0.5) * 0.5);
		const float offsetY = winSize.height * 0.5 - (deltaY * (lines - 0.5) * 0.5);

		for (int i = 0; i < lines; ++i)
		{
			for (int j = 0; j < columns; ++j)
			{
				auto sprite = Sprite::create("board-slot.png");
				sprite->setPosition(offsetX + i * deltaX, offsetY + j * deltaY);
				addChild(sprite);
			}
		}

		return true;
	}
};


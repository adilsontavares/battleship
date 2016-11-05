#pragma once

#include <cocos2d.h>

USING_NS_CC;

class MainGame : public Layer
{
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

		auto sprite = Sprite::create("HelloWorld.png");
		sprite->setPosition(winSize * 0.5);
		addChild(sprite);

		return true;
	}
};
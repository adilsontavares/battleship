#pragma once

#include <cocos2d.h>

#include "MainGameScene.h"

USING_NS_CC;

class  AppDelegate : private cocos2d::Application
{
public:

    virtual void initGLContextAttrs()
	{
		//set OpenGL context attributions,now can only set six attributions:
		//red,green,blue,alpha,depth,stencil
		GLContextAttrs glContextAttrs = { 8, 8, 8, 8, 24, 8 };
		GLView::setGLContextAttrs(glContextAttrs);
	}

    /**
    @brief    Implement Director and Scene init code here.
    @return true    Initialize success, app continue.
    @return false   Initialize failed, app terminate.
    */
	virtual bool applicationDidFinishLaunching()
	{
		// initialize director
		auto director = Director::getInstance();
		auto glview = director->getOpenGLView();
		if (!glview) {
			glview = GLViewImpl::createWithRect("battleship", Rect(0, 0, 960, 640));
			director->setOpenGLView(glview);
		}

		director->getOpenGLView()->setDesignResolutionSize(960, 640, ResolutionPolicy::SHOW_ALL);

		// turn on display FPS
		director->setDisplayStats(true);

		// set FPS. the default value is 1.0/60 if you don't call this
		director->setAnimationInterval(1.0 / 60);

		FileUtils::getInstance()->addSearchPath("res");

		// create a scene. it's an autorelease object
		auto scene = MainGame::createScene();

		// run
		director->runWithScene(scene);

		return true;
	}

    /**
    @brief  The function be called when the application enter background
    @param  the pointer of the application
    */
	virtual void applicationDidEnterBackground()
	{
		Director::getInstance()->stopAnimation();

		// if you use SimpleAudioEngine, it must be pause
		// SimpleAudioEngine::getInstance()->pauseBackgroundMusic();
	}

    /**
    @brief  The function be called when the application enter foreground
    @param  the pointer of the application
    */
	virtual void applicationWillEnterForeground()
	{
		Director::getInstance()->startAnimation();

		// if you use SimpleAudioEngine, it must resume here
		// SimpleAudioEngine::getInstance()->resumeBackgroundMusic();
	}
};
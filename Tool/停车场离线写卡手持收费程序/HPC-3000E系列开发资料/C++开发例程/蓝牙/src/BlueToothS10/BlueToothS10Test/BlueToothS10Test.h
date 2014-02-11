// BlueToothS10Test.h : PROJECT_NAME 应用程序的主头文件
//

#pragma once

#ifndef __AFXWIN_H__
	#error "在包含此文件之前包含“stdafx.h”以生成 PCH 文件"
#endif

#ifdef STANDARDSHELL_UI_MODEL
#include "resource.h"
#endif

// CBlueToothS10TestApp:
// 有关此类的实现，请参阅 BlueToothS10Test.cpp
//

bool AssertResult(int result);

class CBlueToothS10TestApp : public CWinApp
{
public:
	CBlueToothS10TestApp();
	
// 重写
public:
	virtual BOOL InitInstance();

// 实现

	DECLARE_MESSAGE_MAP()
};

extern CBlueToothS10TestApp theApp;

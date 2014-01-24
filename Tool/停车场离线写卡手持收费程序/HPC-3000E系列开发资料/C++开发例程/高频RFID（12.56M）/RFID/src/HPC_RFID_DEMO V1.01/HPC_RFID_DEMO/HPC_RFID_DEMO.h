// HPC_RFID_DEMO.h : PROJECT_NAME 应用程序的主头文件
//

#pragma once

#ifndef __AFXWIN_H__
	#error "在包含此文件之前包含“stdafx.h”以生成 PCH 文件"
#endif

#ifdef STANDARDSHELL_UI_MODEL
#include "resource.h"
#endif

// CHPC_RFID_DEMOApp:
// 有关此类的实现，请参阅 HPC_RFID_DEMO.cpp
//

class CHPC_RFID_DEMOApp : public CWinApp
{
public:
	CHPC_RFID_DEMOApp();
	
// 重写
public:
	virtual BOOL InitInstance();

// 实现

	DECLARE_MESSAGE_MAP()
};

extern CHPC_RFID_DEMOApp theApp;

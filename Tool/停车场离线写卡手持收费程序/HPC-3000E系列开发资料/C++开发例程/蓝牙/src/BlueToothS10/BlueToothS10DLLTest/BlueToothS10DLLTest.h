// BlueToothS10DLLTest.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#ifdef STANDARDSHELL_UI_MODEL
#include "resource.h"
#endif

// CBlueToothS10DLLTestApp:
// See BlueToothS10DLLTest.cpp for the implementation of this class
//

class CBlueToothS10DLLTestApp : public CWinApp
{
public:
	CBlueToothS10DLLTestApp();
	
// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CBlueToothS10DLLTestApp theApp;

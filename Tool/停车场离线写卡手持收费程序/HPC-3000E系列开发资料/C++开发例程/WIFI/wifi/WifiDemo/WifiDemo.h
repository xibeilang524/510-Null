// WifiDemo.h : main header file for the WIFIDEMO application
//

#if !defined(AFX_WIFIDEMO_H__7F1535FB_D1D1_4B01_B513_3BD25C4D03BD__INCLUDED_)
#define AFX_WIFIDEMO_H__7F1535FB_D1D1_4B01_B513_3BD25C4D03BD__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CWifiDemoApp:
// See WifiDemo.cpp for the implementation of this class
//

class CWifiDemoApp : public CWinApp
{
public:
	CWifiDemoApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CWifiDemoApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CWifiDemoApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft eMbedded Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_WIFIDEMO_H__7F1535FB_D1D1_4B01_B513_3BD25C4D03BD__INCLUDED_)

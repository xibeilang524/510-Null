#if !defined(AFX_WZCCONTEXTDLG_H__0A3DDB31_DF37_48AF_8070_63DB7F179427__INCLUDED_)
#define AFX_WZCCONTEXTDLG_H__0A3DDB31_DF37_48AF_8070_63DB7F179427__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// WZCContextDlg.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CWZCContextDlg dialog

class CWZCContextDlg : public CDialog
{
// Construction
public:
	CWZCContextDlg(CWnd* pParent = NULL);   // standard constructor

// Dialog Data
	//{{AFX_DATA(CWZCContextDlg)
	enum { IDD = IDD_DIALOG_WZC_CONTEXT };
	UINT	m_dwWZCScanTimeOut;
	UINT	m_dwWZCScanTimeoutAssociation;
	DWORD	m_dwWZCScanTimeoutConnect;
	DWORD	m_dwWZCScanTimeoutDisconnect;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CWZCContextDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CWZCContextDlg)
		// NOTE: the ClassWizard will add member functions here
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_WZCCONTEXTDLG_H__0A3DDB31_DF37_48AF_8070_63DB7F179427__INCLUDED_)

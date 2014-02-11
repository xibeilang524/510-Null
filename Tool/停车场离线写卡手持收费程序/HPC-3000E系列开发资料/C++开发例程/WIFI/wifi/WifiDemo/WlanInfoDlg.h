#if !defined(AFX_WLANINFODLG_H__E1E1B185_5FA9_4512_B1ED_C2CB761E6B72__INCLUDED_)
#define AFX_WLANINFODLG_H__E1E1B185_5FA9_4512_B1ED_C2CB761E6B72__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// WlanInfoDlg.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CWlanInfoDlg dialog

class CWlanInfoDlg : public CDialog
{
// Construction
public:
	CWlanInfoDlg(CWnd* pParent = NULL);   // standard constructor

// Dialog Data
	//{{AFX_DATA(CWlanInfoDlg)
	enum { IDD = IDD_DIALOG_WLAN_INFO };
	CString	m_szMac;
	CString	m_szSSID;
	long	m_lRssi;
	CString	m_szPrivacyMode;
	CString	m_szPrivacyKey;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CWlanInfoDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CWlanInfoDlg)
	virtual void OnOK();
	virtual void OnCancel();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_WLANINFODLG_H__E1E1B185_5FA9_4512_B1ED_C2CB761E6B72__INCLUDED_)

// WifiDemoDlg.h : header file
//

#if !defined(AFX_WIFIDEMODLG_H__BBC61B09_91AA_4768_B7D0_2D06F334BEC5__INCLUDED_)
#define AFX_WIFIDEMODLG_H__BBC61B09_91AA_4768_B7D0_2D06F334BEC5__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "WirelessCard.h"

/////////////////////////////////////////////////////////////////////////////
// CWifiDemoDlg dialog

class CWifiDemoDlg : public CDialog
{
// Construction
public:
	BOOL Refresh(void);
	void UpdateSSIDList(void);
	CWifiDemoDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CWifiDemoDlg)
	enum { IDD = IDD_WIFIDEMO_DIALOG };
	CListBox	m_listWirelessNetSSID;
	CString	m_szWirelessCardGUID;
	CString	m_szWirelessNetCounts;
	CString	m_szAssociatedSSID;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CWifiDemoDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;
	CWirelessCard m_WirelessCard;
	// Generated message map functions
	//{{AFX_MSG(CWifiDemoDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnDblclkListWirelessnetSsid();
	afx_msg void OnButtonRefresh();
	afx_msg void OnButtonWzcContext();
	afx_msg void OnTimer(UINT nIDEvent);
	afx_msg void OnButtonRemovePreferredNetworkList();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft eMbedded Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_WIFIDEMODLG_H__BBC61B09_91AA_4768_B7D0_2D06F334BEC5__INCLUDED_)

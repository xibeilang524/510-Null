
// sampleConnectDlg.h : header file
//

#pragma once
#include "nvunifiedcontrolctrl1.h"
#include "afxwin.h"


// CsampleConnectDlg dialog
class CsampleConnectDlg : public CDialog
{
// Construction
public:
	CsampleConnectDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_SAMPLECONNECT_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

protected:
	CString GetResolution(int nResolution);

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	CNvunifiedcontrolctrl1 m_ocxCamera;
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedButton2();
	CListBox m_cbEventList;
	DECLARE_EVENTSINK_MAP()
	void OnMDEventStartNvunifiedcontrolctrl1(long nID, long nMD);
	CEdit m_edIpAddress;
	void OnVideoLossNvunifiedcontrolctrl1(long nID);
	void OnNetworkLossNvunifiedcontrolctrl1(long nID);
	CEdit m_edFPS;
	CEdit m_edResolution;
	CEdit m_edBitRate;
};

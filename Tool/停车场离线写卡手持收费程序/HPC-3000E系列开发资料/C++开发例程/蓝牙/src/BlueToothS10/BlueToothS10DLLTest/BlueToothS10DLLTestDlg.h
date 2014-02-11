// BlueToothS10DLLTestDlg.h : header file
//

#pragma once

// CBlueToothS10DLLTestDlg dialog
class CBlueToothS10DLLTestDlg : public CDialog
{
// Construction
public:
	CBlueToothS10DLLTestDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_BLUETOOTHS10DLLTEST_DIALOG };

	void AppendInfo(CString &str);
	HANDLE m_hThread;
	HANDLE m_hInquire;
	HANDLE m_hExitEvent;
	HANDLE m_hReadEvent;
	HANDLE m_hInquireEvent;

	static DWORD ThreadProc(PVOID pArg);
	static DWORD InquireProc(PVOID pArg);

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	afx_msg void OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/);
#endif
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBnstart();
	afx_msg void OnBnClickedBnend();
	afx_msg void OnBnClickedBnsetrole();
	afx_msg void OnBnClickedBnat();
	afx_msg void OnBnClickedBnnormal();
	afx_msg void OnBnClickedBnfltr();
	afx_msg void OnBnClickedBninq();
	afx_msg void OnDestroy();
	afx_msg void OnBnClickedBnTest();
};

// EM1300ScanDemoDlg.h : 头文件
//

#pragma once
#include "afxwin.h"

#define MAXSIZE    512
#define WM_MYMESSAGE WM_USER + 100
// CEM1300ScanDemoDlg 对话框
class CEM1300ScanDemoDlg : public CDialog
{
// 构造
public:
	CEM1300ScanDemoDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_EM1300SCANDEMO_DIALOG };

	HANDLE  m_Thread;                                                   /*  接收线程句柄                */
	HANDLE  m_hExitThreadEvent;				                            /*  串口接收线程退出事件        */	

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持

// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	afx_msg void OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/);
#endif
	DECLARE_MESSAGE_MAP()

	static DWORD ThreadProc(PVOID pArg);
public:
	afx_msg void OnDestroy();
	CComboBox m_cbPattern;
	CButton m_bnSetPattern;
	CButton m_bnStart;
	CButton m_bnEnd;
	CButton m_bnVersion;
	CButton m_bnClear;
	afx_msg void OnBnClickedBnSetpattern();
	afx_msg void OnBnClickedBnStart();
	afx_msg void OnBnClickedBnEnd();
	afx_msg void OnBnClickedBnVersion();
	CString m_strDisp;
	afx_msg void OnBnClickedBnclear();
	LRESULT OnMyMessage(WPARAM wParam, LPARAM lParam);
	BOOL PreTranslateMessage(MSG* pMsg);
	afx_msg void OnTimer(UINT_PTR nIDEvent);
};

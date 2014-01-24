// Se955ScanDemoDlg.h : 头文件
//

#pragma once


#include "Ue966DLL.h"
#include "afxwin.h"

#define MAXSIZE    512
// CSe955ScanDemoDlg 对话框
class CSe955ScanDemoDlg : public CDialog
{
// 构造
public:
	CSe955ScanDemoDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_SE955SCANDEMO_DIALOG };


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
public:
	afx_msg void OnBnClickedStart();
	afx_msg void OnBnClickedTerminate();
	afx_msg void OnBnClickedStartscan();
	afx_msg void OnBnClickedClosescan();
	afx_msg void OnBnClickedSofttrig();
	afx_msg void OnBnClickedContinuetrig();
	afx_msg void OnDestroy();
	CComboBox m_comboBaud;
	CString m_strdisp;
	CComboBox m_comboCom;
	afx_msg void OnBnClickedClr();
	afx_msg void OnBnClickedVersion();
	HANDLE  m_Thread;                                                   /*  接收线程句柄                */
    HANDLE  m_hExitThreadEvent;				                            /*  串口接收线程退出事件        */	
private:
	static DWORD ThreadProc(PVOID pArg);
};

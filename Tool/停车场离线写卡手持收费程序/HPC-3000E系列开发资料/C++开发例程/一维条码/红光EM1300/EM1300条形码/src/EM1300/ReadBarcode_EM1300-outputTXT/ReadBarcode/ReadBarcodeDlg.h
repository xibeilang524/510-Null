// ReadBarcodeDlg.h : 头文件
//

#pragma once

#include "PicView.h"

#define WM_MY_MESSAGE (WM_USER+100)

enum {
	CLEAR = 0,
	EXIT,
	NOTHING=-1,
};

#define MAXSIZE    512
// CReadBarcodeDlg 对话框
class CReadBarcodeDlg : public CDialog
{
// 构造
public:
	CReadBarcodeDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_READBARCODE_DIALOG };


	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持

// 实现
protected:
	HICON m_hIcon;
	HANDLE m_hThread;    
	HANDLE m_hRecvBroadcastThread;
	HANDLE m_hEnableThread;
	HANDLE m_hExitThreadEvent;
	HANDLE m_hEvent;
	CString exePath;
	CDC memdc;
	CDC m_dcUp;
	CDC m_dcDown;
	CPicView *m_pBnUp;
	CPicView *m_pBnDown;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	afx_msg void OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/);
#endif
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedExit();
	afx_msg void OnBnClickedClear();
	BOOL PreTranslateMessage(MSG* pMsg);
	afx_msg void OnDestroy();
	static DWORD ThreadProc(PVOID pArg);
	static DWORD EnableThreadProc(PVOID pArg);
	static DWORD RecvBroadcastThreadProc(PVOID pArg);
	afx_msg void OnPaint();
	void SelectButton(int num);
	void DeselectButton(int num);
	void Animation(int num);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg LRESULT OnMyMessage(WPARAM wParam,LPARAM lParam);
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	void Show(CString &str);
	bool Print(const CString &barcode);
	void UnicastMsg(CString &msg);
// 	BOOL UnloadDriver(void);
// 	BOOL LoadDriver(void);
	BOOL ExportFile(LPCTSTR lpName, LPCTSTR lpType, LPCTSTR lpFileName);
};

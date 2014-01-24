#pragma once
#include "afxcmn.h"
#include <vector>
#include "BlueToothS10.h"
using namespace std;

// CSearch 对话框

class CSearch : public CDialog
{
	DECLARE_DYNAMIC(CSearch)

public:
	CSearch(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CSearch();

	HANDLE m_hThread;
	HANDLE m_hInquire;
	HANDLE m_hExitEvent;
	HANDLE m_hReadEvent;
	HANDLE m_hInquireEvent;

	vector<BlueToothDevice> m_vcBTDs;

	static DWORD ThreadProc(PVOID pArg);
	static DWORD InquireProc(PVOID pArg);

// 对话框数据
	enum { IDD = IDD_DLG_SEARCH };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	virtual BOOL OnInitDialog();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBnInq();
	afx_msg void OnBnClickedBnInqc();
	afx_msg void OnBnClickedBnBind();
	afx_msg void OnBnClickedBnPair();
	afx_msg void OnBnClickedBnLink();
	afx_msg void OnBnClickedBnDisc();
	afx_msg void OnDestroy();
	CListCtrl m_ListDevices;
protected:
	virtual void OnCancel();
	virtual void OnOK();
};

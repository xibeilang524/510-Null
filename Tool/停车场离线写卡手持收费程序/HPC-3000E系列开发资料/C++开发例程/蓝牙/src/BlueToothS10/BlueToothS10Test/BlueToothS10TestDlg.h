// BlueToothS10TestDlg.h : 头文件
//

#pragma once
#include "Module.h"
#include "Setting.h"
#include "Search.h"
#include "Transport.h"
#include "afxcmn.h"

#define DLG_NUM 4
// CBlueToothS10TestDlg 对话框
class CBlueToothS10TestDlg : public CDialog
{
// 构造
public:
	CBlueToothS10TestDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_BLUETOOTHS10TEST_DIALOG };

	CModule		m_ModuleDlg;
	CSetting	m_SettingDlg;
	CSearch		m_SearchDlg;
	CTransport	m_TransportDlg;
	CDialog *	m_pDlgs[DLG_NUM];

	void InsertTab( CDialog* pDlg, UINT nTemplate, int index, LPCTSTR lpszItem );
	void SelChangeTab();

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
	CTabCtrl m_Funcs;
	afx_msg void OnTcnSelchangeTabfunc(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnDestroy();
};

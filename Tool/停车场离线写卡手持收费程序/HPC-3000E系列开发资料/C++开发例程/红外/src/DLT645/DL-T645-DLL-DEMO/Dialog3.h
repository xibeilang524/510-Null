#pragma once
#include "afxwin.h"
#include "resource.h"

// CDialog3 对话框

class CDialog3 : public CDialog
{
	DECLARE_DYNAMIC(CDialog3)

public:
	CDialog3(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CDialog3();

// 对话框数据
	enum { IDD = IDD_DIALOG3 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void InitDialog();
	afx_msg void OnBnClickedFreeze();
	CString m_sAddress;
	CString m_sDisplay;
	CString m_sFreezeTime;
	CString m_sPwOld;
	afx_msg void OnBnClickedChangeBrd();
	afx_msg void OnBnClickedChangePw();
	afx_msg void OnBnClickedClean();
	afx_msg void OnBnClickedClaenAll();
	afx_msg void OnBnClickedCleanEvent();
	int m_nBaundrate;
	CString m_sPwNew;
	CString m_sDataItem;
	CString m_sEventItem;
	CString m_sConsumer;
};

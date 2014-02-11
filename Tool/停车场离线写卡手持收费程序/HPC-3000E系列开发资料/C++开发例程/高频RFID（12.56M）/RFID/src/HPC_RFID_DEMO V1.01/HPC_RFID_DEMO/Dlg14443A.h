#pragma once
#include "afxwin.h"
#include "resource.h"

// CDlg14443A 对话框

class CDlg14443A : public CDialog
{
	DECLARE_DYNAMIC(CDlg14443A)

public:
	CDlg14443A(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CDlg14443A();

// 对话框数据
	enum { IDD = IDD_DLG_14443A };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBtnReadSn();
	CString m_sDisplay;
	afx_msg void OnBnClickedBtnAuthKey();
	afx_msg void OnBnClickedBtnReadBlock();
	afx_msg void OnBnClickedBtnWriteBlock();
	CString m_sKeyA;
	CString m_sWriteData;
	int m_Block;
	void InitDialog(void);
	afx_msg void OnBnClickedBtnClean2();
};

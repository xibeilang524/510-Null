#pragma once
#include "afxwin.h"


// CSetting 对话框

class CSetting : public CDialog
{
	DECLARE_DYNAMIC(CSetting)

public:
	CSetting(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CSetting();

// 对话框数据
	enum { IDD = IDD_DLG_SETTING };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	virtual BOOL OnInitDialog();
	DECLARE_MESSAGE_MAP()
public:
	CComboBox m_Roles;
	afx_msg void OnBnClickedBnAddr();
	afx_msg void OnBnClickedBnSetname();
	afx_msg void OnBnClickedBnGetname();
	afx_msg void OnBnClickedBnSetrole();
	afx_msg void OnBnClickedBnGetrole();
	afx_msg void OnBnClickedBnSetdc();
	afx_msg void OnBnClickedBnGetdc();
	afx_msg void OnBnClickedBnSetac();
	afx_msg void OnBnClickedBnGetac();
	afx_msg void OnBnClickedBnSetmc();
	afx_msg void OnBnClickedBnGetmc();
protected:
	virtual void OnCancel();
	virtual void OnOK();
};

#pragma once
#include "afxwin.h"
#include "resource.h"

// CDialog1 对话框

class CDialog1 : public CDialog
{
	DECLARE_DYNAMIC(CDialog1)

public:
	CDialog1(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CDialog1();

// 对话框数据
	enum { IDD = IDD_DIALOG1 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void InitDialog();
	afx_msg void OnBnClickedOpenPort();
	int m_nCom;
	afx_msg void OnBnClickedClosePort();
	CString m_sDisplay;
	CButton m_ctrlOpenPort;
	CButton m_ctrlClosePort;
	CComboBox m_ctrlCom;
};

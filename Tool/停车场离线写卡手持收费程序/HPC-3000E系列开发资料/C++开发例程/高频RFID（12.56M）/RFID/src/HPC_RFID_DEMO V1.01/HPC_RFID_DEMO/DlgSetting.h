#pragma once
#include "afxwin.h"
#include "resource.h"

// CDlgSetting 对话框

class CDlgSetting : public CDialog
{
	DECLARE_DYNAMIC(CDlgSetting)

public:
	CDlgSetting(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CDlgSetting();

// 对话框数据
	enum { IDD = IDD_DLG_SETTING };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBtnOpenPort();
	CComboBox m_CtrCom;
	CString m_sDisplay;
	afx_msg void OnBnClickedBtnClosePort();
	CButton m_ctrlClosePort;
	CButton m_ctrlOpenPort;
	void InitDialog(void);
};

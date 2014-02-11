#pragma once
#include "afxwin.h"
#include "resource.h"

// CDlgFeliCa 对话框

class CDlgFeliCa : public CDialog
{
	DECLARE_DYNAMIC(CDlgFeliCa)

public:
	CDlgFeliCa(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CDlgFeliCa();

// 对话框数据
	enum { IDD = IDD_DLG_FELICA };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	void InitDialog(void);
	CString m_sDisplay;
	afx_msg void OnBnClickedBtnReadFelicaSn();
	afx_msg void OnBnClickedBtnClean();
};

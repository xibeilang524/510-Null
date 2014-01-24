// BatteryDemoDlg.h : 头文件
//

#pragma once

// CBatteryDemoDlg 对话框
class CBatteryDemoDlg : public CDialog
{
// 构造
public:
	CBatteryDemoDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_BATTERYDEMO_DIALOG };


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
    CString m_szVoltage;
    afx_msg void OnTimer(UINT_PTR nIDEvent);
    CString m_szACStatus;
    CString m_szBattStatus;
    CString m_szBattCharging;
    CString m_szPercent;
};

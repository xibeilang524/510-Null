// BKLAdjDlg.h : header file
//

#pragma once

#include "BKBrightnessCtl.h"
#include "afxwin.h"
#include "afxcmn.h"
//定义背光控制结构
typedef struct
{
    BOOL                    fBatteryTapOn;          // reg setting - do we turn on when screen/button tapped?
    BOOL                    fExternalTapOn;         // reg setting - do we turn on when screen/button tapped? 
    DWORD                   dwBattTimeout;          // reg setting - we only want this to deal with special cases 
    DWORD                   dwACTimeout;
    DWORD                   dwBrightnessPercent[5]; //背光亮度 百分之几分别对应5个电源管理级别的亮度
} BKL_CTL_INFO;

// CBKLAdjDlg dialog
class CBKLAdjDlg : public CDialog
{
// Construction
public:
	CBKLAdjDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_BKLADJ_DIALOG };


	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

private:

    BKL_CTL_INFO        m_bklCtlInfo;                   //背光信息控制结构

    //根据注册表加载背光控制结构
    void BacklightGetRegSettings(BKL_CTL_INFO *pBKLinfo);
    //根据背光控制结构设置注册表
    void BacklightSetRegSettings(BKL_CTL_INFO *pBKLinfo);
    //获取背光驱动注册表路径
    void GetBackLightDrvRegPath();
    
    CString     m_strBklDrvRegPath;
// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	afx_msg void OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/);
#endif
	DECLARE_MESSAGE_MAP()
private:
    CButton     m_ctlCheckBatteryAutoOff;
    CComboBox   m_ctlComboBatteryTime;
    CComboBox   m_ctlComboACTime;
    CStatic     m_ctlStaticBrightness;
    CSliderCtrl m_ctlSliderBrightness;
    HANDLE      m_hBackLight;
    DWORD       m_dwBrightnessRuntime;
public:
    afx_msg void OnBnClickedCheckBatter();
    afx_msg void OnBnClickedCheckAc();
private:
    CButton m_ctlCheckACAutoOff;
public:
    afx_msg void OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
    afx_msg void OnCbnSelchangeComboBattTime();
    afx_msg void OnCbnSelchangeComboAcTime();
protected:
    virtual void OnCancel();
    virtual void OnOK();
};

// BatteryDemoDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "BatteryDemo.h"
#include "BatteryDemoDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CBatteryDemoDlg 对话框

CBatteryDemoDlg::CBatteryDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CBatteryDemoDlg::IDD, pParent)
    , m_szVoltage(_T(""))
    , m_szACStatus(_T(""))
    , m_szBattStatus(_T(""))
    , m_szBattCharging(_T(""))
    , m_szPercent(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CBatteryDemoDlg::DoDataExchange(CDataExchange* pDX)
{
    CDialog::DoDataExchange(pDX);
    DDX_Text(pDX, IDC_EDIT_VOLATAGE, m_szVoltage);
    DDX_Text(pDX, IDC_STATIC_AC_STATUS, m_szACStatus);
    DDX_Text(pDX, IDC_STATIC_BATT_STATUS, m_szBattStatus);
    DDX_Text(pDX, IDC_STATIC_BATT_STATUS_CHARGING, m_szBattCharging);
    DDX_Text(pDX, IDC_EDIT_PERCENT, m_szPercent);
}

BEGIN_MESSAGE_MAP(CBatteryDemoDlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
    ON_WM_TIMER()
END_MESSAGE_MAP()


// CBatteryDemoDlg 消息处理程序

BOOL CBatteryDemoDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码
	SetTimer(1,100,NULL);
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CBatteryDemoDlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_BATTERYDEMO_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_BATTERYDEMO_DIALOG));
	}
}
#endif


void CBatteryDemoDlg::OnTimer(UINT_PTR nIDEvent)
{
    // TODO: Add your message handler code here and/or call default

    SYSTEM_POWER_STATUS_EX2 powerInfo;                                  /* 系统电源状态结构体           */

    if ( GetSystemPowerStatusEx2( &powerInfo,
                                  sizeof(SYSTEM_POWER_STATUS_EX2),
                                  FALSE) != 0 ) {                       /* 获取系统电源状态信息         */

        m_szVoltage.Format(_T("电压 %dmV"),powerInfo.BatteryVoltage);   /* 显示电池电压                 */
        m_szPercent.Format(_T("电量 %d%%"),powerInfo.BatteryLifePercent);/* 显示电池电量百分比           */

        /*
        *  下面是显示电池充电器的使用状态，当连接充电器后，系统使用
        *  充电器供电。未连接充电器时使用电池供电
        */
        if (powerInfo.ACLineStatus == AC_LINE_UNKNOWN) {
            m_szACStatus = _T("未知电源");
        }else if ( (powerInfo.ACLineStatus&AC_LINE_ONLINE) == AC_LINE_ONLINE ) {
            m_szACStatus = _T("正在使用外部电源");
        }else if ( (powerInfo.ACLineStatus&AC_LINE_OFFLINE) == AC_LINE_OFFLINE ) {
            m_szACStatus = _T("正在使用电池电源");
        } else {
            m_szACStatus = _T("未知电源");
        }

        /*
        *  下面是显示电池级别，即电池的电量状态
        */
        if ( (powerInfo.BatteryFlag == BATTERY_FLAG_UNKNOWN) ) {
            m_szBattStatus = _T("未知的电池级别");
        } else if ( (powerInfo.BatteryFlag&BATTERY_FLAG_HIGH) == BATTERY_FLAG_HIGH ) {
            m_szBattStatus = _T("电池电量高");
        } else if ( (powerInfo.BatteryFlag&BATTERY_FLAG_LOW) == BATTERY_FLAG_LOW ) {
            m_szBattStatus = _T("电池电量不足");
        } else if ( (powerInfo.BatteryFlag&BATTERY_FLAG_CRITICAL) == BATTERY_FLAG_CRITICAL ) {
            m_szBattStatus = _T("电池电量严重不足");
        } else {
            m_szBattStatus = _T("未知的电池级别");
        } 

        /*
        *  下面是显示电池是否正在充电
        */
        if ( (powerInfo.BatteryFlag&BATTERY_FLAG_CHARGING) == BATTERY_FLAG_CHARGING ) {
            m_szBattCharging = _T("正在充电");
        } else {
            if (( powerInfo.ACLineStatus&AC_LINE_ONLINE) == AC_LINE_ONLINE ) {
                m_szBattCharging = _T("使用外部电源");
            } else {
                m_szBattCharging = _T("使用电池");
            }
        }



        UpdateData(FALSE);
    }

    CDialog::OnTimer(nIDEvent);
}

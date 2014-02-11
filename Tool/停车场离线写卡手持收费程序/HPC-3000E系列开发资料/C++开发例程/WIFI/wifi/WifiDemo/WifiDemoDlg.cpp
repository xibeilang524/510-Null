// WifiDemoDlg.cpp : implementation file
//

#include "stdafx.h"
#include "WifiDemo.h"
#include "WifiDemoDlg.h"
#include "WlanInfoDlg.h"
#include "WZCContextDlg.h"
#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CWifiDemoDlg dialog

CWifiDemoDlg::CWifiDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CWifiDemoDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CWifiDemoDlg)
	m_szWirelessCardGUID = _T("");
	m_szWirelessNetCounts = _T("");
	m_szAssociatedSSID = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CWifiDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CWifiDemoDlg)
	DDX_Control(pDX, IDC_LIST_WIRELESSNET_SSID, m_listWirelessNetSSID);
	DDX_Text(pDX, IDC_EDIT_WIRELESSCARD_GUID, m_szWirelessCardGUID);
	DDX_Text(pDX, IDC_EDIT_WIRELESSNET_COUNTS, m_szWirelessNetCounts);
	DDX_Text(pDX, IDC_EDIT_ASSOCIATED_SSID, m_szAssociatedSSID);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CWifiDemoDlg, CDialog)
	//{{AFX_MSG_MAP(CWifiDemoDlg)
	ON_LBN_DBLCLK(IDC_LIST_WIRELESSNET_SSID, OnDblclkListWirelessnetSsid)
	ON_BN_CLICKED(IDC_BUTTON_REFRESH, OnButtonRefresh)
	ON_BN_CLICKED(IDC_BUTTON_WZC_CONTEXT, OnButtonWzcContext)
	ON_WM_TIMER()
	ON_BN_CLICKED(IDC_BUTTON_REMOVE_PREFERRED_NETWORK_LIST, OnButtonRemovePreferredNetworkList)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CWifiDemoDlg message handlers

BOOL CWifiDemoDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	CenterWindow(GetDesktopWindow());	// center to the hpc screen

	// TODO: Add extra initialization here
	if (Refresh() == FALSE) {
		PostQuitMessage(0);
		return TRUE;
	}
	SetTimer(1,3000,NULL);
	return TRUE;  // return TRUE  unless you set the focus to a control
}

/*********************************************************************************************************
** Function name:           Refresh
**
** Descriptions:            刷新显示信息
**
** input parameters:        None
** output parameters:       None
** Returned value:          TRUE：成功，FALSE：失败 
*********************************************************************************************************/
BOOL CWifiDemoDlg::Refresh()
{
	/*
	* step 1,获取网卡名称
	*/
	TCHAR szWirelessCardGuid[MAX_PATH];
	if( m_WirelessCard.GetFirstWirelessNetworkCard(szWirelessCardGuid,MAX_PATH) == FALSE) {
		MessageBox(_T("未找到无线网卡"));
		return FALSE;
	}
	m_szWirelessCardGUID = szWirelessCardGuid;
	
	/*
	* step 2,查询网卡
	*/
	if(m_WirelessCard.Query() == FALSE) {
		MessageBox(_T("查询网卡信息失败"));
		return FALSE;
	}
	
	/*
	* step 3,获取搜索到的无线网络个数
	*/
	m_szWirelessNetCounts.Format(_T("%d"),m_WirelessCard.GetSSIDCounts());
	
	/*
	* step 4,判断是否连接到网络
	*/
	TCHAR szAssociatedSSID[33];
	if ( m_WirelessCard.IsAssociated(szAssociatedSSID,33) == TRUE ) {
		m_szAssociatedSSID = szAssociatedSSID;
	}else{
		m_szAssociatedSSID = _T("未连接");
	}
	UpdateData(FALSE);

	/* 
	* step 5,显示搜索到的无线网络
	*/
	UpdateSSIDList();
	
	return TRUE;
}

/*********************************************************************************************************
** Function name:           UpdateSSIDList
**
** Descriptions:            显示搜索到的网络
**
** input parameters:        None
** output parameters:       None
** Returned value:          None 
*********************************************************************************************************/
void CWifiDemoDlg::UpdateSSIDList()
{
	int iSel = m_listWirelessNetSSID.GetCurSel();
	m_listWirelessNetSSID.ResetContent();
	
	DWORD dwCounts = m_WirelessCard.GetSSIDCounts();
	WZC_WLAN_CONFIG cfg;
	TCHAR szSSID[33];

	for (DWORD i = 0; i < dwCounts; i++) {
		m_WirelessCard.GetWlanConfig(i,&cfg);
		m_WirelessCard.GetWlanSSID(&cfg,szSSID,33);
		m_listWirelessNetSSID.AddString(szSSID);
	}
	if (iSel != LB_ERR) {
		m_listWirelessNetSSID.SetCurSel(iSel);
	}
}

/*********************************************************************************************************
** Function name:           OnDblclkListWirelessnetSsid
**
** Descriptions:            双击ListBox时显示网络信息
**
** input parameters:        None
** output parameters:       None
** Returned value:          None 
*********************************************************************************************************/
void CWifiDemoDlg::OnDblclkListWirelessnetSsid() 
{
	// TODO: Add your control notification handler code here
	int iSel = m_listWirelessNetSSID.GetCurSel();
	if (iSel == LB_ERR) {
		return;
	}

	CWlanInfoDlg *pDlg = new CWlanInfoDlg;
	if (pDlg == NULL) {
		return;
	}

	WZC_WLAN_CONFIG *pCfg = new WZC_WLAN_CONFIG;
	if (pCfg == NULL) {
		return;
	}

	TCHAR szSSID[33];
	TCHAR szMac[32];
	if ( m_WirelessCard.GetWlanConfig(iSel,pCfg) == FALSE ) {           /* 得到当前的网络配置           */  
		return;
	}
	m_WirelessCard.GetWlanSSID(pCfg,szSSID,33);                         /* 获取SSID                     */
	pDlg->m_szSSID = szSSID;
	
	m_WirelessCard.GetWlanMacAddress(pCfg,szMac,32);                    /* 取得MAC地址                  */
	pDlg->m_szMac = szMac;
	
	m_WirelessCard.GetWlanRssi(pCfg,&pDlg->m_lRssi);                    /* 取得信号强度                 */
	
	TCHAR szPrivacyMode[36];
	m_WirelessCard.GetWlanPrivacyMode(pCfg,szPrivacyMode,36);           /* 取得加密模式                 */
	pDlg->m_szPrivacyMode = szPrivacyMode;

// 	if (pCfg->Privacy == Ndis802_11WEPEnabled) {
// 		pDlg->m_szPrivacyKey = _T("输入WEP密码");                       /* 现在支持共享网络的WEP加密    */
// 	} else {
// 		pDlg->m_szPrivacyKey = _T("本程序不支持的加密方式");
// 	}

	if (pDlg->DoModal() == IDOK ) {
		if(m_WirelessCard.InterpretEncryptionKeyValue(pCfg,pDlg->m_szPrivacyKey.GetBuffer(pDlg->m_szPrivacyKey.GetLength()+1),0,false))
		{
			m_WirelessCard.AddToPreferredNetworkList(pCfg);
		}
	}

	delete pCfg;
	delete pDlg;
}

/*********************************************************************************************************
** Function name:           OnButtonRefresh
**
** Descriptions:            刷新按钮消息处理
**
** input parameters:        None
** output parameters:       None
** Returned value:          None 
*********************************************************************************************************/
void CWifiDemoDlg::OnButtonRefresh() 
{
	// TODO: Add your control notification handler code here

	CButton *pButton = (CButton*)GetDlgItem(IDC_BUTTON_REFRESH);
	pButton->EnableWindow(FALSE);

	if (Refresh() == FALSE) {
		PostQuitMessage(0);
	}

	pButton->EnableWindow(TRUE);
}

/*********************************************************************************************************
** Function name:           OnButtonWzcContext
**
** Descriptions:            WZC配置按钮消息处理
**
** input parameters:        None
** output parameters:       None
** Returned value:          None 
*********************************************************************************************************/
void CWifiDemoDlg::OnButtonWzcContext() 
{
	// TODO: Add your control notification handler code here
	CWZCContextDlg dlg;
	WZC_CONTEXT wzcContex;
	ZeroMemory(&wzcContex, sizeof(WZC_CONTEXT));
	if( m_WirelessCard.GetWzcContext(&wzcContex) == FALSE) {            /* 获取WZC配置                  */
		MessageBox(_T("获取WZC配置失败"),_T("错误"),MB_ICONHAND);
		return;
	}

	/*
	*  显示WZC的配置信息
	*/
	dlg.m_dwWZCScanTimeOut = wzcContex.tmTr;
	dlg.m_dwWZCScanTimeoutAssociation = wzcContex.tmTp;
	dlg.m_dwWZCScanTimeoutConnect = wzcContex.tmTc;
	dlg.m_dwWZCScanTimeoutDisconnect = wzcContex.tmTf;

	if  ( dlg.DoModal() == IDOK ) {
		/*
		* 设置WZC的配置信息
		*/
		wzcContex.tmTr = dlg.m_dwWZCScanTimeOut;
		wzcContex.tmTp = dlg.m_dwWZCScanTimeoutAssociation;
		wzcContex.tmTc = dlg.m_dwWZCScanTimeoutConnect;
		wzcContex.tmTf = dlg.m_dwWZCScanTimeoutDisconnect;
		
		if( m_WirelessCard.SetWzcContext(&wzcContex) == FALSE) {
			MessageBox(_T("WZC配置失败"),_T("错误"),MB_ICONHAND);
			return;
		}
	}
}

/*********************************************************************************************************
** Function name:           OnTimer
**
** Descriptions:            定时刷新网络信息
**
** input parameters:        nIDEvent    定时器ID
** output parameters:       None
** Returned value:          None 
*********************************************************************************************************/
void CWifiDemoDlg::OnTimer(UINT nIDEvent) 
{
	// TODO: Add your message handler code here and/or call default
	if (Refresh() == FALSE) {
		PostQuitMessage(0);
	}
	CDialog::OnTimer(nIDEvent);
}

/*********************************************************************************************************
** Function name:           OnButtonRemovePreferredNetworkList
**
** Descriptions:            断开所有的网络连接（将首选网络列表移除）
**
** input parameters:        None
** output parameters:       None
** Returned value:          None 
*********************************************************************************************************/
void CWifiDemoDlg::OnButtonRemovePreferredNetworkList() 
{
	// TODO: Add your control notification handler code here
	m_WirelessCard.RemoveAllPreferredNetworkList();
	Refresh();
}

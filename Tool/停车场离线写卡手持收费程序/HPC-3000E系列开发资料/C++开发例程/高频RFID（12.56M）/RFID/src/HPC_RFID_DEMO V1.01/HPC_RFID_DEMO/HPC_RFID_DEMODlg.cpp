// HPC_RFID_DEMODlg.cpp : 实现文件
//

#include "stdafx.h"
#include "HPC_RFID_DEMO.h"
#include "HPC_RFID_DEMODlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


CDialog			*pDialog[6];
CDlgSetting		m_page1;
CDlg14443A		m_page2;
CDlg14443B		m_page3;
CDlg15693		m_page4;
CDlgFeliCa		m_page5;

int m_CurSelTab;

// CHPC_RFID_DEMODlg 对话框

CHPC_RFID_DEMODlg::CHPC_RFID_DEMODlg(CWnd* pParent /*=NULL*/)
	: CDialog(CHPC_RFID_DEMODlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CHPC_RFID_DEMODlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_TAB1, m_ctrTab);
}

BEGIN_MESSAGE_MAP(CHPC_RFID_DEMODlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB1, &CHPC_RFID_DEMODlg::OnTcnSelchangeTab1)
END_MESSAGE_MAP()


// CHPC_RFID_DEMODlg 消息处理程序

BOOL CHPC_RFID_DEMODlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码
	//为Tab Control增加两个页面
	m_ctrTab.InsertItem(0, _T("设置"));
	m_ctrTab.InsertItem(1, _T("14443A"));
	m_ctrTab.InsertItem(2, _T("14443B"));
	m_ctrTab.InsertItem(3, _T("15693"));
	m_ctrTab.InsertItem(4, _T("FeliCa"));
	m_ctrTab.SetActiveWindow();

	//创建两个对话框
	m_page1.Create(IDD_DLG_SETTING, &m_ctrTab);
	m_page2.Create(IDD_DLG_14443A, &m_ctrTab);
	m_page3.Create(IDD_DLG_14443B, &m_ctrTab);
	m_page4.Create(IDD_DLG_15693, &m_ctrTab);
	m_page5.Create(IDD_DLG_FELICA, &m_ctrTab);

	//设定在Tab内显示的范围
	CRect rc;
	m_ctrTab.GetClientRect(&rc);
	rc.top += 22;
	rc.bottom -= 3;
	rc.left += 3;
	rc.right -= 3;
	m_page1.MoveWindow(&rc);
	m_page2.MoveWindow(&rc);
	m_page3.MoveWindow(&rc);
	m_page4.MoveWindow(&rc);
	m_page5.MoveWindow(&rc);

	//用数组把对话框对象指针保存起来
	pDialog[0] = &m_page1;
	pDialog[1] = &m_page2;
	pDialog[2] = &m_page3;
	pDialog[3] = &m_page4;
	pDialog[4] = &m_page5;

	//显示初始页面
	pDialog[0]->ShowWindow(SW_SHOW);
	pDialog[1]->ShowWindow(SW_HIDE);
	pDialog[2]->ShowWindow(SW_HIDE);
	pDialog[3]->ShowWindow(SW_HIDE);
	pDialog[4]->ShowWindow(SW_HIDE);

	//保存当前选择
	m_CurSelTab = 0; 
	m_page1.m_CtrCom.SetCurSel(1);
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CHPC_RFID_DEMODlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_HPC_RFID_DEMO_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_HPC_RFID_DEMO_DIALOG));
	}
}
#endif


void CHPC_RFID_DEMODlg::OnTcnSelchangeTab1(NMHDR *pNMHDR, LRESULT *pResult)
{
	// TODO: 在此添加控件通知处理程序代码
	pDialog[m_CurSelTab]->ShowWindow(SW_HIDE);
	m_CurSelTab = m_ctrTab.GetCurSel();
	pDialog[m_CurSelTab]->ShowWindow(SW_SHOW); 
	
	
	switch (m_CurSelTab)
	{
	case 0:
		m_page1.InitDialog();
		break;

	case 1:
		m_page2.InitDialog();
		break;

	case 2:
		m_page3.InitDialog();
		break;

	case 3:
		m_page4.InitDialog();
		break;

	case 4:
		m_page5.InitDialog();
		break;

	default:
		break;
	}
	*pResult = 0;
}

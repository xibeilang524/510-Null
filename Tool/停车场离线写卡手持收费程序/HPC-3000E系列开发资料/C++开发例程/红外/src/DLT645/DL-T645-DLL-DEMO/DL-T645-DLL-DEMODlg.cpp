// DL-T645-DLL-DEMODlg.cpp : 实现文件
//

#include "stdafx.h"
#include "DL-T645-DLL-DEMO.h"
#include "DL-T645-DLL-DEMODlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

CDialog *pDialog[4];
CDialog1 m_page1;
CDialog2 m_page2;
CDialog3 m_page3;
CDialog4 m_page4;

int m_CurSelTab;

// CDLT645DLLDEMODlg 对话框

CDLT645DLLDEMODlg::CDLT645DLLDEMODlg(CWnd* pParent /*=NULL*/)
	: CDialog(CDLT645DLLDEMODlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CDLT645DLLDEMODlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_TAB1, m_ctrTab);
}

BEGIN_MESSAGE_MAP(CDLT645DLLDEMODlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB1, &CDLT645DLLDEMODlg::OnTcnSelchangeTab1)
END_MESSAGE_MAP()


// CDLT645DLLDEMODlg 消息处理程序

BOOL CDLT645DLLDEMODlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码
	CRect rc0;
	GetClientRect(&rc0);
	rc0.top = 1;
	rc0.left = 0;
	rc0.right += 3;
	rc0.bottom += 25;
	MoveWindow(&rc0);

	//为Tab Control增加两个页面
	m_ctrTab.InsertItem(0, _T("设置"));
	m_ctrTab.InsertItem(1, _T("2007-1"));
	m_ctrTab.InsertItem(2, _T("2007-2"));
	m_ctrTab.InsertItem(3, _T("1997"));
	m_ctrTab.SetActiveWindow();

	//创建两个对话框
	m_page1.Create(IDD_DIALOG1, &m_ctrTab);
	m_page2.Create(IDD_DIALOG2, &m_ctrTab);
	m_page3.Create(IDD_DIALOG3, &m_ctrTab);
	m_page4.Create(IDD_DIALOG4, &m_ctrTab);

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

	//用数组把对话框对象指针保存起来
	pDialog[0] = &m_page1;
	pDialog[1] = &m_page2;
	pDialog[2] = &m_page3;
	pDialog[3] = &m_page4;

	//显示初始页面
	pDialog[0]->ShowWindow(SW_SHOW);
	pDialog[1]->ShowWindow(SW_HIDE);
	pDialog[2]->ShowWindow(SW_HIDE);
	pDialog[3]->ShowWindow(SW_HIDE);

	//保存当前选择
	m_CurSelTab = 0; 
	m_page1.m_ctrlCom.SetCurSel(1);
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CDLT645DLLDEMODlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_DLT645DLLDEMO_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_DLT645DLLDEMO_DIALOG));
	}
}
#endif


void CDLT645DLLDEMODlg::OnTcnSelchangeTab1(NMHDR *pNMHDR, LRESULT *pResult)
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
		m_page3.m_sAddress = m_page2.m_sAddress;
		m_page3.InitDialog();
		break;

	case 3:
		m_page4.InitDialog();
		break;

	default:
		break;
	}

	*pResult = 0;
}

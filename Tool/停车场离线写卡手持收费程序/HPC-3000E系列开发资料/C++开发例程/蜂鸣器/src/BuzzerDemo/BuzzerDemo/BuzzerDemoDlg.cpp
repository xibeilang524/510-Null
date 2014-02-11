// BuzzerDemoDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "BuzzerDemo.h"
#include "BuzzerDemoDlg.h"
#include "epcBuzzerLib.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CBuzzerDemoDlg 对话框

CBuzzerDemoDlg::CBuzzerDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CBuzzerDemoDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CBuzzerDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CBuzzerDemoDlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BZR_ON, &CBuzzerDemoDlg::OnBnClickedBzrOn)
	ON_BN_CLICKED(IDC_BZR_OFF, &CBuzzerDemoDlg::OnBnClickedBzrOff)
	ON_BN_CLICKED(IDC_BZR_BEEPS, &CBuzzerDemoDlg::OnBnClickedBzrBeeps)
	ON_BN_CLICKED(IDC_BZR_STATUS, &CBuzzerDemoDlg::OnBnClickedBzrStatus)
END_MESSAGE_MAP()


// CBuzzerDemoDlg 消息处理程序

BOOL CBuzzerDemoDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CBuzzerDemoDlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_BUZZERDEMO_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_BUZZERDEMO_DIALOG));
	}
}
#endif


void CBuzzerDemoDlg::OnBnClickedBzrOn()
{
	// TODO: Add your control notification handler code here    
	BOOL bRet;

	bRet = epcBuzzerOn(0);                                   /*  使蜂鸣器一直蜂鸣            */
	if (bRet == FALSE ){
		MessageBox(_T("蜂鸣器蜂鸣失败"));
		return;
	}
	return;

}

void CBuzzerDemoDlg::OnBnClickedBzrOff()
{
	// TODO: Add your control notification handler code here
	BOOL bRet;

	bRet = epcBuzzerOff();                                   /*  蜂鸣器禁止                  */
	if (bRet == FALSE ){
		MessageBox(_T("蜂鸣器禁止失败"));
		return;
	}
	return;

}

void CBuzzerDemoDlg::OnBnClickedBzrBeeps()
{
	// TODO: Add your control notification handler code here
	BOOL bRet; 
	bRet = epcBuzzerBeeps(5,200,200);                         /*  蜂鸣器鸣叫5次               */
	if (bRet == FALSE ){
		MessageBox(_T("蜂鸣器鸣叫失败"));
		return;
	}    

}

void CBuzzerDemoDlg::OnBnClickedBzrStatus()
{
	// TODO: Add your control notification handler code here
	DWORD dwStatus; 

	dwStatus = epcBuzzerGetStatus();                          /*  读蜂鸣器状态                */
	if (dwStatus > 1){
		MessageBox(_T("读蜂鸣器状态失败"));
		return;
	}
	if (dwStatus == 0){
		MessageBox(_T("蜂鸣器处于蜂鸣状态"));
	} else {
		MessageBox(_T("蜂鸣器处于禁止状态"));
	}
}

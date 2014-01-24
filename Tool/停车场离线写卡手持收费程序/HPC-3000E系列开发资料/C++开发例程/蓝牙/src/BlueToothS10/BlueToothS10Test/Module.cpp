// Module.cpp : 实现文件
//

#include "stdafx.h"
#include "BlueToothS10Test.h"
#include "Module.h"
#include "BlueToothS10.h"
#include "tk.h"


// CModule 对话框

IMPLEMENT_DYNAMIC(CModule, CDialog)

CModule::CModule(CWnd* pParent /*=NULL*/)
	: CDialog(CModule::IDD, pParent)
{

}

CModule::~CModule()
{
}

void CModule::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_CB_COMS, m_Coms);
	DDX_Control(pDX, IDC_CB_OPERMODES, m_Modes);
}


BEGIN_MESSAGE_MAP(CModule, CDialog)
	ON_BN_CLICKED(IDC_BN_INIT, &CModule::OnBnClickedBnInit)
	ON_BN_CLICKED(IDC_BN_CLOSE, &CModule::OnBnClickedBnClose)
	ON_BN_CLICKED(IDC_BN_OPERMODE, &CModule::OnBnClickedBnOpermode)
	ON_BN_CLICKED(IDC_BN_VERSION, &CModule::OnBnClickedBnVersion)
	ON_BN_CLICKED(IDC_BN_ORGL, &CModule::OnBnClickedBnOrgl)
	ON_BN_CLICKED(IDC_BN_REBOOT, &CModule::OnBnClickedBnReboot)
END_MESSAGE_MAP()


// CModule 消息处理程序

BOOL CModule::OnInitDialog()
{
	CDialog::OnInitDialog();
	m_Coms.AddString(_T("COM1"));
	m_Coms.AddString(_T("COM2"));
	m_Coms.AddString(_T("COM3"));
	m_Coms.AddString(_T("COM4"));
	m_Coms.AddString(_T("COM5"));
	m_Coms.SetCurSel(2);

	m_Modes.AddString(_T("AT Command"));
	m_Modes.AddString(_T("Communicaiton"));
	m_Modes.SetCurSel(1);

	GetDlgItem(IDC_BN_CLOSE)->EnableWindow(FALSE);

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}
void CModule::OnBnClickedBnInit()
{
	if(AssertResult(S10_Init((S10_Coms)(COM1+m_Coms.GetCurSel()))))
	{
		GetDlgItem(IDC_BN_CLOSE)->EnableWindow(TRUE);
		GetDlgItem(IDC_BN_INIT)->EnableWindow(FALSE);
	}
}

void CModule::OnBnClickedBnClose()
{
	if(AssertResult(S10_Close()))
	{
		GetDlgItem(IDC_BN_CLOSE)->EnableWindow(FALSE);
		GetDlgItem(IDC_BN_INIT)->EnableWindow(TRUE);
	}
}

void CModule::OnBnClickedBnOpermode()
{
	AssertResult(S10_SetOperationMode((S10_OperationMode)m_Modes.GetCurSel()));
}

void CModule::OnBnClickedBnVersion()
{
	char str[1024];
	WCHAR wstr[1024];
	unsigned int len = 1024;
	if(AssertResult(S10_GetModuleVersion(str,len)))
	{
		str[len]=0;
		TK::Ansi2Unicode(wstr,str);
		MessageBox(wstr);
	}
}

void CModule::OnBnClickedBnOrgl()
{
	AssertResult(S10_RestoreFactorySettings());
}

void CModule::OnBnClickedBnReboot()
{
	AssertResult(S10_Reboot());
}

void CModule::OnOK()
{
}

void CModule::OnCancel()
{
}

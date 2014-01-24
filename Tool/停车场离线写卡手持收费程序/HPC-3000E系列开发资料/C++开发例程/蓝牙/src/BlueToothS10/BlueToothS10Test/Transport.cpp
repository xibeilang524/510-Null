// Transport.cpp : 实现文件
//

#include "stdafx.h"
#include "BlueToothS10Test.h"
#include "Transport.h"
#include "MyString.h"
#include "BlueToothS10.h"
#include "tk.h"


// CTransport 对话框

IMPLEMENT_DYNAMIC(CTransport, CDialog)

CTransport::CTransport(CWnd* pParent /*=NULL*/)
	: CDialog(CTransport::IDD, pParent)
{

}

CTransport::~CTransport()
{
}

void CTransport::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_CK_INHEX, m_ckInHex);
	DDX_Control(pDX, IDC_CK_OUTHEX, m_ckOutHex);
}


BEGIN_MESSAGE_MAP(CTransport, CDialog)
	ON_BN_CLICKED(IDC_BN_INCLEAR, &CTransport::OnBnClickedBnInclear)
	ON_BN_CLICKED(IDC_BN_OUTCLEAR, &CTransport::OnBnClickedBnOutclear)
	ON_BN_CLICKED(IDC_BN_SEND, &CTransport::OnBnClickedBnSend)
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_CK_INHEX, &CTransport::OnBnClickedCkInhex)
	ON_BN_CLICKED(IDC_CK_OUTHEX, &CTransport::OnBnClickedCkOuthex)
END_MESSAGE_MAP()


// CTransport 消息处理程序

BOOL CTransport::OnInitDialog()
{
	CDialog::OnInitDialog();

	m_hExitEvent = CreateEvent(NULL,FALSE,FALSE,NULL);
	m_hRecvThread = CreateThread(NULL,0,RecvThreadProc,this,0,NULL);

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}
void CTransport::OnBnClickedBnInclear()
{
	GetDlgItem(IDC_TXT_IN)->SetWindowText(_T(""));
}

void CTransport::OnBnClickedBnOutclear()
{
	GetDlgItem(IDC_TXT_OUT)->SetWindowText(_T(""));
}

void CTransport::OnBnClickedBnSend()
{
	unsigned char c[1024];
	unsigned int len = 0;
	CString str;
	GetDlgItem(IDC_TXT_OUT)->GetWindowText(str);
	if(m_ckOutHex.GetCheck())
	{
		len = MyString::StringToByte(str,c,1024);
	}
	else
	{
		len = TK::Unicode2Ansi((char*)c,str) - 1;
	}
	if(len)
		AssertResult(S10_SendData(c,len));
}

DWORD CTransport::RecvThreadProc(PVOID pArg)
{
	CTransport * pDlg = (CTransport*)pArg;
	unsigned char buf[1024];
	CString str,singlec,txt;
	while(true)
	{
		if(WaitForSingleObject(pDlg->m_hExitEvent,0) == WAIT_OBJECT_0) break;
		unsigned int len = 1024;
		if(S10_RecvData(buf,len) == SUCCESS_SETTING)
		{
			if(pDlg->m_ckInHex.GetCheck())
			{
				MyString::ByteToString(buf,&str,len);
			}
			else
			{
				str="";
				for(int i=0;i<len;i++)
				{
					singlec.Format(_T("%c"),buf[i]);
					str+=singlec;
				}
			}
			pDlg->GetDlgItem(IDC_TXT_IN)->GetWindowText(txt);
			txt += str;
			pDlg->GetDlgItem(IDC_TXT_IN)->SetWindowText(txt);
		}
		Sleep(10);
	}
	return 0;
}
void CTransport::OnDestroy()
{
	CDialog::OnDestroy();
	SetEvent(m_hExitEvent);
	WaitForSingleObject(m_hRecvThread,500);
	CloseHandle(m_hRecvThread);
	CloseHandle(m_hExitEvent);
}

void CTransport::OnBnClickedCkInhex()
{
	CString str,singc;
	GetDlgItem(IDC_TXT_IN)->GetWindowText(str);
	char c[1024];
	if(m_ckInHex.GetCheck())
	{
		int len = TK::Unicode2Ansi(c,str);
		MyString::ByteToString((BYTE*)c,&str,len-1);
		GetDlgItem(IDC_TXT_IN)->SetWindowText(str);
	}
	else
	{
		int len = MyString::StringToByte(str,(BYTE*)c,1024);
		str = "";
		for(int i=0;i<len;i++)
		{
			singc.Format(_T("%c"),c[i]);
			str += singc;
		}
		GetDlgItem(IDC_TXT_IN)->SetWindowText(str);
	}
}

void CTransport::OnBnClickedCkOuthex()
{
	CString str,singc;
	GetDlgItem(IDC_TXT_OUT)->GetWindowText(str);
	char c[1024];
	if(m_ckOutHex.GetCheck())
	{
		int len = TK::Unicode2Ansi(c,str);
		MyString::ByteToString((BYTE*)c,&str,len-1);
		GetDlgItem(IDC_TXT_OUT)->SetWindowText(str);
	}
	else
	{
		int len = MyString::StringToByte(str,(BYTE*)c,1024);
		str = "";
		for(int i=0;i<len;i++)
		{
			singc.Format(_T("%c"),c[i]);
			str += singc;
		}
		GetDlgItem(IDC_TXT_OUT)->SetWindowText(str);
	}
}

void CTransport::OnOK()
{
}

void CTransport::OnCancel()
{
}

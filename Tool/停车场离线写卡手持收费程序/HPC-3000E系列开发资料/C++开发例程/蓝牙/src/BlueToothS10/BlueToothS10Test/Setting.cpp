// Setting.cpp : 实现文件
//

#include "stdafx.h"
#include "BlueToothS10Test.h"
#include "Setting.h"
#include "BlueToothS10.h"
#include "tk.h"


// CSetting 对话框

IMPLEMENT_DYNAMIC(CSetting, CDialog)

CSetting::CSetting(CWnd* pParent /*=NULL*/)
	: CDialog(CSetting::IDD, pParent)
{

}

CSetting::~CSetting()
{
}

void CSetting::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_CB_ROLES, m_Roles);
}


BEGIN_MESSAGE_MAP(CSetting, CDialog)
	ON_BN_CLICKED(IDC_BN_ADDR, &CSetting::OnBnClickedBnAddr)
	ON_BN_CLICKED(IDC_BN_SETNAME, &CSetting::OnBnClickedBnSetname)
	ON_BN_CLICKED(IDC_BN_GETNAME, &CSetting::OnBnClickedBnGetname)
	ON_BN_CLICKED(IDC_BN_SETROLE, &CSetting::OnBnClickedBnSetrole)
	ON_BN_CLICKED(IDC_BN_GETROLE, &CSetting::OnBnClickedBnGetrole)
	ON_BN_CLICKED(IDC_BN_SETDC, &CSetting::OnBnClickedBnSetdc)
	ON_BN_CLICKED(IDC_BN_GETDC, &CSetting::OnBnClickedBnGetdc)
	ON_BN_CLICKED(IDC_BN_SETAC, &CSetting::OnBnClickedBnSetac)
	ON_BN_CLICKED(IDC_BN_GETAC, &CSetting::OnBnClickedBnGetac)
	ON_BN_CLICKED(IDC_BN_SETMC, &CSetting::OnBnClickedBnSetmc)
	ON_BN_CLICKED(IDC_BN_GETMC, &CSetting::OnBnClickedBnGetmc)
END_MESSAGE_MAP()


void Str2Addr(CString sString,BYTE *pData)
{
	sString.Remove(' ');
	int nLength = sString.GetLength();
	CString zero=_T("000000000000");
	if(nLength<12)
		sString=zero.Left(12-nLength)+sString;
	else
		sString=sString.Right(12);
	nLength=12;
	CString strSingleByte;
	for (int i = 0; i < nLength/2; i++)
	{
		strSingleByte = sString.Mid(2*i, 2);
		if (strSingleByte.IsEmpty())
		{
			pData[i] = 0;
		}
		else
		{
			pData[i] = 0;
			strSingleByte.MakeUpper();
			for (int j = 0; j < 2; j++)
			{
				pData[i] <<= 4;
				if (isdigit(strSingleByte[j]))
				{// 为数字 '0' ~ '9'
					pData[i] |= strSingleByte[j] - '0';
				}
				else
				{// 为数字 'A' ~ 'F'
					pData[i] |= strSingleByte[j] - 'A' + 0x0A;
				}
			}
		}
	}
}

void Addr2Str(CString &sString,BYTE *pData)
{
	sString.Format(_T("%02X%02X%02X%02X%02X%02X"),pData[0],pData[1],pData[2],pData[3],pData[4],pData[5]);
}
// CSetting 消息处理程序

BOOL CSetting::OnInitDialog()
{
	CDialog::OnInitDialog();

	m_Roles.AddString(L"Slave");
	m_Roles.AddString(L"Master");
	m_Roles.AddString(L"Loopback");
	m_Roles.SetCurSel(1);

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}
void CSetting::OnBnClickedBnAddr()
{
	unsigned char addr[6];
	CString str;
	if(AssertResult(S10_GetAddress(addr)))
	{
		Addr2Str(str,addr);
		GetDlgItem(IDC_TXT_ADDR)->SetWindowText(str);
	}
}

void CSetting::OnBnClickedBnSetname()
{
	CString str;
	GetDlgItem(IDC_TXT_NAME)->GetWindowText(str);
	char c[1024];
	int len = TK::Unicode2Ansi(c,str);
	AssertResult(S10_SetDeviceName(c,len-1));
}

void CSetting::OnBnClickedBnGetname()
{
	char c[1024];
	WCHAR wc[1024];
	unsigned int len = 1024;
	if(AssertResult(S10_GetDeviceName(c,len)))
	{
		c[len]=0;
		TK::Ansi2Unicode(wc,c);
		GetDlgItem(IDC_TXT_NAME)->SetWindowText(wc);
	}
}

void CSetting::OnBnClickedBnSetrole()
{
	AssertResult(S10_SetRole(S10_Role(m_Roles.GetCurSel()+Slave)));
}

void CSetting::OnBnClickedBnGetrole()
{
	S10_Role role;
	if(AssertResult(S10_GetRole(role)))
	{
		m_Roles.SetCurSel(role-Slave);
	}
}


void Str2UI(CString sString,unsigned int *pData)
{
	sString.Remove(' ');
	int nLength = sString.GetLength();
	sString.MakeUpper();
	*pData = 0;
	for(int i=0;i<nLength;i++)
	{
		*pData *= 16;
		char c = (char)(sString.GetAt(i));
		if(isdigit(c)) *pData += c-'0';
		else	*pData += c-'A' + 10;
	}
}
void UI2Str(CString &sString,unsigned long Data)
{
	sString.Format(_T("%X"),Data);
}

void CSetting::OnBnClickedBnSetdc()
{
	CString str;
	GetDlgItem(IDC_TXT_DC)->GetWindowText(str);
	unsigned int dc;
	Str2UI(str,&dc);
	AssertResult(S10_SetDeviceClass(dc));
}

void CSetting::OnBnClickedBnGetdc()
{
	CString str;
	unsigned int dc;
	if(AssertResult(S10_GetDeviceClass(dc)))
	{
		UI2Str(str,dc);
		GetDlgItem(IDC_TXT_DC)->SetWindowText(str);
	}
}

void CSetting::OnBnClickedBnSetac()
{
	CString str;
	GetDlgItem(IDC_TXT_AC)->GetWindowText(str);
	unsigned int ac;
	Str2UI(str,&ac);
	AssertResult(S10_SetAccessCode(ac));
}

void CSetting::OnBnClickedBnGetac()
{
	CString str;
	unsigned int ac;
	if(AssertResult(S10_GetAccessCode(ac)))
	{
		UI2Str(str,ac);
		GetDlgItem(IDC_TXT_AC)->SetWindowText(str);
	}
}

void CSetting::OnBnClickedBnSetmc()
{
	CString str;
	GetDlgItem(IDC_TXT_MC)->GetWindowText(str);
	char c[1024];
	int len = TK::Unicode2Ansi(c,str);
	AssertResult(S10_SetMatchingCode(c,len-1));
}

void CSetting::OnBnClickedBnGetmc()
{
	char c[1024];
	WCHAR wc[1024];
	unsigned int len = 1024;
	if(AssertResult(S10_GetMatchingCode(c,len)))
	{
		c[len]=0;
		TK::Ansi2Unicode(wc,c);
		GetDlgItem(IDC_TXT_MC)->SetWindowText(wc);
	}
}

void CSetting::OnCancel()
{
}

void CSetting::OnOK()
{
}

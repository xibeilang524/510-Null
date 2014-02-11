// Dialog3.cpp : 实现文件
//

#include "stdafx.h"
#include "DL-T645-DLL-DEMO.h"
#include "Dialog3.h"
#include "DLT645.h"
#include "MyString.h"
#include "MyBeep.h"

MyString MS2;
CMyBeep BP2;
// CDialog3 对话框

IMPLEMENT_DYNAMIC(CDialog3, CDialog)

CDialog3::CDialog3(CWnd* pParent /*=NULL*/)
	: CDialog(CDialog3::IDD, pParent)
	, m_sDisplay(_T(""))
	, m_sFreezeTime(_T(""))
	, m_sPwOld(_T(""))
	, m_nBaundrate(0)
	, m_sPwNew(_T(""))
	, m_sDataItem(_T(""))
	, m_sEventItem(_T(""))
	, m_sConsumer(_T(""))
{

}

CDialog3::~CDialog3()
{
}

void CDialog3::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_DISPLAY, m_sDisplay);
	DDX_Text(pDX, IDC_FREEZE_TIME, m_sFreezeTime);
	DDX_Text(pDX, IDC_PASSWORD_OLD, m_sPwOld);
	DDX_CBIndex(pDX, IDC_BAUND_RATE, m_nBaundrate);
	DDX_Text(pDX, IDC_PASSWORD_NEW, m_sPwNew);
	DDX_Text(pDX, IDC_DATA_ITEM, m_sDataItem);
	DDX_Text(pDX, IDC_EVENT_ITEM, m_sEventItem);
	DDX_Text(pDX, IDC_CONSUMER, m_sConsumer);
}


BEGIN_MESSAGE_MAP(CDialog3, CDialog)
	ON_BN_CLICKED(IDC_FREEZE, &CDialog3::OnBnClickedFreeze)
	ON_BN_CLICKED(IDC_CHANGE_BRD, &CDialog3::OnBnClickedChangeBrd)
	ON_BN_CLICKED(IDC_CHANGE_PW, &CDialog3::OnBnClickedChangePw)
	ON_BN_CLICKED(IDC_CLEAN, &CDialog3::OnBnClickedClean)
	ON_BN_CLICKED(IDC_CLAEN_ALL, &CDialog3::OnBnClickedClaenAll)
	ON_BN_CLICKED(IDC_CLEAN_EVENT, &CDialog3::OnBnClickedCleanEvent)
END_MESSAGE_MAP()



void CDialog3::InitDialog()
{
	m_sConsumer = (_T("00 00 00 01"));
	m_sPwOld = (_T("02 00 00 00"));
	m_sPwNew = (_T("02 00 00 00"));
	m_sDataItem = (_T("03 0C 00 04"));
	m_sEventItem = (_T("ff ff ff ff"));
	//m_sAddress = (_T("00 00 00 00 00 00"));
	m_sFreezeTime = (_T("99 99 99 99"));
	m_sDisplay = (_T(""));
	m_nBaundrate = 1;
	UpdateData(FALSE);
}

// CDialog3 消息处理程序

void CDialog3::OnBnClickedFreeze()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE Time[4];
	BYTE Addr[6];
	MS2.StringToByte(m_sAddress,Addr,6);
	MS2.StringToByte(m_sFreezeTime,Time,4);
	if (DLT645_2007_Freeze(Addr,Time))
	{
		m_sDisplay = _T("冻结成功！\r\n");
		BP2.BeepOk();
	}
	else
	{
		m_sDisplay = _T("冻结失败！\r\n");
		BP2.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog3::OnBnClickedChangeBrd()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE BaundRate[6] = {0x02,0x04,0x08,0x10,0x20,0x40};
	BYTE Z;
	BYTE Addr[6];
	Z = BaundRate[m_nBaundrate];
	MS2.StringToByte(m_sAddress,Addr,6);
	if (DLT645_2007_ChangeBaudrate(Addr, Z))
	{
		m_sDisplay = _T("更改通信速率成功！\r\n");
		BP2.BeepOk();
	}
	else
	{
		m_sDisplay = _T("更改通信速率失败！\r\n");
		BP2.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog3::OnBnClickedChangePw()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE PwOld[4],PwNew[4],CPwDI[4];
	BYTE Addr[6];
	MS2.StringToByte(m_sAddress,Addr,6);
	MS2.StringToByte(m_sPwOld,PwOld,4);
	MS2.StringToByte(m_sPwNew,PwNew,4);
	MS2.StringToByte(m_sDataItem,CPwDI,4);
	if (DLT645_2007_ChangePassword(Addr, CPwDI, PwOld, PwNew))
	{
		m_sDisplay = _T("修改密码成功！\r\n");
		BP2.BeepOk();
	}
	else
	{
		m_sDisplay = _T("修改密码失败！\r\n");
		BP2.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog3::OnBnClickedClean()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE Pw3[4],Consumer3[4];
	BYTE Addr[6];
	MS2.StringToByte(m_sAddress,Addr,6);
	MS2.StringToByte(m_sPwNew,Pw3,4);
	MS2.StringToByte(m_sConsumer,Consumer3,4);

	if (DLT645_2007_CleanMaxRequire(Addr, Pw3, Consumer3))
	{
		m_sDisplay = _T("最大需量清零成功！\r\n");
		BP2.BeepOk();
	}
	else
	{
		m_sDisplay = _T("最大需量清零失败！\r\n");
		BP2.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog3::OnBnClickedClaenAll()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE Pw5[4],Consumer5[4];
	BYTE Addr[6];
	MS2.StringToByte(m_sAddress,Addr,6);
	MS2.StringToByte(m_sPwNew,Pw5,4);
	MS2.StringToByte(m_sConsumer,Consumer5,4);

	if (DLT645_2007_CleanAllData(Addr, Pw5, Consumer5))
	{
		m_sDisplay = _T("电表清零成功！\r\n");
		BP2.BeepOk();
	}
	else
	{
		m_sDisplay = _T("电表清零失败！\r\n");
		BP2.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog3::OnBnClickedCleanEvent()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE Pw4[4],Consumer4[4],CleanData[4];
	BYTE Addr[6];
	MS2.StringToByte(m_sAddress,Addr,6);
	MS2.StringToByte(m_sPwNew,Pw4,4);
	MS2.StringToByte(m_sConsumer,Consumer4,4);
	MS2.StringToByte(m_sEventItem,CleanData,4);

	if (DLT645_2007_CleanEvent(Addr, Pw4, Consumer4,CleanData))
	{
		m_sDisplay = _T("事件清零成功！\r\n");
		BP2.BeepOk();
	}
	else
	{
		m_sDisplay = _T("事件清零失败！\r\n");
		BP2.BeepError();
	}
	UpdateData(FALSE);
}

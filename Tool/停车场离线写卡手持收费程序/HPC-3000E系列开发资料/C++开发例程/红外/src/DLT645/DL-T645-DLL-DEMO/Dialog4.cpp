// Dialog4.cpp : 实现文件
//

#include "stdafx.h"
#include "DL-T645-DLL-DEMO.h"
#include "Dialog4.h"
#include "DLT645.h"
#include "MyString.h"
#include "MyBeep.h"

MyString MS3;
CMyBeep BP3;
// CDialog4 对话框

IMPLEMENT_DYNAMIC(CDialog4, CDialog)

CDialog4::CDialog4(CWnd* pParent /*=NULL*/)
	: CDialog(CDialog4::IDD, pParent)
	, m_sDataItem1(_T(""))
	, m_sDataItem2(_T(""))
	, m_sData(_T(""))
	, m_sTime(_T(""))
	, m_sAddress(_T(""))
	, m_sPassWordOld(_T(""))
	, m_sPassWordNew(_T(""))
	, m_nBaundRate(0)
	, m_sDisplay(_T(""))
{

}

CDialog4::~CDialog4()
{
}

void CDialog4::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_DATA_ITEM2, m_sDataItem1);
	DDX_Text(pDX, IDC_DATA_ITEM4, m_sDataItem2);
	DDX_Text(pDX, IDC_DATA3, m_sData);
	DDX_Text(pDX, IDC_TIME3, m_sTime);
	DDX_Text(pDX, IDC_ADDRESS2, m_sAddress);
	DDX_Text(pDX, IDC_PW_OLD2, m_sPassWordOld);
	DDX_Text(pDX, IDC_PW_NEW2, m_sPassWordNew);
	DDX_CBIndex(pDX, IDC_BAUND_RATE, m_nBaundRate);
	DDX_Text(pDX, IDC_OUTPUT, m_sDisplay);
}


BEGIN_MESSAGE_MAP(CDialog4, CDialog)
	ON_BN_CLICKED(IDC_READ_DATA, &CDialog4::OnBnClickedReadData)
	ON_BN_CLICKED(IDC_WRITE_DATA2, &CDialog4::OnBnClickedWriteData2)
	ON_BN_CLICKED(IDC_SEND_TIME, &CDialog4::OnBnClickedSendTime)
	ON_BN_CLICKED(IDC_WRITE_ADDR2, &CDialog4::OnBnClickedWriteAddr2)
	ON_BN_CLICKED(IDC_CHANGE_PW, &CDialog4::OnBnClickedChangePw)
	ON_BN_CLICKED(IDC_CLEAN2, &CDialog4::OnBnClickedClean2)
	ON_BN_CLICKED(IDC_CHANGE_BRD2, &CDialog4::OnBnClickedChangeBrd2)
END_MESSAGE_MAP()

void CDialog4::InitDialog()
{
	m_sDataItem1 = (_T("10 90"));
	m_sDataItem2 = (_T("10 90"));
	m_sData = (_T("00 00 00 00"));
	m_sTime = (_T("00 00 12 13 07 11"));
	m_sAddress = (_T("26 56 00 10 10 20"));
	m_sPassWordOld = (_T("02 00 00 00"));
	m_sPassWordNew = (_T("02 00 00 00"));
	m_sDisplay = (_T(""));
	m_nBaundRate = 1;
	UpdateData(FALSE);
}


// CDialog4 消息处理程序

void CDialog4::OnBnClickedReadData()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE Addr[6],DI[2],ReadBuff[255];
	CString sString;
	MS3.StringToByte(m_sAddress,Addr,6);
	MS3.StringToByte(m_sDataItem1,DI,2);
	if (DLT645_1997_ReadData(Addr, DI, ReadBuff))
	{
		MS3.ByteToString(&ReadBuff[4], &sString, ReadBuff[1]-2);
		m_sDisplay = _T("读取成功！数据如下：\r\n");
		m_sDisplay += sString;
		m_sDisplay += _T("\r\n");
		BP3.BeepOk();
	}
	else
	{
		m_sDisplay = _T("读取数据失败！\r\n");
		BP3.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog4::OnBnClickedWriteData2()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE Addr[6],DI[2],WriteBuff[6],len;
	CString sString;
	MS3.StringToByte(m_sAddress,Addr,6);
	MS3.StringToByte(m_sDataItem2,DI,2);
	len = (BYTE) MS3.StringToByte(m_sData,WriteBuff,6);
	if (DLT645_1997_WriteData(Addr, DI, WriteBuff, len))
	{
		m_sDisplay = _T("写数据成功！\r\n");
		BP3.BeepOk();
	}
	else
	{
		MS3.ByteToString(&len, &sString, 1);
		m_sDisplay = _T("写数据失败！\r\n");
		m_sDisplay += sString;
		BP3.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog4::OnBnClickedSendTime()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE Time[6];
	MS3.StringToByte(m_sTime,Time,6);
	if (DLT645_1997_BroadcastTime(Time))
	{
		m_sDisplay = _T("广播校时信息已发送！\r\n");
		BP3.BeepOk();
	}
	else
	{
		m_sDisplay = _T("广播校时信息发送失败！\r\n");
		BP3.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog4::OnBnClickedWriteAddr2()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE Addr[6];
	MS3.StringToByte(m_sAddress,Addr,6);
	if (DLT645_1997_WriteAddr(Addr))
	{
		m_sDisplay = _T("写通信地址成功！\r\n");
		BP3.BeepOk();
	}
	else
	{
		m_sDisplay = _T("写通信地址失败！\r\n");
		BP3.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog4::OnBnClickedChangePw()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE PwOld[4],PwNew[4];
	BYTE Addr[6];
	MS3.StringToByte(m_sAddress,Addr,6);
	MS3.StringToByte(m_sPassWordOld,PwOld,4);
	MS3.StringToByte(m_sPassWordNew,PwNew,4);
	if (DLT645_1997_ChangePassword(Addr, PwOld, PwNew))
	{
		m_sDisplay = _T("修改密码成功！\r\n");
		BP3.BeepOk();
	}
	else
	{
		m_sDisplay = _T("修改密码失败！\r\n");
		BP3.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog4::OnBnClickedClean2()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE Addr[6];
	MS3.StringToByte(m_sAddress,Addr,6);

	if (DLT645_1997_CleanMaxRequire(Addr))
	{
		m_sDisplay = _T("最大需量清零成功！\r\n");
		BP3.BeepOk();
	}
	else
	{
		m_sDisplay = _T("最大需量清零失败！\r\n");
		BP3.BeepError();
	}
	UpdateData(FALSE);
}

void CDialog4::OnBnClickedChangeBrd2()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE BaundRate[6] = {0x02,0x04,0x08,0x10,0x20,0x40};
	BYTE Z;
	BYTE Addr[6];
	Z = BaundRate[m_nBaundRate];
	MS3.StringToByte(m_sAddress,Addr,6);
	if (DLT645_1997_ChangeBaudrate(Addr, Z))
	{
		m_sDisplay = _T("更改通信速率成功！\r\n");
		BP3.BeepOk();
	}
	else
	{
		m_sDisplay = _T("更改通信速率失败！\r\n");
		BP3.BeepError();
	}
	UpdateData(FALSE);
}

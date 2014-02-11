// Dlg14443A.cpp : 实现文件
//

#include "stdafx.h"
#include "HPC_RFID_DEMO.h"
#include "Dlg14443A.h"
#include "HPC_RFID_DLL.h"
#include "MyString.h"

MyString MS1;

// CDlg14443A 对话框

IMPLEMENT_DYNAMIC(CDlg14443A, CDialog)

CDlg14443A::CDlg14443A(CWnd* pParent /*=NULL*/)
	: CDialog(CDlg14443A::IDD, pParent)
	, m_sDisplay(_T(""))
	, m_sKeyA(_T(""))
	, m_sWriteData(_T(""))
	, m_Block(0)
{

}

CDlg14443A::~CDlg14443A()
{
}

void CDlg14443A::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDT_DISPLAY, m_sDisplay);
	DDX_Text(pDX, IDC_EDT_KEY_A, m_sKeyA);
	DDX_Text(pDX, IDC_EDT_WRITE_DATA, m_sWriteData);
	DDX_CBIndex(pDX, IDC_COM_BLOCK, m_Block);
}


BEGIN_MESSAGE_MAP(CDlg14443A, CDialog)
	ON_BN_CLICKED(IDC_BTN_READ_SN, &CDlg14443A::OnBnClickedBtnReadSn)
	ON_BN_CLICKED(IDC_BTN_AUTH_KEY, &CDlg14443A::OnBnClickedBtnAuthKey)
	ON_BN_CLICKED(IDC_BTN_READ_BLOCK, &CDlg14443A::OnBnClickedBtnReadBlock)
	ON_BN_CLICKED(IDC_BTN_WRITE_BLOCK, &CDlg14443A::OnBnClickedBtnWriteBlock)
	ON_BN_CLICKED(IDC_BTN_CLEAN2, &CDlg14443A::OnBnClickedBtnClean2)
END_MESSAGE_MAP()

void CDlg14443A::InitDialog(void)
{
	m_sKeyA = (_T("FF FF FF FF FF FF"));
	m_sWriteData = (_T("00 11 22 33 44 55 66 77 88 99 AA BB CC DD EE FF"));
	m_Block = 1;
	UpdateData(FALSE);
}
// CDlg14443A 消息处理程序

void CDlg14443A::OnBnClickedBtnReadSn()
{
	BYTE i,data[32];
	CString ss;

	if (RFID_STATUS_OK == ISO14443A_ReadCardSn(data))
	{
		m_sDisplay += "OK!读到MF1卡号：";
		for (i=0; i<4; i++)
		{
			ss.Format(_T("%02X "),data[i]);
			m_sDisplay += ss;
		}
		m_sDisplay += "\r\n";
	}
	else
	{
		m_sDisplay += "ERROR!读卡号失败！\r\n";
	}
	UpdateData(FALSE);
}

void CDlg14443A::OnBnClickedBtnAuthKey()
{
	UpdateData(TRUE);
	BYTE Key[6];
	if (MS1.StringToByte(m_sKeyA, Key, 6) != 6)
	{
		m_sDisplay += "ERROR!密钥少于六字节！\r\n";
		GetDlgItem(IDC_EDT_DISPLAY)->SetWindowText(m_sDisplay);
		return;
	}
	
	if (RFID_STATUS_OK == ISO14443A_MF1AuthKey(m_Block, Key, MF1_KEY_SOURCE_EXT | MF1_KEY_TYPE_A))
	{
		m_sDisplay += "OK!验证密钥成功！\r\n";
	}
	else
	{
		m_sDisplay += "ERROR!验证密钥失败！\r\n";
	}
	GetDlgItem(IDC_EDT_DISPLAY)->SetWindowText(m_sDisplay);
}

void CDlg14443A::OnBnClickedBtnReadBlock()
{
	UpdateData(TRUE);
	BYTE ReadData[16];
	BYTE i;
	CString ss;

	if (RFID_STATUS_OK == ISO14443A_ReadMF1Block(m_Block, ReadData))
	{
		m_sDisplay += "OK!读出的数据为：\r\n";
		for(i = 0; i<16; i++)
		{
			ss.Format(_T("%02X "),ReadData[i]);
			m_sDisplay += ss;
			if ((i == 7)||(i == 15))
			{
				m_sDisplay += "\r\n";
			}
		}
	}
	else
	{
		m_sDisplay += "ERROR!读块失败！\r\n";
	}
	GetDlgItem(IDC_EDT_DISPLAY)->SetWindowText(m_sDisplay);
}

void CDlg14443A::OnBnClickedBtnWriteBlock()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData(TRUE);
	BYTE WriteData[16];
	BYTE i;
	CString ss;

	if (MS1.StringToByte(m_sWriteData, WriteData, 16) != 16)
	{
		m_sDisplay += "ERROR!待写入的数据少于16字节！\r\n";
		GetDlgItem(IDC_EDT_DISPLAY)->SetWindowText(m_sDisplay);
		return;
	}

	if((m_Block % 4) == 3)
	{
		if (IDYES != MessageBox(_T("此块为密钥块，确认要写入？"), NULL, MB_YESNO))
		{
			return;
		}
		if ((WriteData[6] != 0xff)||(WriteData[7] != 0x07)||(WriteData[8] != 0x80)||(WriteData[9] != 0x69))
		{
			m_sDisplay += "ERROR!数据格式不正确，写入失败！\r\n";
			GetDlgItem(IDC_EDT_DISPLAY)->SetWindowText(m_sDisplay);
			return;
		}
	}

	if (RFID_STATUS_OK == ISO14443A_WriteMF1Block(m_Block, WriteData))
	{
		m_sDisplay += "OK!写入的数据为：\r\n";
		for(i = 0; i<16; i++)
		{
			ss.Format(_T("%02X "),WriteData[i]);
			m_sDisplay += ss;
			if ((i == 7)||(i == 15))
			{
				m_sDisplay += "\r\n";
			}
		}
	}
	else
	{
		m_sDisplay += "ERROR!写块失败！\r\n";
	}
	GetDlgItem(IDC_EDT_DISPLAY)->SetWindowText(m_sDisplay);
}



void CDlg14443A::OnBnClickedBtnClean2()
{
	m_sDisplay = "";
	UpdateData(FALSE);
}

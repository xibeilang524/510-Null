// WlanInfoDlg.cpp : implementation file
//

#include "stdafx.h"
#include "WifiDemo.h"
#include "WlanInfoDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CWlanInfoDlg dialog


CWlanInfoDlg::CWlanInfoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CWlanInfoDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CWlanInfoDlg)
	m_szMac = _T("");
	m_szSSID = _T("");
	m_lRssi = 0;
	m_szPrivacyMode = _T("");
	m_szPrivacyKey = _T("");
	//}}AFX_DATA_INIT
}


void CWlanInfoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CWlanInfoDlg)
	DDX_Text(pDX, IDC_EDIT_MAC, m_szMac);
	DDX_Text(pDX, IDC_EDIT_SSID, m_szSSID);
	DDX_Text(pDX, IDC_EDIT_RSSI, m_lRssi);
	DDX_Text(pDX, IDC_EDIT_PRIVACY_MODE, m_szPrivacyMode);
	DDX_Text(pDX, IDC_EDIT_PRIVACY_KEY, m_szPrivacyKey);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CWlanInfoDlg, CDialog)
	//{{AFX_MSG_MAP(CWlanInfoDlg)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CWlanInfoDlg message handlers

void CWlanInfoDlg::OnOK() 
{
	// TODO: Add extra validation here
	
	CDialog::OnOK();
}

void CWlanInfoDlg::OnCancel() 
{
	// TODO: Add extra cleanup here
	
	CDialog::OnCancel();
}

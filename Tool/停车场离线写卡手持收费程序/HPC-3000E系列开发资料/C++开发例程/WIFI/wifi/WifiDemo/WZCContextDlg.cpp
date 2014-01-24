// WZCContextDlg.cpp : implementation file
//

#include "stdafx.h"
#include "WifiDemo.h"
#include "WZCContextDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CWZCContextDlg dialog


CWZCContextDlg::CWZCContextDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CWZCContextDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CWZCContextDlg)
	m_dwWZCScanTimeOut = 0;
	m_dwWZCScanTimeoutAssociation = 0;
	m_dwWZCScanTimeoutConnect = 0;
	m_dwWZCScanTimeoutDisconnect = 0;
	//}}AFX_DATA_INIT
}


void CWZCContextDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CWZCContextDlg)
	DDX_Text(pDX, IDC_EDIT_WZC_SCAN_TIME_OUT, m_dwWZCScanTimeOut);
	DDX_Text(pDX, IDC_EDIT_WZC_SCAN_TIME_OUT_ASSOCIATION, m_dwWZCScanTimeoutAssociation);
	DDX_Text(pDX, IDC_EDIT_WZC_SCAN_TIME_OUT_CONNECT, m_dwWZCScanTimeoutConnect);
	DDX_Text(pDX, IDC_EDIT_WZC_SCAN_TIME_OUT_DISCONNECT, m_dwWZCScanTimeoutDisconnect);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CWZCContextDlg, CDialog)
	//{{AFX_MSG_MAP(CWZCContextDlg)
		// NOTE: the ClassWizard will add message map macros here
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CWZCContextDlg message handlers

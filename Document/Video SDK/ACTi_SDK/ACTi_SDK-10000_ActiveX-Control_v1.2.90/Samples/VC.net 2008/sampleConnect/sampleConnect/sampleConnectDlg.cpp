
// sampleConnectDlg.cpp : implementation file
//

#include "stdafx.h"
#include "sampleConnect.h"
#include "sampleConnectDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CsampleConnectDlg dialog




CsampleConnectDlg::CsampleConnectDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CsampleConnectDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CsampleConnectDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_NVUNIFIEDCONTROLCTRL1, m_ocxCamera);
	DDX_Control(pDX, IDC_LIST1, m_cbEventList);
	DDX_Control(pDX, IDC_EDIT1, m_edIpAddress);
	DDX_Control(pDX, IDC_EDIT2, m_edFPS);
	DDX_Control(pDX, IDC_EDIT3, m_edResolution);
	DDX_Control(pDX, IDC_EDIT4, m_edBitRate);
}

BEGIN_MESSAGE_MAP(CsampleConnectDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDOK, &CsampleConnectDlg::OnBnClickedOk)
	ON_BN_CLICKED(IDC_BUTTON1, &CsampleConnectDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CsampleConnectDlg::OnBnClickedButton2)
END_MESSAGE_MAP()


// CsampleConnectDlg message handlers

BOOL CsampleConnectDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CsampleConnectDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CsampleConnectDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CsampleConnectDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CsampleConnectDlg::OnBnClickedOk()
{
	// TODO: Add your control notification handler code here
	OnOK();
}

void CsampleConnectDlg::OnBnClickedButton1()
{
	// TODO: Add your control notification handler code here

	CString cstrIpAddress = _T("");

	m_edIpAddress.GetWindowText(cstrIpAddress);

	if (0 == cstrIpAddress.GetLength())
	{
		return;
	}
	
	m_ocxCamera.SetID(1);
	m_ocxCamera.SetStreamType(0);
	m_ocxCamera.SetMediaSource(cstrIpAddress);	
	m_ocxCamera.SetMediaType(1);	//_MEDIA_TYPE_PREVIEW 0:RTP 1:TCP 
	m_ocxCamera.SetDeviceType(CNvunifiedcontrolctrl1::DeviceType::SINGLE_MODE);	//single=0 , quad =1
	m_ocxCamera.SetTCPVideoStreamID(0);
	m_ocxCamera.SetMediaChannel(1);
	m_ocxCamera.SetMediaUsername(_T("admin"));
	m_ocxCamera.SetMediaPassword(_T("123456"));
	m_ocxCamera.SetRegisterPort(6000);
	m_ocxCamera.SetControlPort(6001);
	m_ocxCamera.SetStreamingPort(6002);
	m_ocxCamera.SetRTSPPort(7070);
	m_ocxCamera.SetHttpPort(80);
	m_ocxCamera.SetMute(0);
	m_ocxCamera.SetRTPVideoTrackNumber(0);
	m_ocxCamera.SetRTPAudioTrackNumber(0);
	m_ocxCamera.EnableMotionDetection();
	m_ocxCamera.Connect(0);

	CString strTmp;

	strTmp.Format((_T("%d")),m_ocxCamera.GetFps());

	m_edFPS.SetWindowText(strTmp);

	strTmp.Format((_T("%d")),m_ocxCamera.GetBitRate());

	m_edBitRate.SetWindowText(strTmp);

	strTmp.Format(GetResolution(m_ocxCamera.GetResolution()));

	m_edResolution.SetWindowText(strTmp);
}

void CsampleConnectDlg::OnBnClickedButton2()
{
	// TODO: Add your control notification handler code here
	m_ocxCamera.Play();

	//m_cbEventList.AddString(_T("12345"));
}
BEGIN_EVENTSINK_MAP(CsampleConnectDlg, CDialog)
	ON_EVENT(CsampleConnectDlg, IDC_NVUNIFIEDCONTROLCTRL1, 8, CsampleConnectDlg::OnMDEventStartNvunifiedcontrolctrl1, VTS_I4 VTS_I4)
	ON_EVENT(CsampleConnectDlg, IDC_NVUNIFIEDCONTROLCTRL1, 17, CsampleConnectDlg::OnVideoLossNvunifiedcontrolctrl1, VTS_I4)
	ON_EVENT(CsampleConnectDlg, IDC_NVUNIFIEDCONTROLCTRL1, 2, CsampleConnectDlg::OnNetworkLossNvunifiedcontrolctrl1, VTS_I4)
END_EVENTSINK_MAP()

void CsampleConnectDlg::OnMDEventStartNvunifiedcontrolctrl1(long nID, long nMD)
{
	// TODO: Add your message handler code here

	m_cbEventList.AddString(_T("md event"));
}

void CsampleConnectDlg::OnVideoLossNvunifiedcontrolctrl1(long nID)
{
	// TODO: Add your message handler code here
}

void CsampleConnectDlg::OnNetworkLossNvunifiedcontrolctrl1(long nID)
{
	// TODO: Add your message handler code here
}

CString CsampleConnectDlg::GetResolution(int nResolution)
{
	CString cstrResolution;
	switch(nResolution)
	{
		case 0: 
			cstrResolution = _T("720x480");
			break;
		case 1:
			cstrResolution = _T("352x240");
			break;
		case 2: 
			cstrResolution = _T("160x112");
			break;
		case 3: 
			cstrResolution = _T("720x480");
			break;
		case 4: 
			cstrResolution = _T("352x288");
			break;
		case 5: 
			cstrResolution = _T("176x144");
			break;
		case 6: 
			cstrResolution = _T("176x120");
			break;
		case 44: 
			cstrResolution = _T("1600x1200");
			break;
		case 45: 
			cstrResolution = _T("1920x1080");
			break;
		case 64: 
			cstrResolution = _T("640x480");
			break;
		case 65: 
			cstrResolution = _T("1280x720");
			break;
		case 66: 
			cstrResolution = _T("1280x960");
			break;
		case 67: 
			cstrResolution = _T("1280x1024");
			break;
		default: 
			break;
	}

	return cstrResolution ;
}


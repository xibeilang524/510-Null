// BlueToothS10DLLTestDlg.cpp : implementation file
//

#include "stdafx.h"
#include "BlueToothS10DLLTest.h"
#include "BlueToothS10DLLTestDlg.h"
#include "BlueToothS10.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CBlueToothS10DLLTestDlg dialog

CBlueToothS10DLLTestDlg::CBlueToothS10DLLTestDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CBlueToothS10DLLTestDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CBlueToothS10DLLTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CBlueToothS10DLLTestDlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BNSTART, &CBlueToothS10DLLTestDlg::OnBnClickedBnstart)
	ON_BN_CLICKED(IDC_BNEND, &CBlueToothS10DLLTestDlg::OnBnClickedBnend)
	ON_BN_CLICKED(IDC_BNSETROLE, &CBlueToothS10DLLTestDlg::OnBnClickedBnsetrole)
	ON_BN_CLICKED(IDC_BNAT, &CBlueToothS10DLLTestDlg::OnBnClickedBnat)
	ON_BN_CLICKED(IDC_BNNORMAL, &CBlueToothS10DLLTestDlg::OnBnClickedBnnormal)
	ON_BN_CLICKED(IDC_BNFLTR, &CBlueToothS10DLLTestDlg::OnBnClickedBnfltr)
	ON_BN_CLICKED(IDC_BNINQ, &CBlueToothS10DLLTestDlg::OnBnClickedBninq)
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_BN_TEST, &CBlueToothS10DLLTestDlg::OnBnClickedBnTest)
END_MESSAGE_MAP()


// CBlueToothS10DLLTestDlg message handlers

BOOL CBlueToothS10DLLTestDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	m_hReadEvent = CreateEvent(NULL,TRUE,FALSE,NULL);
	m_hExitEvent = CreateEvent(NULL,TRUE,FALSE,NULL);
	m_hInquireEvent = CreateEvent(NULL,FALSE,FALSE,NULL);
	m_hThread = CreateThread(NULL,0,ThreadProc,this,0,NULL);
	m_hInquire = CreateThread(NULL,0,InquireProc,this,0,NULL);
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CBlueToothS10DLLTestDlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_BLUETOOTHS10DLLTEST_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_BLUETOOTHS10DLLTEST_DIALOG));
	}
}
#endif


void CBlueToothS10DLLTestDlg::OnBnClickedBnstart()
{
	if(S10_Init(COM3) != SUCCESS_SETTING)
	{
		MessageBox(L"start bluetooth commu failed");
	}
}


void CBlueToothS10DLLTestDlg::OnBnClickedBnend()
{
	if(S10_Close() != SUCCESS_SETTING)
	{
		MessageBox(L"end bluetooth commu failed");
	}
}

void CBlueToothS10DLLTestDlg::OnBnClickedBnsetrole()
{
	int cnt = 1;
	while(cnt--)
	{
		int retcode = S10_SetRole(Master);
		if(retcode != SUCCESS_SETTING)
		{
			CString str;
			str.Format(L"set role master failed %d",retcode);
			MessageBox(str);
		}
		else
		{
			MessageBox(L"success");
		}
	}

}

void CBlueToothS10DLLTestDlg::OnBnClickedBnat()
{
	if(S10_SetOperationMode(ATCommandMode) != SUCCESS_SETTING)
	{
		MessageBox(L"set ATCommand mode failed");
	}
}

void CBlueToothS10DLLTestDlg::OnBnClickedBnnormal()
{
	if(S10_SetOperationMode(CommunicationMode) != SUCCESS_SETTING)
	{
		MessageBox(L"set CommunicationMode mode failed");
	}
}

void CBlueToothS10DLLTestDlg::OnBnClickedBnfltr()
{
	int cnt = 1;
	while(cnt--)
	{
		int retcode = S10_SetFilterMode(false,false);
		if(retcode != SUCCESS_SETTING)
		{
			CString str;
			str.Format(L"set role master failed %d",retcode);
			MessageBox(str);
		}
		else
		{
			MessageBox(L"success");
		}
	}
}

void CBlueToothS10DLLTestDlg::AppendInfo(CString &str)
{
	CEdit * pEdit = (CEdit*) GetDlgItem(IDC_TXT_INFO);
	int pre = pEdit->GetWindowTextLength();
	pEdit->SetSel(pre,pre);
	pEdit->ReplaceSel(str);
	int now = pEdit->GetWindowTextLength();
	pEdit->SetSel(pre,now);
}
void CBlueToothS10DLLTestDlg::OnBnClickedBninq()
{
	SetEvent(m_hReadEvent);
	SetEvent(m_hInquireEvent);
// 	int retcode = S10_InquireDevice(true,1024,60);
// 	if(retcode != SUCCESS_SETTING)
// 	{
// 		CString str;
// 		str.Format(L"inquire device failed %d",retcode);
// 		MessageBox(str);
// 	}
// 	Sleep(100);
// 	ResetEvent(m_hReadEvent);
}

DWORD CBlueToothS10DLLTestDlg::ThreadProc(PVOID pArg)
{
	CBlueToothS10DLLTestDlg * pDlg = (CBlueToothS10DLLTestDlg*)pArg;
	HANDLE handle[2];
	handle[0] = pDlg->m_hExitEvent;
	handle[1] = pDlg->m_hReadEvent;
	CString str;
	while(true)
	{
		int waitobj = WaitForMultipleObjects(2,handle,FALSE,INFINITE);
		if(waitobj != WAIT_OBJECT_0+1) break;
		BlueToothDevice btd;
		if(S10_GetInquiredDevice(btd))
		{
			str.Format(L"addr:%02X%02X%02X%02X%02X%02X\r\ndc:%04x\r\nrssi:%d\r\n",btd.addr[0],btd.addr[1],
				btd.addr[2],btd.addr[3],btd.addr[4],btd.addr[5],btd.deviceClass,btd.rssi);
			pDlg->AppendInfo(str);
		}
	}
	return 0;
}

DWORD CBlueToothS10DLLTestDlg::InquireProc(PVOID pArg)
{
	CBlueToothS10DLLTestDlg * pDlg = (CBlueToothS10DLLTestDlg*)pArg;
	HANDLE handle[2];
	handle[0] = pDlg->m_hExitEvent;
	handle[1] = pDlg->m_hInquireEvent;
	while(true)
	{
		int waitobj = WaitForMultipleObjects(2,handle,FALSE,INFINITE);
		if(waitobj != WAIT_OBJECT_0 +1) break;
		int retcode = S10_InquireDevice(false,1024,60);
		if(retcode != SUCCESS_SETTING)
		{
			CString str;
			str.Format(L"inquire device failed %d",retcode);
			pDlg->MessageBox(str);
		}
		Sleep(100);
		ResetEvent(pDlg->m_hReadEvent);
	}
	return 0;
}
void CBlueToothS10DLLTestDlg::OnDestroy()
{
	CDialog::OnDestroy();
	S10_Close();
	SetEvent(m_hExitEvent);
	WaitForSingleObject(m_hThread,500);
	WaitForSingleObject(m_hInquire,500);
	CloseHandle(m_hReadEvent);
	CloseHandle(m_hExitEvent);
	CloseHandle(m_hInquireEvent);
	CloseHandle(m_hThread);
	CloseHandle(m_hInquire);
}

void CBlueToothS10DLLTestDlg::OnBnClickedBnTest()
{
	if(S10_SetOperationMode(ATCommandMode)!=SUCCESS_SETTING) {MessageBox(_T("S10_SetOperationMode error"));return;}
	if(S10_Init(COM3)!=SUCCESS_SETTING) {MessageBox(_T("S10_Init error"));return;}
	int cnt;
	int retcode ;
	unsigned char buf[1024];
	unsigned int bufLength=1024;
	S10_Role role;
	const int MAXCNT = 10;
	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_Reboot())!=SUCCESS_SETTING) {MessageBox(_T("S10_Reboot error")); if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	Sleep(5000);
	cnt=MAXCNT;
	while(cnt--)
	{if((retcode=S10_GetModuleVersion((char*)buf,bufLength))!=SUCCESS_SETTING)	{MessageBox(_T("S10_GetModuleVersion error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));bufLength=1024;goto finish;}
	if(bufLength == 0)
	{
		buf[bufLength]=0;
	}
	memset(buf,0,bufLength);
	}
	

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_RestoreFactorySettings())!=SUCCESS_SETTING) {MessageBox(_T("S10_RestoreFactorySettings error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_GetAddress(buf))!=SUCCESS_SETTING) {MessageBox(_T("S10_GetAddress error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_GetDeviceName((char*)buf,bufLength))!=SUCCESS_SETTING) {MessageBox(_T("S10_GetDeviceName error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_SetDeviceName((char*)buf,bufLength))!=SUCCESS_SETTING) {MessageBox(_T("S10_SetDeviceName error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_GetRole(role))!=SUCCESS_SETTING) {MessageBox(_T("S10_GetRole error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_SetRole(Master))!=SUCCESS_SETTING) {MessageBox(_T("S10_SetRole error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	unsigned int deviceClass;
	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_GetDeviceClass(deviceClass))!=SUCCESS_SETTING) {MessageBox(_T("S10_GetDeviceClass error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_SetDeviceClass(deviceClass))!=SUCCESS_SETTING) {MessageBox(_T("S10_SetDeviceClass error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	unsigned int accessCode;
	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_GetAccessCode(accessCode))!=SUCCESS_SETTING) {MessageBox(_T("S10_GetAccessCode error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_SetAccessCode(accessCode))!=SUCCESS_SETTING) {MessageBox(_T("S10_SetAccessCode error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout")); goto finish;}

	cnt=MAXCNT;
	bufLength = 1024;
	while(cnt--) if((retcode=S10_GetMatchingCode((char*)buf,bufLength))!=SUCCESS_SETTING) {MessageBox(_T("S10_GetMatchingCode error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_SetMatchingCode((char*)buf,bufLength))!=SUCCESS_SETTING) {MessageBox(_T("S10_SetMatchingCode error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	bool bBinding;
	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_GetConnectMode(bBinding))!=SUCCESS_SETTING) {MessageBox(_T("S10_GetConnectMode error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_SetConnectMode(bBinding))!=SUCCESS_SETTING) {MessageBox(_T("S10_SetConnectMode error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_GetBindingAddress(buf))!=SUCCESS_SETTING) {MessageBox(_T("S10_GetBindingAddress error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_SetBindingAddress(buf))!=SUCCESS_SETTING) {MessageBox(_T("S10_SetBindingAddress error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	unsigned int safeLevel,encryptLevel;
	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_GetSafeParameters(safeLevel,encryptLevel))!=SUCCESS_SETTING) {MessageBox(_T("S10_GetSafeParameters error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_SetSafeParameters(safeLevel,encryptLevel))!=SUCCESS_SETTING) {MessageBox(_T("S10_SetSafeParameters error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_InquireCancel())!=SUCCESS_SETTING) {MessageBox(_T("S10_InquireCancel error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_AutoDormany(true))!=SUCCESS_SETTING) {MessageBox(_T("S10_AutoDormany error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	bool fltrDeviceClass,fltrAccessCode;
	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_GetFilterMode(fltrDeviceClass,fltrAccessCode))!=SUCCESS_SETTING) {MessageBox(_T("S10_GetFilterModeerror"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	while(cnt--) if((retcode=S10_SetFilterMode(false,false))!=SUCCESS_SETTING) {MessageBox(_T("S10_SetFilterModeerror"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}

	cnt=MAXCNT;
	BlueToothDevice btd;
	while(cnt--)
	{
		if((retcode=S10_InquireDevice(false,1,30))!=SUCCESS_SETTING)	{MessageBox(_T("S10_InquireDevice error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}
		if((retcode=S10_GetInquiredDevice(btd))==false)	{MessageBox(_T("S10_GetInquiredDevice error"));goto finish;}
		bufLength=1024;
		if((retcode=S10_GetRemoteDeviceName(btd.addr,(char*)buf,bufLength))!=SUCCESS_SETTING)	{MessageBox(_T("S10_GetRemoteDeviceName error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}
		if((retcode=S10_MatchDevice(btd.addr,20))!=SUCCESS_SETTING)	{MessageBox(_T("S10_MatchDevice error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}
		if((retcode=S10_ConnectDevice(btd.addr))!=SUCCESS_SETTING)	{MessageBox(_T("S10_ConnectDevice error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}
		if((retcode=S10_Disconnect())!=SUCCESS_SETTING)	{MessageBox(_T("S10_Disconnect error"));if(retcode==RECEIVETIMEOUT) MessageBox(_T("timeout"));goto finish;}
	}
finish:
	CString str;str.Format(L"%d %d",cnt,retcode);MessageBox(str);
	S10_Close();
}

// Search.cpp : 实现文件
//

#include "stdafx.h"
#include "BlueToothS10Test.h"
#include "Search.h"
#include "BlueToothS10.h"


// CSearch 对话框

IMPLEMENT_DYNAMIC(CSearch, CDialog)

CSearch::CSearch(CWnd* pParent /*=NULL*/)
	: CDialog(CSearch::IDD, pParent)
{

}

CSearch::~CSearch()
{
}

void CSearch::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_DEVICES, m_ListDevices);
}


BEGIN_MESSAGE_MAP(CSearch, CDialog)
	ON_BN_CLICKED(IDC_BN_INQ, &CSearch::OnBnClickedBnInq)
	ON_BN_CLICKED(IDC_BN_INQC, &CSearch::OnBnClickedBnInqc)
	ON_BN_CLICKED(IDC_BN_BIND, &CSearch::OnBnClickedBnBind)
	ON_BN_CLICKED(IDC_BN_PAIR, &CSearch::OnBnClickedBnPair)
	ON_BN_CLICKED(IDC_BN_LINK, &CSearch::OnBnClickedBnLink)
	ON_BN_CLICKED(IDC_BN_DISC, &CSearch::OnBnClickedBnDisc)
	ON_WM_DESTROY()
END_MESSAGE_MAP()


// CSearch 消息处理程序

BOOL CSearch::OnInitDialog()
{
	CDialog::OnInitDialog();


	m_hReadEvent = CreateEvent(NULL,TRUE,FALSE,NULL);
	m_hExitEvent = CreateEvent(NULL,TRUE,FALSE,NULL);
	m_hInquireEvent = CreateEvent(NULL,FALSE,FALSE,NULL);
	m_hThread = CreateThread(NULL,0,ThreadProc,this,0,NULL);
	m_hInquire = CreateThread(NULL,0,InquireProc,this,0,NULL);

	m_ListDevices.InsertColumn(0,_T("Addr"),LVCFMT_LEFT,100,-1);
	m_ListDevices.InsertColumn(1,_T("Class"),LVCFMT_LEFT,100,-1);

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}
void CSearch::OnBnClickedBnInq()
{
	S10_SetFilterMode(false,false);	//	设置过滤规则
	SetEvent(m_hReadEvent);
	SetEvent(m_hInquireEvent);
}

void CSearch::OnBnClickedBnInqc()
{
	S10_InquireCancel();
}

void CSearch::OnBnClickedBnBind()
{
	if(m_ListDevices.GetSelectionMark() !=-1)
	{
		if(AssertResult(S10_SetBindingAddress(m_vcBTDs[m_ListDevices.GetSelectionMark()].addr)))
		{
			MessageBox(_T("bind device success"));
		}
	}
}

void CSearch::OnBnClickedBnPair()
{
	if(m_ListDevices.GetSelectionMark() !=-1)
	{
		if(AssertResult(S10_MatchDevice(m_vcBTDs[m_ListDevices.GetSelectionMark()].addr,20)))
		{
			MessageBox(_T("match device success"));
		}
	}
}

void CSearch::OnBnClickedBnLink()
{
	if(m_ListDevices.GetSelectionMark() !=-1)
	{
		if(AssertResult(S10_ConnectDevice(m_vcBTDs[m_ListDevices.GetSelectionMark()].addr)))
		{
			MessageBox(_T("connect device success"));
		}
	}
}

void CSearch::OnBnClickedBnDisc()
{
	AssertResult(S10_Disconnect());
}

void CSearch::OnDestroy()
{
	CDialog::OnDestroy();

	SetEvent(m_hExitEvent);
	WaitForSingleObject(m_hThread,500);
	WaitForSingleObject(m_hInquire,500);
	CloseHandle(m_hReadEvent);
	CloseHandle(m_hExitEvent);
	CloseHandle(m_hInquireEvent);
	CloseHandle(m_hThread);
	CloseHandle(m_hInquire);
}



DWORD CSearch::ThreadProc(PVOID pArg)
{
	CSearch * pDlg = (CSearch*)pArg;
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
			pDlg->m_vcBTDs.push_back(btd);
			pDlg->m_ListDevices.InsertItem(pDlg->m_vcBTDs.size()-1,_T(""));
			str.Format(_T("%02X%02X,%02X,%02X%02X%02X"),btd.addr[0],btd.addr[1],
				 				btd.addr[2],btd.addr[3],btd.addr[4],btd.addr[5]);
			pDlg->m_ListDevices.SetItemText(pDlg->m_vcBTDs.size()-1,0,str);
			str.Format(_T("%04X"),btd.deviceClass);
			pDlg->m_ListDevices.SetItemText(pDlg->m_vcBTDs.size()-1,1,str);
		}
		Sleep(10);
	}
	return 0;
}

DWORD CSearch::InquireProc(PVOID pArg)
{
	CSearch * pDlg = (CSearch*)pArg;
	HANDLE handle[2];
	handle[0] = pDlg->m_hExitEvent;
	handle[1] = pDlg->m_hInquireEvent;
	while(true)
	{
		int waitobj = WaitForMultipleObjects(2,handle,FALSE,INFINITE);
		if(waitobj != WAIT_OBJECT_0 +1) break;
		pDlg->m_ListDevices.DeleteAllItems();
		pDlg->m_vcBTDs.clear();
		AssertResult(S10_InquireDevice(false,1024,60));
		Sleep(100);
		ResetEvent(pDlg->m_hReadEvent);
	}
	return 0;
}
void CSearch::OnCancel()
{
}

void CSearch::OnOK()
{
}

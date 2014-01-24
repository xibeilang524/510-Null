// EM1300ScanDemoDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "EM1300ScanDemo.h"
#include "EM1300ScanDemoDlg.h"
#include "EM3070DLL.h"
#include "epcBuzzerLib.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

CString strDisp = _T("");                                           /* 初始化                       */

// CEM1300ScanDemoDlg 对话框
UCHAR pattern[]={SCAN_MANUAL,SCAN_AUTO,SCAN_CONTINUE,SCAN_SINGLE};

CEM1300ScanDemoDlg::CEM1300ScanDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CEM1300ScanDemoDlg::IDD, pParent)
	, m_strDisp(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CEM1300ScanDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_CB_PATTERN, m_cbPattern);
	DDX_Control(pDX, IDC_BN_SETPATTERN, m_bnSetPattern);
	DDX_Control(pDX, IDC_BN_START, m_bnStart);
	DDX_Control(pDX, IDC_BN_END, m_bnEnd);
	DDX_Control(pDX, IDC_BN_VERSION, m_bnVersion);
	DDX_Control(pDX, IDC_BNCLEAR, m_bnClear);
}

BEGIN_MESSAGE_MAP(CEM1300ScanDemoDlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_BN_SETPATTERN, &CEM1300ScanDemoDlg::OnBnClickedBnSetpattern)
	ON_BN_CLICKED(IDC_BN_START, &CEM1300ScanDemoDlg::OnBnClickedBnStart)
	ON_BN_CLICKED(IDC_BN_END, &CEM1300ScanDemoDlg::OnBnClickedBnEnd)
	ON_BN_CLICKED(IDC_BN_VERSION, &CEM1300ScanDemoDlg::OnBnClickedBnVersion)
	ON_BN_CLICKED(IDC_BNCLEAR, &CEM1300ScanDemoDlg::OnBnClickedBnclear)
	ON_MESSAGE(WM_MYMESSAGE,OnMyMessage)
	ON_WM_TIMER()
END_MESSAGE_MAP()


// CEM1300ScanDemoDlg 消息处理程序

BOOL CEM1300ScanDemoDlg::OnInitDialog()
{
	CDialog::OnInitDialog();
    int ret;

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	::SetWindowPos(this->m_hWnd,HWND_TOPMOST,0,0,GetSystemMetrics(SM_CXSCREEN),
		GetSystemMetrics(SM_CYSCREEN), 0);
	m_cbPattern.AddString(L"手动扫描");
	m_cbPattern.AddString(L"自动扫描");
	m_cbPattern.AddString(L"持续扫描");
	m_cbPattern.AddString(L"单次连续自动扫描");
	m_cbPattern.AddString(L"");
	m_cbPattern.SetCurSel(0);

	DWORD     dwThreadId;

	//int ret = EM1300SerialInit(COM2);
    ret = EM3070SerialInit(COM2);

	if( ret != INIT_OK) {
		CString str;
		str.Format(_T("%d"),ret);
		AfxMessageBox(_T("启动通信失败！")+str);	
		::PostQuitMessage(0);
	}

	m_hExitThreadEvent = CreateEvent(NULL, TRUE, FALSE, NULL);            /*  创建串口接收线程退出事件  */

	m_Thread = CreateThread(NULL, 0, ThreadProc, this, 0, &dwThreadId);    /* 创建线程                 */

	/*
	*  判断线程是否创建成功
	*/
	if (!m_Thread) {
		CloseHandle(m_hExitThreadEvent);				                    /* 关闭线程退出事件句柄    */
		AfxMessageBox(_T("创建线程失败！"));

		::PostQuitMessage(0);
	}	
	
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CEM1300ScanDemoDlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_EM1300SCANDEMO_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_EM1300SCANDEMO_DIALOG));
	}
}
#endif


/*********************************************************************************************************
** Function name:           ThreadProc
** Descriptions:            线程函数                        
** input parameters:        pArg                   对象指针			
** output parameters:       NONE                            
** 
** Returned value:          0(线程结束)
*********************************************************************************************************/
DWORD CEM1300ScanDemoDlg::ThreadProc(PVOID pArg)
{
	BYTE ReceiveBuf[MAXSIZE];
	BYTE ucCount = 0;
	BYTE i;
	CString strTmp;
	DWORD   ulState;
    LPWSTR wBuf;

	/*
	*  取得实时显示框的句柄
	*/
	CEM1300ScanDemoDlg* pDlg = (CEM1300ScanDemoDlg*)pArg;                              
	
	while(TRUE) {

		/* 
		*  等待线程退出事件 
		*/
		if (WaitForSingleObject(pDlg->m_hExitThreadEvent, 0) == WAIT_OBJECT_0)  {
			break;	
		}

		/*
		*  判断接收数据是否成功
		*/
		ulState = EM3070GetDecodeData(ReceiveBuf, &ucCount);
		if (ulState == RECEIVE_SUCCESS) {
// 			for (i = 0; i < ucCount; i++) {
// 				strTmp.Format(_T("%c"), ReceiveBuf[i]);   
// 				strDisp += strTmp; 
// 			}	
			//MultiByteToWideChar();
//            MultiByteToWideChar(CP_ACP, 0, (LPCSTR)ReceiveBuf, -1, wBuf, ucCount);
            ReceiveBuf[ucCount] = 0;
            CString temp;
            temp = ReceiveBuf;
            strDisp += temp;
            ::SendMessage(pDlg->m_hWnd,WM_MYMESSAGE,0,0);
			epcBuzzerOn(50);
			strDisp = _T("");                                           /* 将strDisp清除                 */
		}
		Sleep(2);                                                       /* 线程睡眠2ms                   */
	}
	return 0;	
}

void CEM1300ScanDemoDlg::OnDestroy()
{
	CDialog::OnDestroy();
	if(!EM3070SerialTerminate())
	{
		AfxMessageBox(_T("终止通信失败!"));
	}

	if (m_Thread != NULL) {	
		SetEvent(m_hExitThreadEvent);								    /*  通知串口接收线程退出        */

		WaitForSingleObject(m_Thread, 500);		                        /*  等待线程退出                */
		CloseHandle(m_Thread);										    /*  关闭接收线程句柄            */
		CloseHandle(m_hExitThreadEvent);				                /*  关闭线程退出事件句柄        */		
		m_Thread = NULL;
	}
}

void CEM1300ScanDemoDlg::OnBnClickedBnSetpattern()
{
	int curPattern = m_cbPattern.GetCurSel();
	if(EM3070TriggerState(pattern[curPattern]) != SUCCESS_SETTING)
	{
		AfxMessageBox(_T("设置触发模式失败"));
		return;
	}
	if(curPattern == 0)
	{
		m_bnStart.EnableWindow();
		m_bnEnd.EnableWindow();
	}
	else
	{
		m_bnStart.EnableWindow(FALSE);
		m_bnEnd.EnableWindow(FALSE);
	}
}

void CEM1300ScanDemoDlg::OnBnClickedBnStart()
{
	if(EM3070DecodeState(TRUE) != SUCCESS_SETTING)
	{
		AfxMessageBox(_T("启动解码失败!"));
	}
}

void CEM1300ScanDemoDlg::OnBnClickedBnEnd()
{
	if(EM3070DecodeState(FALSE) != SUCCESS_SETTING)
	{
		AfxMessageBox(_T("停止解码失败!"));
	}
}

void CEM1300ScanDemoDlg::OnBnClickedBnVersion()
{
	DWORD  ulState;
	ulState = EM3070GetVersion();
	CString str;
	str.Format(L"软件版本 :V%.2lf!",ulState*1.0/100);
	AfxMessageBox(str);
}

void CEM1300ScanDemoDlg::OnBnClickedBnclear()
{
	CEdit * disp=(CEdit*)GetDlgItem(IDC_DISP);
	disp->SetSel(0,-1);
	disp->Clear();
	disp->SetFocus();
}

LRESULT CEM1300ScanDemoDlg::OnMyMessage(WPARAM wParam, LPARAM lParam)
{
	CEdit *disp = (CEdit*)GetDlgItem(IDC_DISP);            /* 获得控件句柄                 */

	int prev = disp->GetWindowTextLength();
	disp->SetSel(prev,prev);
	disp->ReplaceSel(strDisp + _T("读取成功！\r\n"));
	int next = disp->GetWindowTextLength();
	disp->SetSel(prev,next);
	disp->SetFocus();
	return 0;
}


volatile bool bContinue = false;
volatile bool bTimerset = false;
volatile bool bF20Uped	= false;
BOOL CEM1300ScanDemoDlg::PreTranslateMessage(MSG* pMsg)
{
	static DWORD pre = 0;
	if (pMsg->message == WM_KEYDOWN && bF20Uped == true && bContinue == true)
	{
		EM3070TriggerState(SCAN_MANUAL);
		bContinue = false;
	}
	else if (pMsg->message == WM_KEYDOWN && pMsg->wParam==VK_F20 && !bContinue)//键盘对应数字或ASCII码    
	{
		if(!bTimerset)
			SetTimer(1,5000,NULL);
		bTimerset=true;
		DWORD now=GetTickCount();
		if(now-pre>300)
		{
			pre=now;
			int ret=EM3070DecodeState(TRUE);
			if (ret != SUCCESS_SETTING) 
			{
				AfxMessageBox(L"设置错误！");
				return 0;
			}
		}
		bF20Uped = false;
	}
	else if (pMsg->message == WM_KEYUP && pMsg->wParam==VK_F20)
	{
		if(bTimerset)
			KillTimer(1);
		bTimerset=false;
		bF20Uped = true;
	}
	return CDialog::PreTranslateMessage(pMsg); 
}
void CEM1300ScanDemoDlg::OnTimer(UINT_PTR nIDEvent)
{
	if(nIDEvent == 1)
	{
		bContinue=true;
		EM3070TriggerState(SCAN_CONTINUE);
		KillTimer(1);
		bTimerset=false;
	}

	CDialog::OnTimer(nIDEvent);

	CDialog::OnTimer(nIDEvent);
}

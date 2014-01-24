// Se955ScanDemoDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "Se955ScanDemo.h"
#include "Se955ScanDemoDlg.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CSe955ScanDemoDlg 对话框

CSe955ScanDemoDlg::CSe955ScanDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSe955ScanDemoDlg::IDD, pParent)
	, m_strdisp(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_Thread = NULL;
	m_hExitThreadEvent = NULL;
}

void CSe955ScanDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_BAUD, m_comboBaud);
	DDX_Text(pDX, IDC_DISP, m_strdisp);
	DDX_Control(pDX, IDC_COM, m_comboCom);
}

BEGIN_MESSAGE_MAP(CSe955ScanDemoDlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_START, &CSe955ScanDemoDlg::OnBnClickedStart)
	ON_BN_CLICKED(IDC_TERMINATE, &CSe955ScanDemoDlg::OnBnClickedTerminate)
	ON_BN_CLICKED(IDC_STARTSCAN, &CSe955ScanDemoDlg::OnBnClickedStartscan)
	ON_BN_CLICKED(IDC_CLOSESCAN, &CSe955ScanDemoDlg::OnBnClickedClosescan)
	ON_BN_CLICKED(IDC_SOFTTRIG, &CSe955ScanDemoDlg::OnBnClickedSofttrig)
	ON_BN_CLICKED(IDC_CONTINUETRIG, &CSe955ScanDemoDlg::OnBnClickedContinuetrig)
	ON_BN_CLICKED(IDC_VERSION, &CSe955ScanDemoDlg::OnBnClickedVersion)
	ON_BN_CLICKED(IDC_CLR, &CSe955ScanDemoDlg::OnBnClickedClr)
	ON_WM_DESTROY()
END_MESSAGE_MAP()


// CSe955ScanDemoDlg 消息处理程序

/*********************************************************************************************************
** Function name:           OnInitDialog
** Descriptions:            对话框初始化代码                        
** input parameters:        NONE			
** output parameters:       NONE                            
** 
** Returned value:          NONE
*********************************************************************************************************/
BOOL CSe955ScanDemoDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码
	m_comboCom.SetCurSel(0);                                            /* 默认选用COM2                  */
	m_comboBaud.SetCurSel(5);			                                /* 默认选用9600波特率            */
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CSe955ScanDemoDlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_SE955SCANDEMO_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_SE955SCANDEMO_DIALOG));
	}
}
#endif



static UCHAR GucComNo = 0;


/*********************************************************************************************************
串口号表
*********************************************************************************************************/
const UCHAR     GenPortTbl[5]   = {COM1, COM2, COM3, COM4, COM5};


/*********************************************************************************************************
波特率表
*********************************************************************************************************/
const UCHAR     GenBaudTbl[8]   = {B_RATE300,  B_RATE600,   B_RATE1200, B_RATE2400, 
B_RATE4800, B_RATE9600, B_RATE19200, B_RATE38400};

/*********************************************************************************************************
停止位表
*********************************************************************************************************/
const UCHAR     GenStopTbl[2]   = {STOP_BITONE, STOP_BITTWO};


/*********************************************************************************************************
校验位表
*********************************************************************************************************/
const UCHAR     GenParityTbl[5] = {DAT_ODD, DAT_EVEN, DAT_MARK, DAT_SPACE, DAT_NONE};
/*********************************************************************************************************



/*********************************************************************************************************
** Function name:           OnDestroy
** Descriptions:            销毁窗口时调用                      
** input parameters:        NONE			
** output parameters:       NONE                            
** 
** Returned value:          NONE
*********************************************************************************************************/
void CSe955ScanDemoDlg::OnDestroy() 
{
	CDialog::OnDestroy();

	// TODO: Add your message handler code here
	Se955SerialTerminate();	

	if (m_Thread != NULL) {	
		SetEvent(m_hExitThreadEvent);								    /*  通知串口接收线程退出        */

		WaitForSingleObject(m_Thread, 500);		                        /*  等待线程退出                */
		CloseHandle(m_Thread);										    /*  关闭接收线程句柄            */
		CloseHandle(m_hExitThreadEvent);				                /*  关闭线程退出事件句柄        */		
		m_Thread = NULL;
	}
	
}



/*********************************************************************************************************
** Function name:           OnBnClickedStart
** Descriptions:            启动通信                        
** input parameters:        NONE			
** output parameters:       NONE                            
** 
** Returned value:          NONE
*********************************************************************************************************/
void CSe955ScanDemoDlg::OnBnClickedStart()
{
	// TODO: Add your control notification handler code here
	CButton   *pcbtnOpenClose;
	DWORD     dwThreadId;
    UCHAR     ucBaud;

    /* 
	 *  取得串口波特率、停止位、奇偶校验位
	 */
	UpdateData(TRUE);
 	GucComNo = GenPortTbl[m_comboCom.GetCurSel()];
    
	/* 
	 *  取得串口波特率、停止位、奇偶校验位
	 */
	GucComNo = GenPortTbl[m_comboCom.GetCurSel()];
    ucBaud   = GenBaudTbl[m_comboBaud.GetCurSel()];	
   
	/* 
	 *  获取串口号启动通信
	 */	
	if(Se955SerialInit(GucComNo,ucBaud,STOP_BITONE,DAT_NONE) != INIT_OK) {
		AfxMessageBox(_T("启动通信失败！"));	
		return;
	}

	m_hExitThreadEvent = CreateEvent(NULL, TRUE, FALSE, NULL);            /*  创建串口接收线程退出事件  */
	
	m_Thread = CreateThread(NULL, 0, ThreadProc, this, 0, &dwThreadId);    /* 创建线程                 */
	  
	/*
	 *  判断线程是否创建成功
	 */
	if (!m_Thread) {
		CloseHandle(m_hExitThreadEvent);				                    /* 关闭线程退出事件句柄    */
		AfxMessageBox(_T("创建线程失败！"));
		return;
	}	
	pcbtnOpenClose = (CButton *)GetDlgItem(IDC_START); 	                /* 设置"启动通信"按钮无效         */
	pcbtnOpenClose->EnableWindow(FALSE);
    
	pcbtnOpenClose = (CButton *)GetDlgItem(IDC_TERMINATE); 	            /* 设置"终止通信"按钮有效         */
	pcbtnOpenClose->EnableWindow(TRUE);

}


/*********************************************************************************************************
** Function name:           OnBnClickedTerminate
** Descriptions:            终止通信                        
** input parameters:        NONE			
** output parameters:       NONE                            
** 
** Returned value:          NONE
*********************************************************************************************************/
void CSe955ScanDemoDlg::OnBnClickedTerminate()
{
	// TODO: Add your control notification handler code here
	CButton   *pcbtnOpenClose;

	/* 
	 *  关闭串口
	 */
	Se955SerialTerminate();

	if (m_Thread != NULL) {	
		SetEvent(m_hExitThreadEvent);								    /*  通知串口接收线程退出        */

		WaitForSingleObject(m_Thread, 500);		                        /*  等待线程退出                */
		CloseHandle(m_Thread);										    /*  关闭接收线程句柄            */
		CloseHandle(m_hExitThreadEvent);				                /*  关闭线程退出事件句柄        */		
		m_Thread = NULL;
	}
   


    /* 
	 *  设置"启动通信"按钮有效， 设置"终止通信"按钮无效
	 */
	pcbtnOpenClose = (CButton *)GetDlgItem(IDC_START); 	
	pcbtnOpenClose->EnableWindow(TRUE);
    
	pcbtnOpenClose = (CButton *)GetDlgItem(IDC_TERMINATE); 	
	pcbtnOpenClose->EnableWindow(FALSE);	
}



/*********************************************************************************************************
** Function name:           OnBnClickedStartscan
** Descriptions:            开始扫描                        
** input parameters:        NONE			
** output parameters:       NONE                            
** 
** Returned value:          NONE
*********************************************************************************************************/
void CSe955ScanDemoDlg::OnBnClickedStartscan()
{
	// TODO: Add your control notification handler code here
	if (Se955DecodeState(TRUE) != SUCCESS_SETTING) {
		AfxMessageBox(_T("设置错误！"));
	}

}

/*********************************************************************************************************
** Function name:           OnBnClickedClosescan
** Descriptions:            关闭扫描                        
** input parameters:        NONE			
** output parameters:       NONE                            
** 
** Returned value:          NONE
*********************************************************************************************************/
void CSe955ScanDemoDlg::OnBnClickedClosescan()
{
	// TODO: Add your control notification handler code here
	if (Se955DecodeState(FALSE) != SUCCESS_SETTING) {
		AfxMessageBox(_T("设置错误！"));
	}
}

/*********************************************************************************************************
** Function name:           OnBnClickedSofttrig
** Descriptions:            设置为软件扫描方式                       
** input parameters:        NONE			
** output parameters:       NONE                            
** 
** Returned value:          NONE
*********************************************************************************************************/
void CSe955ScanDemoDlg::OnBnClickedSofttrig()
{
	// TODO: Add your control notification handler code here
	CButton   *pcbtnOpenClose;

	if (Se955TriggerState(SCAN_HOST) != SUCCESS_SETTING) {
		AfxMessageBox(_T("设置错误！"));
	}

	pcbtnOpenClose = (CButton *)GetDlgItem(IDC_STARTSCAN); 	           /* 设置"开始扫描"按钮有效       */
	pcbtnOpenClose->EnableWindow(TRUE);

	pcbtnOpenClose = (CButton *)GetDlgItem(IDC_CLOSESCAN); 	           /* 设置"关闭扫描"按钮有效       */
	pcbtnOpenClose->EnableWindow(TRUE);
}

/*********************************************************************************************************
** Function name:           OnBnClickedContinuetrig
** Descriptions:            设置为持续扫描方式                        
** input parameters:        NONE			
** output parameters:       NONE                            
** 
** Returned value:          NONE
*********************************************************************************************************/
void CSe955ScanDemoDlg::OnBnClickedContinuetrig()
{
	// TODO: Add your control notification handler code here	
	CButton   *pcbtnOpenClose;

	if (Se955TriggerState(SCAN_CONTINUE) != SUCCESS_SETTING) {
		AfxMessageBox(_T("设置错误！"));
	}

	pcbtnOpenClose = (CButton *)GetDlgItem(IDC_STARTSCAN); 	           /* 设置"开始扫描"按钮无效         */
	pcbtnOpenClose->EnableWindow(FALSE);

	pcbtnOpenClose = (CButton *)GetDlgItem(IDC_CLOSESCAN); 	           /* 设置"关闭扫描"按钮无效         */
	pcbtnOpenClose->EnableWindow(FALSE);
}






/*********************************************************************************************************
** Function name:           ThreadProc
** Descriptions:            线程函数                        
** input parameters:        pArg                   对象指针			
** output parameters:       NONE                            
** 
** Returned value:          0(线程结束)
*********************************************************************************************************/
DWORD CSe955ScanDemoDlg::ThreadProc(PVOID pArg)
{
	BYTE ReceiveBuf[MAXSIZE];
	BYTE ucCount = 0;
	BYTE i;
	CString strTmp;
	CString strDisp = _T("");                                           /* 初始化                       */
	DWORD   ulState;
	
	/*
	 *  取得实时显示框的句柄
	 */
	CSe955ScanDemoDlg* pDlg = (CSe955ScanDemoDlg*)pArg;                              
    CEdit *pceditRcv = (CEdit*)pDlg->GetDlgItem(IDC_DISP);            /* 获得控件句柄                 */
	
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
		ulState = Se955GetDecodeData(ReceiveBuf, &ucCount);
		if (ulState == RECEIVE_SUCCESS) {
			for (i = 0; i < ucCount; i++) {
				strTmp.Format(_T("%c"), ReceiveBuf[i]);   
				strDisp += strTmp; 
			}	

			pDlg->m_strdisp += strDisp;
			pDlg->m_strdisp += _T("\r\n读取成功！\r\n");
			pceditRcv->SetWindowText(pDlg->m_strdisp);
			
			strDisp = _T("");                                           /* 将strDisp清除                 */
		}
		Sleep(2);                                                       /* 线程睡眠2ms                   */
	}
	return 0;	
}




/*********************************************************************************************************
** Function name:           OnBnClickedVersion
** Descriptions:            获取软件包版本                       
** input parameters:        NONE			
** output parameters:       NONE                            
** 
** Returned value:          NONE
*********************************************************************************************************/
void CSe955ScanDemoDlg::OnBnClickedVersion()
{
	// TODO: Add your control notification handler code here
	DWORD  ulState;
	ulState = Se955GetVersion();
	switch (ulState)
	{
		case 100:
		AfxMessageBox(_T("软件版本是V1.00！"));
		break;
		case 101:
		AfxMessageBox(_T("软件版本是V1.01！"));
		break;
		default:
		break;
	}
}


/*********************************************************************************************************
** Function name:           OnBnClickedClr
** Descriptions:            清除显示窗口内容                       
** input parameters:        NONE			
** output parameters:       NONE                            
** 
** Returned value:          NONE
*********************************************************************************************************/
void CSe955ScanDemoDlg::OnBnClickedClr()
{
	// TODO: Add your control notification handler code here
	m_strdisp = _T("");	
	UpdateData(FALSE);
}

// ReadBarcodeDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ReadBarcode.h"
#include "ReadBarcodeDlg.h"
#include "epcBuzzerLib.h"
#include "winsock2.h"
#include <set>
#include "BlueToothS10.h"
#include "BeepUtil.h"
#include "EM1300dll.h"

using namespace std;

#pragma comment( lib, "Ws2.lib" )
#define BROADCAST_PORT 4531
#define UNICAST_PORT 4532
WSADATA wsaData;

set<ULONG> hosts;


int num=0;
#ifdef _DEBUG
#define new DEBUG_NEW
#endif

volatile bool bContinue = false;
volatile bool bTimerset = false;
volatile bool bF20Uped	= false;
volatile bool bDecodeState	= false;



#define SCREEN_WIDTH (GetSystemMetrics(SM_CXSCREEN))
#define SCREEN_HEIGHT (GetSystemMetrics(SM_CYSCREEN))
#define TRANSXFROM240(x) (x*SCREEN_WIDTH/240)
#define TRANSYFROM320(y) (y*SCREEN_HEIGHT/320) 
#define UPPICPATH _T("bitmap\\up-ReadBarcode.bmp")
#define DOWNPICPATH _T("bitmap\\down-ReadBarcode.bmp")
volatile bool isread=FALSE;
bool canexit=false;
CString strDisp;
CString strStore;
CString TimeString;
CString strBTInfo = _T("");
DWORD pre=0;
CRect rshow=CRect(TRANSXFROM240(20),TRANSYFROM320(50),TRANSXFROM240(220),TRANSYFROM320(250));
CRect rclear=CRect(TRANSXFROM240(5),TRANSYFROM320(265),TRANSXFROM240(119),TRANSYFROM320(305));
CRect rexit=CRect(TRANSXFROM240(120),TRANSYFROM320(265),TRANSXFROM240(234),TRANSYFROM320(305));
#define BUTTONNUM 2
CRect buttonPos[BUTTONNUM]={
	rclear,
	rexit
};

// CReadBarcodeDlg 对话框

CReadBarcodeDlg::CReadBarcodeDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CReadBarcodeDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hThread = NULL;
	m_hExitThreadEvent = NULL;
	m_hEvent=NULL;
}

void CReadBarcodeDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_SHOW, strStore);
}

BEGIN_MESSAGE_MAP(CReadBarcodeDlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_WM_DESTROY()
	ON_WM_PAINT()
	ON_WM_LBUTTONDOWN()
	ON_MESSAGE(WM_MY_MESSAGE,OnMyMessage)
	ON_WM_TIMER()
END_MESSAGE_MAP()


// CReadBarcodeDlg 消息处理程序

BOOL CReadBarcodeDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	BOOL bRet;

    // 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
    //  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	WSAStartup(MAKEWORD(2,2),&wsaData);

	::SetWindowPos(this->m_hWnd,HWND_TOPMOST,0,0,GetSystemMetrics(SM_CXSCREEN),
		GetSystemMetrics(SM_CYSCREEN), 0);

	SipShowIM(SIPF_OFF);
	
    //获取系统时间
    SYSTEMTIME showNowTime;  
    //获取系统时间类   
    GetLocalTime(&showNowTime);  
    TimeString.Format(_T("%04d-%02d-%02d-%02d-%02d-%02d.txt")
    ,showNowTime.wYear  
    ,showNowTime.wMonth  
    ,showNowTime.wDay  
    ,showNowTime.wHour  
    ,showNowTime.wMinute  
    ,showNowTime.wSecond);   
    
    WCHAR cc[100];
	GetModuleFileName(NULL,cc,sizeof(cc));
	int len=wcslen(cc);
	while(cc[len-1]!='\\')
		len--;
	cc[len]=0;
	exePath=(CString)(cc);
	BeepUtil::Init(exePath);
	S10_Init(S10_COM4);
	S10_SetOperationMode(S10_CommunicationMode);
	
	this->GetDlgItem(IDC_SHOW)->MoveWindow(&rshow);
	int ret;
	if((ret = EM1300SerialInit(COM2)) != INIT_OK) {
		CString str;
		str.Format(_T("%d"),ret);
		AfxMessageBox(str);
		AfxMessageBox(_T("启动通信失败！"));	
		//AfxMessageBox(_T("start communicate failure！"));
		exit(-1);
	}

	if(SUCCESS_SETTING != EM1300SpecialCommand("#99912406;#99912403;",20))
	{
		AfxMessageBox(_T("取消CODE39前/后缀失败"));
	}

	m_hExitThreadEvent = CreateEvent(NULL, FALSE, FALSE, NULL);            /*  创建串口接收线程退出事件  */
	m_hEvent=CreateEvent(NULL,TRUE,FALSE,NULL);
	canexit=FALSE;
//	InitializeCriticalSection(&g_cs);
	isread=FALSE;

	m_hThread = CreateThread(NULL, 0, ThreadProc, this, 0, 0);    /* 创建线程                 */

	m_hRecvBroadcastThread = CreateThread(NULL,0,RecvBroadcastThreadProc,this,0,NULL);

	/*
	*  判断线程是否创建成功
	*/
	if (!m_hThread) {
		CloseHandle(m_hExitThreadEvent);				                    /* 关闭线程退出事件句柄    */
		CloseHandle(m_hEvent);
		canexit=TRUE;
		AfxMessageBox(_T("创建线程失败！"));
		//AfxMessageBox(_T("create thread failure！"));
		exit(-1);
	}	

// 	if (EM1300TriggerState(SCAN_HOST) != SUCCESS_SETTING) {
// 		AfxMessageBox(_T("设置错误！"));
// 		//AfxMessageBox(_T("configure error！"));
// 		exit(-1);
// 	}


	CDC *pdc=GetDC();
	if(!memdc.CreateCompatibleDC(pdc))
	{
		::PostQuitMessage(0);
	}

	m_pBnUp=new CPicView(&memdc,exePath+UPPICPATH);
	m_pBnDown=new CPicView(&memdc,exePath+DOWNPICPATH);

	CBitmap bmp;
	bmp.CreateCompatibleBitmap(pdc,SCREEN_WIDTH,SCREEN_HEIGHT);
	memdc.SelectObject(&bmp);

	CRect fullScreen(0,0,SCREEN_WIDTH,SCREEN_HEIGHT);
	m_pBnUp->drawDefault(&fullScreen);

	/*hTimeThread=CreateThread(0,0,TimePro,this,0,0);*/
//	m_hEnableThread = CreateThread(NULL, 0, EnableThreadProc, this, 0, 0);

/*
	EM1300CodeBarEnable(UPCA_CODE,ENABLE);
	EM1300CodeBarEnable(UPCE_CODE,ENABLE);
	EM1300CodeBarEnable(UPCE1_CODE,ENABLE);
	EM1300CodeBarEnable(EAN8_CODE,ENABLE);
	EM1300CodeBarEnable(EAN13_CODE,ENABLE);
	EM1300CodeBarEnable(BOOKLANDEAN,ENABLE);
	EM1300CodeBarEnable(CODE128,ENABLE);
	EM1300CodeBarEnable(EAN_128,ENABLE);
	EM1300CodeBarEnable(ISBT_128,ENABLE);
	EM1300CodeBarEnable(CODE39,ENABLE);
	EM1300CodeBarEnable(TRIOPTIC_CODE39,ENABLE);
	EM1300CodeBarEnable(CODE39_FULL,ENABLE);
	EM1300CodeBarEnable(CODE93,ENABLE);
	EM1300CodeBarEnable(CODE11,ENABLE);
	EM1300CodeBarEnable(INTERLEAVED,ENABLE);
	EM1300CodeBarEnable(DISCRETE,ENABLE);
	EM1300CodeBarEnable(CODABAR,ENABLE);	
	EM1300CodeBarEnable(MSI,ENABLE);	
	EM1300CodeBarEnable(CHINESE_2OF5,ENABLE);	
	EM1300CodeBarEnable(RSS_14,ENABLE);	
	EM1300CodeBarEnable(RSS_LIMITED,ENABLE);	
	EM1300CodeBarEnable(RSS_EXPANDED,ENABLE);	
*/


	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

/*
DWORD TimePro(PVOID threadData)
{
	DWORD pre=0;
	CReadBarcodeDlg *pDlg=(CReadBarcodeDlg*)threadData;
	while(1)
	{
		DWORD now=GetTickCount();
		CString str;
		str.Format(L"\r\n  %d\r\n",now);
		RETAILMSG(1, (str));	
		if(now-pre>100)
		{
			pre=now;
			::PostMessage(pDlg->m_hWnd,WM_KEYDOWN,VK_F20,0);
			//::SendMessage(pDlg->m_hWnd,WM_LBUTTONDOWN,0,19660900);
		}
		Sleep(50);
	}
}*/

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CReadBarcodeDlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_READBARCODE_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_READBARCODE_DIALOG));
	}
}
#endif

CSize ScreenSize = CSize(SCREEN_WIDTH,SCREEN_HEIGHT);
void CReadBarcodeDlg::SelectButton(int num)
{
	m_pBnDown->drawDefault(&buttonPos[num],&ScreenSize);
	InvalidateRect(&buttonPos[num],TRUE);
}
void CReadBarcodeDlg::DeselectButton(int num)
{
	m_pBnUp->drawDefault(&buttonPos[num],&ScreenSize);
	InvalidateRect(&buttonPos[num],TRUE);
}
void CReadBarcodeDlg::Animation(int num)
{
	SelectButton(num);
	UpdateWindow();
	Sleep(100);
	DeselectButton(num);
	UpdateWindow();
}
void CReadBarcodeDlg::OnBnClickedExit()
{
	Animation(EXIT);
	//Print(strBTInfo);
	//::PostQuitMessage(0);
    //将条码信息打印到文本文档中（20131010）
	UpdateData(TRUE);
	unsigned char  *strbuf = new BYTE[20*1024];
	char strbuf1[MAXSIZE];
	DWORD dwlen = 0, i = 0;
	dwlen = strStore.GetLength();

	for (i = 0; i < dwlen; i ++) {
		strbuf[i] = (unsigned char)strStore.GetAt(i);
	}

	//输出到文本文件中
	CFile file;
    //TimeString = _T("record.txt");
	if(file.Open(exePath+TimeString,CFile::modeWrite/*|CFile::modeNoTruncate*/|CFile::modeCreate))
	{
		//file.SeekToEnd();
		file.SeekToBegin();
		file.Write(strbuf,dwlen);
		file.Flush();
		file.Close();
	}  

	//释放堆内存
	delete[] strbuf;
	strbuf = NULL;
}

void CReadBarcodeDlg::OnBnClickedClear()
{
	Animation(CLEAR);
	CEdit * show=(CEdit*)GetDlgItem(IDC_SHOW);
	show->SetSel(0,-1);
	//show->ReplaceSel(L"");
	show->Clear();
	show->SetFocus();
	num=0;
}

#if 0
BOOL CReadBarcodeDlg::PreTranslateMessage(MSG* pMsg)
{
	char s[256];
	// 	if (pMsg->message == WM_KEYDOWN && bF20Uped == true && bContinue == true)
	// 	{
	// 		Se955TriggerState(SCAN_HOST);
	// 		bContinue = false;
	// 	}
	// 	else 
	if (pMsg->message == WM_KEYDOWN && pMsg->wParam==VK_F20
		/*&& bF20Uped*/	//	连续读模式下删除此行
		//		&& !bContinue
		)//键盘对应数字或ASCII码    
	{
		int ret;
		if (!bDecodeState) {
			ret = EM1300DecodeState(TRUE);
			if (ret != SUCCESS_SETTING && ret != ACK_FAILED) 
			{
				CString str;
				if(ret==DATA_ERR_SELECT)
					str=L"发送数据错误";
				AfxMessageBox(str+L"设置错误！");
				return 0;
			}
			//             sprintf(s,"%d\n",GetTickCount());
			//             Log(s,strlen(s)); 
			bDecodeState = TRUE;
		}
		bF20Uped = FALSE;
	}
	else if (pMsg->message == WM_KEYUP && pMsg->wParam==VK_F20)
	{
		EM1300DecodeState(FALSE);   //原始程序删除此行
		bDecodeState = FALSE;
		bF20Uped = TRUE;
	}
	return CDialog::PreTranslateMessage(pMsg); 
}
#endif

#if 1
BOOL CReadBarcodeDlg::PreTranslateMessage(MSG* pMsg)
{
    char s[256];
	if (pMsg->message == WM_KEYDOWN && bF20Uped == true && bContinue == true)
	{
		if(SUCCESS_SETTING == EM1300TriggerState(SCAN_SINGLE))
		{
			bContinue = false;
		}
	}
	else if (pMsg->message == WM_KEYDOWN && pMsg->wParam==VK_F20 && !bContinue)    //键盘对应数字或ASCII码    
    {
		if(!bTimerset)
			SetTimer(1,5000,NULL);                             //当橙色键持续按下5秒时，自动进入持续扫描模式
		bTimerset=true;
		int ret;
        if (!bDecodeState) {
            ret = EM1300DecodeState(TRUE);
            if (ret != SUCCESS_SETTING && ret != ACK_FAILED) 
            {
                CString str;
                if(ret==DATA_ERR_SELECT)
                    str=L"发送数据错误";
                AfxMessageBox(str+L"设置错误！");
                return 0;
            }
//             sprintf(s,"%d\n",GetTickCount());
//             Log(s,strlen(s)); 
            bDecodeState = TRUE;
        }
        bF20Uped = false;
    }
    else if (pMsg->message == WM_KEYUP && pMsg->wParam==VK_F20)
    {
		if(bTimerset)
			KillTimer(1);
		bTimerset=false;
		bDecodeState = FALSE;
		bF20Uped = TRUE;
		EM1300DecodeState(FALSE);   //原始程序删除此行
    }
    return CDialog::PreTranslateMessage(pMsg); 
}
#else
BOOL CReadBarcodeDlg::PreTranslateMessage(MSG* pMsg)
{
	if (pMsg->message == WM_KEYDOWN && bF20Uped == true && bContinue == true)
	{
		if(SUCCESS_SETTING == EM1300TriggerState(SCAN_SINGLE))
		{
			bContinue = false;
		}
	}
	else if (pMsg->message == WM_KEYDOWN && pMsg->wParam==VK_F20 && !bContinue)//键盘对应数字或ASCII码    
	{
		if(!bTimerset)
		SetTimer(1,3000,NULL);
		bTimerset=true;
		DWORD now=GetTickCount();
		if(now-pre>300)
		//if(now-pMsg->time < 100)
		{
			pre=now;

			//EM1300TriggerState(SCAN_HOST);
			int ret=EM1300DecodeState(TRUE);
			if (ret != SUCCESS_SETTING) 
			{
				CString str;
				str.Format(_T("%d"),ret);
				AfxMessageBox(str+L"设置错误！");
				return 0;
			}
		}
		bF20Uped = false;
	}
	else if (pMsg->message == WM_KEYUP && pMsg->wParam==VK_F20)
	{
		if(bTimerset)
		KillTimer(1);
		EM1300DecodeState(FALSE);   //原始程序删除此行
        bTimerset=false;
		bF20Uped = true;
	}
	return CDialog::PreTranslateMessage(pMsg); 
}
#endif

void CReadBarcodeDlg::OnDestroy()
{
	CDialog::OnDestroy();
	BeepUtil::BeepClose();
	EM1300SerialTerminate();	    
	if(m_hRecvBroadcastThread)
	{
		SetEvent(m_hExitThreadEvent);
		WaitForSingleObject(m_hRecvBroadcastThread,500);
		CloseHandle(m_hRecvBroadcastThread);
	}

	if(m_hThread)
	{
		SetEvent(m_hExitThreadEvent);
		canexit=TRUE;
		WaitForSingleObject(m_hThread,500);
		CloseHandle(m_hThread);
		CloseHandle(m_hExitThreadEvent);
		CloseHandle(m_hEvent);
		m_hThread=NULL;
	}
	if(m_hEnableThread)
	{
		WaitForSingleObject(m_hEnableThread,5000);
		CloseHandle(m_hEnableThread);
	}
	S10_Close();

#if ENABLE_SWITCH
	//¹Ø±ÕÇý¶¯
	if (GhFile != INVALID_HANDLE_VALUE) {
		CloseHandle(GhFile);                    /* ÊÍ·Å¾ä±ú       */
	}
#endif
}

DWORD CReadBarcodeDlg::ThreadProc(PVOID pArg)
{
	BYTE ReceiveBuf[MAXSIZE];
	BYTE ucCount = 0;
	BYTE i;
	CString strTmp;
	strDisp = _T("");                                           /* 初始化                       */
	DWORD   ulState;
	CReadBarcodeDlg* pDlg = (CReadBarcodeDlg*)pArg;                              
	CEdit *show = (CEdit*)pDlg->GetDlgItem(IDC_SHOW);            /* 获得控件句柄                 */

	while(TRUE) {
		if(canexit)
			break;
		int buzzertime = 45;
		//	if(WaitForSingleObject(pDlg->m_hEvent,INFINITE)==WAIT_OBJECT_0)
		do{
			ulState = EM1300GetDecodeData(ReceiveBuf, &ucCount);
			if(ulState == RECEIVE_SUCCESS) 
			{
				//epcBuzzerAsyncOn(20);
				BeepUtil::BeepOk();
				for (i = 0; i < ucCount; i++) 
				{
					strTmp.Format(_T("%c"), ReceiveBuf[i]);   
					strDisp += strTmp; 
				}	
				::SendMessage(pDlg->m_hWnd,WM_MY_MESSAGE,0,0);
			}					                        
		}while(ulState == RECEIVE_SUCCESS);
		Sleep(2);
	}
	return 0;	
}
LRESULT CReadBarcodeDlg::OnMyMessage(WPARAM wParam, LPARAM lParam)
{				
	if(num==100)
	{
		//OnBnClickedClear();
	}
#if 0
	unsigned char strbuf[MAXSIZE];
    char strbuf1[MAXSIZE];
	DWORD dwlen = 0, i = 0;
#endif

	//static int cnt = 0;
	//CString tmp;
    //tmp.Format(L"%d:",++cnt);
	//Show(tmp+_T("读取条码：")+strDisp+_T("\r\n"));    
	Show(strDisp+_T("\r\n"));    

#if 0
    dwlen = strDisp.GetLength();
    i = 1;
    strbuf[0] = ';'; 
    for (i = 1; i < dwlen+1; i ++) {
        strbuf[i] = (unsigned char)strDisp.GetAt(i-1);
    }
    strbuf[i] = '\r';
    strbuf[i+1] = '\n';
    
    //输出到文本文件中
	CFile file;
	if(file.Open(exePath+_T("record.txt"),CFile::modeWrite|CFile::modeNoTruncate|CFile::modeCreate))
	{
		file.SeekToEnd();
		file.Write(strbuf,dwlen+3);
		file.Flush();
		file.Close();
	}  
#endif

    UnicastMsg(strDisp);
	//Print(strDisp);
	strBTInfo = strDisp;
	strDisp=_T("");
	num++;
	return 0;
}

void CReadBarcodeDlg::Show(CString &str)
{
	CEdit * show= (CEdit*)GetDlgItem(IDC_SHOW);
	int pre=show->GetWindowTextLength();
	show->SetSel(show->GetWindowTextLength(),show->GetWindowTextLength());
	show->ReplaceSel(str);
	int now=show->GetWindowTextLength();
	show->SetSel(pre,now);
}
void CReadBarcodeDlg::OnPaint()
{
	CPaintDC dc(this); // device context for painting
	dc.BitBlt(0,0,SCREEN_WIDTH,SCREEN_HEIGHT,&memdc,0,0,SRCCOPY);
}

int whichButton(CPoint point)
{
	int x=point.x,y=point.y;
	for(int i=0;i<BUTTONNUM;i++)
	{
		if(x>=buttonPos[i].left && x<=buttonPos[i].right 
			&& y>=buttonPos[i].top && y<=buttonPos[i].bottom)
			return i;
	}
	return NOTHING;
}

void CReadBarcodeDlg::OnLButtonDown(UINT nFlags, CPoint point)
{
	int p=whichButton(point);
	switch(p)
	{
	case CLEAR:OnBnClickedClear();break;
	case EXIT:OnBnClickedExit();break;
	}
	CDialog::OnLButtonDown(nFlags, point);
}
void CReadBarcodeDlg::OnTimer(UINT_PTR nIDEvent)
{
	if(nIDEvent == 1)
	{
		//EM1300TriggerState(SCAN_CONTINUE);
		int ret;
		if(SUCCESS_SETTING == (ret = EM1300TriggerState(SCAN_CONTINUE)))
		{
			bContinue=true;
			KillTimer(1);
			bTimerset=false;
		}
	}

	CDialog::OnTimer(nIDEvent);
}


int U2M(LPSTR t, LPCWSTR f, UINT CodePage)//UnicodeToMultiByte
{
    int nLen = WideCharToMultiByte (CodePage,0,f,-1,NULL,0,NULL,NULL);
    LPSTR lpsz = new char[nLen];
    WideCharToMultiByte(CodePage,0,f,-1,lpsz,nLen,NULL,NULL);
    strcpy(t, lpsz);
    delete[] lpsz;
    return nLen;
} 

bool CReadBarcodeDlg::Print(const CString &barcode)
{
    unsigned char c[257];
    int barLength = U2M((char*)c,barcode,CP_ACP)-1;
    c[barLength]=0x0A;
    unsigned char bar[260];
    bar[0] = 0x1D;
    bar[1] = 0x6B;
    bar[2] = 0x48;
    bar[3] = barLength;
    unsigned char speed[]={0x1C,0x73,0x01};
    unsigned char mid[3]={0x1B,0x61,0x01};
    unsigned char under[]={0x1D,0x48,0x02};
    unsigned char start[7]={0x1C,0x28,0x4C,0x02,0x00,0x43,0x30};
    unsigned char end[7]={0x1C,0x28,0x4C,0x02,0x00,0x41,0x30};
    memcpy(bar+4,c,barLength);
    unsigned int sendlength = sizeof(speed);
    S10_SendData(speed,sendlength);
    sendlength = sizeof(start);
    S10_SendData(start,sendlength);
    sendlength = sizeof(mid);
    S10_SendData(mid,sendlength);
    sendlength = sizeof(under);
    S10_SendData(under,sendlength);
    sendlength = 1;
    S10_SendData(c+barLength,sendlength);
    sendlength = barLength+4;
    S10_SendData(bar,sendlength);

    CTime tm = CTime::GetCurrentTime();
    CString strtm = tm.Format(L"%Y/%m/%d %H:%M:%S");
    unsigned char ctm[100];
    U2M((char*)ctm,strtm,CP_ACP);
    ctm[strtm.GetLength()] = 0x0A;
    sendlength = strtm.GetLength()+1;
    S10_SendData(ctm,sendlength);

    /*BlueTooth.epcSerialSendData(c,pt.GetLength()+1);*/
    sendlength = 1;
    S10_SendData(c+barLength,sendlength);
    sendlength = sizeof(end);
    S10_SendData(end,sendlength);
    //S10_SetOperationMode(S10_ATCommandMode);
    //S10_Disconnect();
    return true;
}

DWORD CReadBarcodeDlg::RecvBroadcastThreadProc(PVOID pArg)
{
    CReadBarcodeDlg * pDlg = (CReadBarcodeDlg*)pArg;
    SOCKET s;
    SOCKADDR_IN addr,from;
    BOOL bBroadCast = TRUE;
    int fromlength = sizeof(SOCKADDR);
    addr.sin_family = AF_INET;
    addr.sin_addr.S_un.S_addr = INADDR_ANY;
    addr.sin_port = htons(BROADCAST_PORT);
    //addr.sin_port = htons(UNICAST_PORT);

    s = socket(AF_INET,SOCK_DGRAM,IPPROTO_UDP);
    setsockopt(s,SOL_SOCKET,SO_BROADCAST,(char*)&bBroadCast,sizeof(BOOL));
    bind(s,(sockaddr*)&addr,sizeof(addr));
    while(true)
    {
        if(WAIT_OBJECT_0 == WaitForSingleObject(pDlg->m_hExitThreadEvent,0))
            break;
        char buf[256]={0};
        int recvLen = recvfrom(s,buf,256,0,(sockaddr*)&from,&fromlength);
        CString str,singlec;
        for(int i=0;i<recvLen;i++)
        {
            singlec.Format(_T("%c"),buf[i]);
            str+=singlec;
        }
        //AfxMessageBox(str);
        if(recvLen == 4 && 0 == memcmp(buf,"host",strlen("host")))
        {
            hosts.insert(from.sin_addr.S_un.S_addr);
            unsigned char * pcAddr = (unsigned char *)&from.sin_addr.S_un.S_addr;
            CString strAddr;
            strAddr.Format(_T("Ìí¼Ó:%u.%u.%u.%u\r\n"),pcAddr[0],pcAddr[1],pcAddr[2],pcAddr[3]);
            pDlg->Show(strAddr);
        }
        Sleep(300);
    }
    closesocket(s);
    return 0;
}

void CReadBarcodeDlg::UnicastMsg(CString &msg)
{
    char c[260];
    int barLength = U2M((char*)c,msg,CP_ACP)-1;
    SOCKET s;
    SOCKADDR_IN addr;
    for(set<ULONG>::iterator itr = hosts.begin();itr != hosts.end();itr++)
    {
        s = socket(AF_INET,SOCK_DGRAM,IPPROTO_UDP);
        if(INVALID_SOCKET == s) return;
        addr.sin_family = AF_INET;
        addr.sin_port = htons(UNICAST_PORT);
        addr.sin_addr.S_un.S_addr = *itr;
        if(SOCKET_ERROR == sendto(s,c,barLength,0,(SOCKADDR*)&addr,sizeof(addr)))
        {
            AfxMessageBox(_T("error"));
        }
        closesocket(s);
    }
}

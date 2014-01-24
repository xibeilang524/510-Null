// ServerDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "Server.h"
#include "ServerDlg.h"
#include "CeCom.h"
#include "Cmd.h"
#include "tk.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CServerDlg 对话框


UINT16 CRCCheckSum( BYTE* pucData, UINT32 uiLen);
CServerDlg::CServerDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CServerDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CServerDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CServerDlg, CDialog)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_WM_DESTROY()
END_MESSAGE_MAP()


void CALLBACK  DisplayRcvData(void *pDlg, BYTE *pucBuf, DWORD dwRcvLen)
{	
	CRITICAL_SECTION  csDisp;
	CString str;
	CServerDlg	*p  = (CServerDlg *)pDlg;
	if(!(p && ::IsWindow(p->GetSafeHwnd())))
	{
		return;
	}
	byte buf[256];

	char show[256];

	InitializeCriticalSection(&csDisp);		                        //  初始化临界区对象   
	EnterCriticalSection(&csDisp);		

	memcpy(show,pucBuf,dwRcvLen);
	show[dwRcvLen] = 0;
	str += L"received :";
	for(int i=0;i < dwRcvLen;i++)
	{
		CString tmp;
		tmp.Format(L" %02x",pucBuf[i]);
		str+=tmp;
	}
	//TK::Ansi2Unicode(wch,show);
	str += L"\r\n";

	memcpy(buf,show,dwRcvLen);
	reverse(buf,buf+dwRcvLen);

	Com_SendData(buf,dwRcvLen);


	str += L"send :";
	for(int i=0;i<dwRcvLen;i++)
	{
		CString tmp;
		tmp.Format(L" %02x",buf[i]);
		str+=tmp;
	}
	str += L"\r\n";

	p->AppendInfo(str);

	LeaveCriticalSection(&csDisp);			                        //  退出临界区  
	DeleteCriticalSection(&csDisp);			                        //  释放临界区对象资源
}

// CServerDlg 消息处理程序

BOOL CServerDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	Com_OpenPort(0);

	Com_Clear();

	Com_RcvDataTread(255, 100, (PFUN_COMRCV)DisplayRcvData, this);

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CServerDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标显示。
//
HCURSOR CServerDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CServerDlg::OnDestroy()
{
	CDialog::OnDestroy();

	Com_ClosePort();
}

void CServerDlg::AppendInfo(CString &str)
{

	CEdit * show= (CEdit*)GetDlgItem(IDC_SHOW);
	int pre=show->GetWindowTextLength();
	show->SetSel(show->GetWindowTextLength(),show->GetWindowTextLength());
	show->ReplaceSel(str);
	int now=show->GetWindowTextLength();
	show->SetSel(pre,now);
}

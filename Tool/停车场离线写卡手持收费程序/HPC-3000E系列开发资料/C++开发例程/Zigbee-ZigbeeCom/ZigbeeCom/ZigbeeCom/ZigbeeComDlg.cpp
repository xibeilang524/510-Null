// ZigbeeComDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ZigbeeCom.h"
#include "ZigbeeComDlg.h"
#include "ZyZigbeeSDK.h"
#include "tk.h"
#include "Cmd.h"
#include "ConfFile.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CZigbeeComDlg 对话框

CZigbeeComDlg::CZigbeeComDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CZigbeeComDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

	m_com = 1;
	m_baudRate = 115200;
	m_nAddr = 0;
	m_Panid = 0x1001;
	m_hReadThread	= CreateThread(NULL,0,ReadThreadProc,(LPVOID)this,CREATE_SUSPENDED,0);
	m_hReadEvent	= CreateEvent(NULL,TRUE,FALSE,NULL);
	m_hExitEvent	= CreateEvent(NULL,FALSE,FALSE,NULL);
	//m_hTimeoutEvent = CreateEvent(NULL,FALSE,FALSE,NULL);
}

void CZigbeeComDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CZigbeeComDlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_WM_CREATE()
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_BNSEARCH, &CZigbeeComDlg::OnBnClickedBnsearch)
	ON_CBN_SELCHANGE(IDC_DEVICES, &CZigbeeComDlg::OnCbnSelchangeDevices)
	ON_BN_CLICKED(IDC_BNCLEAR, &CZigbeeComDlg::OnBnClickedBnclear)
	ON_BN_CLICKED(IDC_BNSEND, &CZigbeeComDlg::OnBnClickedBnsend)
END_MESSAGE_MAP()


// CZigbeeComDlg 消息处理程序


void GetCurrentProgramPath_note(TCHAR* szResult)
{
	TCHAR PathTemp[MAX_PATH];
	GetModuleFileName(NULL,PathTemp,MAX_PATH);
	TCHAR * s = _tcsrchr(PathTemp, _T('\\'));
	if (s) s[1]=0;
	_stprintf(szResult, _T("%s"), PathTemp);
	return;
}


BOOL CZigbeeComDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	ResumeThread(m_hReadThread);

	if(ZYZB_OpenCom(L"COM1:",m_baudRate,8,0,0,0,0,0,0,1500) == false)
	{
		MessageBox(_T("打开串口失败,请检查是否被占用"),_T("提示"), MB_OK|MB_ICONERROR);
		return -1;
	}

	if(!ZYZB_CEL_LoadInfo())
	{
		MessageBox(_T("加载Zigbee模块信息失败"), _T("提示"), MB_OK|MB_ICONERROR );
		return -1;
	}

	TCHAR PathTemp[MAX_PATH];
	GetCurrentProgramPath_note(PathTemp);
	m_ExePath.Format(_T("%s"),PathTemp);
	CConfFile conf(m_ExePath+_T("default.conf"));
	if(conf.Read(m_Panid,m_channels,m_speeds) == false)
	{
		MessageBox(_T("读取配置文件失败,将采用默认设置！"),_T("提示"),MB_OK|MB_ICONERROR);
		m_Panid = 0x1001;
		m_channels.clear();
		m_speeds.clear();
		for(int i= 11;i<26;i++)
			m_channels.push_back(i);
		for(int i=0;i<2;i++)
			m_speeds.push_back(i);
	}

	// 设置目标节点地址
	if ( !ZYZB_CEL_SetProperty( ZYZB_CEL_DEV_DSTNETID, &m_nAddr, sizeof(m_nAddr) ) )
	{
		MessageBox( _T("设定目标ZBCOM地址失败！"), _T("提示"), MB_OK|MB_ICONERROR );
		return -1;
	}
	if ( !ZYZB_CEL_SetProperty( ZYZB_CEL_DEV_PANID, &m_Panid, sizeof(m_Panid) ) )
	{
		MessageBox( _T("设定Zigbee模块PANID失败！"), _T("提示"), MB_OK|MB_ICONERROR );
		return -1;
	}

	::SetWindowPos( m_hWnd, HWND_TOPMOST, 0, 0, GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN), 
		SWP_NOOWNERZORDER|SWP_SHOWWINDOW );
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CZigbeeComDlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_ZIGBEECOM_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_ZIGBEECOM_DIALOG));
	}
}
#endif

int CZigbeeComDlg::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CDialog::OnCreate(lpCreateStruct) == -1)
		return -1;

	return 0;
}

void CZigbeeComDlg::OnDestroy()
{
	CDialog::OnDestroy();
	if(m_hReadThread != NULL)
	{
		SetEvent(m_hExitEvent);
		WaitForSingleObject(m_hReadThread,500);
		CloseHandle(m_hReadThread);
		CloseHandle(m_hReadEvent);
		CloseHandle(m_hExitEvent);
		m_hReadThread=NULL;
	}
	ZYZB_CloseCom();
}

void CZigbeeComDlg::OnBnClickedBnsearch()
{
	CComboBox * combo=(CComboBox*)GetDlgItem(IDC_DEVICES);
	for(int i = 0 ; i < combo->GetCount() ; i++)
	{
		combo->DeleteString(0);
	}
	m_Addrs.clear();
	if(ZYZB_ComCheckOpen() == false)
	{
		MessageBox(_T("串口未打开"), _T("提示"), MB_OK|MB_ICONERROR );
		return;
	}
	ZYZB_CEL_ClearSearchArr();
	bool ok=false;
	for(int i=0;i<m_channels.size();i++)
		for(int j=0;j<m_speeds.size();j++)
	{
		if(ZYZB_CEL_SearchDev(m_channels[i],m_speeds[j]) == TRUE)
		{
			CString tmp;
			tmp.Format(L"i==%d j==%d",i,j);
			//tmp.Format(L"i==%d",i);
		//	MessageBox(L"搜索设备成功"+tmp);
			//break;
			ok=true;
		}
	}	
	if(!ok)
	{
		MessageBox(_T("搜索设备失败"), _T("提示"), MB_OK|MB_ICONERROR );
		return;
	}
	CString str;
	int cnt = ZYZB_CEL_GetSearchDevCount();
	str.Format(L"共搜索到%d个设备",cnt);
	char  buf[256];
	WCHAR wch[256];
	for(int i = 0 ; i < cnt ; i++)
	{
// 		ZYZB_CEL_LoadRemoteDevInfo(i);
// 		ZYZB_CEL_GetRemoteDevProperty(i,ZYZB_CEL_DEV_NAME,buf,256);
// 		TK::Ansi2Unicode(wch,buf);
// 		combo->AddString(wch);
		WORD addr;
		ZYZB_CEL_GetSearchDevProperty(i,ZYZB_CEL_SEARCH_ID,&addr,2);
		CString str;
		str.Format(L"0x%04X",addr);
		combo->AddString(str);
		m_Addrs.push_back(addr);
	}
	if(cnt)
	{
		combo->SetCurSel(0);
	}
	MessageBox(str);

}
void CZigbeeComDlg::Show(CString &str)
{
	CEdit *edit =(CEdit*)GetDlgItem(IDC_SHOW);
	edit->SetWindowText(str);
}
void CZigbeeComDlg::AppendInfo(CString &str)
{
	CEdit * show= (CEdit*)GetDlgItem(IDC_SHOW);
	int pre=show->GetWindowTextLength();
	show->SetSel(show->GetWindowTextLength(),show->GetWindowTextLength());
	show->ReplaceSel(str);
	int now=show->GetWindowTextLength();
}
DWORD WINAPI CZigbeeComDlg::ReadThreadProc( LPVOID lpram )
{
	CZigbeeComDlg * dlg=(CZigbeeComDlg*)lpram;
	if(!dlg || !::IsWindow(dlg->GetSafeHwnd()))
	{
		return 0;
	}
	HANDLE handle[2];
	handle[0] = dlg->m_hReadEvent;
	handle[1] = dlg->m_hExitEvent;
	while(true)
	{
		DWORD waitobj = WaitForMultipleObjects(2,handle,FALSE,INFINITE);
		if(waitobj == WAIT_OBJECT_0 + 1) break;
		else
		{
			DWORD dwReal;
			byte buf[256];
			::ZeroMemory(buf,sizeof(buf));
			if(ZYZB_CEL_Read(buf,255,&dwReal))
			{
				buf[dwReal]=0;
				WCHAR wc[256];
				TK::Ansi2Unicode(wc,(char*)buf);
				CString str(wc);
				dlg->AppendInfo(str);
				ResetEvent(dlg->m_hReadEvent);
			}
		}
	}
	return 0;
}

void CZigbeeComDlg::OnCbnSelchangeDevices()
{
	CComboBox * combo = (CComboBox*) GetDlgItem(IDC_DEVICES);
	int item = combo->GetCurSel();
	if(item != -1)
	{
		if ( !ZYZB_CEL_SetProperty( ZYZB_CEL_DEV_DSTNETID, &m_Addrs[item], sizeof(m_Addrs[item]) ) )
		{
			MessageBox( _T("设定目标ZBCOM地址失败！"), _T("提示"), MB_OK|MB_ICONERROR );
		}
	}
}

void CZigbeeComDlg::OnBnClickedBnclear()
{
	CEdit * edit=(CEdit*)GetDlgItem(IDC_RCV);
	edit->SetSel(0,-1);
	edit->Clear();
}

void CZigbeeComDlg::OnBnClickedBnsend()
{
	CString str;
	GetDlgItem(IDC_INFO)->GetWindowText(str);
	char package[256];
	TK::Unicode2Ansi(package,str);

	DWORD realWriteByte;
	if ( !ZYZB_CEL_Write( package, strlen(package), &realWriteByte ) )
	{
		::MessageBox( NULL, _T("发送数据错误！"), _T("错误"), MB_OK|MB_ICONERROR );
	}
	else
	{
		SetEvent(m_hReadEvent);
	}
}

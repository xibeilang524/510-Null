
// VSExampleDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "VSExample.h"
#include "VSExampleDlg.h"
#include "afxdialogex.h"
#include "jsoncpp\include\json\json.h"
#include <functional>
#include <sstream>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CVSExampleDlg 对话框




CVSExampleDlg::CVSExampleDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CVSExampleDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CVSExampleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT2, laneEdit);
	DDX_Control(pDX, IDC_EDIT3, userEdit);
	DDX_Control(pDX, IDC_EDIT1, textEdit);
}

BEGIN_MESSAGE_MAP(CVSExampleDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, &CVSExampleDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CVSExampleDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &CVSExampleDlg::OnBnClickedButton3)
	ON_MESSAGE(WM_DO, &CVSExampleDlg::onMsg)
END_MESSAGE_MAP()


// CVSExampleDlg 消息处理程序

BOOL CVSExampleDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码

	try
	{
		m_client.reset(new ECClient);
	}
	catch(...)
	{
		MessageBoxA("Load EtcController.dll Error", "Error", 0);
		::exit(1);
	}

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CVSExampleDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作区矩形中居中
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
		CDialogEx::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CVSExampleDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

DWORD __stdcall invokeAndDelete(void *param)
{
	auto func = reinterpret_cast<std::function<void()>*>(param);
	try
	{
		(*func)();
	}
	catch(...)
	{
		MessageBoxA(0, "Exception", "Error", 0);
	}
	delete func;
	return 0;
}

LRESULT CVSExampleDlg::onMsg(WPARAM wParam, LPARAM lParam)
{
	invokeAndDelete(reinterpret_cast<void*>(lParam));
	return 0;
}

template <typename T>
void runInParallel(T&& t)
{
	auto func = new std::function<void()>(std::forward<T>(t));
	HANDLE hThread = CreateThread(nullptr, 0, invokeAndDelete, reinterpret_cast<void*>(func), 0, nullptr);
	if(hThread == 0)
	{
		delete func;
	}
	else
	{
		CloseHandle(hThread);
	}
}


template <typename T>
void CVSExampleDlg::runInMain(T&& t)
{
	auto func = new std::function<void()>(std::forward<T>(t));
	PostMessageA(WM_DO, 0, reinterpret_cast<LPARAM>(func));
}

void CVSExampleDlg::OnBnClickedButton1()
{
	runInParallel([this]()
	{
		char conf[4096] = {0}, err[1024] = {0};
        int num = 0;
        int rc = m_client->Initialize(conf, &num, err);
		auto self = this;
		if(rc == 0)
		{
			std::string strConf(conf);
			Json::Value json;
			Json::Reader reader;

			if(!reader.parse(strConf, json))
			{
				return;
			}

			std::string username = json["UserName"].asString();
			std::string laneno = json["LaneNo"].asString();

			runInMain([=]()
			{
				CString str;
				self->textEdit.GetWindowTextA(str);

				CString resp(strConf.c_str());
				resp.Replace("\n", "\r\n");
				std::string all = std::string(str.GetBuffer(0)) + "\r\n" + resp.GetBuffer(0);

				self->userEdit.SetWindowTextA(username.c_str());
				self->laneEdit.SetWindowTextA(laneno.c_str());

				self->textEdit.SetWindowTextA(all.c_str());
				self->textEdit.LineScroll(self->textEdit.GetLineCount());

				self->userEdit.UpdateData(FALSE);
				self->laneEdit.UpdateData(FALSE);
				self->textEdit.UpdateData(FALSE);
			});
		}
		else
		{
			std::string strErr(err);
			runInMain([=]()
			{
				CString str;
				self->textEdit.GetWindowTextA(str);
				CString resp(strErr.c_str());
				resp.Replace("\n", "\r\n");
				std::string all = std::string(str.GetBuffer(0)) + "\r\n" + resp.GetBuffer(0);

				self->textEdit.SetWindowTextA(all.c_str());
				self->textEdit.LineScroll(self->textEdit.GetLineCount());
				self->textEdit.UpdateData(FALSE);
			});
		}
	});
	// TODO: 在此添加控件通知处理程序代码
}


void CVSExampleDlg::OnBnClickedButton2()
{
	runInParallel([this]()
	{
		m_client->Uninstall();
	});
	// TODO: 在此添加控件通知处理程序代码
}


void CVSExampleDlg::OnBnClickedButton3()
{
	CString str;

	userEdit.GetWindowTextA(str);
	Json::Value json;
	json["UserName"] = str.GetBuffer(0);

	laneEdit.GetWindowTextA(str);
	int laneno = strtol(str.GetBuffer(0), nullptr, 10);


	std::string strJson = json.toStyledString();

	runInParallel([=]()
	{
		char buf[4096] = {0};
		int rc = m_client->StatusQuery(laneno, strJson.c_str(), buf);

		CString strBuf(buf);
		strBuf.Replace("\n", "\r\n");
		std::string strResp = strBuf.GetBuffer(0);

		auto self = this;
		runInMain([=]()
		{
			CString str;
			self->textEdit.GetWindowTextA(str);
			std::stringstream ss;
			ss << rc;

			std::string all = std::string(str.GetBuffer(0)) + "\r\n" + ss.str() + "\r\n" + strResp;

			self->textEdit.SetWindowTextA(all.c_str());
			self->textEdit.LineScroll(self->textEdit.GetLineCount());
			self->textEdit.UpdateData(FALSE);
		});
	});
	// TODO: 在此添加控件通知处理程序代码
}

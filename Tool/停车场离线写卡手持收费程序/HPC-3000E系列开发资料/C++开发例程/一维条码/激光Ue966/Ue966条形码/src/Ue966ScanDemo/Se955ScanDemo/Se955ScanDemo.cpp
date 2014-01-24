// Se955ScanDemo.cpp : 定义应用程序的类行为。
//

#include "stdafx.h"
#include "Se955ScanDemo.h"
#include "Se955ScanDemoDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CSe955ScanDemoApp

BEGIN_MESSAGE_MAP(CSe955ScanDemoApp, CWinApp)
END_MESSAGE_MAP()


// CSe955ScanDemoApp 构造
CSe955ScanDemoApp::CSe955ScanDemoApp()
	: CWinApp()
{
	// TODO: 在此处添加构造代码，
	// 将所有重要的初始化放置在 InitInstance 中
}


// 唯一的一个 CSe955ScanDemoApp 对象
CSe955ScanDemoApp theApp;

// CSe955ScanDemoApp 初始化

BOOL CSe955ScanDemoApp::InitInstance()
{

	// 标准初始化
	// 如果未使用这些功能并希望减小
	// 最终可执行文件的大小，则应移除下列
	// 不需要的特定初始化例程
	// 更改用于存储设置的注册表项
	// TODO: 应适当修改该字符串，
	// 例如修改为公司或组织名
	SetRegistryKey(_T("应用程序向导生成的本地应用程序"));

	CSe955ScanDemoDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: 在此处放置处理何时用“确定”来关闭
		//  对话框的代码
	}

	// 由于对话框已关闭，所以将返回 FALSE 以便退出应用程序，
	//  而不是启动应用程序的消息泵。
	return FALSE;
}

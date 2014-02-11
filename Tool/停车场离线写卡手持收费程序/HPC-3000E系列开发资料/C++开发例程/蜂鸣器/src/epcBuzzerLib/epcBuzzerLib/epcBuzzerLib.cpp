// epcBuzzerLib.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "epcBuzzerLib.h"
#include <windows.h>
#include <commctrl.h>

/****************************************Copyright (c)****************************************************
**                            Guangzhou ZHIYUAN electronics Co.,LTD.
**                                      
**                                 http://www.embedtools.com
**
**--------------File Info---------------------------------------------------------------------------------
** File name:               epcBuzzerLib.cpp
** Latest modified Date:    2007-12-6
** Latest Version:          1.0
** Descriptions:            蜂鸣器用户库封装代码
**
**--------------------------------------------------------------------------------------------------------
** Created by:              Lizhenming
** Created date:            2007-12-5
** Version:                 1.0
** Descriptions:            The original version
**
*********************************************************************************************************/

#include "stdafx.h"
#include "epcBuzzerLib.h"


static HANDLE hBuzzerFile = INVALID_HANDLE_VALUE;                       /*  蜂鸣器驱动文件句柄          */
void (*GfnNotify)(BOOL) = NULL;                                         /*  异步操作完成调用的回调函数  */                    

BOOL APIENTRY DllMain( HANDLE hModule, 
					  DWORD  ul_reason_for_call, 
					  LPVOID lpReserved
					  )
{
	UNREFERENCED_PARAMETER(hModule);
	UNREFERENCED_PARAMETER(lpReserved);

	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		hBuzzerFile = CreateFile(TEXT("BZR1:"),                     /*  打开蜂鸣器驱动             */
			GENERIC_READ | GENERIC_WRITE, 
			0, 
			NULL, 
			OPEN_EXISTING, 
			0, 
			0);
		if (hBuzzerFile == INVALID_HANDLE_VALUE) {
			return FALSE; 
		}
		break;

	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
		break;
	case DLL_PROCESS_DETACH:
		if (hBuzzerFile != INVALID_HANDLE_VALUE) {
			CloseHandle(hBuzzerFile);                               /*  关闭蜂鸣器驱动              */
			hBuzzerFile = INVALID_HANDLE_VALUE;
		}
		break;
	}
	return TRUE;
}


/*********************************************************************************************************
** Function name:   		epcBuzzerOn
** Descriptions:    		本函数使蜂鸣器蜂鸣指定时间
** input parameters:   	    dwOntime    蜂鸣器持续蜂鸣的时间(ms)，其中0表示一直蜂鸣
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    本函数会阻塞dwOntime(ms)时间，但dwOntime为0时会立刻返回
*********************************************************************************************************/
EPCBUZZERLIB_API BOOL epcBuzzerOn (DWORD dwOntime)
{
	BOOL bRet;
	BYTE ucTmp;
	DWORD dwActLen;

	if (hBuzzerFile == INVALID_HANDLE_VALUE){
		return FALSE;                                                   /*  驱动句柄没打开              */
	}

	ucTmp = 0;
	bRet = WriteFile(hBuzzerFile, &ucTmp, 1, &dwActLen, NULL);          /*  使蜂鸣器蜂鸣                */
	if (bRet == FALSE || dwActLen != 1){
		return FALSE;
	}

	if (dwOntime == 0){                                                 /*  蜂鸣器一直蜂鸣              */
		return TRUE;                                                    /*  立刻返回                    */
	} else {          
		Sleep(dwOntime);                                                /*  挂起指定时间                */
		bRet = epcBuzzerOff();                                          /*  禁止蜂鸣器                  */
	} 

	return TRUE;
}

/*********************************************************************************************************
** Function name:   		epcBuzzerOff
** Descriptions:    		本函数使蜂鸣器禁止
** input parameters:   	    无
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
*********************************************************************************************************/
EPCBUZZERLIB_API BOOL epcBuzzerOff (VOID)
{
	BOOL bRet;
	BYTE ucTmp;
	DWORD dwActLen;

	if (hBuzzerFile == INVALID_HANDLE_VALUE){
		return FALSE;                                                   /*  驱动句柄没打开              */
	}

	ucTmp = 1;
	bRet = WriteFile(hBuzzerFile, &ucTmp, 1, &dwActLen, NULL);          /*  使蜂鸣器禁止                */
	if (bRet == FALSE || dwActLen != 1){
		return FALSE;
	}

	return TRUE;
}

/*********************************************************************************************************
** Function name:   		epcBuzzerGetStatus
** Descriptions:    		本函数获取蜂鸣器的状态
** input parameters:   	    无
** output parameters:   	无
** Returned value:     	    0--蜂鸣器蜂鸣；1--蜂鸣器禁止；其它--出现错误
*********************************************************************************************************/
EPCBUZZERLIB_API DWORD epcBuzzerGetStatus (VOID)
{
	BOOL bRet;
	BYTE ucStatus;
	DWORD dwActLen;

	if (hBuzzerFile == INVALID_HANDLE_VALUE){
		return (DWORD)-1;                                               /*  驱动句柄没打开              */
	}

	bRet = ReadFile(hBuzzerFile, &ucStatus, 1, &dwActLen, NULL);        /*  读取蜂鸣器状态              */
	if (bRet == FALSE || dwActLen != 1){
		return (DWORD)-1;
	}

	return (DWORD)ucStatus;
}

/*********************************************************************************************************
** Function name:   		epcBuzzerBeepsProc
** Descriptions:    		本函数使蜂鸣器发出指定次数的鸣叫。本函数为epcBuzzerBeeps()函数中新建线程的入口函数
** input parameters:   	    ((DWORD*)lparam)[0]     蜂鸣器鸣叫次数
**                          ((DWORD*)lparam)[1]     蜂鸣器蜂鸣时间(ms)
**                          ((DWORD*)lparam)[2]     蜂鸣器禁止时间(ms)
** output parameters:   	无
** Returned value:     	    0
** Note:                    lparam的空间大小为3*sizeof(DWORD)，由调用者申请，本函数负责释放。
*********************************************************************************************************/
static DWORD epcBuzzerBeepsProc (LPVOID lparam)
{
	BOOL bRet = TRUE;
	DWORD dwCnt;
	DWORD dwOntime;
	DWORD dwOfftime;
	DWORD dwFlashTimes;

	dwFlashTimes = ((DWORD*)lparam)[0];
	dwOntime     = ((DWORD*)lparam)[1];
	dwOfftime    = ((DWORD*)lparam)[2];
	delete[] lparam;                                                    /*  释放由调用者申请的内存      */

	for (dwCnt=0; dwCnt<dwFlashTimes; dwCnt++){

		bRet = epcBuzzerOn(0);                                          /*  使蜂鸣器蜂鸣                */
		if (bRet == FALSE){
			break;
		}
		Sleep(dwOntime);                                                /*  蜂鸣指定时间                */

		bRet = epcBuzzerOff();                                          /*  使蜂鸣器禁止                */
		if (bRet == FALSE){
			break;
		}
		Sleep(dwOfftime);                                               /*  禁止指定时间                */
	}

	if (GfnNotify != NULL){                                             /*  如果设置了回调函数，则调用  */
		GfnNotify(bRet);                                                /*  回调函数通知应用程序        */
	}

	return 0;
}

/*********************************************************************************************************
** Function name:   		epcBuzzerBeeps
** Descriptions:    		本函数使蜂鸣器发出指定次数的鸣叫
** input parameters:   	    dwFlashTimes        蜂鸣器鸣叫次数
**                          dwOntime            蜂鸣器蜂鸣时间(ms)
**                          dwOfftime           蜂鸣器禁止时间(ms)
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    本函数以异步方式执行，不会被阻塞
*********************************************************************************************************/
EPCBUZZERLIB_API BOOL epcBuzzerBeeps (DWORD dwFlashTimes, DWORD dwOntime, DWORD dwOfftime)
{ 
	if (hBuzzerFile == INVALID_HANDLE_VALUE){
		return FALSE;                                                   /*  驱动句柄没打开              */
	}

	HANDLE hBzrThread;
	DWORD *pdwParam; 
	/*
	*	申请3*sizeof(DWORD)空间来保存传给新建线程的参数，申请的空间由新建线程负责释放
	*/
	pdwParam   = new DWORD[3];
	pdwParam[0] = dwFlashTimes;
	pdwParam[1] = dwOntime;
	pdwParam[2] = dwOfftime;

	hBzrThread = CreateThread(NULL, 0, epcBuzzerBeepsProc,
		(void*)pdwParam, 0, NULL);                 /*  在新建线程中完成蜂鸣器工作  */

	if (hBzrThread == NULL){
		delete[] pdwParam;                                              /*  创建线程失败，释放申请内存  */
		return FALSE;
	}
	CloseHandle(hBzrThread);

	return TRUE;
}

/*********************************************************************************************************
** Function name:   		epcBuzzerAsyncOn
** Descriptions:    		本函数使蜂鸣器蜂鸣指定时间
** input parameters:   	    dwOntime    蜂鸣器持续蜂鸣的时间(ms)，其中0表示一直蜂鸣
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    本函数以异步方式执行，不会被阻塞
*********************************************************************************************************/
EPCBUZZERLIB_API BOOL epcBuzzerAsyncOn (DWORD dwOntime)
{
	if (dwOntime == 0){
		return epcBuzzerOn(dwOntime);                                   /*  0表示一直蜂鸣               */
	} else {
		return epcBuzzerBeeps(1, dwOntime, 0);
	}
}

/*********************************************************************************************************
** Function name:   		epcBuzzerCallBackFunc
** Descriptions:    		设置回调函数指针，当异步的蜂鸣器操作任务完成后，会调用该回调函数通知用户程序。
** input parameters:   	    lpfnNotify      回调函数指针,如果是NULL，则表示不需要通知用户程序。函数类
**                                          型为void (*lpfnNotify)(BOOL bResult),bResult为执行结果，
**                                          TRUE表示执行成功，FALSE表示执行失败    
** output parameters:   	无
** Returned value:     	    无
*********************************************************************************************************/
EPCBUZZERLIB_API VOID epcBuzzerSetCallBackFunc( void (*lpfnNotify)(BOOL))
{
	GfnNotify = lpfnNotify;
}


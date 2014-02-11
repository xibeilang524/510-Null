/****************************************Copyright (c)****************************************************
**                            Guangzhou ZHIYUAN electronics Co.,LTD.
**                                      
**                                 http://www.embedtools.com
**
**--------------File Info---------------------------------------------------------------------------------
** File name:               epcLedLib.cpp
** Latest modified Date:    2007-12-6
** Latest Version:          1.0
** Descriptions:            LED用户库封装代码
**
**--------------------------------------------------------------------------------------------------------
** Created by:              Lizhenming
** Created date:            2007-12-5
** Version:                 1.0
** Descriptions:            The original version
**
*********************************************************************************************************/

#include "stdafx.h"
#include "epcLedLib.h"
#include "Led.h"

static HANDLE hLedFile = INVALID_HANDLE_VALUE;                          /*  LED驱动文件句柄             */
void (*GfnNotify1)(BOOL) = NULL;                                        /*  LED1异步操作完成的回调函数  */                    
void (*GfnNotify2)(BOOL) = NULL;                                        /*  LED2异步操作完成的回调函数  */                    


/*********************************************************************************************************
** Function name:   		DllMain
** Descriptions:    		epcLed库入口函数
** input parameters:   	    hModule             本dll的句柄
**                          ul_reason_for_call  被调用原因
**                          lpReserved          保留
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
*********************************************************************************************************/
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
            hLedFile = CreateFile(TEXT("LED1:"),                         /*  打开 LED 驱动              */
                GENERIC_READ | GENERIC_WRITE, 
                0, 
                NULL, 
                OPEN_EXISTING, 
                0, 
                0);
            if (hLedFile == INVALID_HANDLE_VALUE) {
                return FALSE; 
            }
            break;
            
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
            break;
		case DLL_PROCESS_DETACH:
            if (hLedFile != INVALID_HANDLE_VALUE) {
                CloseHandle(hLedFile);                                  /*  关闭 LED 驱动               */
                hLedFile = INVALID_HANDLE_VALUE;
            }
			break;
    }
    return TRUE;
}

/*********************************************************************************************************
** Function name:   		epcLedOn
** Descriptions:    		本函数点亮指定的LED灯
** input parameters:   	    dwLedNum        LED编号：LED1,LED2
**                          dwOntime        LED点亮的持续时间(ms)，其中0表示一直点亮
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    本函数会阻塞dwOntime(ms)时间，但dwOntime为0时会立刻返回
*********************************************************************************************************/
EPCLEDLIB_API BOOL epcLedOn (DWORD dwLedNum, DWORD dwOntime)
{
    BOOL bRet;
    DWORD dwIoCtrl;

    if (dwLedNum != LED1 && dwLedNum != LED2){
        return FALSE;                                                   /*  LED号码错误                 */
    }
        
    if (hLedFile == INVALID_HANDLE_VALUE){
        return FALSE;                                                   /*  驱动句柄没打开              */
    }

    dwIoCtrl = (dwLedNum == LED1) ? LED1_ON : LED2_ON;        
    bRet = DeviceIoControl(hLedFile, dwIoCtrl, NULL,
        0, NULL, 0, NULL, NULL);                                        /*  点亮LED                     */
    if (bRet == FALSE){
        return FALSE;
    }

    if (dwOntime == 0){                                                 /*  一直点亮LED                 */
        return TRUE;                                                    /*  立刻返回                    */
    } else {          
        Sleep(dwOntime);                                                /*  挂起指定时间                */
        bRet = epcLedOff(dwLedNum);                                     /*  熄灭LED                     */
    } 
    
    return TRUE;
}

/*********************************************************************************************************
** Function name:   		epcLedOff
** Descriptions:    		本函数熄灭指定的LED灯
** input parameters:   	    dwLedNum        LED编号：LED1,LED2
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
*********************************************************************************************************/
EPCLEDLIB_API BOOL epcLedOff (DWORD dwLedNum)
{
    BOOL bRet;
    DWORD dwIoCtrl;

    if (dwLedNum != LED1 && dwLedNum != LED2){
        return FALSE;                                                   /*  LED号码错误                 */
    }
        
    if (hLedFile == INVALID_HANDLE_VALUE){
        return FALSE;                                                   /*  驱动句柄没打开              */
    }

    dwIoCtrl = (dwLedNum == LED1) ? LED1_OFF : LED2_OFF;        
    bRet = DeviceIoControl(hLedFile, dwIoCtrl, NULL,
        0, NULL, 0, NULL, NULL);                                        /*  熄灭LED                     */
    if (bRet == FALSE){
        return FALSE;
    }
    
    return TRUE;
}


/*********************************************************************************************************
** Function name:   		epcLedGetStatus
** Descriptions:    		本函数获取指定的LED灯的状态
** input parameters:   	    dwLedNum        LED编号：LED1,LED2
** output parameters:   	无
** Returned value:     	    0--点亮；1--熄灭；其它--出现错误
*********************************************************************************************************/
EPCLEDLIB_API DWORD epcLedGetStatus (DWORD dwLedNum)
{
    BOOL bRet;
    DWORD dwIoCtrl;
    BYTE ucStatus;
    DWORD dwActLen;
    
    if (dwLedNum != LED1 && dwLedNum != LED2){
        return (DWORD)-1;                                               /*  LED号码错误                 */
    }
    
    if (hLedFile == INVALID_HANDLE_VALUE){
        return (DWORD)-1;                                               /*  驱动句柄没打开              */
    }
    
    dwIoCtrl = (dwLedNum == LED1) ? LED1_GET_STATUS : LED2_GET_STATUS;        
    bRet = DeviceIoControl(hLedFile, dwIoCtrl, NULL,
        0, &ucStatus, 1, &dwActLen, NULL);                              /*  获取LED状态                 */
    if (bRet == FALSE || dwActLen != 1){
        return (DWORD)-1;
    }
    
    return (DWORD)ucStatus;
}

/*********************************************************************************************************
** Function name:   		epcLedFlashProc
** Descriptions:    		本函数使指定的LED灯闪烁。本函数为epcLedFlash()函数中新建线程的入口函数
** input parameters:   	    ((DWORD*)lparam)[0]     LED编号：LED1,LED2
**                          ((DWORD*)lparam)[1]     LED闪烁次数
**                          ((DWORD*)lparam)[2]     LED点亮的持续时间(ms)
**                          ((DWORD*)lparam)[3]     LED熄灭的持续时间(ms)
** output parameters:   	无
** Returned value:     	    0
** Note:                    lparam的空间大小为4*sizeof(DWORD)，由调用者申请，本函数负责释放。
*********************************************************************************************************/
static DWORD epcLedFlashProc(LPVOID lparam)
{
    BOOL bRet = TRUE;
    DWORD dwCnt;
    DWORD dwLedNum;
    DWORD dwOntime;
    DWORD dwOfftime;
    DWORD dwFlashTimes;

    dwLedNum     = ((DWORD*)lparam)[0];
    dwFlashTimes = ((DWORD*)lparam)[1];
    dwOntime     = ((DWORD*)lparam)[2];
    dwOfftime    = ((DWORD*)lparam)[3];
    delete[] lparam;                                                    /*  释放由调用者申请的内存      */

    for (dwCnt=0; dwCnt<dwFlashTimes; dwCnt++){
        
        bRet = epcLedOn(dwLedNum, 0);                                   /*  点亮LED                     */
        if (bRet == FALSE){
            break;
        }
        Sleep(dwOntime);                                                /*  点亮指定时间                */
        
        bRet = epcLedOff(dwLedNum);                                     /*  熄灭LED                     */
        if (bRet == FALSE){
            break;
        }
        Sleep(dwOfftime);                                               /*  熄灭指定时间                */
    }
    /*
     *	调用回调函数通知应用程序任务完成
     */
    if (dwLedNum == LED1 && GfnNotify1 != NULL){
        GfnNotify1(bRet);
    }

    if (dwLedNum == LED2 && GfnNotify2 != NULL){
        GfnNotify2(bRet);
    }
    return 0;
}

/*********************************************************************************************************
** Function name:   		epcLedFlash
** Descriptions:    		本函数使指定的LED灯闪烁
** input parameters:   	    dwLedNum            LED编号：LED1,LED2
**                          dwFlashTimes        LED闪烁次数
**                          dwOntime            LED点亮的持续时间(ms)
**                          dwOfftime           LED熄灭的持续时间(ms)
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    本函数以异步方式执行，不会被阻塞
*********************************************************************************************************/
EPCLEDLIB_API BOOL epcLedFlash (DWORD dwLedNum, DWORD dwFlashTimes, DWORD dwOntime, DWORD dwOfftime)
{
    if (dwLedNum != LED1 && dwLedNum != LED2){
        return FALSE;                                                   /*  LED号码错误                 */
    }
        
    if (hLedFile == INVALID_HANDLE_VALUE){
        return FALSE;                                                   /*  驱动句柄没打开              */
    }
                                                  
    HANDLE hLedThread;
    DWORD *pdwParam;

    /*
     *	申请4*sizeof(DWORD)空间来保存传给新建线程的参数，申请的空间由新建线程负责释放
     */
    pdwParam = new DWORD[4];                                                      
    pdwParam[0] = dwLedNum; 
    pdwParam[1] = dwFlashTimes;
    pdwParam[2] = dwOntime;
    pdwParam[3] = dwOfftime;

    hLedThread = CreateThread(NULL, 0, epcLedFlashProc,
                             (void*)pdwParam, 0, NULL);                 /*  在新建线程中完成LED工作     */
                                                                           
    if (hLedThread == NULL){
        delete[] pdwParam;                                              /*  创建线程失败，释放申请内存  */
        return FALSE;
    }
    CloseHandle(hLedThread);
    
    return TRUE;
}

/*********************************************************************************************************
** Function name:   		epcLedAsyncOn
** Descriptions:    		本函数点亮指定的LED灯
** input parameters:   	    dwLedNum            LED编号：LED1,LED2
**                          dwOntime            LED点亮的持续时间(ms)，其中0表示一直点亮
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    本函数以异步方式执行，不会被阻塞
*********************************************************************************************************/
EPCLEDLIB_API BOOL epcLedAsyncOn (DWORD dwLedNum, DWORD dwOntime)
{
    if (dwOntime == 0){
        return epcLedOn(dwLedNum, dwOntime);                            /*  0表示一直点亮               */
    } else {
        return epcLedFlash(dwLedNum, 1, dwOntime, 0);
    }
}

/*********************************************************************************************************
** Function name:   		epcLedCallBackFunc
** Descriptions:    		设置回调函数指针，当异步的LED操作完成后，会调用该回调函数通知用户程序。
** input parameters:   	    dwLedNum        LED编号：LED1,LED2
**                          lpfnNotify      回调函数指针，如果是NULL，则表示不需要通知用户程序。函数类
**                                          型为void (*lpfnNotify)(BOOL bResult),bResult为执行结果，
**                                          TRUE表示执行成功，FALSE表示执行失败                                          
** output parameters:   	无
** Returned value:     	    无
*********************************************************************************************************/
EPCLEDLIB_API VOID epcLedSetCallBackFunc( DWORD dwLedNum, void (*lpfnNotify)(BOOL))
{
    if (dwLedNum == LED1){
        GfnNotify1 = lpfnNotify;
    } else if (dwLedNum == LED2){
        GfnNotify2 = lpfnNotify;
    }
}

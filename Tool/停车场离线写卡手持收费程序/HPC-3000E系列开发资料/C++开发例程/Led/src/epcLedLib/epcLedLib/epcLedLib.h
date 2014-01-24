
// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the EPCLEDLIB_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// EPCLEDLIB_API functions as being imported from a DLL, wheras this DLL sees symbols
// defined with this macro as being exported.
#ifdef EPCLEDLIB_EXPORTS
#define EPCLEDLIB_API __declspec(dllexport)
#else
#define EPCLEDLIB_API __declspec(dllimport)
#endif

#define  LED1   1
#define  LED2   2
 
#ifdef __cplusplus
extern "C" {
#endif

/*********************************************************************************************************
** Function name:   		epcLedOn
** Descriptions:    		此函数点亮指定的LED灯
** input parameters:   	    dwLedNum            LED编号：1--LED1，2--LED2
**                          dwOntime            LED点亮的持续时间(ms)，其中0表示一直点亮
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    该函数会阻塞dwOntime(ms)时间，但dwOntime为0时会立刻返回
*********************************************************************************************************/
EPCLEDLIB_API BOOL epcLedOn (DWORD dwLedNum, DWORD dwOntime);

/*********************************************************************************************************
** Function name:   		epcLedAsyncOn
** Descriptions:    		此函数点亮指定的LED灯
** input parameters:   	    dwLedNum            LED编号：1--LED1，2--LED2
**                          dwOntime            LED点亮的持续时间(ms)，其中0表示一直点亮
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    该函数以异步方式执行，不会被阻塞
*********************************************************************************************************/
EPCLEDLIB_API BOOL epcLedAsyncOn (DWORD dwLedNum, DWORD dwOntime);

/*********************************************************************************************************
** Function name:   		epcLedOff
** Descriptions:    		此函数熄灭指定的LED灯
** input parameters:   	    dwLedNum            LED编号：1--LED1，2--LED2
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
*********************************************************************************************************/
EPCLEDLIB_API BOOL epcLedOff (DWORD dwLedNum);

/*********************************************************************************************************
** Function name:   		epcLedFlash
** Descriptions:    		此函数使指定的LED灯闪烁
** input parameters:   	    dwLedNum            LED编号：1--LED1，2--LED2
**                          dwFlashTimes        LED闪烁次数
**                          dwOntime            LED点亮的持续时间(ms)
**                          dwOfftime           LED熄灭的持续时间(ms)
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    该函数以异步方式执行，不会被阻塞
*********************************************************************************************************/
EPCLEDLIB_API BOOL epcLedFlash (DWORD dwLedNum, DWORD dwFlashTimes, DWORD dwOntime, DWORD dwOfftime);

/*********************************************************************************************************
** Function name:   		epcLedGetStatus
** Descriptions:    		此函数获取指定的LED灯的状态
** input parameters:   	    dwLedNum            LED编号：1--LED1，2--LED2
** output parameters:   	无
** Returned value:     	    0--点亮；1--熄灭；其它--出现错误
*********************************************************************************************************/
EPCLEDLIB_API DWORD epcLedGetStatus (DWORD dwLedNum);

/*********************************************************************************************************
** Function name:   		epcLedCallBackFunc
** Descriptions:    		设置回调函数指针，当异步的LED操作完成后，会调用该回调函数通知用户程序。
** input parameters:   	    dwLedNum        LED编号：1--LED1，2--LED2
**                          lpfnNotify      回调函数指针，如果是NULL，则表示不需要通知用户程序。函数类
**                                          型为void (*lpfnNotify)(BOOL bResult),bResult为执行结果，
**                                          TRUE表示执行成功，FALSE表示执行失败                                          
** output parameters:   	无
** Returned value:     	    无
*********************************************************************************************************/
EPCLEDLIB_API VOID epcLedSetCallBackFunc( DWORD dwLedNum, void (*lpfnNotify)(BOOL));

#ifdef __cplusplus
}
#endif
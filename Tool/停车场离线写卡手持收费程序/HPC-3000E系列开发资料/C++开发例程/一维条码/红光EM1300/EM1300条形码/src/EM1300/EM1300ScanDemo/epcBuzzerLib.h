
// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the EPCBUZZERLIB_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// EPCBUZZERLIB_API functions as being imported from a DLL, wheras this DLL sees symbols
// defined with this macro as being exported.
#ifdef EPCBUZZERLIB_EXPORTS
#define EPCBUZZERLIB_API __declspec(dllexport)
#else
#define EPCBUZZERLIB_API __declspec(dllimport)
#endif



#ifdef __cplusplus
extern "C" {
#endif
    
/*********************************************************************************************************
** Function name:   		epcBuzzerOn
** Descriptions:    		本函数使蜂鸣器蜂鸣指定时间
** input parameters:   	    dwOntime    蜂鸣器持续蜂鸣的时间(ms)，其中0表示一直蜂鸣
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    本函数会阻塞dwOntime(ms)时间，但dwOntime为0时会立刻返回
*********************************************************************************************************/
EPCBUZZERLIB_API BOOL epcBuzzerOn (DWORD dwOntime);

/*********************************************************************************************************
** Function name:   		epcBuzzerOff
** Descriptions:    		本函数使蜂鸣器禁止
** input parameters:   	    无
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
*********************************************************************************************************/
EPCBUZZERLIB_API BOOL epcBuzzerOff (VOID);

/*********************************************************************************************************
** Function name:   		epcBuzzerGetStatus
** Descriptions:    		本函数获取蜂鸣器的状态
** input parameters:   	    无
** output parameters:   	无
** Returned value:     	    0--蜂鸣器蜂鸣；1--蜂鸣器禁止；其它--出现错误
*********************************************************************************************************/
EPCBUZZERLIB_API DWORD epcBuzzerGetStatus (VOID);

/*********************************************************************************************************
** Function name:   		epcBuzzerAsyncOn
** Descriptions:    		本函数使蜂鸣器蜂鸣指定时间
** input parameters:   	    dwOntime    蜂鸣器持续蜂鸣的时间(ms)，其中0表示一直蜂鸣
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    本函数以异步方式执行，不会被阻塞
*********************************************************************************************************/
EPCBUZZERLIB_API BOOL epcBuzzerAsyncOn (DWORD dwOntime);

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
EPCBUZZERLIB_API BOOL epcBuzzerBeeps (DWORD dwFlashTimes, DWORD dwOntime, DWORD dwOfftime);

/*********************************************************************************************************
** Function name:   		epcBuzzerSetCallBackFunc
** Descriptions:    		设置回调函数指针，当异步的蜂鸣器操作任务完成后，会调用该回调函数通知用户程序。
** input parameters:   	    lpfnNotify      回调函数指针,如果是NULL，则表示不需要通知用户程序。函数类
**                                          型为void (*lpfnNotify)(BOOL bResult),bResult为执行结果，
**                                          TRUE表示执行成功，FALSE表示执行失败    
** output parameters:   	无
** Returned value:     	    无
*********************************************************************************************************/
EPCBUZZERLIB_API VOID epcBuzzerSetCallBackFunc( void (*lpfnNotify)(BOOL));

#ifdef __cplusplus
}
#endif
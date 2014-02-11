/****************************************Copyright (c)****************************************************
**                            Guangzhou ZHIYUAN electronics Co.,LTD.
**                                      
**                                 http://www.embedtools.com
**
**--------------File Info---------------------------------------------------------------------------------
** File name:               EM3070DLL.h
** Latest modified Date:    2012-04-16
** Latest Version:          V1.01
** Descriptions:            EM3070软件包头文件(应用程序专用)
**
*********************************************************************************************************/
#ifndef    __EM3070DLL_H
#define    __EM3070DLL_H

#if defined(__cplusplus)
extern "C"
{
#endif


#ifdef EM3070DLL_API				// Linked as a DLL
#define EM3070DLL_API		__declspec(dllimport)
#else
#define EM3070DLL_API		__declspec(dllexport)
#endif

/*********************************************************************************************************
  串口操作错误返回值定义
*********************************************************************************************************/
#define    COM_OK               0                                      /* 串口打开成功                  */
#define    COM_ERR_PARA         41                                     /* 串口参数设置错误              */
#define    COM_ERR_TIME         42                                     /* 超时参数设置错误              */
#define    COM_ERR_USING        43                                     /* 串口已经被使用                */
#define    COM_ERR_OPEN         44                                     /* 串口打开错误                  */                          
#define    COM_NOTOPEN          45                                     /* 串口未打开                    */                                  

/*********************************************************************************************************
  读写数据操作返回值定义
*********************************************************************************************************/
#define    SUCCESS_SETTING      47                                     /* 成功设置						*/
#define    SEND_OK              48                                     /* 发送成功						*/
#define    CREATETHREADFAIL     49                                     /* 创建线程失败					*/
#define    INIT_OK              50                                     /* 初始化成功						*/
#define    DATA_ISNULL          51                                     /* 队列中数据为空					*/ 
#define    RECEIVE_SUCCESS      52                                     /* 从队列中获取数据成功				*/         
#define    DATA_ERR_SELECT      53                                     /* 参数错误选择					*/
#define    ACK_SUCCESS          54                                     /* 应答成功						*/
#define    ACK_FAILED           55                                     /* 应答失败						*/
#define    MODE_FAILED          56                                     /* 模式设置错误					*/
#define    IS_WRITING           57                                     /* 数据正在写入，不能读取			*/ 
#define    UNDEFINE             58                                     /* 无法设置类型					*/
#define    SYSTEMNOSAFE         59                                     /* 非安全系统						*/
#define	   TAIL_FAILED			60									   /* 设置尾字符失败					*/
#define	   SCANMODENOTSUPPORT	61									   /* 扫描模式不支持					*/
#define	   CODEBARNOTSUPPORT	61									   /* 扫描模式不支持					*/
#define    ACK_TIMEOUT          55                                     /* 应答超时						*/
#define	   PREX_FAILED			62									   /* 设置尾字符失败					*/
#define	   FACTORY_FAILED		63									   /* 恢复出厂设置失败					*/


/*********************************************************************************************************
  条形码使能参数号(EM3070默认情况下所有码制均使能)
*********************************************************************************************************/
#define    CODE128				0                                     /* CODE 128码参数号              */
#define    EAN_8				1                                     /* EAN-8码参数号           */
#define    EAN_13				2                                     /* EAN-13码参数号               */
#define    UPC_E				3                                     /* UPC_E码参数号                 */
#define    UPC_A				4                                     /* UPC_A码参数号                */
#define    INTERLEAVED2OF5		5                                     /* INTERLEAVED2OF5码参数号                  */
#define    MATRIX2OF5			6                                     /* ISBN码参数号                  */      
#define    CODE39				7                                    /* ITF-14码参数号                */
#define    CODABAR              8                                    /* Deutsche 14码参数号           */
#define    CODE93               9                                    /* Deutsche 12码参数号           */
#define    UCCEAN_128			10                                    /* COOP 25码参数号               */
#define    GS1DATABAR           11                                    /* Matrix 2 of 5码参数号         */
#define    EANUCCCOMPOSITE      12                                    /* Industrial 25码参数号         */
#define    UPCEANCOMPOSITE      13                                    /* Standard 25码参数号           */
#define    CODE11				14                                    /* Code 39码参数号               */
#define    ISBN				    15                                    /* Codabar码参数号               */
#define    Industrial_25		16                                    /* Code 93码参数号               */
#define    Standard_25			17                                    /* Code 11码参数号               */
#define    PLESSEY				18                                    /* Plessey码参数号               */
#define    MSI_PLESSEY          19                                    /* MSI-Plessey码参数号           */
#define    PDF_417              20                                    /* GS1 Databar码参数号           */
#define    QR_CODE              21                                    /* GS1 Databar码参数号           */
#define    AZTEC                22                                    /* GS1 Databar码参数号           */
#define    DATAMATRIX           23                                    /* GS1 Databar码参数号           */
#define    CHINESESENSECODE     24                                    /* GS1 Databar码参数号           */
#define    MAXICODE             25                                    /* GS1 Databar码参数号           */

/*********************************************************************************************************
  条形码使能与禁能
*********************************************************************************************************/
#define    ENABLE               0x01                                    /* 使能                         */
#define    DISABLE              0x00                                    /* 禁能                         */



/*********************************************************************************************************
  串口号参数宏定义
*********************************************************************************************************/
#define    COM1                 0
#define    COM2                 1
#define    COM3                 2
#define    COM4                 3
#define    COM5                 4
#define    COM6                 5
#define    COM7                 6
#define    COM8                 7
#define    COM9                 8

/*********************************************************************************************************
  触发方式参数宏定义
*********************************************************************************************************/
#define	   SCAN_MANUAL		    00						               /* 手动扫描模式					*/
#define    SCAN_AUTO            10                                     /* 自动扫描模式					*/
#define    SCAN_CONTINUE        20                                     /* 持续扫描模式					*/
#define    SCAN_SINGLE          30                                  /* 单次连续扫描模式					*/

/*********************************************************************************************************
** Function name:           EM3070SerialInit
** Descriptions:            串口接收数据初始化                         
** input parameters:        ucPort         串口号,如COM1,COM2
** output parameters:       NONE
** 
** Returned value:          正确返回INIT_OK, 串口设置参数错误返回COM_ERR_PARA, 超时设置错误
**                          返回COM_ERR_TIME, 串口已打开返回COM_ERR_USING, 
**							串口打开错误COM_ERR_OPEN,串口接收线程创建失败返回CREATETHREADFAIL.
*********************************************************************************************************/
EM3070DLL_API  int EM3070SerialInit(unsigned char ucPort);






/*********************************************************************************************************
** Function name:           EM3070SerialTerminate
** Descriptions:            EM3070通信终止                         
** input parameters:        NONE													
** output parameters:       NONE
** Returned value:          操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
EM3070DLL_API  bool EM3070SerialTerminate(void);







/*********************************************************************************************************
** Function name:           EM3070DecodeState
** Descriptions:            开始扫描译码、停止扫描译码                         
** input parameters:        bDecodeState       扫描译码状态(值为true时，开始扫描译码
**                                                          值为false时，停止扫描译码)
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,
**							发送成功收到失败应答ACK_FAILED
**                          或者未收到应答返回ACK_TIMEOUT
*********************************************************************************************************/
EM3070DLL_API  int EM3070DecodeState(bool bDecodeState);









/*********************************************************************************************************
** Function name:           EM3070TriggerState
** Descriptions:            触发方式设置                         
** input parameters:        ucTriggerState          触发方式(值为SCAN_INTERVAL时，间隔扫描模式
**                                                           值为SCAN_PERCEIVE时，感应扫描模式
**                                                           值为SCAN_CONTINUE时， 持续扫描模式
**                                                           值为SCAN_LAZYPERCEIVE时， 延迟感应扫描模式
**                                                           值为SCAN_SINGLE时，单次扫描模式)                                                      
**														
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口参数错误返回COM_ERR_SELECT,
**							发送成功收到失败应答ACK_FAILED
**                          或者未收到应答返回ACK_TIMEOUT
**							或者触发扫描模式不支持返回SCANMODENOTSUPPORT
*********************************************************************************************************/
EM3070DLL_API  int EM3070TriggerState(unsigned char TriggerState);








/*********************************************************************************************************
** Function name:           GetDecodeData
** Descriptions:            接收条形码数据                         
** input parameters:        NONE														
** output parameters:       pucReceiveBuf   接收数据缓冲区指针
**							pucCount        接收数据个数指针
** Returned value:          正确返回RECEIVE_SUCCESS
**							队列中数据为空返回DATA_ISNULL
**                          数据正在写无法读取时返回IS_WRITING.
*********************************************************************************************************/
EM3070DLL_API  int EM3070GetDecodeData(unsigned char *pucRecieveBuf, unsigned char *pucCount);








/*********************************************************************************************************
** Function name:           EM3070GetVersion
**
** Descriptions:            获取EM3070软件包版本号
**                          
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          返回当前软件包版本号.
**                          (返回100，表示V1.00；返回110，表示V1.10......)
*********************************************************************************************************/
EM3070DLL_API unsigned int EM3070GetVersion (void);







/*********************************************************************************************************
** Function name:           EM3070CodeBarEnable
** Descriptions:            设置条形码使能、禁能                         
** input parameters:        ucCodeBar           条形码参数号
**                          ucEnable            使能与禁能(ENABLE表示使能，DISABLE表示禁能)
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,
**							发送成功收到失败应答ACK_FAILED
**                          或者未收到应答返回ACK_TIMEOUT
**                          或者条形码码制不支持返回CODEBARNOTSUPPORT
*********************************************************************************************************/
EM3070DLL_API int EM3070CodeBarEnable(unsigned char ucCodeBar, unsigned char ucEnable);





/*********************************************************************************************************
** Function name:           EM3070CodeBarRestoreSetting
** Descriptions:            重置条码码制设置 
**                           
** input parameters:        ucCodeBar           条形码参数号
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,
**							发送成功收到失败应答ACK_FAILED
**                          或者未收到应答返回ACK_TIMEOUT
**                          或者条形码码制不支持返回CODEBARNOTSUPPORT
*********************************************************************************************************/
EM3070DLL_API int EM3070CodeBarRestoreSetting(unsigned char ucCodeBar);






/*********************************************************************************************************
** Function name:           EM3070FactorySettings
** Descriptions:            EM3070恢复出厂设置                         
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,
**							发送成功收到失败应答ACK_FAILED
**                          或者未收到应答返回ACK_TIMEOUT
*********************************************************************************************************/
EM3070DLL_API int EM3070FactorySettings(void);







/*********************************************************************************************************
** Function name:           EM3070SpecialCommand
** Descriptions:            特殊接口,用于迫切需要,但是暂未提供的功能实现                         
** input parameters:        cmd					命令字符串
**							length				命令长度
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,
**							发送成功收到失败应答ACK_FAILED
**                          或者未收到应答返回ACK_TIMEOUT
*********************************************************************************************************/
EM3070DLL_API int EM3070SpecialCommand(char cmd[],int length);


#if defined(__cplusplus)
}
#endif

#endif
/*********************************************************************************************************
  END FILE
*********************************************************************************************************/




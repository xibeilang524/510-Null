/****************************************Copyright (c)****************************************************
**                            Guangzhou ZHIYUAN electronics Co.,LTD.
**                                      
**                                 http://www.embedtools.com
**
**--------------File Info---------------------------------------------------------------------------------
** File name:               Se955DLL.h
** Latest modified Date:    2011-04-16
** Latest Version:          V1.00
** Descriptions:            Se955软件包头文件(应用程序专用)
**
**--------------------------------------------------------------------------------------------------------
** Created by:              曹 欢
** Created date:            2011-04-16
** Version:                 V1.00
** Descriptions:            原始版本
**
**--------------------------------------------------------------------------------------------------------
** Modified by:                  
** Modified date:           
** Version:                 
** Descriptions:           
**
*********************************************************************************************************/
#ifndef    __Se955DLL_H
#define    __Se955DLL_H

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
#define    SUCCESS_SETTING      47                                     /* 成功设置                      */
#define    SEND_OK              48                                     /* 发送成功                      */
#define    CREATETHREADFAIL     49                                     /* 创建线程失败                  */
#define    INIT_OK              50                                     /* 初始化成功                    */
#define    DATA_ISNULL          51                                     /* 队列中数据为空                */ 
#define    RECEIVE_SUCCESS      52                                     /* 从队列中获取数据成功          */         
#define    DATA_ERR_SELECT      53                                     /* 参数错误选择                  */
#define    ACK_SUCCESS          54                                     /* 应答成功                      */
#define    ACK_FAILED           55                                     /* 应答失败                      */
#define    MODE_FAILED          56                                     /* 模式设置错误                  */
#define    IS_WRITING           57                                     /* 数据正在写入，不能读取        */ 

/*********************************************************************************************************
  条形码使能参数号(可以实现各种编码的条形码的使能与禁能,默认为全部使能)
*********************************************************************************************************/
#define    UPCA_CODE            0x01                                    /* UPC_A码参数号                */
#define    UPCE_CODE            0x02                                    /* UPC_E码参数号                */
#define    UPCE1_CODE           0x0C                                    /* UPC_E1码参数号               */
#define    EAN8_CODE            0x04                                    /* EAN8码参数号                 */
#define    EAN13_CODE           0x03                                    /* EAN13码参数号                */
#define    CODE128              0x08                                    /* CODE128码参数号              */
#define    EAN_128              0x0E                                    /* EAN_128码参数号              */
#define    EISBT_128            0x54                                    /* EISBT_128码参数号            */
#define    CODE39               0x00                                    /* CODE39码参数号               */
#define    TRIOPTIC_CODE39      0x0D                                    /* TRIOPTIC_CODE39码参数号      */
#define    CODE93               0x09                                    /* CODE93码参数号               */
#define    CODE11               0x0A                                    /* CODE11码参数号               */
#define    INTERLEAVED          0x06                                    /* INTERLEAVED 2 OF 5码参数号   */
#define    DISCRETE             0x05                                    /* DISCRETE 2 OF 5码参数号      */
#define    CODABAR              0x07                                    /* CODABAR码参数号              */
#define    TRANMITFORMAT        0xEB                                    /* CODABAR码参数号              */
#define    BOOKLANDEAN          0x53                                    /* BOOKLANDEAN码参数号          */

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
  SE955的波特率编码
*********************************************************************************************************/
#define    B_RATE300            0x01                                    /* 波特率300                    */
#define    B_RATE600            0x02                                    /* 波特率600                    */
#define    B_RATE1200           0x03                                    /* 波特率1200                   */
#define    B_RATE2400           0x04                                    /* 波特率2400                   */
#define    B_RATE4800           0x05                                    /* 波特率4800                   */
#define    B_RATE9600           0x06                                    /* 波特率9600                   */
#define    B_RATE19200          0x07                                    /* 波特率19200                  */
#define    B_RATE38400          0x08                                    /* 波特率38400                  */

/*********************************************************************************************************
  Se955奇偶校验位参数宏定义
*********************************************************************************************************/
#define	   DAT_ODD              0x00   	                                /* 奇校验                       */
#define	   DAT_EVEN             0x01   		                            /* 偶校验                       */
#define	   DAT_MARK             0x02  	                                /* 标号校验                     */
#define	   DAT_SPACE            0x03                                    /* 空格校验                     */
#define	   DAT_NONE             0x04		                            /* 无校验 (default)	            */

/*********************************************************************************************************
  Se955停止位参数宏定义
*********************************************************************************************************/
#define	   STOP_BITONE          0x01		                            /* 停止位1位                    */
#define	   STOP_BITTWO          0x02		                            /* 停止位2位	                */

/*********************************************************************************************************
  触发方式参数宏定义
*********************************************************************************************************/
#define    SCAN_HOST            0x08                                   /* 软件模式                      */
#define    SCAN_LEVEL           0x00                                   /* 硬件模式电平触发              */
#define    SCAN_CONTINUE        0x04                                   /* 持续扫描                      */
#define    SCAN_PULSE           0x02                                   /* 硬件模式脉冲触发              */

/*********************************************************************************************************
** Function name:           Se955SerialInit
** Descriptions:            串口接收数据初始化                         
** input parameters:        ucPort         串口号,如COM1,COM2
**							ucBaud         波特率设置
**							ucStop		   停止位设置
**							ucParity	   校验位设置
** output parameters:       NONE
** 
** Returned value:          正确返回INIT_OK, 串口设置参数错误返回COM_ERR_PARA, 超时设置错误
**                          返回COM_ERR_TIME, 串口已打开返回COM_ERR_USING, 
**							串口打开错误COM_ERR_OPEN,串口接收线程创建失败返回CREATETHREADFAIL.
*********************************************************************************************************/
extern "C" __declspec(dllexport)  int Se955SerialInit(UCHAR ucPort, UCHAR ucBaud, UCHAR ucStop, UCHAR ucParity);






/*********************************************************************************************************
** Function name:           Se955SerialTerminate
** Descriptions:            Se955通信终止                         
** input parameters:        NONE													
** output parameters:       NONE
** Returned value:          操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
extern "C" __declspec(dllexport)  BOOL Se955SerialTerminate(void);







/*********************************************************************************************************
** Function name:           Se955AimState
** Descriptions:            设置Se955瞄准、取消瞄准                         
** input parameters:        bAimState         瞄准状态(值为TRUE时，瞄准
**                                                     值为FALSE时，取消瞄准)
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
extern "C" __declspec(dllexport)  int Se955AimState(BOOL bAimState);







/*********************************************************************************************************
** Function name:           Se955DecodeState
** Descriptions:            开始扫描译码、停止扫描译码                         
** input parameters:        bDecodeState       扫描译码状态(值为TRUE时，开始扫描译码
**                                                          值为FALSE时，停止扫描译码)
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
extern "C" __declspec(dllexport)  int Se955DecodeState(BOOL bDecodeState);








/*********************************************************************************************************
** Function name:           Se955ScanningState
** Descriptions:            使能扫描、禁能扫描                         
** input parameters:        bScanningState        使能状态(值为TRUE时，使能扫描
**                                                         值为FALSE时，禁能扫描)
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
extern "C" __declspec(dllexport)  int Se955ScanningState(BOOL bScanningState);







/*********************************************************************************************************
** Function name:           Se955LedState
** Descriptions:            设置译码LED灯亮、灭                         
** input parameters:        bLedState         LED状态(值为TRUE时，LED灯亮
**                                                    值为FALSE时，LED灯灭)
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
extern "C" __declspec(dllexport)  int Se955LedState(BOOL bLedState);








/*********************************************************************************************************
** Function name:           Se955TriggerState
** Descriptions:            触发方式设置                         
** input parameters:        ucTriggerState          触发方式(值为SCAN_LEVEL时，硬件电平触发扫描
**                                                           值为SCAN_PULSE时，硬件脉冲触发扫描
**                                                           值为SCAN_HOST时， 软件触发扫描
**                                                           值为SCAN_CONTINUE时，持续触发扫描)                                                      
**														
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
extern "C" __declspec(dllexport)  int Se955TriggerState(UCHAR ucTriggerState);








/*********************************************************************************************************
** Function name:           GetDecodeData
** Descriptions:            接收条形码数据                         
** input parameters:        NONE														
** output parameters:       pucReceiveBuf   接收数据缓冲区指针
**							pucCount        接收数据个数指针
** Returned value:          正确返回RECEIVE_SUCCESS
**							队列中数据为空返回DATA_ISNULL.
*********************************************************************************************************/
extern "C" __declspec(dllexport)  int Se955GetDecodeData(BYTE *pucRecieveBuf, BYTE *pucCount);








/*********************************************************************************************************
** Function name:           Se955GetVersion
**
** Descriptions:            获取Se955软件包版本号
**                          
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          返回当前软件包版本号.
**                          (返回100，表示V1.00；返回110，表示V1.10......)
*********************************************************************************************************/
extern "C" __declspec(dllexport) DWORD Se955GetVersion (void);







/*********************************************************************************************************
** Function name:           Se955CodeBarEnable
** Descriptions:            设置条形码使能、禁能                         
** input parameters:        ucCodeBar           条形码参数号
**                          ucEnable            使能与禁能(ENABLE表示使能，DISABLE表示禁能)
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
extern "C" __declspec(dllexport) int Se955CodeBarEnable(UCHAR ucCodeBar, UCHAR ucEnable);




/*********************************************************************************************************
** Function name:           Se955FactorySettings
** Descriptions:            Se955恢复出厂设置                         
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
extern "C" __declspec(dllexport) int Se955FactorySettings(void);



#endif
/*********************************************************************************************************
  END FILE
*********************************************************************************************************/




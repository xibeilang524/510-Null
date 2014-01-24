/****************************************Copyright (c)****************************************************
**                            Guangzhou ZHIYUAN electronics Co.,LTD.
**                                      
**                                 http://www.embedtools.com
**
**--------------File Info---------------------------------------------------------------------------------
** File name:               Ue966DLL.h
** Latest modified Date:    2011-04-16
** Latest Version:          V1.00
** Descriptions:            Ue966软件包头文件(应用程序专用)
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
#ifndef    __Ue966DLL_H
#define    __Ue966DLL_H

#if defined(__cplusplus)
extern "C"
{
#endif


#ifdef Ue966DLL_API				// Linked as a DLL
#define Ue966DLL_API		__declspec(dllimport)
#else
#define Ue966DLL_API		__declspec(dllexport)
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
#define    UNDEFINE             58                                     /* 无法设置类型                  */
#define    SYSTEMNOSAFE         59                                     /* 非安全系统                    */
#define    TAIL_FAILED          60                                     /* 非安全系统                    */

/*********************************************************************************************************
  条形码使能参数号(可以实现各种编码的条形码的使能与禁能)
*********************************************************************************************************/
#define    UPCA_CODE            0x01                                    /* UPC_A码参数号                */
#define    UPCE_CODE            0x02                                    /* UPC_E码参数号                */
#define    UPCE1_CODE           0x0C                                    /* UPC_E1码参数号               */
#define    EAN8_CODE            0x04                                    /* EAN8码参数号                 */
#define    EAN13_CODE           0x03                                    /* EAN13码参数号                */
#define    BOOKLANDEAN          0x53                                    /* BOOKLANDEAN码参数号          */
#define    CODE128              0x08                                    /* CODE128码参数号              */
#define    EAN_128              0x0E                                    /* EAN_128码参数号              */
#define    ISBT_128             0x54                                    /* ISBT_128码参数号            */
#define    CODE39               0x00                                    /* CODE39码参数号               */
#define    TRIOPTIC_CODE39      0x0D                                    /* TRIOPTIC_CODE39码参数号      */
#define    CODE39_FULL          0x11                                    /* CODE39 FULL ASCII码参数号    */
#define    CODE93               0x09                                    /* CODE93码参数号               */
#define    CODE11               0x0A                                    /* CODE11码参数号               */
#define    INTERLEAVED          0x06                                    /* INTERLEAVED 2 OF 5码参数号   */
#define    DISCRETE             0x05                                    /* DISCRETE 2 OF 5码参数号      */
#define    CODABAR              0x07                                    /* CODABAR码参数号              */
#define    MSI                  0x0B                                    /* MSI码参数号                  */
#define    CHINESE_2OF5         0xFF                                    /* CHINESE 2 OF 5码参数号       */
#define    RSS_14               0xFE                                    /* RSS_14码参数号               */
#define    RSS_LIMITED          0xFD                                    /* RSS_LIMITED码参数号          */
#define    RSS_EXPANDED         0xFC                                    /* RSS_EXPANDED码参数号         */


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
  Ue966的波特率编码
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
  Ue966奇偶校验位参数宏定义
*********************************************************************************************************/
#define	   DAT_ODD              0x00   	                                /* 奇校验                       */
#define	   DAT_EVEN             0x01   		                            /* 偶校验                       */
#define	   DAT_MARK             0x02  	                                /* 标号校验                     */
#define	   DAT_SPACE            0x03                                    /* 空格校验                     */
#define	   DAT_NONE             0x04		                            /* 无校验 (default)	            */

/*********************************************************************************************************
  Ue966停止位参数宏定义
*********************************************************************************************************/
#define	   STOP_BITONE          0x01		                            /* 停止位1位                    */
#define	   STOP_BITTWO          0x02		                            /* 停止位2位	                */

/*********************************************************************************************************
  触发方式参数宏定义
*********************************************************************************************************/
#define    SCAN_HOST            0x08                                   /* 软件模式                      */
#define    SCAN_CONTINUE        0x04                                   /* 持续扫描                      */

/*********************************************************************************************************
** Function name:           Ue966SerialInit
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
Ue966DLL_API  int Ue966SerialInit(unsigned char ucPort, unsigned char ucBaud, unsigned char ucStop, unsigned char ucParity);






/*********************************************************************************************************
** Function name:           Ue966SerialTerminate
** Descriptions:            Ue966通信终止                         
** input parameters:        NONE													
** output parameters:       NONE
** Returned value:          操作成功返回true, 操作失败返回false.
*********************************************************************************************************/
Ue966DLL_API  bool Ue966SerialTerminate(void);








/*********************************************************************************************************
** Function name:           Ue966DecodeState
** Descriptions:            开始扫描译码、停止扫描译码                         
** input parameters:        bDecodeState       扫描译码状态(值为true时，开始扫描译码
**                                                          值为false时，停止扫描译码)
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
Ue966DLL_API  int Ue966DecodeState(bool bDecodeState);









/*********************************************************************************************************
** Function name:           Ue966TriggerState
** Descriptions:            触发方式设置                         
** input parameters:        ucTriggerState          触发方式( 值为SCAN_HOST时， 软件触发扫描
**                                                           值为SCAN_CONTINUE时，持续触发扫描)                                                      
**														
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
Ue966DLL_API  int Ue966TriggerState(unsigned char ucTriggerState);








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
Ue966DLL_API  int Ue966GetDecodeData(unsigned char *pucRecieveBuf, unsigned char *pucCount);








/*********************************************************************************************************
** Function name:           Ue966GetVersion
**
** Descriptions:            获取Ue966软件包版本号
**                          
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          返回当前软件包版本号.
**                          (返回100，表示V1.00；返回110，表示V1.10......)
*********************************************************************************************************/
Ue966DLL_API unsigned int Ue966GetVersion (void);







/*********************************************************************************************************
** Function name:           Ue966CodeBarEnable
** Descriptions:            设置条形码使能、禁能                         
** input parameters:        ucCodeBar           条形码参数号
**                          ucEnable            使能与禁能(ENABLE表示使能，DISABLE表示禁能)
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
Ue966DLL_API int Ue966CodeBarEnable(unsigned char ucCodeBar, unsigned char ucEnable);





/*********************************************************************************************************
** Function name:           Ue966CodeBarSetLength
** Descriptions:            设置条形码长度 
**                           
** input parameters:        ucCodeBar           条形码参数号
**                          ucLength1           长度1
**                          ucLength2           长度2
**                                              如果只要设置一个特定长度,则将ucLength1设置为需要的长度,
**                                              ucLength2设置为0;
**                                              如果需要设置两个特定长度,则分别设置好ucLength1和ucLength2,
**                                              此时,ucLength1必须大于ucLength2;
**                                              如果需要设置区间长度,则分别设置好ucLength1和ucLength2
**                                              ucLength1必须小于ucLength2,长度范围为ucLength1到ucLength2;
**                                              如果需要设置任意长度,则将ucLength1和ucLength2均设置为0
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED 
*********************************************************************************************************/
Ue966DLL_API int Ue966CodeBarSetLength(unsigned char ucCodeBar, unsigned char ucLength1, unsigned char ucLength2);






/*********************************************************************************************************
** Function name:           Ue966FactorySettings
** Descriptions:            Ue966恢复出厂设置                         
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
**							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
**                          或者未收到应答返回ACK_FAILED	
*********************************************************************************************************/
Ue966DLL_API int Ue966FactorySettings(void);



#if defined(__cplusplus)
}
#endif

#endif
/*********************************************************************************************************
  END FILE
*********************************************************************************************************/




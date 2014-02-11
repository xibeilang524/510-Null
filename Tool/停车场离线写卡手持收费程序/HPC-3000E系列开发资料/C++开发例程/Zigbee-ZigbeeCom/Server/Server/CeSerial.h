// CeSerial.h: interface for the CCeSerial class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CESERIAL_H__8DCCE440_633D_4524_BF0A_DAA447B2794C__INCLUDED_)
#define AFX_CESERIAL_H__8DCCE440_633D_4524_BF0A_DAA447B2794C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000



/*********************************************************************************************************
  声明串口接收回调函数指针 数据类型
*********************************************************************************************************/
typedef void  (CALLBACK *PFUN_COMRCV)(LPVOID pvUserParam, BYTE *pucBuf, DWORD dwRcvLen);

/*********************************************************************************************************
串口操作错误返回值定义
*********************************************************************************************************/
#define COM_OK              0
#define COM_ERR_PARA        41
#define COM_ERR_TIME        42
#define COM_ERR_USING       43
#define COM_ERR_OPEN        44

/*********************************************************************************************************
  串口号定义
*********************************************************************************************************/
typedef enum  {
	ECOM1 = 1,
	ECOM2,
	ECOM3,
	ECOM4,
    ECOM5,
    ECOM6,
    ECOM7,
    ECOM8,
    ECOM9
}	EPort;


/*********************************************************************************************************
  波特率设置值定义
*********************************************************************************************************/
typedef enum  {
		EBaud110     = 110,		                                        /*  110 bits/s                  */
		EBaud300     = 300,		                                        /*  300 bits/s                  */
		EBaud600     = 600,		                                        /*  600 bits/s                  */
		EBaud1200    = 1200,	                                        /*  1200 bits/s                 */
		EBaud2400    = 2400,	                                        /*  2400 bits/s                 */
		EBaud4800    = 4800,	                                        /*  4800 bits/s                 */
		EBaud9600    = 9600,	                                        /*  9600 bits/s                 */
		EBaud14400   = 14400,	                                        /*  14400 bits/s                */
		EBaud19200   = 19200,	                                        /*  19200 bits/sec              */
		EBaud38400   = 38400,	                                        /*  38400 bits/sec              */
		EBaud56000   = 56000,	                                        /*  56000 bits/sec              */
		EBaud57600   = 57600,	                                        /*  57600 bits/sec              */
		EBaud115200  = 115200	                                        /*  115200 bits/sec	            */
}	EBaudrate;


/*********************************************************************************************************
  数据位设置值定义
*********************************************************************************************************/
typedef enum  {
		EData5       =  5,			                                    /*  5 bits per byte             */
		EData6       =  6,			                                    /*  6 bits per byte             */
		EData7       =  7,			                                    /*  7 bits per byte             */
		EData8       =  8			                                    /*  8 bits per byte (default)   */
}	EDataBits;


/*********************************************************************************************************
  停止位设置值定义
*********************************************************************************************************/
typedef enum  {
		EStop1       = 0,			                                    /*  1 stopbit (default)         */
		EStop1_5     = 1,			                                    /*  1.5 stopbit                 */
		EStop2       = 2			                                    /*  2 stopbits                  */
}	EStopBits;


/*********************************************************************************************************
  奇偶较验设置值定义
*********************************************************************************************************/
typedef enum  {
		EParNone    = NOPARITY,    	                                    /*  无校验 (default)	        */
		EParOdd     = ODDPARITY,		                                /*  奇校验                      */
		EParEven    = EVENPARITY,  	                                    /*  偶校验                      */
		EParMark    = MARKPARITY,  	                                    /*  标号校验                    */
		EParSpace   = SPACEPARITY		                                /*  空格校验                    */
}  EParity;


/*********************************************************************************************************
   DTR硬件流控设置值定义
*********************************************************************************************************/
typedef enum  {
		EDtrDisable    = DTR_CONTROL_DISABLE,    	                    /*  DTR置为OFF (default)	    */
		EDtrEnable     = DTR_CONTROL_ENABLE,		                    /*  DTR置为ON                   */
		EDtrHandshake  = DTR_CONTROL_HANDSHAKE,  	                    /*  允许DTR“握手”             */
}  EDtrCon;


/*********************************************************************************************************
  RTS硬件流控设置值定义
*********************************************************************************************************/
typedef enum  {
		ERtsDisable    = RTS_CONTROL_DISABLE,    	                    /*  RTS置为OFF (default)        */
		ERtsEnable     = RTS_CONTROL_ENABLE,		                    /*  RTS置为ON                   */
		ERtsHandshake  = RTS_CONTROL_HANDSHAKE,  	        /*  当接收缓冲区数据小于半满时RTS为ON,      */
                                                            /*  当接收缓冲区数据超过四分之三满时RTS为OFF*/
		ERtsToggle     = RTS_CONTROL_TOGGLE,  	                        /*  有数据发送时RTS置为ON,      */
                                                                        /*  数据发送完后RTS恢复为OFF    */
}  ERtsCon;



/*********************************************************************************************************
  CCeSerial类定义
*********************************************************************************************************/
//class  AFX_EXT_CLASS  CCeSerial  
class  CCeSerial 
{
public:
	CCeSerial();
	virtual ~CCeSerial();

    /* 
     *  打开串口
     */
	int epcSerialOpen(EPort  Port, EBaudrate  BaudRate);

	int epcSerialOpen(EPort      Port,      EBaudrate  BaudRate, 
                      EDataBits  DataBits,  EStopBits  StopBits, EParity  Parity);

	int epcSerialOpen(EPort      Port,      EBaudrate  BaudRate, 
                      EDataBits  DataBits,  EStopBits  StopBits, EParity  Parity,
		              EDtrCon    DTRShake,  ERtsCon    RTSShake);
	
	/* 
     *  关闭串口
     */
	BOOL epcSerialClose(void);
	
    /* 
     *  发送数据 (阻塞式)
     */
	BOOL epcSerialSendData(BYTE *pucSendBuf, DWORD dwLength);

    /* 
     *  串口接收数据 (阻塞式)
     *  dwLength为要接收的数据个数，最大为1024字节.
     */
	DWORD epcSerialRcvData(BYTE  *pucRcvBuf, DWORD  dwLength, DWORD  dwOutTime, BOOL  bClrComBuf);  
	

    /* 
     *  串口接收数据 (非阻塞式)
     *  说明：调用本方法会建立一个线程处理数据接收，且只有用epcSerialRcvData()或epcSerialClose()时 
     *        才会关闭线程 (接收线程被关闭之前，不能再次调用本方法)
     */   
	BOOL epcSerialRcvDataTread(DWORD  dwLength,	                        /*  接收数据个数，最大为1024    */
					           DWORD  dwOutTime,		                /*  接收超时时间设置            */
		                       PFUN_COMRCV  pfunOnComRcv,	            /*  接收数据成功回调函数指针    */
					           LPVOID  pvUserParam);	                /*  回调函数需要的用户参数变量  */
    /*
     *	清除接收缓冲区
     */
    BOOL epcSerialRxClear();
	int CCeSerial::epcSetRTS(BOOL bEnRts);

private:
	DCB     dcbCOM;								                        /*  串口参数结构体              */
	HANDLE  hCOM;							                            /*  串口操作句柄                */

	HANDLE  hRcvThread;						                            /*  接收线程句柄                */
    HANDLE  hExitThreadEvent;				                            /*  串口接收线程退出事件        */		

	/* 
     *  串口接收线程及相应变量	
     */
	DWORD        dwRcvLength;
	PFUN_COMRCV  pfunRcvOnComRcv;
	LPVOID       pvRcvUserParam;

	static DWORD  WINAPI epcSerialComRcvTread(LPVOID  pvParam);

    /* 
     *  串口参数设置方法
     */
	BOOL epcSerialSetCom(EBaudrate  BaudRate,  EDataBits  DataBits, 
                         EStopBits  StopBits,  EParity    Parity,
				         EDtrCon    DTRShake,  ERtsCon    RTSShake);
public:
	BOOL epcCloseRcvThread(void);
};


#endif // !defined(AFX_CESERIAL1_H__D0079FDD_3F13_44EF_92CD_28E045E6F309__INCLUDED_)



/*********************************************************************************************************
  END FILE
*********************************************************************************************************/
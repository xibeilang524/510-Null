// CeSerial.cpp: implementation of the CCeSerial class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "CeSerial.h"
//#include "epcSerial.h"


//#ifdef _DEBUG
//#undef THIS_FILE
//static char THIS_FILE[]=__FILE__;
//#define new DEBUG_NEW
//#endif



/*********************************************************************************************************
** Function name:           CCeSerial
**
** Descriptions:            CCeSerial构造函数。初始化各成员变量。
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          无
**
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
CCeSerial::CCeSerial ()
{
	hCOM             = INVALID_HANDLE_VALUE;		                    /*  串口操作句柄                */
	hRcvThread       = NULL;					                        /*  串口接收线程句柄            */
	hExitThreadEvent = NULL;			                                /*  串口接收线程退出事件        */	
	
	dwRcvLength      = 1024;					                        /*  接收线程每次接收数据的个数  */
	pfunRcvOnComRcv  = NULL;				                            /*  接收数据成功回调函数指针    */
	pvRcvUserParam   = NULL;					                        /*  回调函数需要的用户参数变量  */
}




/*********************************************************************************************************
** Function name:           ~CCeSerial
**
** Descriptions:            CCeSerial拆构函数。关闭串口，如果串口接收线程没有退出，则通知其退出。
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          无
**
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
CCeSerial::~CCeSerial ()
{
	epcSerialClose();					                                /*  关闭串口                    */	
}

int CCeSerial::epcSetRTS(BOOL bEnRts)
{
	/* 判断串口是否已打开，打开则返回 */
	if (hCOM == INVALID_HANDLE_VALUE) {
		return (COM_ERR_USING);	
	}

	if (bEnRts)  
	{	
		if (EscapeCommFunction (hCOM, SETRTS))
		{
			return COM_OK;
		}
	}  
	else
	{
		if (EscapeCommFunction (hCOM, CLRRTS))
		{
			return COM_OK;
		}
	}
	return COM_ERR_PARA;
}


/*********************************************************************************************************
** Function name:           epcSerialSetCom
**
** Descriptions:            设置串口波特率，数据位，停止位，奇偶校验位等。
**                          使用本函数前必须打开好串口，并且把句柄保存在hCOM中。
** input parameters:        BaudRate        波特率
**                          DataBits        数据位
**                          StopBits        停止位
**                          Parity          奇偶校验位
**                          DTRShake        DTR硬件流控制设置
**                          RTSShake        RTS硬件流控制设置
** output parameters:       NONE
** 
** Returned value:          正确返回COM_OK, 串口设置参数错误返回COM_ERR_PARA, 超时设置错误返回COM_ERR_TIME.
**
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:             黄绍斌
** Modified date:           2008/11/20
**                          修改"设置超时参数"段，增加超时时间。(V1.02)
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
int CCeSerial::epcSerialSetCom (EBaudrate  BaudRate, 
					            EDataBits  DataBits,  EStopBits  StopBits, EParity  Parity,
			                    EDtrCon    DTRShake,  ERtsCon    RTSShake)
{
	COMMTIMEOUTS  ctoTimeOut;

	/*
	 *	设置串口参数
	 */ 
	GetCommState(hCOM, &dcbCOM);						                /*  读取串口的DCB               */
	dcbCOM.BaudRate = BaudRate;							                /*  波特率设置                  */ 
	dcbCOM.ByteSize = DataBits;							                /*  数据位设置                  */	
	dcbCOM.StopBits = StopBits;							                /*  停止位设置                  */
	dcbCOM.Parity   = Parity;								            /*  奇偶校验设置                */

    if (Parity != EParNone)  {
        dcbCOM.fParity = TRUE;			                                /*  奇偶校验使能控制            */
    }  else  {
        dcbCOM.fParity = FALSE;
    }
	     	
	dcbCOM.fBinary = TRUE;									            /*  允许二进制模式              */

	if (DTRShake == EDtrDisable)	{							
		dcbCOM.fDtrControl  = DTRShake;			                        /*  硬件流量控制设置            */	
		dcbCOM.fOutxDsrFlow = FALSE;
    }  else  {
		dcbCOM.fDtrControl  = DTRShake;
		dcbCOM.fOutxDsrFlow = TRUE;
	}
	
	if (RTSShake == ERtsDisable)  {	
		dcbCOM.fRtsControl  = RTSShake;
		dcbCOM.fOutxCtsFlow = FALSE;
    }  else  {
		dcbCOM.fRtsControl  = RTSShake;
		dcbCOM.fOutxCtsFlow = TRUE;
	}
	
	dcbCOM.fOutX = FALSE;						    /*  禁止软件流控制。fOutX: 收到Xoff后停止发送       */
	dcbCOM.fInX  = FALSE;							/*  禁止软件流控制。fInX: 缓冲区接收满后发送Xoff    */
	dcbCOM.fTXContinueOnXoff = FALSE;				/*  禁止软件流控制。fInX事件之后，发送是否继续运行  */
	
	/*
	 *  设置状态参数
	 */
	SetCommMask(hCOM, EV_RXCHAR);					                    /*  串口事件:接收到一个字符     */	
	SetupComm(hCOM, 16384, 16384);					                    /*  设置接收与发送的缓冲区大小  */
	if ( !SetCommState(hCOM, &dcbCOM) )  {			                    /*  设置串口的DCB               */	
		//RETAILMSG(1,(TEXT("无法按当前参数配置端口，请检查参数!")));
		epcSerialClose();						    /*  ClosePort()会设置hCOM = INVALID_HANDLE_VALUE    */
		return (COM_ERR_PARA);
	}
	
	/*
     *  设置超时参数
     */
	GetCommTimeouts(hCOM, &ctoTimeOut);		
	ctoTimeOut.ReadIntervalTimeout         = MAXDWORD;			        /*  接收字符间最大时间间隔      */
	ctoTimeOut.ReadTotalTimeoutMultiplier  = 1;		
	ctoTimeOut.ReadTotalTimeoutConstant    = 1;		                /*  读数据总超时常量            */
	ctoTimeOut.WriteTotalTimeoutMultiplier = 1;
	ctoTimeOut.WriteTotalTimeoutConstant   = 1;  /*  写数据总超时常量 (写为0量表示没有超时控制)      */		
	if ( !SetCommTimeouts(hCOM, &ctoTimeOut) )  {
		//RETAILMSG(1,(TEXT("无法设置超时参数!")));
		epcSerialClose();
		return (COM_ERR_TIME);
	}

	return (COM_OK);
}




/*********************************************************************************************************
  串口端口驱动名
*********************************************************************************************************/
const TCHAR GstrPortTbl[9][12] = {_T("COM1:"), _T("COM2:"), _T("COM3:"), _T("COM4:"),
                                  _T("COM5:"), _T("COM6:"), _T("COM7:"), _T("COM8:"), _T("COM9:")};


/*********************************************************************************************************
** Function name:           epcSerialOpen
**
** Descriptions:            打开串口。串口工作模式为8位数据位，1位停止位，无奇偶校验位。
**                          
** input parameters:        Port        串口号,如ECOM1,ECOM2
**                          BaudRate    波特率,如EBaud9600,EBaud115200
** output parameters:       NONE
** 
** Returned value:          正确返回COM_OK, 串口设置参数错误返回COM_ERR_PARA, 超时设置错误
**                          返回COM_ERR_TIME, 串口已打开返回COM_ERR_USING, 串口打开错误COM_ERR_OPEN.
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
int CCeSerial::epcSerialOpen (EPort  Port, EBaudrate  BaudRate)
{	
    int  iErr;

	/* 
     *  判断串口是否已打开，打开则返回
     */
	if (hCOM != INVALID_HANDLE_VALUE) {
        return (COM_ERR_USING);	
    }

	/* 
     *  参数判断
     */
	if ( (Port > ECOM9) || (Port < ECOM1) ) {	
		//RETAILMSG(1,(TEXT("打开串口号参数错误.")));
		return (COM_ERR_PARA);
	}

	if ( (BaudRate > EBaud115200) || (BaudRate < EBaud110) ) {
		//RETAILMSG(1,(TEXT("波特率参数错误.")));
		return (COM_ERR_PARA);
	}

	/* 
     *  打开串口  (对GstrPortTbl[]进行查表时,注意下标为0对应于ECOM1)
     */
	hCOM = CreateFile(GstrPortTbl[Port-1], GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
	if (hCOM == INVALID_HANDLE_VALUE)  {
		//RETAILMSG(1,(TEXT("无法打开端口或端口已打开!请检查是否已被占用.")));
		return (COM_ERR_OPEN);
	}

	/* 
     *  设置串口 (8位数据位，1位停止位，无奇偶校验，禁止硬件流控制)
     */
    iErr = epcSerialSetCom(BaudRate, EData8, EStop1, EParNone, EDtrDisable, ERtsDisable);
    if (iErr !=  COM_OK) {
        return (iErr); 
    }
	
	/* 
     *  清除收/发缓冲区
     */
	PurgeComm(hCOM, PURGE_TXCLEAR | PURGE_RXCLEAR);	 	

	return (COM_OK);
}




/*********************************************************************************************************
** Function name:           epcSerialOpen
**
** Descriptions:            打开串口。硬件流控制禁止。
**                          
** input parameters:        Port        串口号,如ECOM1,ECOM2
**                          BaudRate    波特率,如EBaud9600,EBaud115200
**                          DataBits    数据位,如EData8
**                          StopBits    停止位,如EStop1 
**                          Parity      奇偶校验位,如EParNone, EParOdd
** output parameters:       NONE
** 
** Returned value:          正确返回COM_OK, 串口设置参数错误返回COM_ERR_PARA, 超时设置错误
**                          返回COM_ERR_TIME, 串口已打开返回COM_ERR_USING, 串口打开错误COM_ERR_OPEN.
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
int CCeSerial::epcSerialOpen (EPort      Port,     EBaudrate  BaudRate, 
						      EDataBits  DataBits, EStopBits  StopBits, EParity  Parity)
{	
    int  iErr;

	/* 
     *  判断串口是否已打开，打开则返回
     */
	if (hCOM != INVALID_HANDLE_VALUE) {
        return (COM_ERR_USING);	
    }
	
	/* 
     *  参数判断
     */
	if ( (Port > ECOM9) || (Port < ECOM1) ) {	
		//RETAILMSG(1,(TEXT("打开串口号参数错误.")));
		return (COM_ERR_PARA);
	}
	
	if ( (BaudRate > EBaud115200) || (BaudRate < EBaud110) ) {
		//RETAILMSG(1,(TEXT("波特率参数错误.")));
		return (COM_ERR_PARA);
	}
	
	/* 
     *  打开串口
     */
	hCOM = CreateFile(GstrPortTbl[Port-1], GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
	if (hCOM == INVALID_HANDLE_VALUE) {
		//RETAILMSG(1,(TEXT("无法打开端口或端口已打开!请检查是否已被占用.")));
		return (COM_ERR_OPEN);
	}
	
	/* 
     *  设置串口 (禁止硬件流控制)
     */
	iErr = epcSerialSetCom(BaudRate, DataBits, StopBits, Parity, EDtrDisable, ERtsDisable); 
    if (iErr !=  COM_OK) {
        return (iErr); 
    }
	
	/* 
     *  清除收/发缓冲区
     */
	PurgeComm(hCOM, PURGE_TXCLEAR | PURGE_RXCLEAR);	 	
	
	return (COM_OK);
}




/*********************************************************************************************************
** Function name:           epcSerialOpen
**
** Descriptions:            打开串口
**                          
** input parameters:        Port        串口号,如ECOM1,ECOM2
**                          BaudRate    波特率,如EBaud9600,EBaud115200
**                          DataBits    数据位,如EData8
**                          StopBits    停止位,如EStop1 
**                          Parity      奇偶校验位,如EParNone, EParOdd
**                          DTRShake    DTR硬件流控制设置
**                          RTSShake    RTS硬件流控制设置
** output parameters:       NONE
** 
** Returned value:          正确返回COM_OK, 串口设置参数错误返回COM_ERR_PARA, 超时设置错误
**                          返回COM_ERR_TIME, 串口已打开返回COM_ERR_USING, 串口打开错误COM_ERR_OPEN.
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
int CCeSerial::epcSerialOpen (EPort      Port,      EBaudrate  BaudRate, 
						      EDataBits  DataBits,  EStopBits  StopBits, EParity  Parity,
						      EDtrCon    DTRShake,  ERtsCon    RTSShake)
{ 
    int  iErr;
    
	/* 
     *  判断串口是否已打开，打开则返回
     */
	if (hCOM != INVALID_HANDLE_VALUE) {
        return (COM_ERR_USING);	
    }
	
	/* 
     *  参数判断
     */
	if ( (Port > ECOM9) || (Port < ECOM1) ) {	
		//RETAILMSG(1,(TEXT("打开串口号参数错误.")));
		return (COM_ERR_PARA);
	}
	
	if( (BaudRate > EBaud115200) || (BaudRate < EBaud110) ) {
		//RETAILMSG(1,(TEXT("波特率参数错误.")));
		return (COM_ERR_PARA);
	}
	
	/* 
     *  打开串口
     */
	hCOM = CreateFile(GstrPortTbl[Port-1], GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
	if (hCOM == INVALID_HANDLE_VALUE) {
		//RETAILMSG(1,(TEXT("无法打开端口或端口已打开!请检查是否已被占用.")));
		return (COM_ERR_OPEN);
	}
	
	/* 
     *  设置串口 (禁止硬件流控制)
     */
	iErr = epcSerialSetCom(BaudRate, DataBits, StopBits, Parity, DTRShake, RTSShake); 
    if (iErr !=  COM_OK) {
        return (iErr); 
    }
	
	/* 
     *  清除收/发缓冲区
     */
	PurgeComm(hCOM, PURGE_TXCLEAR | PURGE_RXCLEAR);	 	
	
	return (COM_OK);
}




/*********************************************************************************************************
** Function name:           epcSerialClose
**
** Descriptions:            关闭串口。会设置hCOM = INVALID_HANDLE_VALUE。
**                          
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          操作成功返回TRUE, 操作失败返回FALSE.
**                          (操作失败原因: hCOM值错误)
**
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
BOOL CCeSerial::epcSerialClose (void)
{
	if (hRcvThread != NULL) {	
		SetEvent(hExitThreadEvent);				                        /*  通知串口接收线程退出        */
		WaitForSingleObject(hRcvThread, 500);		                    /*  等待线程退出                */
		
        CloseHandle(hRcvThread);					                    /*  关闭接收线程句柄            */
		CloseHandle(hExitThreadEvent);				                    /*  关闭线程退出事件句柄        */		
		hRcvThread = NULL;
	}
	
	if (hCOM != INVALID_HANDLE_VALUE) {
		SetCommMask(hCOM, 0);		
		PurgeComm(hCOM, PURGE_TXCLEAR | PURGE_RXCLEAR);	                /*  清除收/发缓冲               */
		CloseHandle(hCOM);								                /*  关闭串口操作句柄            */
		hCOM = INVALID_HANDLE_VALUE;
		return (TRUE);
	}

	return (FALSE);
}





/*********************************************************************************************************
** Function name:           epcSerialSendData
**
** Descriptions:            发送串口数据
**                          
** input parameters:        pucSendBuf      发送数据缓冲区指针
**                          dwLength        发送数据个数
** output parameters:       NONE
** 
** Returned value:          操作成功返回TRUE, 操作失败返回FALSE.
**                          (操作失败原因: 参数错误或串口没有打开)
**
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
BOOL CCeSerial::epcSerialSendData (BYTE  *pucSendBuf, DWORD  dwLength)
{
	DWORD    dwActLen;
	COMSTAT  comstatTest;
	DWORD    dwErrorFlags;

	/*
	 *  参数判断
	 */ 
    if ( (pucSendBuf == NULL) || (dwLength < 1) ) {
        return (FALSE);
    }

	/*
	 *  判断串口是否已经打开	
	 */ 
	if (hCOM == INVALID_HANDLE_VALUE) {
		//RETAILMSG(1,(TEXT("串口未打开!")));
		return (FALSE);
	}

	/*
	 *  清除串口错误	
	 */ 
	ClearCommError(hCOM, &dwErrorFlags, &comstatTest);

	/*
	 *  发送数据	
	 */ 
	WriteFile(hCOM, pucSendBuf, dwLength, &dwActLen, NULL);	            
	return (TRUE);
}






/*********************************************************************************************************
** Function name:           epcSerialRcvData
**
** Descriptions:            接收串口数据。阻塞方式。
**                          
** input parameters:        dwLength        要接收数据个数 (最大为1024)
**                          dwOutTime       接收超时时间 (要大于10)
**                          bClrComBuf      为TRUE时,在接收前先清除串口接收缓冲区, 为FLASE时不清除
** output parameters:       pucRcvBuf       接收数据缓冲区指针
** 
** Returned value:          读出的数据个数 (参数错误、串口没有打开或没有接收到数据，返回0).
**
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
DWORD CCeSerial::epcSerialRcvData (BYTE  *pucRcvBuf, DWORD  dwLength, DWORD  dwOutTime, BOOL  bClrComBuf)
{
//    COMMTIMEOUTS  ctoTimeOut;
	DWORD         dwLengthRcv;
	COMSTAT       comstatTest;
	DWORD         dwErrorFlags;

	/*
	 *  参数判断
	 */ 
    if ( (pucRcvBuf == NULL) || (dwLength < 1) ) {
        return (0);
    }

    if (dwLength > 1024) {
        dwLength = 1024;
    }

    if (dwOutTime < 1) {
        dwOutTime = 1;
    }

	/*
	 *  判断串口是否已经打开	
	 */ 
	if (hCOM == INVALID_HANDLE_VALUE) {
		//RETAILMSG(1,(TEXT("串口未打开!")));
		return (0);
	}

	/*
	 *  关闭串口接收线程
	 */
	//if (hRcvThread != NULL) {		
	//	SetEvent(hExitThreadEvent);				                        /*  通知串口接收线程退出        */
	//	WaitForSingleObject(hRcvThread, 500);		                    /*  等待线程退出                */
		
    //    CloseHandle(hRcvThread);					                    /*  关闭接收线程句柄            */
	//	CloseHandle(hExitThreadEvent);				                    /*  关闭线程退出事件句柄        */		
	//	hRcvThread = NULL;
	//}

	/*
	 *  设置超时参数	
	 */ 
	//GetCommTimeouts(hCOM, &ctoTimeOut);		
	//ctoTimeOut.ReadIntervalTimeout        = dwOutTime;	                /*  接收字符间最大时间间隔      */
	//ctoTimeOut.ReadTotalTimeoutMultiplier = 1;		
	//ctoTimeOut.ReadTotalTimeoutConstant   = dwOutTime;	                /*  读数据总超时常量            */		
	//SetCommTimeouts(hCOM, &ctoTimeOut);

	/*
	 *  清除串口错误	
	 */ 
	ClearCommError(hCOM, &dwErrorFlags, &comstatTest);

	/*
	 *  清除接收缓冲区	
	 */ 
    if (bClrComBuf) {
        PurgeComm(hCOM, PURGE_RXCLEAR);	
    }

	/*
	 *  接收数据	
	 */ 
	ReadFile(hCOM, pucRcvBuf, dwLength, &dwLengthRcv, NULL);
	return (dwLengthRcv);
}





/*********************************************************************************************************
** Function name:           epcSerialRcvDataTread
**
** Descriptions:            串口接收线程
**                          
** input parameters:        pvParam         线程参数,创建线程时传入
** output parameters:       NONE
** 
** Returned value:          线程退出时返回0, 返回值没特殊含义
**
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
DWORD  WINAPI CCeSerial::epcSerialComRcvTread (LPVOID  pvParam)
{
	DWORD  dwLength;
	BOOL   bReadState;

	BYTE  *pucRcvBuf = new BYTE[1028];

	CCeSerial *pCeSerial = (CCeSerial *)pvParam;

	while (TRUE)  {	
        /* 
         *  等待线程退出事件 
         */
        if (WaitForSingleObject(pCeSerial->hExitThreadEvent, 0) == WAIT_OBJECT_0)  {
            break;	
        }

		if (pCeSerial->hCOM != INVALID_HANDLE_VALUE) {	
            /* 
             *  串口读取数据 
             */
			bReadState = ReadFile(pCeSerial->hCOM, pucRcvBuf, pCeSerial->dwRcvLength, &dwLength, NULL);
			if (bReadState) {					
                /* 
                 *  接收成功调用回调函数 
                 */
                if(dwLength != 0) {
                    pCeSerial->pfunRcvOnComRcv(pCeSerial->pvRcvUserParam, pucRcvBuf, dwLength);		
                }
			}
        }  else  {
            break;                                                      /*  串口句柄出错，退出线程      */
        }

        Sleep(1);
	}		

	delete[] pucRcvBuf;
	return (0);
}




/*********************************************************************************************************
** Function name:           epcSerialRcvDataTread
**
** Descriptions:            接收串口数据。线程方式。
**                          
** input parameters:        dwLength        接收数据个数，最大为1024
**                          dwOutTime       接收超时时间控制 (单位为mS，最小为10)
**                          pfunOnComRcv    接收数据成功回调函数指针
**                          pvUserParam     回调函数需要的用户参数变量
** output parameters:       NONE
** 
** Returned value:          操作成功返回TRUE, 操作失败返回FALSE.
**                          (操作失败原因: 参数错误、串口没有打开或串口接收线程已建立过)
**
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
BOOL CCeSerial::epcSerialRcvDataTread (DWORD        dwLength,     DWORD   dwOutTime,
				                       PFUN_COMRCV  pfunOnComRcv, LPVOID  pvUserParam)

{
	DWORD         dwThreadID;
	COMMTIMEOUTS  ctoTimeOut;
	
	/* 
     *  参数判断
     */
    if ( (dwLength < 1) || (pfunOnComRcv == NULL) ) {
        return (FALSE);
    }

    if (dwLength > 1024) {
        dwLength = 1024;
    }

    if (dwOutTime < 10) {
        dwOutTime = 10;
    }
	
	/* 
     *  判断串口是否已经打开
     */
	if (hCOM == INVALID_HANDLE_VALUE)  {
		//RETAILMSG(1,(TEXT("串口未打开!")));
		return (FALSE);
	}
	
	/* 
     *  判断串口接收线程是否已建立
     */
	if (hRcvThread != NULL) {	
        //RETAILMSG(1,(TEXT("串口接收线程已建立过!")));
		return (FALSE);	
	}

	/* 
     *  设置超时参数
     */
	GetCommTimeouts(hCOM, &ctoTimeOut);		
	ctoTimeOut.ReadIntervalTimeout        = dwOutTime;			        /*  接收字符间最大时间间隔      */
	ctoTimeOut.ReadTotalTimeoutMultiplier = 1;		
	ctoTimeOut.ReadTotalTimeoutConstant   = dwOutTime;	                /*  读数据总超时常量            */	
	SetCommTimeouts(hCOM, &ctoTimeOut);

	/* 
     *  设置线程参数
     */
	dwRcvLength     = dwLength;
	pfunRcvOnComRcv = pfunOnComRcv;
	pvRcvUserParam  = pvUserParam;

	/* 
     *  创建串口接收线程			
     */
	hExitThreadEvent = CreateEvent(NULL, TRUE, FALSE, NULL);            /*  创建串口接收线程退出事件    */			
	hRcvThread = CreateThread(0, 0, epcSerialComRcvTread, this, 0, &dwThreadID);
	if (hRcvThread == NULL)  {
		CloseHandle(hExitThreadEvent);				                    /* 关闭线程退出事件句柄         */
		//RETAILMSG(1,(TEXT("创建接收线程失败!")));
		return (FALSE);
	}	

	return (TRUE);
}




/*********************************************************************************************************
** Function name:           epcSerialRxClear
**
** Descriptions:            清除接收缓冲区。
**                          
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          操作成功返回TRUE, 操作失败返回FALSE.
**                          (操作失败原因: 串口没有打开)
**
** Created by:              黄绍斌
** Created Date:            2007/10/10
**--------------------------------------------------------------------------------------------------------
** Modified by:
** Modified date:
**--------------------------------------------------------------------------------------------------------
*********************************************************************************************************/
BOOL CCeSerial::epcSerialRxClear()
{
    COMSTAT  comstatTest;
    DWORD    dwErrorFlags;
    
    /*
	 *  判断串口是否已经打开	
	 */ 
	if (hCOM == INVALID_HANDLE_VALUE) {
		//RETAILMSG(1,(TEXT("串口未打开!")));
		return (FALSE);
	}
    
    /*
	 *  清除串口错误	
	 */ 
	ClearCommError(hCOM, &dwErrorFlags, &comstatTest);

	/*
	 *  清除接收缓冲区	
	 */     
    PurgeComm(hCOM, PURGE_RXCLEAR);	    

    return (TRUE);    
}



BOOL CCeSerial::epcCloseRcvThread(void)
{
	if (hRcvThread != NULL) {		
		SetEvent(hExitThreadEvent);				                        /*  通知串口接收线程退出        */
		WaitForSingleObject(hRcvThread, 500);		                    /*  等待线程退出                */

		CloseHandle(hRcvThread);					                    /*  关闭接收线程句柄            */
		CloseHandle(hExitThreadEvent);				                    /*  关闭线程退出事件句柄        */		
		hRcvThread = NULL;
		return TRUE;
	}
	else
	{
		return FALSE;
	}
}


/*********************************************************************************************************
  END FILE
*********************************************************************************************************/
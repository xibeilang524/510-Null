#include "stdafx.h"

#define CECOM_GLOBALS

#include "CeCom.h"
#include "CeSerial.h"

CCeSerial CeSerial;



BOOL Com_OpenPort(int m_Port)
{	
	EPort ePort = (EPort)(ECOM1 + m_Port);
	return CeSerial.epcSerialOpen(ePort, EBaud115200);
}

BOOL Com_ClosePort(void)
{
	return CeSerial.epcSerialClose();
}


BOOL Com_SendData(unsigned char *SendBuf, unsigned char SendLen)
{
	return CeSerial.epcSerialSendData(SendBuf, SendLen);
}


/*********************************************************************************************************
** 函数名称:		Com_RcvDataTread
** 功能描述:		接收串口数据。线程方式。
** 输入参数:        ucLength        接收数据个数，最大为64
**                  dwOutTime       接收超时时间控制 (单位为mS，最小为10)
**                  pfunOnComRcv    接收数据成功回调函数指针
**                  pvUserParam     回调函数需要的用户参数变量
** 输出参数:		NONE
** 
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
BOOL Com_RcvDataTread(BYTE ucLength, DWORD dwOutTime, PFUN_COMRCV pfunOnComRcv, LPVOID pvUserParam)
{
	return CeSerial.epcSerialRcvDataTread((DWORD)ucLength, dwOutTime, pfunOnComRcv, pvUserParam);
}


DWORD Com_RcvData(BYTE  *pucRcvBuf, DWORD  dwLength, DWORD  dwOutTime, BOOL  bClrComBuf)
{
	return CeSerial.epcSerialRcvData(pucRcvBuf, dwLength, dwOutTime, bClrComBuf); 
}

BOOL Com_Clear()
{
	return CeSerial.epcSerialRxClear();
}

BOOL CloseRcvThread(void)
{
	return CeSerial.epcCloseRcvThread();
}


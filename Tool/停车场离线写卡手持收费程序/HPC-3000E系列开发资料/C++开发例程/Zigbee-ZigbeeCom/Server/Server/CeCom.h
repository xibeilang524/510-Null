
#ifndef __CECOM_H
#define __CECOM_H

#ifdef __cplusplus
extern "C" {
#endif

#ifdef CECOM_GLOBALS
#define	CECOM_EXT
#else
#define	CECOM_EXT	extern
#endif



/*********************************************************************************************************
声明串口接收回调函数指针 数据类型
*********************************************************************************************************/
typedef void  (CALLBACK *PFUN_COMRCV)(LPVOID pvUserParam, BYTE *pucBuf, DWORD dwRcvLen);

BOOL Com_OpenPort(int m_Port);

BOOL Com_ClosePort(void);

BOOL Com_SendData(unsigned char *SendBuf, unsigned char SendLen);

BOOL Com_RcvDataTread(BYTE ucLength, DWORD dwOutTime, PFUN_COMRCV pfunOnComRcv, LPVOID pvUserParam);

DWORD Com_RcvData(BYTE  *pucRcvBuf, DWORD  dwLength, DWORD  dwOutTime, BOOL  bClrComBuf); 

BOOL Com_Clear();

BOOL CloseRcvThread(void);


#ifdef __cplusplus
}
#endif

#endif //__CECOM_H
#ifndef __ZYZIGBEESDK_323DSJFFO9U9898_h
#define __ZYZIGBEESDK_323DSJFFO9U9898_h


//常量定义
////////////////////////  CEL属性  ///////////////////////
#define ZYZB_CEL_DEV_NAME		1		//名字
#define ZYZB_CEL_DEV_PWD		2		//密码
#define ZYZB_CEL_DEV_FVER		3		//固件版本
#define ZYZB_CEL_DEV_WORKTYPE	4		//工作类型
#define ZYZB_CEL_DEV_CHANNEL	5		//通道号
#define ZYZB_CEL_DEV_PANID		6		//PAN ID
#define ZYZB_CEL_DEV_LOCALNETID	7
#define ZYZB_CEL_DEV_LOCALMAC	8
#define ZYZB_CEL_DEV_DSTNETID	9
#define ZYZB_CEL_DEV_DSTMAC		10
#define ZYZB_CEL_DEV_TRANRATE	11		//传输速率
#define ZYZB_CEL_DEV_POWER		12		//发送功率
#define ZYZB_CEL_DEV_RTY		13		//发送重试次数
#define ZYZB_CEL_DEV_RTYSPACE	14		//发送重试间隔
#define ZYZB_CEL_DEV_BAUD		15		//串口波特率
#define ZYZB_CEL_DEV_DATABIT	16		//数据位
#define ZYZB_CEL_DEV_PARITY		17		//校验位
#define ZYZB_CEL_DEV_STOPBIT	18		//停止位
#define ZYZB_CEL_DEV_SENDMODE	19		//发送模式
#define ZYZB_CEL_DEV_SAMPLERATE	20		//采样率
#define ZYZB_CEL_DEV_COMPRESS_SCHEME	21		//压缩方式


#define ZYZB_CEL_SEARCH_TYPE		1		//类型
#define ZYZB_CEL_SEARCH_CHANNEL		2		//通道
#define ZYZB_CEL_SEARCH_RATE		3		//速率
#define ZYZB_CEL_SEARCH_PANID		4		//PAN ID
#define ZYZB_CEL_SEARCH_ID			5		//ID
#define ZYZB_CEL_SEARCH_WORKMODE	6		//工作方式



//SDK接口
BOOL __stdcall ZYZB_OpenCom(	LPCTSTR	szCom,
								int		iBautRate,
								int		iByteSize,
								int		iParity, 
								int		iStopBits,
								int		iDtrCtl,
								int		iRtsCtl,
								int		iCtsCtl,
								int		iDsrCtl,
								int		iResponse);
BOOL __stdcall ZYZB_CloseCom();
DWORD __stdcall ZYZB_GetLastError();
BOOL __stdcall ZYZB_ComCheckOpen();

//////////////////CEL//////////////////////////////////////
BOOL __stdcall ZYZB_CEL_LoadInfo();
BOOL __stdcall ZYZB_CEL_GetProperty(int iPropertyType, void* pData, int iBufLen);
BOOL __stdcall ZYZB_CEL_SetProperty(int iPropertyType, void* pData, int iBufLen);
BOOL __stdcall ZYZB_CEL_SubmitInfo();
BOOL __stdcall ZYZB_CEL_ResetDev();			//复位设备
BOOL __stdcall ZYZB_CEL_ResetToDefault();	//恢复出厂设置


//搜索设备
void __stdcall ZYZB_CEL_ClearSearchArr();
BOOL __stdcall ZYZB_CEL_SearchDev(int iChannel, int iRate);
int  __stdcall ZYZB_CEL_GetSearchDevCount();
BOOL __stdcall ZYZB_CEL_GetSerarchDevProperty(int iIndex, int iType, void *pOut, int iBufLen);

BOOL __stdcall ZYZB_CEL_LoadRemoteDevInfo(int iIndex);
BOOL __stdcall ZYZB_CEL_GetRemoteDevProperty(int iIndex, int iType, void *pOut, int iBufLen);
BOOL __stdcall ZYZB_CEL_SetRemoteDevProperty(int iIndex, int iType, void *pOut, int iBufLen);
BOOL __stdcall ZYZB_CEL_SubmitRemoteDevInfo(int iIndex);
BOOL __stdcall ZYZB_CEL_ResetRemoteDev(int iIndex);			//复位设备
BOOL __stdcall ZYZB_CEL_ResetRemoteToDefault(int iIndex);	//恢复出厂设置


BOOL __stdcall  ZYZB_CEL_Read (LPVOID szBuffer, const DWORD dwSize,DWORD *plReadedSize,DWORD dwTimeOut=1500);
BOOL __stdcall  ZYZB_CEL_Write(LPVOID szBuffer, const DWORD dwSize,DWORD *plWrittenSize,DWORD dwTimeOut=1500);
#endif//(__ZYZIGBEESDK_323DSJFFO9U9898_h)

/****************************************Copyright (c)****************************************************
**                            Guangzhou ZHIYUAN electronics Co.,LTD.
**                                      
**                                 http://www.embedtools.com
**
**--------------File Info---------------------------------------------------------------------------------
** File name:               WirelessCard.h
** Latest modified Date:    2011-06-14
** Latest Version:          1.0
** Descriptions:            
**
**--------------------------------------------------------------------------------------------------------
** Created by:              LiuFei
** Created date:            2011-06-14
** Version:                 1.0
** Descriptions:            Wifi的WZC例程
**
**--------------------------------------------------------------------------------------------------------
** Modified by:             
** Modified date:           
** Version:                 
** Descriptions:            
**
*********************************************************************************************************/

#if !defined(AFX_WIRELESSCARD_H__8A39692A_7C22_43A7_8303_6460A7F5154C__INCLUDED_)
#define AFX_WIRELESSCARD_H__8A39692A_7C22_43A7_8303_6460A7F5154C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/*
*  包含WZC API和相关结构体的头文件
*/
#include "wzcsapi.h"


class CWirelessCard  
{
private:
	TCHAR         *m_szWirelessCard1;                                   /* 第一块无线网卡的GUID         */
	INTF_ENTRY_EX  m_Intf;                                              /* 保存查询到的网卡信息         */
    DWORD          m_dwOutFlags;                                        /* 保存查询时输出的Flag         */
	DWORD          m_dwSSIDCounts;                                      /* 保存查询时有多少个无线网络   */
public:
/*********************************************************************************************************
** Function name:           CWirelessCard
**
** Descriptions:            构建函数，主要是申请缓存，初始化类属性
**
** input parameters:        None
** output parameters:       None
** Returned value:          None
*********************************************************************************************************/
	CWirelessCard();

/*********************************************************************************************************
** Function name:           ~CWirelessCard
**
** Descriptions:            析构函数，删除申请的内存
**
** input parameters:        None
** output parameters:       None
** Returned value:          None
*********************************************************************************************************/
	virtual ~CWirelessCard();

/*********************************************************************************************************
** Function name:           GetFirstWirelessNetworkCard
**
** Descriptions:            获取第一块无线网卡的GUID(名称) 
**
** input parameters:        dwLen             szWirelessCard1的长度
** output parameters:       szWirelessCard1   无线网卡的名称
** Returned value:          TRUE：成功，FALSE：失败 
*********************************************************************************************************/	
	BOOL GetFirstWirelessNetworkCard(OUT TCHAR *szWirelessCard1 = NULL,
						             IN DWORD dwLen = 0);

/*********************************************************************************************************
** Function name:           Query
**
** Descriptions:            查询网卡，填充m_Intf和m_dwOutFlags，m_dwSSIDCounts
**
** input parameters:        None
** output parameters:       None
** Returned value:          TRUE：成功，FALSE：失败 
*********************************************************************************************************/
	BOOL Query(void);

/*********************************************************************************************************
** Function name:           GetSSIDCounts
**
** Descriptions:            获取扫描到的网络个数
**
** input parameters:        None
** output parameters:       None
** Returned value:          获取扫描到的网络个数
*********************************************************************************************************/
	DWORD GetSSIDCounts(void);

/*********************************************************************************************************
** Function name:           IsAssociated
**
** Descriptions:            查询是否连接到一个网络，输出网络连接名称
**
** input parameters:        dwLen      pszSSID 的长度
** output parameters:       pszSSID    连接成功时为网络连接名称，否则为<NO Associated>
** Returned value:          TRUE：成功，FALSE：失败 
*********************************************************************************************************/
	BOOL IsAssociated(OUT TCHAR *pszSSID = NULL,
		              IN DWORD dwLen = 0);

/*********************************************************************************************************
** Function name:           GetWzcContext
**
** Descriptions:            查询是Wireless zero config 的配置，如扫描间隔等
**
** input parameters:        None
** output parameters:       pContext    WZC配置上下文
** Returned value:          TRUE：成功，FALSE：失败 
*********************************************************************************************************/
	BOOL GetWzcContext(WZC_CONTEXT *pContext);

/*********************************************************************************************************
** Function name:           SetWzcContext
**
** Descriptions:            设置Wireless zero config 的配置，如扫描间隔等
**
** input parameters:        None
** output parameters:       pContext    WZC配置上下文
** Returned value:          TRUE：成功，FALSE：失败 
*********************************************************************************************************/
	BOOL SetWzcContext(WZC_CONTEXT *pContext);

/*********************************************************************************************************
** Function name:           GetWlanConfig
**
** Descriptions:            按照索引，获取WZC_WLAN_CONFIG，之前必须执行DoQuery
**
** input parameters:        None
** output parameters:       None
** Returned value:          TRUE 成功，FALSE 失败
*********************************************************************************************************/
	BOOL GetWlanConfig(DWORD dwIndex,WZC_WLAN_CONFIG *pWlanConfig);

/*********************************************************************************************************
** Function name:           GetWlanSSID
**
** Descriptions:            从WZC_WLAN_CONFIG中提取SSID
**
** input parameters:        pWlanConfig  网络配置结构体指针
**                          dwLen        pszSSID长度，必须大于等于33
** output parameters:       pszSSID      网络名称，大小必须大于等于33(SSID最大为32)
** Returned value:          TRUE 成功，FALSE 失败
*********************************************************************************************************/
	BOOL GetWlanSSID(const IN WZC_WLAN_CONFIG *pWlanConfig,
					 OUT TCHAR *pszSSID,
					 IN DWORD dwLen);  

/*********************************************************************************************************
** Function name:           GetWlanMacAddress
**
** Descriptions:            从WZC_WLAN_CONFIG中提取MAC地址
**
** input parameters:        pWlanConfig  网络配置结构体指针
**                          dwLen        pszMac长度，必须大于等于32
** output parameters:       pszMac       MAC地址(Wide Char)，大小必须大于等于32
** Returned value:          TRUE 成功，FALSE 失败
*********************************************************************************************************/	
	BOOL GetWlanMacAddress(const IN WZC_WLAN_CONFIG *pWlanConfig,
		                   OUT TCHAR *pszMac,
		                   IN DWORD dwLen);

/*********************************************************************************************************
** Function name:           GetWlanRssi
**
** Descriptions:            从WZC_WLAN_CONFIG中提取信号强度
**
** input parameters:        pWlanConfig  网络配置结构体指针
** output parameters:       plRssi       信号强度
** Returned value:          TRUE 成功，FALSE 失败
*********************************************************************************************************/	
	BOOL GetWlanRssi(const IN WZC_WLAN_CONFIG *pWlanConfig,
					 OUT LONG *plRssi);

/*********************************************************************************************************
** Function name:           GetWlanPrivacyMode
**
** Descriptions:            从WZC_WLAN_CONFIG中提取加密方式
**
** input parameters:        pWlanConfig   网络配置结构体指针
**                          dwLen         szPrivacyMode的长度
** output parameters:       szPrivacyMode 加密方式，长度必须大于等于36
** Returned value:          TRUE 成功，FALSE 失败
*********************************************************************************************************/
	BOOL GetWlanPrivacyMode(const IN WZC_WLAN_CONFIG *pWlanConfig,
							OUT TCHAR *szPrivacyMode,
							IN DWORD dwLen);

/*********************************************************************************************************
** Function name:           AddToPreferredNetworkList
**
** Descriptions:            将WZC_WLAN_CONFIG，添加到首选列表
**
** input parameters:        pWlanConfig   网络配置结构体指针
** output parameters:       None
** Returned value:          TRUE 成功，FALSE 失败
*********************************************************************************************************/
	BOOL AddToPreferredNetworkList(IN WZC_WLAN_CONFIG *pWlanConfig);

/*********************************************************************************************************
** Function name:           RemoveAllPreferredNetworkList
**
** Descriptions:            删除所有的首选列表
**
** input parameters:        None
** output parameters:       None
** Returned value:          TRUE 成功，FALSE 失败
*********************************************************************************************************/
	BOOL RemoveAllPreferredNetworkList(void);

/*********************************************************************************************************
** Function name:           InterpretEncryptionKeyValue
**
** Descriptions:            为WZC_WLAN_CONFIG添加一个Key，该函数目前仅支持开放系统WEP的5、13位ASCII密码，
**
** input parameters:        pWlanConfig       网络配置结构体指针
**                          szEncryptionKey   密钥，包含密钥字符串，以L'\0'结束
**                          ulKeyIndex        密钥索引
** output parameters:       None
** Returned value:          TRUE 成功，FALSE 失败
*********************************************************************************************************/
	BOOL InterpretEncryptionKeyValue(IN OUT WZC_WLAN_CONFIG *pWlanConfig,
									 IN TCHAR *szEncryptionKey,
									 IN ULONG ulKeyIndex,
									 IN bool bNeed8021X);
private:
/*********************************************************************************************************
** Function name:           EncryptWepKMaterial
**
** Descriptions:            密钥转换1
**
** input parameters:        pWlanConfig       网络配置结构体指针
** output parameters:       None
** Returned value:          TRUE 成功，FALSE 失败
*********************************************************************************************************/
	static void EncryptWepKMaterial(IN OUT WZC_WLAN_CONFIG* pwzcConfig);
public:
	
};

#endif // !defined(AFX_WIRELESSCARD_H__8A39692A_7C22_43A7_8303_6460A7F5154C__INCLUDED_)

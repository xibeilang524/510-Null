// BlueToothS10.h : BlueToothS10 DLL 的主头文件
//

#pragma once


#if defined(__cplusplus)
extern "C"
{
#endif

#ifdef BlueToothS10_API
#define BlueToothS10_API __declspec(dllimport)
#else
#define BlueToothS10_API __declspec(dllexport)
#endif

/*********************************************************************************************************
**	函数返回值定义
*********************************************************************************************************/
#define SUCCESS_SETTING				0
#define SYSTEMNOTSUPPORT			1
#define COMNOTOPEN					5
#define COMISUSING					6
#define COMOPENERROR				7
#define OPENDRIVEFAILED				2
#define OPERATIONMODENOTSUPPORT		3
#define ROLENOTSUPPORT				8
#define ENDFAILED					10
#define RECEIVETIMEOUT				11
#define RECEIVEEXCEPTION			12
#define ERROR_SETTING				13
#define FAILURE_SETTING				14
#define UNKNOWN_SETTING				15
#define PARAMINVALID				17
#define SETINQUIRECONFFAILED		18
#define DISC_LINK_LOSS				19
#define DISC_NO_SLC					20
#define DISC_TIMEOUT				21
#define DISC_ERROR					22
#define BUFFERTOOSMALL				23
#define SETTOATMODEFAILED			24
#define COMPARAMERROR				25
#define MODEERROR					26
#define SENDDATAERROR				27

//操作模式
enum S10_OperationMode		
{
	ATCommandMode,			//AT命令模式
	CommunicationMode		//通信模式
};

//蓝牙模块角色
enum S10_Role
{
	Slave,
	Master,
	Loopback
};

//串口
enum S10_Coms
{
	COM1 = 1,
	COM2,
	COM3,
	COM4,
	COM5,	
	COM6,
	COM7,
	COM8
};

//搜索蓝牙设备结构体
struct BlueToothDevice
{
	unsigned char addr[6];		//地址
	unsigned long deviceClass;	//设备类
	short rssi;					//RSSI信号强度
};


/*********************************************************************************************************
** Function name:           S10_GetDLLVersion
** Descriptions:            获取动态链接库版本号                   
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          返回动态链接库版本号（返回100，表示V1.00；返回110，表示V1.10……
*********************************************************************************************************/
BlueToothS10_API int S10_GetDLLVersion();


/*********************************************************************************************************
** Function name:           S10_SetOperationMode
** Descriptions:            设置串口操作模式                         
** input parameters:        mode       操作模式
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			成功
**							OPENDRIVEFAILED			打开蓝牙驱动失败
**							PARAMINVALID			操作模式不支持
**							FAILURE_SETTING			设置操作模式失败
*********************************************************************************************************/
BlueToothS10_API int S10_SetOperationMode(S10_OperationMode mode);



/*********************************************************************************************************
** Function name:           S10_Init
** Descriptions:            初始化                         
** input parameters:        com       串口号
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			初始化成功
**							COMPARAMERROR			串口参数错误
**							SETTOATMODEFAILED		设置AT命令模式失败
**							COMISUSING				串口已被占用
**							COMOPENERROR			串口打开错误
**							SYSTEMNOTSUPPORT		系统不安全
*********************************************************************************************************/
BlueToothS10_API int S10_Init(S10_Coms com);




/*********************************************************************************************************
** Function name:           S10_Close
** Descriptions:            关闭蓝牙通信                         
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			关闭成功
**							FAILURE_SETTING			关闭失败
*********************************************************************************************************/
BlueToothS10_API int S10_Close();


/*********************************************************************************************************
** Function name:           S10_Reboot
** Descriptions:            重启蓝牙模块                         
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_Reboot();


/*********************************************************************************************************
** Function name:           S10_GetModuleVersion
** Descriptions:            获取蓝牙模块软件版本号                   
** input parameters:        buf				版本字符串输入缓冲区指针              
**							bufLength		输入缓冲区长度
** output parameters:       bufLength		版本字符串长度(不包括空字符）
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							BUFFERTOOSMALL			缓冲区长度太小
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetModuleVersion(char buf[],unsigned int &bufLength);


/*********************************************************************************************************
** Function name:           S10_RestoreFactorySettings
** Descriptions				蓝牙模块恢复出厂默认设置             
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_RestoreFactorySettings();


/*********************************************************************************************************
** Function name:           S10_GetAddress
** Descriptions:            获取本地蓝牙地址                   
** input parameters:        addr			蓝牙地址输入缓冲区指针(6字节) 
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetAddress(unsigned char addr[]);



/*********************************************************************************************************
** Function name:           S10_SetDeviceName
** Descriptions				设置蓝牙设备名称              
** input parameters:        buf			蓝牙设备名称字符串指针
**						    bufLength	字符串长度(不包括空字符）
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_SetDeviceName(char buf[],unsigned int bufLength);



/*********************************************************************************************************
** Function name:           S10_GetDeviceName
** Descriptions:            获取蓝牙设备名称                   
** input parameters:        buf				设备名称输入缓冲区指针              
**							bufLength		输入缓冲区长度
** output parameters:       bufLength		设备名称字符串长度(不包括空字符）
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							BUFFERTOOSMALL			缓冲区长度太小
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetDeviceName(char buf[],unsigned int &bufLength);



/*********************************************************************************************************
** Function name:           S10_GetRemoteDeviceName
** Descriptions:            获取远程蓝牙设备名称                   
** input parameters:        addr			远程蓝牙设备地址(6字节) 
**							buf				远程设备名称输入缓冲区指针              
**							bufLength		输入缓冲区长度
** output parameters:       bufLength		远程设备名称字符串长度(不包括空字符）
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							BUFFERTOOSMALL			缓冲区长度太小
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetRemoteDeviceName(unsigned char addr[],char buf[],unsigned int &bufLength);



/*********************************************************************************************************
** Function name:           S10_SetRole
** Descriptions:            设置蓝牙模块角色                         
** input parameters:        role       蓝牙模块角色
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							PARAMINVALID			模块角色不支持
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_SetRole(S10_Role role);



/*********************************************************************************************************
** Function name:           S10_GetRole
** Descriptions:            获取蓝牙模块角色                   
** input parameters:        NONE
** output parameters:       role			蓝牙模块角色
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetRole(S10_Role &role);



/*********************************************************************************************************
** Function name:           S10_SetDeviceClass
** Descriptions:            设置蓝牙设备类                         
** input parameters:        deviceClass       蓝牙设备类
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_SetDeviceClass(unsigned int deviceClass);



/*********************************************************************************************************
** Function name:           S10_GetDeviceClass
** Descriptions:            获取蓝牙设备类                         
** input parameters:        NONE
** output parameters:       deviceClass       蓝牙设备类
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetDeviceClass(unsigned int &deviceClass);



/*********************************************************************************************************
** Function name:           S10_SetAccessCode
** Descriptions:            设置查询访问码                         
** input parameters:        accessCode       查询访问码
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_SetAccessCode(unsigned int accessCode);



/*********************************************************************************************************
** Function name:           S10_GetAccessCode
** Descriptions:            获取查询访问码                         
** input parameters:        NONE
** output parameters:       accessCode       查询访问码
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetAccessCode(unsigned int &accessCode);



/*********************************************************************************************************
** Function name:           S10_SetMatchingCode
** Descriptions:            设置配对码                         
** input parameters:        buf				配对码字符串指针                 
**							bufLength       配对码长度(不包括空字符）
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			参数错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_SetMatchingCode(char buf[],unsigned int bufLength);



/*********************************************************************************************************
** Function name:           S10_GetMatchingCode
** Descriptions:            获取配对码                   
** input parameters:        buf				配对码输入缓冲区指针              
**							bufLength		输入缓冲区长度
** output parameters:       bufLength		配对码字符串长度(不包括空字符）
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							BUFFERTOOSMALL			缓冲区长度太小
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetMatchingCode(char buf[],unsigned int &bufLength);


/*********************************************************************************************************
** Function name:           S10_SetConnectMode
** Descriptions:            设置连接模式                         
** input parameters:        bBinding		是否使用指定蓝牙连接模式（通过设置绑定地址）    
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_SetConnectMode(bool bBinding);




/*********************************************************************************************************
** Function name:           S10_GetConnectMode
** Descriptions:            获取连接模式                         
** input parameters:        NONE    
** output parameters:       bBinding		是否使用指定蓝牙连接模式（通过设置绑定地址）
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetConnectMode(bool &bBinding);



/*********************************************************************************************************
** Function name:           S10_SetBindingAddress
** Descriptions:            设置蓝牙模块绑定地址                         
** input parameters:        addr       目标蓝牙模块地址(6字节)
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_SetBindingAddress(unsigned char addr[]);


/*********************************************************************************************************
** Function name:           S10_GetBindingAddress
** Descriptions:            获取蓝牙模块绑定地址                         
** input parameters:        addr       绑定蓝牙模块地址缓冲区(6字节) 
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetBindingAddress(unsigned char addr[]);


/*********************************************************************************************************
** Function name:           S10_SetSafeParameters
** Descriptions:            设置安全加密模式                         
** input parameters:        safeLevel       安全等级（0-3）
**							encryptLevel    加密等级（0-2）
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							PARAMINVALID			等级参数不正确
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_SetSafeParameters(unsigned int safeLevel,unsigned int encryptLevel);



/*********************************************************************************************************
** Function name:           S10_GetSafeParameters
** Descriptions:            获取安全加密模式                         
** input parameters:        NONE
** output parameters:       safeLevel       安全等级（0-3）
**							encryptLevel    加密等级（0-2）
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetSafeParameters(unsigned int &safeLevel,unsigned int &encryptLevel);



/*********************************************************************************************************
** Function name:           S10_InquireDevice
** Descriptions:            查询蓝牙设备                         
** input parameters:        rssi       是否显示信号强度       
**					        maxcnt     最多蓝牙设备响应数   
**					        timeout    查询设备超时时间(秒）
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							SETINQUIRECONFFAILED	设置查询参数失败
**							PARAMINVALID			超时时间参数不正确（必须在1-60)
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_InquireDevice(bool rssi,unsigned int maxcnt,unsigned int timeout);



/*********************************************************************************************************
** Function name:           S10_GetInquiredDevice
** Descriptions:            获取查询到的蓝牙设备                         
** input parameters:		NONE
** output parameters:       btd			蓝牙设备信息结构体
** 
** Returned value:          true					获取成功
**							false					获取失败
*********************************************************************************************************/
BlueToothS10_API bool S10_GetInquiredDevice(BlueToothDevice &btd);



/*********************************************************************************************************
** Function name:           S10_InquireCancel
** Descriptions:            取消蓝牙设备查询                         
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			取消成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			取消失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_InquireCancel();



/*********************************************************************************************************
** Function name:           S10_MatchDevice
** Descriptions:            蓝牙设备配对                         
** input parameters:		addr		目标蓝牙模块地址(6字节)     
**							timeout		配对超时时间(秒）
** output parameters:       NONE		
** 
** Returned value:          SUCCESS_SETTING			配对成功
**							COMNOTOPEN				串口未打开（未初始化）
**							PARAMINVALID			超时时间参数不正确（配对超时时间必须在1-20)
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			配对失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_MatchDevice(unsigned char addr[],unsigned int timeout);



/*********************************************************************************************************
** Function name:           S10_ConnectDevice
** Descriptions:            蓝牙设备连接                         
** input parameters:		addr		目标蓝牙模块地址(6字节)  
** output parameters:       NONE		
** 
** Returned value:          SUCCESS_SETTING			连接成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			连接失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_ConnectDevice(unsigned char addr[]);




/*********************************************************************************************************
** Function name:           S10_Disconnect
** Descriptions:            断开蓝牙设备连接                         
** input parameters:		NONE  
** output parameters:       NONE		
** 
** Returned value:          SUCCESS_SETTING			断开成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							DISC_LINK_LOSS			连接丢失
**							DISC_NO_SLC				没有连接
**							DISC_TIMEOUT			断开超时
**							DISC_ERROR				断开错误
**							FAILURE_SETTING			断开失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_Disconnect();



/*********************************************************************************************************
** Function name:           S10_AutoSleep
** Descriptions:            设置休眠使能                         
** input parameters:		enable		是否使能自动休眠  
** output parameters:       NONE		
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_AutoSleep(bool enable);


/*********************************************************************************************************
** Function name:           S10_SetFilterMode
** Descriptions:            设置蓝牙设备过滤准则                         
** input parameters:        filterDeviceClass       是否使用设备类过滤   
**					        filterAccessCode		是否使用访问码过滤
** output parameters:       NONE
** 
** Returned value:          SUCCESS_SETTING			设置成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			设置失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_SetFilterMode(bool filterDeviceClass,bool filterAccessCode);



/*********************************************************************************************************
** Function name:           S10_GetFilterMode
** Descriptions:            获取蓝牙设备过滤准则                         
** input parameters:        NONE
** output parameters:       filterDeviceClass       是否使用设备类过滤   
**					        filterAccessCode		是否使用访问码过滤
** 
** Returned value:          SUCCESS_SETTING			获取成功
**							COMNOTOPEN				串口未打开（未初始化）
**							RECEIVETIMEOUT			接收数据超时
**							FAILURE_SETTING			获取失败
**							ERROR_SETTING			遇到错误
**							MODEERROR				模式错误（不处于AT模式）
*********************************************************************************************************/
BlueToothS10_API int S10_GetFilterMode(bool &filterDeviceClass,bool &filterAccessCode);



/*********************************************************************************************************
** Function name:           S10_SendData
** Descriptions:            在通信模式下发送数据                         
** input parameters:        buf				发送缓冲区指针
**					        bufLength		发送数据长度
** output parameters:       bufLength		实际发送字节数
** 
** Returned value:          SUCCESS_SETTING			发送成功
**							COMNOTOPEN				串口未打开（未初始化）
**							SENDDATAERROR			发送数据错误
**							MODEERROR				模式错误（不处于通信模式）
*********************************************************************************************************/
BlueToothS10_API int S10_SendData(unsigned char buf[],unsigned int &bufLength);



/*********************************************************************************************************
** Function name:           S10_RecvData
** Descriptions:            在通信模式下接收数据                         
** input parameters:        buf				接收缓冲区指针
**					        bufLength		接收缓冲区长度
** output parameters:       bufLength		实际接收字节数
** 
** Returned value:          SUCCESS_SETTING			接收成功
**							COMNOTOPEN				串口未打开（未初始化）
**							MODEERROR				模式错误（不处于通信模式）
*********************************************************************************************************/
BlueToothS10_API int S10_RecvData(unsigned char buf[],unsigned int &bufLength);



/*********************************************************************************************************
** Function name:           S10_GetLastError
** Descriptions:            获取上次错误的代号                         
** input parameters:        NONE
** output parameters:       NONE
** 
** Returned value:          -1		没有错误
**							其他		错误代号
*********************************************************************************************************/
BlueToothS10_API int S10_GetLastError();


#if defined(__cplusplus)
}
#endif
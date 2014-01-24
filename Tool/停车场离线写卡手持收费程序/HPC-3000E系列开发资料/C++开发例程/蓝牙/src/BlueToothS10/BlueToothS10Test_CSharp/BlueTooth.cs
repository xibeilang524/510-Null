using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ZLG
{
    enum S10_RetCode
    {
        SUCCESS_SETTING	=			    0,
        SYSTEMNOTSUPPORT	=			1,
        COMNOTOPEN	=					5,
        COMISUSING	=					6,
        COMOPENERROR	=				7,
        OPENDRIVEFAILED	=				2,
        OPERATIONMODENOTSUPPORT	=		3,
        ROLENOTSUPPORT	=				8,
        ENDFAILED	=					10,
        RECEIVETIMEOUT	=				11,
        RECEIVEEXCEPTION	=			12,
        ERROR_SETTING	=				13,
        FAILURE_SETTING	=				14,
        UNKNOWN_SETTING	=				15,
        PARAMINVALID	=				17,
        SETINQUIRECONFFAILED	=		18,
        DISC_LINK_LOSS	=				19,
        DISC_NO_SLC	=					20,
        DISC_TIMEOUT	=				21,
        DISC_ERROR	=					22,
        BUFFERTOOSMALL	=				23,
        SETTOATMODEFAILED	=			24,
        COMPARAMERROR	=				25,
        MODEERROR	=					26,
        SENDDATAERROR	=				27
    }

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
    struct BlueToothDevice
    {
	    byte[] addr;		//地址
	    ulong deviceClass;	//设备类
	    short rssi;					//RSSI信号强度
    };
    class BlueTooth
    {
        /*********************************************************************************************************
        ** Function name:           S10_GetDLLVersion
        ** Descriptions:            获取动态链接库版本号                   
        ** input parameters:        NONE
        ** output parameters:       NONE
        ** 
        ** Returned value:          返回动态链接库版本号（返回100，表示V1.00；返回110，表示V1.10……
        *********************************************************************************************************/
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode GetGPRSVersion();

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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SetOperationMode(S10_OperationMode mode);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_Init(S10_Coms com);




        /*********************************************************************************************************
        ** Function name:           S10_Close
        ** Descriptions:            关闭蓝牙通信                         
        ** input parameters:        NONE
        ** output parameters:       NONE
        ** 
        ** Returned value:          SUCCESS_SETTING			关闭成功
        **							FAILURE_SETTING			关闭失败
        *********************************************************************************************************/
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_Close();


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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_Reboot();


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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetModuleVersion(byte[] buf,ref uint bufLength);


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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_RestoreFactorySettings();


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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetAddress(byte []addr);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SetDeviceName(byte[] buf,uint bufLength);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetDeviceName(byte[] buf,ref uint bufLength);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetRemoteDeviceName(byte[] addr,byte[] buf,ref uint bufLength);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SetRole(S10_Role role);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetRole(ref S10_Role role);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SetDeviceClass(uint deviceClass);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetDeviceClass(ref uint deviceClass);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SetAccessCode(uint accessCode);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetAccessCode(ref uint accessCode);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SetMatchingCode(byte[] buf,uint bufLength);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetMatchingCode(byte[] buf,ref uint bufLength);


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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SetConnectMode(bool bBinding);




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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetConnectMode(ref bool bBinding);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SetBindingAddress(byte[] addr);


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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetBindingAddress(byte[] addr);


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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SetSafeParameters(uint safeLevel,uint encryptLevel);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetSafeParameters(ref uint safeLevel,ref uint encryptLevel);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_InquireDevice(bool rssi,uint maxcnt,uint timeout);



        /*********************************************************************************************************
        ** Function name:           S10_GetInquiredDevice
        ** Descriptions:            获取查询到的蓝牙设备                         
        ** input parameters:		NONE
        ** output parameters:       btd			蓝牙设备信息结构体
        ** 
        ** Returned value:          true					获取成功
        **							false					获取失败
        *********************************************************************************************************/
        [DllImport("BlueToothS10.dll")]
        public static extern bool S10_GetInquiredDevice(ref BlueToothDevice btd);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_InquireCancel();



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_MatchDevice(byte[] addr,uint timeout);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_ConnectDevice(byte[] addr);




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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_Disconnect();



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_AutoSleep(bool enable);


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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SetFilterMode(bool filterDeviceClass,bool filterAccessCode);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetFilterMode(ref bool filterDeviceClass,ref bool filterAccessCode);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_SendData(byte[] buf,ref uint bufLength);



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
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_RecvData(byte[] buf,ref uint bufLength);



        /*********************************************************************************************************
        ** Function name:           S10_GetLastError
        ** Descriptions:            获取上次错误的代号                         
        ** input parameters:        NONE
        ** output parameters:       NONE
        ** 
        ** Returned value:          -1		没有错误
        **							其他		错误代号
        *********************************************************************************************************/
        [DllImport("BlueToothS10.dll")]
        public static extern S10_RetCode S10_GetLastError();
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EM1300Space
{
    enum Ret_Value
    {
        COM_ERR_PARA = 41,
        COM_ERR_TIME,
        COM_ERR_USING,
        COM_ERR_OPEN,
        COM_NOTOPEN,
        SUCCESS_SETTING = 47,
        SEND_OK,
        CREATETHREADFAIL,
        INIT_OK,
        DATA_ISNULL,
        RECEIVE_SUCCESS,
        DATA_ERR_SELECT,
        ACK_SUCCESS,
        ACK_FAILED,
        MODE_FAILED,
        IS_WRITING,
        UNDEFINE,
        SYSTEMNOSAFE,
        TAIL_FAILED,
        SCANMODENOTSUPPORT,
        CODEBARNOTSUPPORT,
        ACK_TIMEOUT
    }

    enum Symbol_Type:byte
    {
        CODE128,
        EAN_128,
        AIM_128,
        EAN_8 = 4,
        EAN_13,
        ISSN,
        ISBN,
        UPC_E,
        UPC_A,
        INTERLEAVED2OF5,
        ITF_6,
        ITF_14,
        DEUTSCHE14,
        DEUTSCHE12,
        COOP25,
        MATRIX2OF5,
        INDUSTRIAL25,
        STANDARD25,
        CODE39 = 20,
        CODABAR,
        CODE93,
        CODE11,
        PLESSEY,
        MSI_PLESSEY,
        GS1DATABAR,
    }

    enum Seriao_Port:byte
    {
        COM1,
        COM2,
        COM3,
        COM4,
        COM5,
        COM6,
        COM7,
        COM8,
        COM9
    }

    enum Trigger_State:byte
    {
        TS_INTERVAL = 12,
        TS_PERCEIVE,
        TS_CONTINUE,
        TS_LAZYPERCEIVE,
        TS_SINGLE
    }
    class EM1300DLL
    {
        /*********************************************************************************************************
        ** Function name:           EM1300SerialInit
        ** Descriptions:            串口接收数据初始化                         
        ** input parameters:        ucPort         串口号,如COM1,COM2
        ** output parameters:       NONE
        ** 
        ** Returned value:          正确返回INIT_OK, 串口设置参数错误返回COM_ERR_PARA, 超时设置错误
        **                          返回COM_ERR_TIME, 串口已打开返回COM_ERR_USING, 
        **							串口打开错误COM_ERR_OPEN,串口接收线程创建失败返回CREATETHREADFAIL.
        *********************************************************************************************************/
        [DllImport("EM1300DLL.dll")]
        public static extern Ret_Value EM1300SerialInit(Seriao_Port ucPort);

        /*********************************************************************************************************
        ** Function name:           EM1300SerialTerminate
        ** Descriptions:            EM1300通信终止                         
        ** input parameters:        NONE													
        ** output parameters:       NONE
        ** Returned value:          操作成功返回TRUE, 操作失败返回FALSE.
        *********************************************************************************************************/
        [DllImport("EM1300DLL.dll")]
        public static extern bool EM1300SerialTerminate();

        /*********************************************************************************************************
        ** Function name:           EM1300DecodeState
        ** Descriptions:            开始扫描译码、停止扫描译码                         
        ** input parameters:        bDecodeState       扫描译码状态(值为true时，开始扫描译码
        **                                                          值为false时，停止扫描译码)
        ** output parameters:       NONE
        ** 
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,
        **							发送成功收到失败应答ACK_FAILED
        **                          或者未收到应答返回ACK_TIMEOUT
        *********************************************************************************************************/
        [DllImport("EM1300DLL.dll")]
        public static extern Ret_Value EM1300DecodeState(bool bDecodeState);

        /*********************************************************************************************************
        ** Function name:           EM1300TriggerState
        ** Descriptions:            触发方式设置                         
        ** input parameters:        ucTriggerState          触发方式(值为SCAN_INTERVAL时，间隔扫描模式
        **                                                           值为SCAN_PERCEIVE时，感应扫描模式
        **                                                           值为SCAN_CONTINUE时， 持续扫描模式
        **                                                           值为SCAN_LAZYPERCEIVE时， 延迟感应扫描模式
        **                                                           值为SCAN_SINGLE时，单次扫描模式)                                                      
        **														
        ** output parameters:       NONE
        ** 
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口参数错误返回COM_ERR_SELECT,
        **							发送成功收到失败应答ACK_FAILED
        **                          或者未收到应答返回ACK_TIMEOUT
        **							或者触发扫描模式不支持返回SCANMODENOTSUPPORT
        *********************************************************************************************************/
        [DllImport("EM1300DLL.dll")]
        public static extern Ret_Value EM1300TriggerState(Trigger_State TriggerState);

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
        [DllImport("EM1300DLL.dll")]
        public static extern Ret_Value EM1300GetDecodeData(byte[] pucRecieveBuf, byte[] pucCount);

        /*********************************************************************************************************
        ** Function name:           EM1300GetVersion
        **
        ** Descriptions:            获取EM1300软件包版本号
        **                          
        ** input parameters:        NONE
        ** output parameters:       NONE
        ** 
        ** Returned value:          返回当前软件包版本号.
        **                          (返回100，表示V1.00；返回110，表示V1.10......)
        *********************************************************************************************************/
        [DllImport("EM1300DLL.dll")]
        public static extern uint EM1300GetVersion();

        /*********************************************************************************************************
        ** Function name:           EM1300CodeBarEnable
        ** Descriptions:            设置条形码使能、禁能                         
        ** input parameters:        ucCodeBar           条形码参数号
        **                          ucEnable            使能与禁能(ENABLE表示使能，DISABLE表示禁能)
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,
        **							发送成功收到失败应答ACK_FAILED
        **                          或者未收到应答返回ACK_TIMEOUT
        **                          或者条形码码制不支持返回CODEBARNOTSUPPORT
        *********************************************************************************************************/
        [DllImport("EM1300DLL.dll")]
        public static extern Ret_Value EM1300CodeBarEnable(byte ucCodeBar, byte ucEnable);

        /*********************************************************************************************************
        ** Function name:           EM1300CodeBarRestoreSetting
        ** Descriptions:            重置条码码制设置 
        **                           
        ** input parameters:        ucCodeBar           条形码参数号
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,
        **							发送成功收到失败应答ACK_FAILED
        **                          或者未收到应答返回ACK_TIMEOUT
        **                          或者条形码码制不支持返回CODEBARNOTSUPPORT
        *********************************************************************************************************/
        [DllImport("EM1300DLL.dll")]
        public static extern Ret_Value EM1300CodeBarRestoreSetting(byte ucCodeBar);

        /*********************************************************************************************************
        ** Function name:           EM1300FactorySettings
        ** Descriptions:            EM1300恢复出厂设置                         
        ** input parameters:        NONE
        ** output parameters:       NONE
        ** 
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,
        **							发送成功收到失败应答ACK_FAILED
        **                          或者未收到应答返回ACK_TIMEOUT
        *********************************************************************************************************/
        [DllImport("EM1300DLL.dll")]
        public static extern Ret_Value EM1300FactorySettings();

        /*********************************************************************************************************
        ** Function name:           EM1300SpecialCommand
        ** Descriptions:            特殊接口,用于迫切需要,但是暂未提供的功能实现                         
        ** input parameters:        cmd					命令字符串
        **							length				命令长度
        ** output parameters:       NONE
        ** 
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,
        **							发送成功收到失败应答ACK_FAILED
        **                          或者未收到应答返回ACK_TIMEOUT
        *********************************************************************************************************/
        [DllImport("EM1300DLL.dll")]
        public static extern Ret_Value EM1300SpecialCommand(byte[] cmd, int length);

//         [DllImport("coredll.dll", EntryPoint = "WaitForSingleObject")]
//         public static extern int WaitForSingleObject(int hHandle, int dwMilliseconds);
// 
//         [DllImport("coredll.dll", EntryPoint = "CreateEvent")]
//         public static extern int CreateEvent(int lpEventAttributes, int bManualReset, int bInitialState, int lpName);
// 
//         [DllImport("coredll.dll", EntryPoint = "SetEvent")]
//         public static extern int SetEvent(int hHandle);
// 
//         [DllImport("coredll.dll", EntryPoint = "EventModify")]
//         public static extern bool EventModify(int h, int i);
// 
//         [DllImport("coredll.dll", EntryPoint = "WaitForMultipleObjects")]
//         public static extern int WaitForMultipleObjects(uint nCount, int[] lpHandles, int bWaitAll, int dwMilliseconds);
// 
//         [DllImport("coredll.dll", EntryPoint = "CloseHandle")]
//         public static extern int CloseHandle(int hObject); 
    }
}

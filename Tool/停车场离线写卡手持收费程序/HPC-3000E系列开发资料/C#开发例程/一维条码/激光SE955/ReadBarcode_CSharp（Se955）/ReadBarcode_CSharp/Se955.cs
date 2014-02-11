using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ReadBarcode_CSharp
{
    class Se955
    {

        /*********************************************************************************************************
        ** Function name:           Se955SerialInit
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
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern int Se955SerialInit(byte ucPort, byte ucBaud, byte ucStop, byte ucParity);






        /*********************************************************************************************************
        ** Function name:           Se955SerialTerminate
        ** Descriptions:            Se955通信终止                         
        ** input parameters:        NONE													
        ** output parameters:       NONE
        ** Returned value:          操作成功返回TRUE, 操作失败返回FALSE.
        *********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern bool Se955SerialTerminate();







        /*********************************************************************************************************
        ** Function name:           Se955AimState
        ** Descriptions:            设置Se955瞄准、取消瞄准                         
        ** input parameters:        bAimState         瞄准状态(值为TRUE时，瞄准
        **                                                     值为FALSE时，取消瞄准)
        ** output parameters:       NONE
        ** 
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
        **                          或者未收到应答返回ACK_FAILED	
        *********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern int Se955AimState(bool bAimState);







        /*********************************************************************************************************
        ** Function name:           Se955DecodeState
        ** Descriptions:            开始扫描译码、停止扫描译码                         
        ** input parameters:        bDecodeState       扫描译码状态(值为TRUE时，开始扫描译码
        **                                                          值为FALSE时，停止扫描译码)
        ** output parameters:       NONE
        ** 
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
        **                          或者未收到应答返回ACK_FAILED	
        *********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern int Se955DecodeState(bool bDecodeState);








        /*********************************************************************************************************
        ** Function name:           Se955ScanningState
        ** Descriptions:            使能扫描、禁能扫描                         
        ** input parameters:        bScanningState        使能状态(值为TRUE时，使能扫描
        **                                                         值为FALSE时，禁能扫描)
        ** output parameters:       NONE
        ** 
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
        **                          或者未收到应答返回ACK_FAILED	
        *********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern int Se955ScanningState(bool bScanningState);







        /*********************************************************************************************************
        ** Function name:           Se955LedState
        ** Descriptions:            设置译码LED灯亮、灭                         
        ** input parameters:        bLedState         LED状态(值为TRUE时，LED灯亮
        **                                                    值为FALSE时，LED灯灭)
        ** output parameters:       NONE
        ** 
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
        **                          或者未收到应答返回ACK_FAILED	
        *********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern int Se955LedState(bool bLedState);








        /*********************************************************************************************************
        ** Function name:           Se955TriggerState
        ** Descriptions:            触发方式设置                         
        ** input parameters:        ucTriggerState          触发方式(值为SCAN_LEVEL时，硬件电平触发扫描
        **                                                           值为SCAN_PULSE时，硬件脉冲触发扫描
        **                                                           值为SCAN_HOST时， 软件触发扫描
        **                                                           值为SCAN_CONTINUE时，持续触发扫描)                                                      
        **														
        ** output parameters:       NONE
        ** 
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
        **                          或者未收到应答返回ACK_FAILED	
        *********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern int Se955TriggerState(byte ucTriggerState);








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
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern int Se955GetDecodeData(byte[] pucRecieveBuf, byte[] pucCount);








        /*********************************************************************************************************
        ** Function name:           Se955GetVersion
        **
        ** Descriptions:            获取Se955软件包版本号
        **                          
        ** input parameters:        NONE
        ** output parameters:       NONE
        ** 
        ** Returned value:          返回当前软件包版本号.
        **                          (返回100，表示V1.00；返回110，表示V1.10......)
        *********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern uint Se955GetVersion();







        /*********************************************************************************************************
        ** Function name:           Se955CodeBarEnable
        ** Descriptions:            设置条形码使能、禁能                         
        ** input parameters:        ucCodeBar           条形码参数号
        **                          ucEnable            使能与禁能(ENABLE表示使能，DISABLE表示禁能)
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
        **                          或者未收到应答返回ACK_FAILED	
        *********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern int Se955CodeBarEnable(byte ucCodeBar, byte ucEnable);




        /*********************************************************************************************************
        ** Function name:           Se955FactorySettings
        ** Descriptions:            Se955恢复出厂设置                         
        ** input parameters:        NONE
        ** output parameters:       NONE
        ** 
        ** Returned value:          成功设置返回SUCCESS_SETTING, 串口未打开返回COM_NOTOPEN,
        **							串口号参数错误返回COM_ERR_SELECT,发送成功收到失败应答
        **                          或者未收到应答返回ACK_FAILED	
        *********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("Se955DLL.dll")]
        public static extern int Se955FactorySettings();
    }
}

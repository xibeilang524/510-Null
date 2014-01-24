using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Ralid.Parking.POS.Device
{
    internal class HPC_RFID_DLL
    {
        /************************************************************************************************************
	    ** 函数原型:    int RfidGetDllVersion()
	    ** 函数功能:    获取动态库版本号
	    ** 入口参数:    -
	    ** 出口参数:    -
	    ** 返 回 值:    动态库版本号，100表示V1.00
	    ** 描　  述:    
	    ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern int RfidGetDllVersion();


        /************************************************************************************************************
        ** 函数原型:    void RfidModulePowerOn()
        ** 函数功能:    给RFID模块上电
        ** 入口参数:    -
        ** 出口参数:    -
        ** 返 回 值:    -
        ** 描　  述:    
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern void RfidModulePowerOn();


        /************************************************************************************************************
        ** 函数原型:    void RfidModulePowerOff()
        ** 函数功能:    关闭RFID模块电源
        ** 入口参数:    -
        ** 出口参数:    -
        ** 返 回 值:    -
        ** 描　  述:    
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern void RfidModulePowerOff();


        /************************************************************************************************************
        ** 函数原型:    byte RfidModuleOpenPort(byte ucPort)
        ** 函数功能:    打开与RFID模块通信的端口
        ** 入口参数:    ucPort ：端口号
        ** 出口参数:    -
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte RfidModuleOpenPort(byte ucPort);


        /************************************************************************************************************
        ** 函数原型:    byte RfidModuleClosePort()
        ** 函数功能:    关闭与RFID模块通信的端口
        ** 入口参数:    ucPort ：端口号
        ** 出口参数:    -
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte RfidModuleClosePort();


        /************************************************************************************************************
        ** 函数原型:    byte ISO14443A_ReadCardSn(byte[] pucCardSn)
        ** 函数功能:    读ISO14443A类型卡的卡号
        ** 入口参数:    -
        ** 出口参数:    byte  *pucCardSn; 读出的卡号
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    读出的卡号为4字节
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO14443A_ReadCardSn(byte[] pucCardSn);


        /************************************************************************************************************
        ** 函数原型:    byte ISO14443A_MF1AuthKey(byte ucBlock, byte[] pucKey, byte ucModel)
        ** 函数功能:    对ISO14443A类型Mifare 1 卡验证密钥
        ** 入口参数:    byte ucBlock    ： 操作的块号
        **				byte ucKeyModel ： 密钥模式
        **				ucKeyModel.7密钥来源：KEY_SOURCE_EXT：使用外部输入的密钥；KEY_SOURCE_E2：使用内部E2的密钥
        **				ucKeyModel.6-0密钥类型：KEY_TYPE_A：密钥A； KEY_TYPE_B：密钥B
        **				byte[] pucKey	： 密钥，6字节
        ** 出口参数:	-
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO14443A_MF1AuthKey(byte ucBlock, byte[] pucKey, byte ucKeyModel);


        /************************************************************************************************************
        ** 函数原型:    byte ISO14443A_ReadMF1Block(byte ucBlock, byte[] pucReadBuf)
        ** 函数功能:    读ISO14443A类型Mifare 1 卡一块数据
        ** 入口参数:    byte ucBlock    ： 操作的块号
        ** 出口参数:    byte[] pucReadBuf： 读出的数据
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    读出的数据长度为16字节（一块）
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO14443A_ReadMF1Block(byte ucBlock, byte[] pucReadBuf);


        /************************************************************************************************************
        ** 函数原型:    byte ISO14443A_WriteMF1Block(byte ucBlock, byte[] pucWriteBuf)
        ** 函数功能:    写ISO14443A类型Mifare 1 卡一块数据
        ** 入口参数:    byte ucBlock    ： 操作的块号
        **				byte[] pucWriteBuf： 要写入的数据
        ** 出口参数:	-
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    要写入的数据长度为16字节（一块）
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO14443A_WriteMF1Block(byte ucBlock, byte[] pucWriteBuf);


        /************************************************************************************************************
        ** 函数原型:    byte ISO14443B_ReadCardSn(byte[] pucCardSn)
        ** 函数功能:    读ISO14443B类型卡的卡号
        ** 入口参数:    -
        ** 出口参数:    byte  *pucCardSn; 读出的卡号
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    读出的卡号为4字节
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO14443B_ReadCardSn(byte[] pucCardSn);


        /************************************************************************************************************
        ** 函数原型:    byte ISO14443B_ReadSidCardSn(byte[] pucCardSn)
        ** 函数功能:    读ISO14443B类型二代身份证卡的卡号
        ** 入口参数:    -
        ** 出口参数:    byte  *pucCardSn; 读出的卡号
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    读出的卡号为8字节
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO14443B_ReadSidCardSn(byte[] pucCardSn);


        /************************************************************************************************************
        ** 函数原型:    byte ISO15693_ReadCardSn(byte[] pucCardSn)
        ** 函数功能:    读ISO15693类型卡的卡号
        ** 入口参数:    -
        ** 出口参数:    byte  *pucCardSn; 读出的卡号
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    读出的卡号为8字节
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO15693_ReadCardSn(byte[] pucCardSn);


        /************************************************************************************************************
        ** 函数原型:    byte ISO15693_ReadBlock(byte ucBlock, byte[] pucReadBuf)
        ** 函数功能:    读ISO15693类型卡一块数据
        ** 入口参数:    byte ucBlock    ： 操作的块号
        ** 出口参数:    byte[] pucReadBuf： 读出的数据
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    读出的数据长度为4字节（一块）
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO15693_ReadBlock(byte ucBlock, byte[] pucReadBuf);


        /************************************************************************************************************
        ** 函数原型:    byte ISO15693_WriteBlock(byte ucBlock, byte[] pucWriteBuf)
        ** 函数功能:    写ISO15693类型卡一块数据
        ** 入口参数:    byte ucBlock    ： 操作的块号
        **				byte[] pucWriteBuf： 要写入的数据
        ** 出口参数:	-
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    要写入的数据长度为4字节（一块）
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO15693_WriteBlock(byte ucBlock, byte[] pucWriteBuf);


        /************************************************************************************************************
        ** 函数原型:    byte ISO15693_M24LR64ReadBlock(short usBlock, byte[] pucReadBuf)
        ** 函数功能:    读ISO15693类型卡M24LR64一块数据
        ** 入口参数:    short usBlock    ： 操作的块号
        ** 出口参数:    byte[] pucReadBuf： 读出的数据
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    读出的数据长度为4字节（一块）
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO15693_M24LR64ReadBlock(short usBlock, byte[] pucReadBuf);


        /************************************************************************************************************
        ** 函数原型:    byte ISO15693_M24LR64WriteBlock(short usBlock, byte[] pucWriteBuf)
        ** 函数功能:    写ISO15693类型卡M24LR64一块数据
        ** 入口参数:    short usBlock    ： 操作的块号
        **				byte[] pucWriteBuf： 要写入的数据
        ** 出口参数:	-
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    要写入的数据长度为4字节（一块）
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO15693_M24LR64WriteBlock(short usBlock, byte[] pucWriteBuf);


        /************************************************************************************************************
        ** 函数原型:    byte ISO18092_ReadFelicaSn(byte[] pucCardSn)
        ** 函数功能:    读Felica卡的卡号
        ** 入口参数:    -
        ** 出口参数:    byte  *pucCardSn; 读出的卡号
        ** 返 回 值:    操作结果状态
        **				RFID_STATUS_OK	：	操作成功
        **				其他			：	操作失败
        ** 描　  述:    读出的卡号为8字节
        ************************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("HPC_RFID_DLL.dll")]
        public static extern byte ISO18092_ReadFelicaSn(byte[] pucCardSn);
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;



namespace RFID_CSharp
{
    class DLT645_DLL
    {
/*********************************************************************************************************
** 函数名称:		DLT645_2007_ReadData1
** 功能描述:		读数据                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pDI			数据标识符指针(4字节数据标识符)
** 输出参数:		*pReadBuff		输出数据指针
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
[System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern bool DLT645_2007_ReadData1(byte[] pAddr, byte[] pDI, byte[] pReadBuff);





//1.2
/*********************************************************************************************************
** 函数名称:		DLT645_2007_ReadData2
** 功能描述:		读指定块数数据                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pDI			数据标识符指针(4字节数据标识符)
**					N				负荷记录块数(1字节)
** 输出参数:		*pReadBuff		输出数据指针
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
[System.Runtime.InteropServices.DllImport("DLT645.dll")]
public static extern bool DLT645_2007_ReadData2(byte[] pAddr, byte[] pDI, byte[] pReadBuff, byte N);

//1.3
/*********************************************************************************************************
** 函数名称:		DLT645_2007_ReadData3
** 功能描述:		读指定时间、指定块数数据                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pDI			数据标识符指针(4字节数据标识符)
**					*pTime			时间指针(5字节时间,mmhhDDMMYY)
**					N				负荷记录块数(1字节)
** 输出参数:		*pReadBuff		输出数据指针
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
[System.Runtime.InteropServices.DllImport("DLT645.dll")]
public static extern  bool DLT645_2007_ReadData3(byte[] pAddr, byte[] pDI, byte[] pReadBuff, byte[] pTime, byte N);

//2
/*********************************************************************************************************
** 函数名称:		DLT645_2007_ReadDataNext
** 功能描述:		读后续数据                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pDI			数据标识符指针(4字节数据标识符)
**					SEQ				帧序号
** 输出参数:		*pReadBuff		输出数据指针
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
[System.Runtime.InteropServices.DllImport("DLT645.dll")]
public static extern  bool DLT645_2007_ReadDataNext(byte[] pAddr, byte[] pDI, byte SEQ, byte[] pReadBuff);

//3
/*********************************************************************************************************
** 函数名称:		DLT645_2007_WriteData
** 功能描述:		写数据                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pDI			数据标识符指针(4字节数据标识符)
**					*pPassword		密钥指针(4字节密钥)
**					*pConsumer		操作者码(4字节操作者码)
**					*pWriteData		写入数据指针
**					len				写入数据个数
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
[System.Runtime.InteropServices.DllImport("DLT645.dll")]
public static extern  bool DLT645_2007_WriteData(byte[] pAddr, byte[] pDI, byte[] pPassword, byte[] pConsumer, byte[] pWriteData, byte len);

//4
/*********************************************************************************************************
** 函数名称:		DLT645_2007_ReadAddr
** 功能描述:		读通信地址                        
** 输入参数:        *pAddr			输出地址指针(6字节地址)
** 输出参数:		*pAddr			输出地址指针(6字节地址)
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_2007_ReadAddr(byte[] pAddr);

//5
/*********************************************************************************************************
** 函数名称:		DLT645_2007_WriteAddr
** 功能描述:		写通信地址                        
** 输入参数:        *pAddr			地址指针(6字节地址)
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_2007_WriteAddr(byte[] pAddr);

//6
/*********************************************************************************************************
** 函数名称:		DLT645_2007_BroadcastTime
** 功能描述:		广播校时                        
** 输入参数:        *pTime			时间指针(6字节时间,ssmmhhDDMMYY)
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_2007_BroadcastTime(byte[] pTime);

//7
/*********************************************************************************************************
** 函数名称:		DLT645_2007_Freeze
** 功能描述:		冻结命令                        
** 输入参数:        *pAddr			地址指针(6字节地址)
*pTime			时间指针(4字节时间,mmhhDDMM)
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_2007_Freeze(byte[] pAddr, byte[] pTime);

//8
/*********************************************************************************************************
** 函数名称:		DLT645_2007_ChangeBaudrate
** 功能描述:		更改通信速率                       
** 输入参数:        *pAddr			地址指针(6字节地址)
**					Z				通信速率特征字
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_2007_ChangeBaudrate(byte[] pAddr, byte Z);

//9
/*********************************************************************************************************
** 函数名称:		DLT645_2007_ChangePassword
** 功能描述:		更改密码                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pDI			数据标识符指针(4字节数据标识符)
**					*pPasswordOld	旧密钥指针(4字节密钥)
**					*pPasswordNew	新密钥指针(4字节密钥)
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_2007_ChangePassword(byte[] pAddr, byte[] pDI, byte[] pPasswordOld, byte[] pPasswordNew);

//10
/*********************************************************************************************************
** 函数名称:		DLT645_2007_CleanMaxRequire
** 功能描述:		最大需量清零                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pPassword		密钥指针(4字节密钥)
**					*pConsumer		操作者码(4字节操作者码)
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_2007_CleanMaxRequire(byte[] pAddr, byte[] pPassword, byte[] pConsumer);

//11
/*********************************************************************************************************
** 函数名称:		DLT645_2007_CleanAllData
** 功能描述:		电表清零                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pPassword		密钥指针(4字节密钥)
**					*pConsumer		操作者码(4字节操作者码)
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_2007_CleanAllData(byte[] pAddr,byte[] pPassword, byte[] pConsumer);

//12
/*********************************************************************************************************
** 函数名称:		DLT645_2007_CleanMaxRequire
** 功能描述:		事件清零                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pPassword		密钥指针(4字节密钥)
**					*pConsumer		操作者码(4字节操作者码)
**					*pData			数据指针
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern bool DLT645_2007_CleanEvent(byte[] pAddr, byte[] pPassword, byte[] pConsumer, byte[] pData);

//13
/*********************************************************************************************************
** 函数名称:		DLT645_OpenPort
** 功能描述:		打开串口                        
** 输入参数:        com				串口号
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_OpenPort(int com);

//14
/*********************************************************************************************************
** 函数名称:		DLT645_ClosePort
** 功能描述:		关闭串口                        
** 输入参数:        com				串口号
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_ClosePort(int com);

//15
/*********************************************************************************************************
** 函数名称:		DLT645_GetDllVersion
** 功能描述:		读动态库版本号                        
** 输入参数:        无
** 输出参数:		NON
** 返回数值:		返回动态库版本号.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  int DLT645_GetDllVersion();

//1
/*********************************************************************************************************
** 函数名称:		DLT645_1997_ReadData
** 功能描述:		读数据                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pDI			数据标识符指针(2字节数据标识符)
** 输出参数:		*pReadBuff		输出数据指针
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_1997_ReadData(byte[] pAddr, byte[] pDI, byte[] pReadBuff);

//2
/*********************************************************************************************************
** 函数名称:		DLT645_1997_ReadDataNext
** 功能描述:		读后续数据                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pDI			数据标识符指针(2字节数据标识符)
** 输出参数:		*pReadBuff		输出数据指针
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_1997_ReadDataNext(byte[] pAddr, byte[] pDI, byte[] pReadBuff);

//3
/*********************************************************************************************************
** 函数名称:		DLT645_1997_ReReReadData
** 功能描述:		重读数据                        
** 输入参数:        *pAddr			地址指针(6字节地址)
** 输出参数:		*pReadBuff		输出数据指针
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_1997_ReReadData(byte[]pAddr, byte[]pReadBuff);

//4
/*********************************************************************************************************
** 函数名称:		DLT645_1997_WriteData
** 功能描述:		写数据                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pDI			数据标识符指针(2字节数据标识符)
**					*pWriteData		写入数据指针
**					len				写入数据个数
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_1997_WriteData(byte[]pAddr, byte[]pDI, byte[]pWriteData, byte len);

//5
/*********************************************************************************************************
** 函数名称:		DLT645_1997_BroadcastTime
** 功能描述:		广播校时                        
** 输入参数:        *pTime			时间指针(6字节时间)
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_1997_BroadcastTime(byte[]pTime);

//6
/*********************************************************************************************************
** 函数名称:		DLT645_1997_WriteAddr
** 功能描述:		写通信地址                        
** 输入参数:        *pAddr			地址指针(6字节地址)
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_1997_WriteAddr(byte[]pAddr);

//7
/*********************************************************************************************************
** 函数名称:		DLT645_1997_ChangeBaudrate
** 功能描述:		更改通信速率                       
** 输入参数:        *pAddr			地址指针(6字节地址)
**					Z				通信速率特征字
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_1997_ChangeBaudrate(byte[]pAddr, byte Z);

//8
/*********************************************************************************************************
** 函数名称:		DLT645_1997_ChangePassword
** 功能描述:		更改密码                        
** 输入参数:        *pAddr			地址指针(6字节地址)
**					*pPasswordOld	旧密钥指针(4字节密钥)
**					*pPasswordNew	新密钥指针(4字节密钥)
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
        [System.Runtime.InteropServices.DllImport("DLT645.dll")]
        public static extern  bool DLT645_1997_ChangePassword(byte[]pAddr, byte[]pPasswordOld, byte[]pPasswordNew);

//9
/*********************************************************************************************************
** 函数名称:		DLT645_1997_CleanMaxRequire
** 功能描述:		最大需量清零                        
** 输入参数:        *pAddr			地址指针(6字节地址)
** 输出参数:		NONE
** 返回数值:		操作成功返回TRUE, 操作失败返回FALSE.
*********************************************************************************************************/
[System.Runtime.InteropServices.DllImport("DLT645.dll")]
public static extern  bool DLT645_1997_CleanMaxRequire(byte[]pAddr);
   }
}
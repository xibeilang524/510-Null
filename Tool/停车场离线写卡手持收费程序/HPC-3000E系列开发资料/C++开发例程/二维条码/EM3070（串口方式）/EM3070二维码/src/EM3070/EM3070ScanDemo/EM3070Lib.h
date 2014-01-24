/****************************************Copyright (c)****************************************************
**                            Guangzhou ZHIYUAN electronics Co.,LTD.
**                                      
**                                 http://www.embedtools.com
**
**--------------File Info---------------------------------------------------------------------------------
** File name:               EM1300Lib.h
** Latest modified Date:    
** Latest Version:          
** Descriptions:            
**
**--------------------------------------------------------------------------------------------------------
** Created by:              曹 欢     
** Created date:            2011-04-13 
** Version:                 V1.00
** Descriptions:            原始版本(EM1300Lib类的声明)
**
**--------------------------------------------------------------------------------------------------------
** Modified by:                                 
** Modified date:           
** Version:                 
** Descriptions:            
**
*********************************************************************************************************/
#if !defined(AFX_EM1300LIB_H__97056FA2_C9BC_4095_8E9A_59D91B41F9BC__INCLUDED_)
#define AFX_EM1300LIB_H__97056FA2_C9BC_4095_8E9A_59D91B41F9BC__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define COMMANDLENGTH 9

/**********************************************************************************************************
  数据包结构体
**********************************************************************************************************/
typedef struct __DPacket {
	BYTE Length;                                                       /* 除掉checksum之外,数据包的长度  */
	BYTE Opcode;                                                       /* 操作码                         */
	BYTE MSource;                                                      /* 信息来源                       */
	BYTE Status;                                                       /* 状态字节                       */
	BYTE CheckSumHigh;                                                 /* 校验和高字节                   */
	BYTE CheckSumLow;	                                               /* 校验和低字节                   */
} DataPacket;


typedef struct __SDPacket {
	BYTE Length;                                                       /* 除掉checksum之外，数据包的长度 */
	BYTE Opcode;                                                       /* 操作码                         */
	BYTE MSource;                                                      /* 信息来源                       */
	BYTE Status;                                                       /* 状态字节                       */
	BYTE Selection;	                                                   /* 选择模式                       */
	BYTE CheckSumHigh;                                                 /* 校验和高字节                   */
	BYTE CheckSumLow;	                                               /* 校验和低字节                   */
} SDataPacket;

typedef struct __PDPacket {
	BYTE Length;                                                       /* 除掉checksum之外,数据包的长度  */
	BYTE Opcode;                                                       /* 操作码                         */
	BYTE MSource;                                                      /* 信息来源                       */
	BYTE Status;                                                       /* 状态字节                       */
	BYTE BeepCode;	                                                   /* 蜂鸣器代码                     */	
	BYTE ParaNum;                                                      /* 参数号                         */
	BYTE ParaValue;                                                    /* 参数值                         */
	BYTE CheckSumHigh;                                                 /* 校验和高字节                   */
	BYTE CheckSumLow;                                                  /* 校验和低字节                   */
} PDataPacket;


typedef struct __TDPacket {
	BYTE Length;                                                       /* 除掉checksum之外,数据包的长度  */
	BYTE Opcode;                                                       /* 操作码                         */
	BYTE MSource;                                                      /* 信息来源                       */
	BYTE Status;                                                       /* 状态字节                       */
	BYTE BeepCode;	                                                   /* 蜂鸣器代码                     */	
	BYTE ParaNum1;                                                     /* 参数号1                        */                                                     
	BYTE ParaValue1;                                                   /* 参数号1的值                    */
	BYTE ParaNum2;                                                     /* 参数号2                        */ 
	BYTE ParaValue2;                                                   /* 参数号2的值                    */
	BYTE ParaNum3;                                                     /* 参数号3                        */ 
	BYTE ParaValue3;                                                   /* 参数号3的值                    */
	BYTE CheckSumHigh;                                                 /* 校验和高字节                   */
	BYTE CheckSumLow;                                                  /* 校验和低字节                   */
} TDataPacket;


typedef struct __MDPacket {
	BYTE Length;                                                       /* 除掉checksum之外,数据包的长度  */
	BYTE Opcode;                                                       /* 操作码                         */
	BYTE MSource;                                                      /* 信息来源                       */
	BYTE Status;                                                       /* 状态字节                       */
	BYTE BeepCode;	                                                   /* 蜂鸣器代码                     */	
	BYTE ParaNum;                                                      /* 参数号                         */
	BYTE Offset;                                                       /* 偏移量                         */
	BYTE ParaValue;                                                    /* 参数值                         */
	BYTE CheckSumHigh;                                                 /* 校验和高字节                   */
	BYTE CheckSumLow;                                                  /* 校验和低字节                   */
} MDataPacket;



typedef struct __NDPacket {
	BYTE Length;                                                       /* 除掉checksum之外,数据包的长度  */
	BYTE Opcode;                                                       /* 操作码                         */
	BYTE MSource;                                                      /* 信息来源                       */
	BYTE Status;                                                       /* 状态字节                       */
	BYTE BeepCode;	                                                   /* 蜂鸣器代码                     */	
	BYTE ParaNum1;                                                     /* 参数号1                        */                                                     
	BYTE ParaValue1;                                                   /* 参数号1的值                    */
	BYTE ParaNum2;                                                     /* 参数号2                        */ 
	BYTE ParaValue2;                                                   /* 参数号2的值                    */
	BYTE CheckSumHigh;                                                 /* 校验和高字节                   */
	BYTE CheckSumLow;                                                  /* 校验和低字节                   */
} NDataPacket;

/**********************************************************************************************************
  EM1300Lib公用类定义
**********************************************************************************************************/
class EM1300Lib
{
public:
	EM1300Lib();
	~EM1300Lib();

	/*
	 *  唤醒函数
	 */
	static int  EM1300WakeUp( UCHAR ucPort);

};


#endif // !defined(AFX_EM1300LIB_H__97056FA2_C9BC_4095_8E9A_59D91B41F9BC__INCLUDED_)
/*********************************************************************************************************
  END FILE
*********************************************************************************************************/




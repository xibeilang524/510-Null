using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.ParkService.CANParking
{
    /// <summary>
    /// 指令码

    /// </summary>
    static class OrderCode
    {
        /// <summary>
        /// 三个同步符号
        /// </summary>
        public const byte Syn_Head = 0xFE;
        public const byte Syn_Body = 0xFD;
        public const byte Syn_Tail = 0xFE;

        /// <summary>
        /// 地址码 
        /// </summary>
        public const byte ADDR_Broadcast = 0xF0;   //:0xF0为所有设备广播地址
        public const byte Addr_CtrlBroadCast = 0x3F;     //:0x3F为通道控制器广播地址
        public const byte Addr_Manager = 0x01;           //主机地址
        public const byte Addr_Myself = 0x00;            //:0x00为设备本身地址 (一切接收自身232口指令接收设备的本机概念)
        public const byte Addr_Itself = 0x00;            //一切接收设备的本机概念
        public const byte Addr_LedScreen = 0x60;         //汉字显示屏含数字显示屏，指令集为非字符区
        public const byte ADDR_HeatPrinter = 0x70;//                                    '热敏纸票机指令集为非字符区

        public const byte ADDR_VehicleDetector = 0x80;//                                '车辆检测器容量32个满足区域引导

        public const byte ADDR_CardProvider = 0xA0;//                                   '自动出卡机，含自动收卡机，主控器可挂接此设备，指令集应统一
        public const byte ADDR_Reader = 0xB0;//                                         'ID号读卡头，主控器可挂接此设备，指令集应统一
        public const byte ADDR_AutoBarrier = 0xC0;//                                    '自动道闸机，主控器可挂接此设备，指令集应统一
        public const byte ADDR_IOModel = 0xD0;//                                        '通用IO模块，一级总线设备，背板公用的指令集应统一，范围与地址约同
        public const byte ADDR_ParkingGuider = 0xE0;//                                  '停车引导器，一级总线设备，背板公用的指令集应统一，用作超声波实时车位引导
        public const byte ADDR_Other = 0xF0;//                                          '其他设备

        public const byte Comm_OnLine = 0x01;            //上线报告,参数为0掉电离线/1上电上线/2手动复位/3看门狗复位/4软件复位/5在线应答
        public const byte Comm_OffLine = 0x00;           //离线报告
        public const byte Comm_SoftReset = 0x2;          //工作方式+1字节参数,参数=1=软件复位/5=在线查询
        public const byte Comm_SetDateTime = 0x03;       //时钟设置参数顺序为second,minute,hour,day,month,year,week,DispMode
        public const byte DisplayModel_ChineseRoll = 0x01;            //中文滚动
        public const byte DisplayModel_OnlyShowTime = 0x02;           //只显示时间

        public const byte DisplayModel_EnglishDateTime = 0x03;        //显示英文日期时间
        public const byte DisplayModel_ChineseDateTime = 0x04;        //中文日期时间
        public const byte DisplayModel_ChineseEnglishDateTime = 0x05; //中英文日期时间

        public const byte LED_Display = 0x10;               //直通显示指令+  
        public const byte LEDR_StoreSentance = 0x13;       //永久保存信息
        public const byte Comm_StatusReport = 0x0E;                   //串口状态报告

        public const byte StatusReport_CANShortToGND = 0x05;             //总线对地持续短路或端口故障

        public const byte StatusReport_CANShortTo5V = 0x06;              //总线对对5V电源持续短路或端口故障

        public const byte StatusReport_CAN2ShortToGND = 0x15;             //总线对地持续短路或端口故障

        public const byte StatusReport_CAN2ShortTo5V = 0x16;              //总线对对5V电源持续短路或端口故障

        public const byte SMMR_LinkMode = 0x20;                    //设置设备工作模式
        public const byte Comm_DownLoadCardNotify = 0x21;          //下载卡片通知
        public const byte Comm_DownLoadCard = 0x22;                //下载卡片
        public const byte Comm_UpdateCard = 0x23;                  //修改卡片
        public const byte SMMR_TestOneCard = 0x24;                 //PC下送卡片内码，通道机查询计费。参数7字节：通道号+读头号+卡种类+4字节ID
        public const byte Comm_FetchCardRequest = 0x25;           //提取卡片信息通知
        public const byte Comm_SetVacantNotify = 0x2A;             //设置空余车位
        public const byte Comm_WriteParameterTable = 0x2B;  　　　 //下载VacantLimit车位上下限参数表

        public const byte Comm_TempCardExit = 0x30;                //临时卡出场　现场事件收悉等待（PC机必须在3秒内响应，据此判断是否联机）+口地址+车型(0-15)+
        public const byte Comm_EventValid = 0x31;                  //事件有效放行
        public const byte Comm_EventInvalid = 0x32;　　　　　　　　//事件无效拒绝放行并清除事件


        public const byte Comm_CardDLCompleted = 0x40;   //卡片下载完成

        public const byte Comm_FetchCardAnswer = 0x41;   //提取卡片应答
        public const byte Comm_UpLoadCardDetail = 0x42;        //上传卡片明细
        public const byte Comm_CarPortReport = 0x46;       //上报车位数

        public const byte Comm_CardWaitingEvent = 0x50;       //报告正在发生的事件一条
        public const byte Comm_CardPermitedEvent = 0x51;      //报告已经发生的事件记录一条

        public const byte Comm_FetchEventRequest = 0x33; //提取事件请求
        public const byte Comm_FetchEventAnswer = 0x52;    //获取事件记录通知
        public const byte Comm_HistoryCardEvent = 0x53;  //上报系统事件，将系统中所有的事件信息全部上传给服务器


        public const byte Comm_CardWait = 0x60;          //卡号收妥等待指令+n字节参数+等待过长时间也要重发卡号
        public const byte Comm_CardWaitValid = 0x61;     //卡等待有效放行指令

        public const byte Comm_CardWaitInvalid = 0x63;   //卡等待无效不放行指令
        public const byte Comm_CardValid = 0x63;         //有效卡，自动放行的卡片

        public const byte Comm_CardInvalid = 0x64;       //无效刷卡
        public const byte Comm_CapacityReport = 0x69;    //车位是否已满 
        public const byte Comm_Sense = 0x98;             //地感
        public const byte Comm_SetCardMachineNotify = 0xA0;//开/关发卡机
        public const byte Comm_TakeOutCardNotify = 0xA1;   //出卡
        public const byte Comm_GateOperationNotify = 0xC0;     //道闸操作

        public const byte COMT_CommandEcho = 0x0D;               //命令回声 暂时只有刷卡抬闸的升闸动作指令返回，便于实时模式刷卡抬闸确认（电脑手工抬闸不返回），便于更可靠的确认已进场和出场，再回写数据库。 
                                                                 //例如2号机抬闸返回：FE FD FE 02 00 0D 02 C0(ABMR_Operation) 75('u') B8



        /// <summary>
        /// 道闸操作指令
        /// </summary>
        public const byte ABMR_Operation = 0xC0;         ////道闸直通操作指令+2字节参数'u'0/'u'1/'u'2/'u'3/'u'4/"dw"/"st"=抬闸并保持/抬闸计数落/抬闸不累计/抬闸车过落/抬闸30秒落/落闸/停闸

        /// <summary>
        /// 无效刷卡时语音识别码
        /// </summary>
        public const byte Voice_Invalid_UnRegisterCard = 0x00;        //卡片未注册
        public const byte Voice_Invalid_ReadCard = 0x01;           //请继续读卡，用于作读写器使用
        public const byte Voice_Invalid_ReadOperationCard = 0x02; //请读操作卡，用于作收费等操作
        public const byte Voice_Invalid_ReadManagerCard = 0x03;   //请读管理卡，用于作设置等操作
        public const byte Voice_Invalid_PictureDiff = 0x04;        //图像有差异(等待指令已显示卡号等)
        public const byte Voice_Invalid_CardDelete = 0x05;        //此卡已注销;
        public const byte Voice_Invalid_CardLoss = 0x06;          //此卡已挂失
        public const byte Voice_Invalid_CardLocked = 0x07;        //此卡已锁
        public const byte Voice_Invalid_CardTypeError = 0x08;     //非停车卡类
        public const byte Voice_Invalid_NoEntranceAcsLevel = 0x09;//未准入本场
        public const byte Voice_Invalid_CardExits = 0x0A;         //此卡已在场
        public const byte Voice_Invalid_CardStillOut = 0x0B;      //此卡未进场
        public const byte Voice_Invalid_OwnerFull = 0x0C;         //固定车位满
        public const byte Voice_Invalid_ParkFull = 0x0D;          //车位已满
        public const byte Voice_Invalid_CardNotPaid = 0x0E;       //此卡未交费
        public const byte Voice_Invalid_CardOverTime = 0x0F;      //超时补交费
        public const byte Voice_Invalid_CardExpired = 0x11;       //此卡已过期
        public const byte Voice_Invalid_OverHour = 0x12;          //此卡已过点
        public const byte Voice_Invalid_NoEnoughBalance = 0x13;      //卡余额不足
        public const byte Voice_InTimeError = 0x15;               //

        public const byte CPMR_Lock = 0xA0;                         //出卡机开锁锁定+1字节参数，数，参数=0/1=开锁/锁定不出卡
        public const byte CPMR_Cardout = 0xA1;                         //出卡机出卡指令
        public const byte CPMT_CardButt2 = 0xA6;                          //按取卡按钮2,可用于紧急开闸
        public const byte CCMT_Cap = 0xA7;                         //收卡机收卡一张
        public const byte CPMT_CardButt = 0xA8;                         //按取卡按钮
        public const byte CPMT_CardOut = 0xA9;                         //出卡机出卡一张
        public const byte CPMT_CardIn = 0xAA;                        //出卡机收卡一张
        public const byte CPMT_CardNone = 0xAB;                         //出卡机无卡
        public const byte CPMT_CardFew = 0xAC;                        //出卡机缺卡
        public const byte CPMT_CardLoad = 0xAD;                         //出卡机装卡
        public const byte CPMT_CardAdd = 0xAE;                        //出卡机添卡
        public const byte CPMT_CardJam = 0xAF;                       //出卡机塞卡
        public const byte RWMT_CardID = 0xB8;                         //上传卡号,控制器的读卡器读到卡片 

    }

    /// <summary>
    /// 有效刷卡时语音识别码
    /// </summary>
    public static class CardValidVoice
    {
        /// <summary>
        /// 入场
        /// </summary>
        public const byte Voice_NormalIn = 0x00;
        /// <summary>
        /// 出场
        /// </summary>
        public const byte Voice_NormalOut = 0x01;
        /// <summary>
        /// 欢迎入内场
        /// </summary>
        public const byte Voice_InDoorIn = 0x02; //
        /// <summary>
        /// 欢迎出内场
        /// </summary>
        public const byte Voice_InDoorOut = 0x03;   //
        /// <summary>
        /// 重复入场！，须验证出场，时确认放行，不可自动放行
        /// </summary>
        public const byte Voice_RepeatIn = 0x04;        //
        /// <summary>
        /// 语音:过期卡入场，按时计费！
        /// </summary>
        public const byte Voice_OverDateAsTempIn = 0x05;                        //
        /// <summary>
        /// 语音:欠费卡入场，按时计费！
        /// </summary>
        public const byte Voice_ArrearAsTempIn = 0x06;                        //
        /// <summary>
        /// 语音:重入卡出场，请出示证件，出场时确认放行，不可自动放行
        /// </summary>
        public const byte Voice_RepeatInOut = 0x07;                        //
        /// <summary>
        /// 语音:重复出场！，请出示证件，出场时确认放行，不可自动放行
        /// </summary>
        public const byte Voice_RepeatOut = 0x8;                        //
        /// <summary>
        /// 语音:过期卡出场，请及时延期，人工确认放行,并提醒
        /// </summary>
        public const byte Voice_OverDateOut = 0x09;                        //
        /// <summary>
        /// 语音:欠费卡出场，请及时充值，扣为负值，人工确认放行,并提醒
        /// </summary>
        public const byte Voice_ArrearOut = 0x0A;                       //
        /// <summary>
        /// 语音:过期变通卡，临时卡计费，人工确认放行,并提醒，按登记车型预显计费，但可使用PARK_TypeToll指令更改
        /// </summary>
        public const byte Voice_OverDateAsTempOut = 0x0B;                       //
        /// <summary>
        /// 语音:储值卡余额不足出场，按临时卡计费，人工确认放行,并提醒，按登记车型预显计费，但可使用PARK_TypeToll指令更改
        /// </summary>
        public const byte Voice_NoEnoughBalanceTempOut = 0x0C;                       //
        /// <summary>
        /// 语音:储值卡收费
        /// </summary>
        public const byte Voice_PrepayOut = 0x0D;                       //，
        /// <summary>
        /// 语音:临时卡收费，变通临时卡，临时卡计费，人工确认放行,并提醒，按登记车型预显计费，但可使用PARK_TypeToll指令更改计费前缀，进出场时间等，显示按车型计费的结果，人工确认放行,并提醒
        /// </summary>
        public const byte Voice_TempCardOut = 0x0E;                       //
        /// <summary>
        /// 语音:操作员上班。
        /// </summary>
        public const byte Voice_OperatorOnDuty = 0x0F;                       //
        /// <summary>
        /// 语音:门禁进门！。
        /// </summary>
        public const byte Voice_DoorIn = 0x11;                       //
        /// <summary>
        /// 语音:门禁出门！。
        /// </summary>
        public const byte Voice_DoorOut = 0x12;                       //
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.Park.ParkService.CANParking
{
    internal class PacketCreater
    {
        #region 私有方法
        /// <summary>
        /// 把日期时间转换成字节数组
        /// </summary>
        /// <param name="t">日期</param>
        /// <returns></returns>
        private byte[] DateTimeToBytes(DateTime t)
        {
            byte[] bytes = new byte[6];
            bytes[0] = (byte)t.Second;
            bytes[1] = (byte)t.Minute;
            bytes[2] = (byte)t.Hour;
            bytes[3] = (byte)t.Day;
            bytes[4] = (byte)t.Month;
            bytes[5] = (byte)(t.Year - 2000);
            return bytes;
        }

        private byte[] DateToBytes(DateTime t)
        {
            byte[] bytes = new byte[3];
            bytes[0] = (byte)t.Day;
            bytes[1] = (byte)t.Month;
            bytes[2] = (byte)(t.Year - 2000);
            return bytes;
        }

        /// <summary>
        /// 把字符串转换成字节数组
        /// </summary>
        /// <param name="t">要转换的字符串</param>
        /// <returns></returns>
        private byte[] StringToBytes(string t)
        {
            return Encoding.GetEncoding("GB2312").GetBytes(t);
        }

        private byte[] TimeEntityToBytes(TimeEntity time)
        {
            short s = (short)(time.Hour * 60 + time.Minute);
            return SEBinaryConverter.ShortToBytes(s);
        }

        private byte GetVoice(byte carType)
        {
            CarType ct = CarTypeSetting.Current[carType];
            if (ct != null)
            {
                return (byte)(ct.HardwareCarType + 1);
            }
            else
            {
                return 1; //默认用小型车
            }
        }

        private byte GetHardwareInvalidType(EventInvalidType invalidType)
        {
            switch (invalidType)
            {
                case EventInvalidType.INV_UnRegister:
                    return 0;  //此卡未登记
                case EventInvalidType.INV_ReadCard: //请继续读卡，用于作读写器使用
                    return 1;
                case EventInvalidType.INV_CarPlateWrong:
                case EventInvalidType.INV_CarPlateWrongWithPaid:
                case EventInvalidType.INV_InvalidImg:
                    return 4;  //图像有差异
                case EventInvalidType.INV_Recycled:
                    return 5; //此卡已注销
                case EventInvalidType.INV_Loss:
                    return 6; //此卡已锁定
                case EventInvalidType.INV_Lock:
                    return 7; //此卡已挂失
                case EventInvalidType.INV_Type:
                    return 8; //非停车卡类
                case EventInvalidType.INV_NotActive:
                case EventInvalidType.INV_NoAccessRight:
                case EventInvalidType.INV_DisableNestedPark:
                case EventInvalidType.INV_ForbidTempCard:
                    return 9; //未准入本场
                case EventInvalidType.INV_HaveIn:
                    return 10; //此卡已入场
                case EventInvalidType.INV_StillOut:
                    return 11;  //此卡已出场
                case EventInvalidType.INV_ParkFull:
                    return 13;  //车位已满
                case EventInvalidType.IVN_NotPaid:
                    return 14; //未缴费
                case EventInvalidType.INV_OverTime:
                    return 15;  //超时补交费
                case EventInvalidType.INV_OverDate:
                    return 16; //此卡已过期
                case EventInvalidType.INV_Balance:
                    return 18; //余额不足
                default:
                    return 9;
            }
        }

        /// <summary>
        /// 获取道闸操作指令
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private byte[] GetGateOperationParas(GateOperation action)
        {
            byte[] operation = new byte[2];
            switch (action)
            {
                case GateOperation.Close:
                    operation[0] = (byte)Convert.ToChar("d");
                    operation[1] = (byte)Convert.ToChar("w");
                    break;
                case GateOperation.Open:
                    operation[0] = (byte)Convert.ToChar("u");
                    operation[1] = 0;
                    break;
                case GateOperation.Stop:
                    operation[0] = (byte)Convert.ToChar("s");
                    operation[1] = (byte)Convert.ToChar("t");
                    break;
            }
            return operation;
        }

        private Packet FormatCard(byte address, CardInfo card, ActionType action)
        {
            Packet packet = new Packet();
            packet.Address = address;
            packet.Order = (byte)(action == ActionType.Upate ? OrderCode.Comm_UpdateCard : OrderCode.Comm_DownLoadCard);
            //卡片位置序号
            packet.Parameters.AddRange(SEBinaryConverter.ShortToBytes(card.Index));
            //卡号
            packet.Parameters.AddRange(SEBinaryConverter.IntToBytes((int)Convert.ToUInt32(card.CardID)));
            //注册标记
            packet.Parameters.Add(GetHardwareCardStatus(card.Status));
            //卡片类型字段为一复合字段，高4位为车型，低四位为卡片类型
            byte b = (byte)((byte)card.CarType * 16 + ((byte)card.CardType & 0x0F));
            packet.Parameters.Add(b);
            //有效期
            packet.Parameters.AddRange(DateToBytes(card.ValidDate));
            //入场权限
            packet.Parameters.AddRange(SEBinaryConverter.IntToBytes(0));
            //卡片编号
            packet.Parameters.AddRange(SEBinaryConverter.ShortToBytes(card.CardNum));
            //车牌问候
            packet.AddBytes(new byte[10]);
            //余额,如果侠额小于零时只下发0，因为硬件不支持负数金额，会变成很大的正数
            packet.Parameters.AddRange(SEBinaryConverter.IntToBytes(TariffSetting.Current.TariffOption.FromYuan(card.Balance > 0 ? card.Balance : 0)));
            //入场标记(入场bit7=1)
            packet.Parameters.Add((byte)card.ParkingStatus);
            //进场时间
            packet.Parameters.AddRange(DateTimeToBytes(card.LastDateTime));
            //上次地址
            packet.Parameters.Add((byte)card.LastEntrance);
            //权限组
            packet.Parameters.Add(card.AccessID);
            packet.Parameters.Add(0);
            packet.Parameters.Add(0);
            return packet;
        }

        private byte GetHardwareCardStatus(CardStatus status)
        {
            switch (status)
            {
                case CardStatus.Enabled:
                    return 0;  //已发行
                case CardStatus.Disabled:
                    return 0x20;  //待发行(锁定)
                case CardStatus.Loss:
                    return 0x40; //挂失
                case CardStatus.Deleted:
                case CardStatus.Recycled:
                    return 0x80; //注销
                default:
                    return 0x80;
            }
        }
        #endregion

        #region 公共方法
        public Packet CreateSyncTimePacket(byte address)
        {
            DateTime dt = DateTime.Now;
            Packet packet = new Packet();
            packet.Address = address;
            packet.Order = OrderCode.Comm_SetDateTime;
            packet.Parameters.AddRange(DateTimeToBytes(dt));
            packet.Parameters.Add((byte)(Convert.ToInt32(dt.DayOfWeek) + 1));  //Weekday(t)                '1~7=星期日~六
            packet.Parameters.Add(5);
            return packet;
        }

        public Packet CreateDeviceResetPacket(byte address)
        {
            Packet order = new Packet();
            order.Address = address;
            order.Order = OrderCode.Comm_SoftReset;
            order.Parameters.Add(1);  //复位
            return order;
        }

        public Packet CreateQueryStatusPacket(byte address)
        {
            Packet order = new Packet();
            order.Address = address;
            order.Order = OrderCode.Comm_SoftReset;
            order.Parameters.Add(5);  //在线查询
            return order;
        }

        public Packet CreateSetWorkmodePacket(byte address, byte workmode)
        {
            //生成设置工作模式的消息包 工作模式：0，脱机模式  3，在线模式  4，发行器模式
            Packet p = new Packet();
            p.Address = address;
            p.Order = OrderCode.SMMR_LinkMode;
            p.Parameters.Add(workmode);
            return p;
        }

        public Packet CreateCardEnterWaitPacket(byte address, CardType cardType)
        {
            //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音显示参数+1字节内屏显示模式参数(总为0)+1字节卡类型+2字节卡编号
            Packet p = new Packet();
            p.Order = OrderCode.Comm_CardWait;
            p.Address = address;
            p.AddByte(CardValidVoice.Voice_NormalIn);
            p.AddByte((byte)0);
            p.AddByte((byte)cardType);
            p.AddShort((short)1000);
            return p;
        }

        public Packet CreateMonthCardEnterValidPacket(byte address, DateTime expiredDate)
        {
            //回答月卡时参数为：卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音显示参数+1字节内屏显示模式参数(总为0)+1字节卡类型+2字节卡编号+3字节月卡有效期日月年
            Packet packet = new Packet();
            packet.Order = OrderCode.Comm_CardValid;
            packet.Address = address;
            packet.AddByte(CardValidVoice.Voice_NormalIn);
            packet.AddByte((byte)0);
            packet.AddByte((byte)CardType.MonthRentCard);
            packet.AddShort((short)1000);
            packet.AddBytes(DateToBytes(expiredDate));
            return packet;
        }

        public Packet CreatePrepayCardEnterValidPacket(byte address, DateTime eventDt, decimal balance)
        {
            //回答储值卡入场时参数为：
            //卡等待/有效/无效指令+参数长度(采用可变长度)+char Voice;char OpenMode;char Type;unsigned int Num;struct STTime St_EnterTime;
            //char DecimalDigits;signed long Balance;char Ar_Chr[10];};       //5+3+1+4+10=23字节
            Packet p = new Packet();
            p.Order = OrderCode.Comm_CardValid;
            p.Address = address;
            p.AddByte(CardValidVoice.Voice_NormalIn);
            p.AddByte((byte)0);
            p.AddByte((byte)CardType.PrePayCard);
            p.AddShort((short)1000);
            p.AddByte((byte)eventDt.Second);
            p.AddByte((byte)eventDt.Minute);
            p.AddByte((byte)eventDt.Hour);
            p.AddByte(TariffSetting.Current.TariffOption.PointCount);
            p.AddInt(TariffSetting.Current.TariffOption.FromYuan(balance));
            p.AddBytes(new byte[10]);
            return p;
        }

        public Packet CreateTempCardEnterValidPacket(byte address, DateTime eventDt)
        {
            //回答临时卡入场时参数为：
            //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音显示参数+1字节内屏显示模式参数(总为0)+1字节卡类型+2字节卡编号+3字节临时卡入场秒分时
            Packet p = new Packet();
            p.Order = OrderCode.Comm_CardValid;
            p.Address = address;
            p.AddByte(CardValidVoice.Voice_NormalIn);
            p.AddByte((byte)0);
            p.AddByte((byte)CardType.TempCard);
            p.AddShort((short)1000);
            p.AddByte((byte)eventDt.Second);
            p.AddByte((byte)eventDt.Minute);
            p.AddByte((byte)eventDt.Hour);
            return p;
        }

        public Packet CreateMonthCardExitWaitPacket(byte address, CardEventReport cardEvent)
        {
            //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音显示参数+1字节内屏显示模式参数(总为0)+1字节卡类型+2字节卡编号+3字节月卡有效期日月年
            Packet p = new Packet();
            p.Order = OrderCode.Comm_CardWait;
            p.Address = address;
            p.AddByte(CardValidVoice.Voice_NormalOut);
            p.AddByte((byte)0);
            p.AddByte((byte)CardType.MonthRentCard);
            p.AddShort((short)1000);
            return p;
        }

        public Packet CreatePrepayCardExitWaitPacket(byte address, CardEventReport report)
        {
            //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音参数+1字节开闸模式(总为0)+1字节卡类型+2字节卡编号+
            //6字节临时卡入场秒分时日月年+6字节临时卡出场秒分时日月年+1字节费额小数点位数+2字节收费金额+
            //+1字节显示车型+1字节语音车型+4字节卡片余额     //5+6+6+1+2+1+1+4=26字节
            Packet p = new Packet();
            p.Address = address;
            p.Order = OrderCode.Comm_CardWait;
            if ((report.ParkingStatus & ParkingStatus.AsTempCard) == ParkingStatus.AsTempCard)
            {
                p.AddByte((byte)CardValidVoice.Voice_NoEnoughBalanceTempOut);
            }
            else
            {
                p.AddByte((byte)CardValidVoice.Voice_PrepayOut);
            }
            p.AddByte((byte)0);
            p.AddByte((byte)CardType.PrePayCard);
            p.AddShort((short)1000);
            p.AddDateTime(report.LastDateTime.Value);
            p.AddDateTime(report.EventDateTime);
            p.AddByte(TariffSetting.Current.TariffOption.PointCount);
            p.AddShort((short)TariffSetting.Current.TariffOption.FromYuan(report.CardPaymentInfo.Accounts));
            p.AddByte((byte)((byte)report.CarType + 1));  //车型为0-3 对应的车型语音为1-4
            p.AddByte((byte)((byte)report.CarType + 1));
            p.AddInt(TariffSetting.Current.TariffOption.FromYuan(report.Balance));
            return p;
        }

        public Packet CreateTempCardExitWaitPacket(byte address, CardEventReport report)
        {
            //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音参数+1字节开闸模式(总为0)+1字节卡类型+2字节卡编号+
            //6字节临时卡入场秒分时日月年+6字节临时卡出场秒分时日月年+1字节费额小数点位数+2字节收费金额+1字节显示车型+1字节语音车型
            Packet p = new Packet();
            p.Address = address;
            p.Order = OrderCode.Comm_CardWait;
            p.AddByte((byte)CardValidVoice.Voice_TempCardOut);
            p.AddByte((byte)0);
            p.AddByte((byte)CardType.TempCard);
            p.AddShort((short)1000);
            p.AddDateTime(report.LastDateTime.Value);
            p.AddDateTime(report.EventDateTime);
            p.AddByte(TariffSetting.Current.TariffOption.PointCount);
            p.AddShort((short)TariffSetting.Current.TariffOption.FromYuan(report.CardPaymentInfo.Accounts));
            p.AddByte(GetVoice(report.CarType));  //车型为0-3 对应的车型语音为1-4
            p.AddByte(GetVoice(report.CarType));
            return p;
        }

        public Packet CreateMonthCardExitValidPacket(byte address, DateTime expireDT)
        {
            //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音显示参数+1字节内屏显示模式参数(总为0)+1字节卡类型+2字节卡编号+3字节月卡有效期日月年
            Packet p = new Packet();
            p.Order = OrderCode.Comm_CardValid;
            p.Address = address;
            p.AddByte(CardValidVoice.Voice_NormalOut);
            p.AddByte((byte)0);
            p.AddByte((byte)CardType.MonthRentCard);
            p.AddShort((short)1000);
            p.AddBytes(DateToBytes(expireDT));
            return p;
        }

        public Packet CreatePrepayCardExitValid(byte address, DateTime lastDT, DateTime eventDT, byte carType, decimal balance)
        {
            //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音参数+1字节开闸模式(总为0)+1字节卡类型+2字节卡编号+
            //6字节临时卡入场秒分时日月年+6字节临时卡出场秒分时日月年+1字节费额小数点位数+2字节收费金额+
            //+1字节显示车型+1字节语音车型+4字节卡片余额     //5+6+6+1+2+1+1+4=26字节
            Packet p = new Packet();
            p.Address = address;
            p.Order = OrderCode.Comm_CardValid;
            p.AddByte(CardValidVoice.Voice_NormalOut);
            p.AddByte((byte)0);
            p.AddByte((byte)CardType.PrePayCard);
            p.AddShort((short)1000);
            p.AddDateTime(lastDT);
            p.AddDateTime(eventDT);
            p.AddByte(TariffSetting.Current.TariffOption.PointCount);
            p.AddShort((short)TariffSetting.Current.TariffOption.FromYuan(0));
            p.AddByte(GetVoice(carType));  //车型为0-3 对应的车型语音为1-4
            p.AddByte(GetVoice(carType));
            p.AddInt(TariffSetting.Current.TariffOption.FromYuan(balance));
            return p;
        }

        public Packet CreateTempCardExitValidPacket(byte address, DateTime lastDT, DateTime eventDT, byte carType)
        {
            //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音参数+1字节开闸模式(总为0)+1字节卡类型+2字节卡编号+
            //6字节临时卡入场秒分时日月年+6字节临时卡出场秒分时日月年+1字节费额小数点位数+2字节收费金额+1字节显示车型+1字节语音车型
            Packet p = new Packet();
            p.Address = address;
            p.Order = OrderCode.Comm_CardValid;
            p.AddByte((byte)CardValidVoice.Voice_NormalOut);
            p.AddByte((byte)0);
            p.AddByte((byte)CardType.TempCard);
            p.AddShort((short)1000);
            p.AddDateTime(lastDT);
            p.AddDateTime(eventDT);
            p.AddByte(TariffSetting.Current.TariffOption.PointCount);
            p.AddShort((short)TariffSetting.Current.TariffOption.FromYuan(0));
            p.AddByte(GetVoice(carType));  //车型为0-3 对应的车型语音为1-4
            p.AddByte(GetVoice(carType));
            return p;
        }

        public Packet CreateCardInvalidPacket(byte address, EventInvalidType invalidType, object param)
        {
            if (invalidType == EventInvalidType.INV_OverDate && param is DateTime)
            {
                //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音显示参数+1字节内屏显示模式参数(总为0)+1字节卡类型+2字节卡编号+年月日
                Packet p = new Packet();
                p.Order = OrderCode.Comm_CardInvalid;
                p.Address = address;
                p.AddByte(GetHardwareInvalidType(invalidType));
                p.AddByte(0);
                p.AddByte(0);
                p.AddShort((short)1000);
                p.AddDate((DateTime)param);
                return p;
            }
            else if (invalidType == EventInvalidType.INV_Balance && param is decimal)
            {
                //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音显示参数+1字节内屏显示模式参数(总为0)+1字节卡类型+2字节卡编号+余额
                Packet p = new Packet();
                p.Order = OrderCode.Comm_CardInvalid;
                p.Address = address;
                p.AddByte(GetHardwareInvalidType(invalidType));
                p.AddByte(0);
                p.AddByte(0);
                p.AddShort((short)1000);
                p.AddInt(TariffSetting.Current.TariffOption.FromYuan((decimal)param));
                return p;
            }
            else
            {
                //卡等待/有效/无效指令+参数长度(采用可变长度)+1字节语音显示参数+1字节内屏显示模式参数(总为0)+1字节卡类型+2字节卡编号
                Packet p = new Packet();
                p.Order = OrderCode.Comm_CardInvalid;
                p.Address = address;
                p.AddByte(GetHardwareInvalidType(invalidType));
                p.AddByte(0);
                p.AddByte(0);
                p.AddShort((short)1000);
                return p;
            }
        }

        public Packet CreateLEDDisplayPacket(byte address, byte ledAddress, string msg, bool permanent)
        {
            Packet order = new Packet();
            if (address > 1)
            {
                order.Address = address;
                order.Source = ledAddress;
            }
            else  //1号机LED板显示时地址为LED板地址
            {
                order.Address = ledAddress;
                order.Source = 0;
            }

            if (permanent)  //永久显示时广播到所有的控制器
            {
                order.Order = OrderCode.LEDR_StoreSentance;
                order.Parameters.Add(0);
            }
            else
            {
                order.Order = OrderCode.LED_Display;
            }
            order.Parameters.AddRange(StringToBytes(msg));
            return order;
        }

        public Packet CreateGateOperatePacket(byte address, GateOperation action)
        {
            Packet order = new Packet();
            order.Address = address;
            order.Order = OrderCode.Comm_GateOperationNotify;
            order.Parameters.AddRange(GetGateOperationParas(action));
            return order;
        }

        public List<Packet> CreateInsertCardPackets(byte address, CardInfo card)
        {
            List<Packet> packets = new List<Packet>();
            Packet packet = new Packet();
            packet.Address = address;
            packet.Order = OrderCode.Comm_DownLoadCardNotify;
            packet.Parameters.AddRange(SEBinaryConverter.ShortToBytes((short)card.Index));
            packet.Parameters.AddRange(SEBinaryConverter.ShortToBytes((short)(card.Index + 1)));

            packets.Add(packet);
            packet = FormatCard(address, card, ActionType.Add);
            packets.Add(packet);
            return packets;
        }

        public Packet CreateUpdateCardPacket(byte address, CardInfo card)
        {
            return FormatCard(address, card, ActionType.Upate);
        }

        public Packet CreateTestCardPacket(byte address, CardType cardType, int cardID)
        {
            Packet packet = new Packet();
            packet.Address = address;
            packet.Order = OrderCode.SMMR_TestOneCard;
            packet.Parameters.Add(address);
            packet.Parameters.Add(0xB1);  //weigan1
            packet.Parameters.Add((byte)cardType);
            packet.AddInt(cardID);
            return packet;
        }
        #endregion

        #region 用户设置
        //格式化面板设置 参数长度129字节,下载页号14
        public Packet CreateBordSettingPacket(byte address, EntranceInfo entrance)
        {
            Packet p = new Packet();
            p.Order = OrderCode.Comm_WriteParameterTable;
            p.Address = address;
            p.AddByte((byte)14);   //下载页号
            byte readerMode = 0;
            if (entrance.ReadAndTakeCardNeedCarSense)    //压地感读卡(字节第三位为1)
            {
                readerMode = 4;
            }
            for (int i = 0; i < 9; i++)  //9个读头设置
            {
                p.AddByte(readerMode);
            }
            p.AddByte((byte)4);
            p.AddByte((byte)4);
            p.AddByte((byte)4);  //压地感取卡

            if (entrance.LightEnable)  //压地感开补光灯
            {
                p.AddByte((byte)4);
                p.AddByte((byte)4);
            }
            else
            {
                p.AddByte((byte)0);
                p.AddByte((byte)0);
            }
            p.AddByte((byte)0);
            p.AddByte((byte)0);

            //16字节，单字节二进制功能杂项变通选项
            byte[] bytes = new byte[16];
            if (entrance.LightEnable)
            {
                bytes[3] = 1;
            }
            p.AddBytes(bytes);

            //-------------16字节，单字节100ms选项
            p.AddByte((byte)2);   //地感1检测稳定时间 
            p.AddByte((byte)2);   //地感2检测稳定时间
            p.AddByte((byte)2);   //按钮1检测稳定时间
            p.AddByte((byte)2);   //按钮2检测稳定时间
            p.AddByte(0);  //取卡间隔 
            p.AddByte((byte)(entrance.ReadCardInterval * 10));  //同一张卡读卡间隔
            p.AddBytes(new byte[10]);         //现不用

            //'------------16字节，双字节参数
            p.AddBytes(new byte[16]);

            //'------------32字节，四字节时段参数选项
            if (entrance.LightEnable)
            {
                p.AddTimeEntity(new TimeEntity(0, 0));
                p.AddTimeEntity(new TimeEntity(23, 59));
                p.AddBytes(new byte[28]);
            }
            else
            {
                p.AddBytes(new byte[32]);
            }

            // '-------------32字节，双字节二进制开闸模式
            p.AddBytes(new byte[32]);
            return p;
        }
        #endregion

        #region 费率设置

        public List<Packet> CreateDownloadTariffSettingPacket(byte address, TariffSetting ts)
        {
            byte index = 19;
            List<Packet> packets = new List<Packet>();
            Packet packet = FormatTariffOptions(ts.TariffOption);
            packets.Add(packet);
            //for (int tariffType = 0; tariffType < 4; tariffType++)
            //{
            //    for (int carType = 0; carType < 4; carType++)
            //    {
            //        index += (byte)(tariffType * 4 + carType);
            //        //普通收费
            //        TariffBase tariff = ts.GetTariff(carType, (TariffType)tariffType);
            //        if (tariff != null)
            //        {
            //            packet = FormatTariff(tariff, index, ts.TariffOption);
            //            if (packet != null) packets.Add(packet);
            //        }
            //    }
            //}
            return packets;
        }

        //参数长度为161字节
        private Packet FormatTariffOptions(TollOptionSetting tollOption)
        {
            Packet packet = new Packet();
            packet.Order = OrderCode.Comm_WriteParameterTable;
            packet.Address = CanAddress.HostEntrance;

            packet.AddByte(18);                      //下载表号18
            packet.AddByte(0);                       //不可更改
            packet.AddByte(tollOption.PointCount);   //小数点位数
            packet.AddByte(2); //临时卡可以切换的车型数量
            packet.AddByte(0);                      //为1表示收费需要等待车型确认
            packet.AddByte(0);                      //为1表示免费仍要等待车型确认
            packet.AddByte(2);                      //不可更改！
            packet.AddBytes(new byte[161 - 7]);
            return packet;
        }

        private Packet FormatTariff(TariffBase tariff, int index, TollOptionSetting tos)
        {
            if (tariff is TariffPerDay)
            {
                return FormatTariffPerDay(tariff as TariffPerDay, index, tos);
            }
            else if (tariff is TariffPerTime)
            {
                return FormatTariffPerTime(tariff as TariffPerTime, index, tos);
            }
            else if (tariff is TariffOfLimitation)
            {
                return FormatTariffOfLimitation(tariff as TariffOfLimitation, index, tos);
            }
            return null;
        }

        private Packet FormatTariffOfLimitation(TariffOfLimitation tariff, int index, TollOptionSetting tos)
        {
            Packet packet = new Packet();
            packet.Order = OrderCode.Comm_WriteParameterTable;
            packet.Address = CanAddress.HostEntrance;

            packet.AddByte((byte)index); //下载而号
            //16字节 单字节选项
            packet.AddByte(0x0);
            packet.AddByte(GetVoice(tariff.CarType));
            packet.AddByte(GetVoice(tariff.CarType));
            packet.AddBytes(new byte[13]);
            //以下为公共收费参数 16字节
            packet.AddByte(0x0); //为1表示无入场记录按次收费
            packet.AddShort(0x0); //无入场记录按上次出场时间计算免费时间(分钟)
            packet.AddShort(0x0); //无入场记录按次收费金额
            packet.AddByte(0x0);   //备用
            packet.AddShort(tariff.FreeMinutes); //入场免费时间
            packet.AddShort(0x0);   //免费零头时间
            packet.AddShort(0x0);   //中央收费后免费时间
            if (tariff.FeeOf12Hour > 0)
            {
                packet.AddShort(12 * 60);  //公用的周期限额计费时间(小时)（12小时)
                packet.AddShort((short)(tos.FromYuan(tariff.FeeOf12Hour)));
            }
            else
            {
                packet.AddShort(24 * 60);  //公用的周期限额计费时间(小时)（24小时)
                packet.AddShort((short)(tos.FromYuan(tariff.FeeOf24Hour)));
            }
            //不分时段普通收费参数16byte
            packet.AddByte(0x03); //计费模式 3=仅一次入场收费
            packet.AddBytes(new byte[3]);
            packet.AddShort((short)(tariff.FirstCharge == null ? 0 : tariff.FirstCharge.Minutes));  //入场计费时间
            packet.AddShort((short)tos.FromYuan(tariff.FirstCharge == null ? 0 : tariff.FirstCharge.Fee)); //入场收费金额
            packet.AddShort(tariff.RegularCharge.Minutes);  //单位时间收费：计费时间(分钟)
            packet.AddShort((short)(tos.FromYuan(tariff.RegularCharge.Fee))); //单位时间收费：计费金额
            packet.AddShort(0x0);  //备用1
            packet.AddShort(0x0);  //备用2
            ////下面为空
            packet.AddBytes(new byte[240 - 49]);  //参数总共240字节，
            return packet;
        }

        private Packet FormatTariffPerDay(TariffPerDay tariff, int index, TollOptionSetting tos)
        {
            Packet packet = new Packet();
            packet.Order = OrderCode.Comm_WriteParameterTable;
            packet.Address = CanAddress.HostEntrance;

            packet.AddByte((byte)index); //下载而号
            //16字节 单字节选项
            packet.AddByte(0x0);
            packet.AddByte(GetVoice(tariff.CarType));
            packet.AddByte(GetVoice(tariff.CarType));
            packet.AddBytes(new byte[13]);
            //以下为公共收费参数 16字节
            packet.AddByte(0x0); //为1表示无入场记录按次收费
            packet.AddShort(0x0); //无入场记录按上次出场时间计算免费时间(分钟)
            packet.AddShort(0x0); //无入场记录按次收费金额
            packet.AddByte(0x0);   //备用
            packet.AddShort(tariff.FreeMinutes); //入场免费时间
            packet.AddShort(0x0);   //免费零头时间
            packet.AddShort(0x0);   //中央收费后免费时间
            packet.AddShort((short)(24 * 60));  //公用的周期限额计费时间(小时)（24小时)
            packet.AddShort((short)(tos.FromYuan(tariff.FeePerDay)));
            //不分时段普通收费参数16byte
            packet.AddByte(0x02); //计费模式 2按天
            packet.AddBytes(new byte[15]);
            //下面为空
            packet.AddBytes(new byte[240 - 49]);  //参数总共240字节，
            return packet;
        }

        private Packet FormatTariffPerTime(TariffPerTime tariff, int index, TollOptionSetting tos)
        {
            Packet packet = new Packet();
            packet.Order = OrderCode.Comm_WriteParameterTable;
            packet.Address = CanAddress.HostEntrance;

            packet.AddByte((byte)index); //下载而号
            //16字节 单字节选项
            packet.AddByte(0x0);
            packet.AddByte(GetVoice(tariff.CarType));
            packet.AddByte(GetVoice(tariff.CarType));
            packet.AddBytes(new byte[13]);
            //以下为公共收费参数 16字节
            packet.AddByte(0x0); //为1表示无入场记录按次收费
            packet.AddShort(0x0); //无入场记录按上次出场时间计算免费时间(分钟)
            packet.AddShort(0x0); //无入场记录按次收费金额
            packet.AddByte(0x0);   //备用
            packet.AddShort(tariff.FreeMinutes); //入场免费时间
            packet.AddShort(0x0);   //免费零头时间
            packet.AddShort(0x0);   //中央收费后免费时间
            packet.AddShort(0x0);  //公用的周期限额计费时间(小时)（24小时)
            packet.AddShort(0x0);
            //不分时段普通收费参数16byte
            packet.AddByte(0x01); //计费模式 1按次
            packet.AddBytes(new byte[3]);
            packet.AddShort(0x0);  //入场计费时间
            packet.AddShort((short)tos.FromYuan(tariff.FeePerTime));  //入场收费金额
            packet.AddShort(0x0);  //单位时间收费：计费时间(分钟)
            packet.AddShort(0x0); //单位时间收费：计费金额
            packet.AddShort(0x0);  //备用1
            packet.AddShort(0x0);  //备用2
            ////下面为空
            packet.AddBytes(new byte[240 - 49]);  //参数总共240字节，
            return packet;
        }
        #endregion

        #region 车位总数/余数设置
        //设置车位上下限
        public Packet CreateSetCarPortLimitationPacket(byte address, short upLimit, short downLimit)
        {
            Packet packet = new Packet();
            packet.Order = OrderCode.Comm_WriteParameterTable;
            packet.Parameters.Add((byte)9);          //页号

            packet.Parameters.Add(1);      //分车场数量
            packet.Parameters.Add(0);      //备用字节
            //总车位上下限
            packet.Parameters.AddRange(SEBinaryConverter.ShortToBytes(upLimit));
            packet.Parameters.AddRange(SEBinaryConverter.ShortToBytes(downLimit));
            //分车场车位上下限
            packet.Parameters.AddRange(SEBinaryConverter.ShortToBytes(upLimit));
            packet.Parameters.AddRange(SEBinaryConverter.ShortToBytes(downLimit));
            return packet;
        }

        public Packet CreateSetVacantPacket(byte address, short vacant)
        {
            //设置车位余数
            Packet packet = new Packet();
            packet.Order = OrderCode.Comm_SetVacantNotify;
            //分区位余
            packet.Parameters.AddRange(SEBinaryConverter.ShortToBytes(vacant));
            return packet;
        }

        public Packet CreateSetVacantTextPacket(byte address, string vacantText)
        {
            //车位余字符设置
            Packet packet = new Packet();
            packet.Order = OrderCode.Comm_WriteParameterTable;
            packet.AddByte(10);  //下载页号
            byte[] data = System.Text.UnicodeEncoding.GetEncoding("GB2312").GetBytes(vacantText);
            byte vl = (byte)(data.Length <= 10 ? data.Length : 10);
            packet.AddByte(vl);
            packet.AddByte((byte)(10 - vl));
            packet.AddByte((byte)2);
            packet.AddByte((byte)0);
            byte[] d = new byte[20];
            Array.Copy(data, d, vl);
            packet.AddBytes(d);
            return packet;
        }

        public Packet CreateSetFullTextPacket(byte address, string fullText)
        {
            //满位字符设置
            Packet packet = new Packet();
            packet.Order = OrderCode.Comm_WriteParameterTable;
            packet.AddByte(11);  //下载页号
            byte[] data = System.Text.UnicodeEncoding.GetEncoding("GB2312").GetBytes(fullText);
            packet.AddByte((byte)data.Length);
            packet.AddByte(0);
            packet.AddByte(0);
            packet.AddByte(0);
            byte[] d1 = new byte[20];
            Array.Copy(data, d1, data.Length >= 20 ? 20 : data.Length);
            packet.AddBytes(d1);
            return packet;
        }

        public List<Packet> CreateDownloadCarPortSettingPacket(byte address, CarPortSetting cps)
        {
            List<Packet> packets = new List<Packet>();
            packets.Add(CreateSetCarPortLimitationPacket(address, cps.CarPortUpLimit, cps.CarPortDownLimit));
            packets.Add(CreateSetVacantPacket(address, cps.VacantPort));
            packets.Add(CreateSetFullTextPacket(address, cps.ParkFullText));
            packets.Add(CreateSetVacantTextPacket(address, cps.VacantText));
            return packets;
        }
        #endregion
    }
}

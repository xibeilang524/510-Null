using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ralid.Park.BusinessModel.Interface;
using Ralid.GeneralLibrary;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.LOG;

namespace Ralid.Park.ParkService.CANParking
{
    public delegate void ReportReceivedHandler(object sender, ReportBase  report);

    /// <summary>
    /// 串口通信组件
    /// </summary>
    public class CommComponent
    {
        #region 事件
        /// <summary>
        /// 接收数据
        /// </summary>
        public event ReportReceivedHandler ReportReceviced;
        #endregion 事件

        #region 成员变量
        private PacketStream _streamHelper = null;
        private CommPort _commPort;
        private object _commportLock = new object();
        #endregion 成员变量

        #region 构造函数
        public CommComponent(byte portNum, string settings, short rThreshold)
        {
            _commPort = new CommPort(portNum, settings, rThreshold);
            _commPort.OnDataArrivedEvent += new DataArrivedDelegate(_commPort_OnDataArrivedEvent);
            this._streamHelper = new PacketStream();
            Open();
        }

        public CommComponent(byte portNum)
            : this(portNum, "9600, n, 8, 1", 1)
        {
        }
        #endregion 构造方法

        #region 私有方法
        /// <summary>
        /// 接收串口数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="_data"></param>
        private void _commPort_OnDataArrivedEvent(object sender, byte[] data)
        {
            if (data != null && data.Length > 0)
            {
                this._streamHelper.Append(data);
                Packet packet = _streamHelper.GetAPacket();
                while (packet != null)
                {
                    if (ReportReceviced != null)
                    {
                        ReportBase report = DeformatPacket(packet);
                        if (report != null)
                        {
                            this.ReportReceviced(this, report);
                        }
                    }
                    packet = _streamHelper.GetAPacket();
                }
            }
        }

        /// <summary>
        /// 从字节数组生成时间
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="part">字节数组中包涵的信息,1表示只有日期信息,2表示只有时间信息,0表示同时有日期和时间</param>
        /// <returns></returns>
        private DateTime BytesToDateTime(byte[] bytes)
        {

            if (bytes.Length == 6)
            {
                try
                {
                    return new DateTime(2000 + bytes[5], bytes[4], bytes[3], bytes[2], bytes[1], bytes[0]);
                }
                catch
                {
                    return new DateTime(2000, 1, 1);
                }
            }
            else
            {
                throw new InvalidCastException(string.Format("时间数组大小应该为6,但转换的数据大小为{0}!", bytes.Length));
            }
        }

        private EventInvalidType GetInvalidType(byte invalidType)
        {
            switch (invalidType)
            {
                case 0:
                    return EventInvalidType.INV_UnRegister;  //此卡未登记
                case 4:
                    return EventInvalidType.INV_InvalidImg;  //图像有差异
                case 5:
                    return EventInvalidType.INV_Recycled; //此卡已注销
                case 6:
                    return EventInvalidType.INV_Loss; //此卡已锁定
                case 7:
                    return EventInvalidType.INV_Lock; //此卡已挂失
                case 8:
                    return EventInvalidType.INV_Type; //非停车卡类
                case 9:
                    return EventInvalidType.INV_Invalid; //未准入本场
                case 10:
                    return EventInvalidType.INV_HaveIn; //此卡已入场
                case 11:
                    return EventInvalidType.INV_StillOut;  //此卡已出场
                case 13:
                    return EventInvalidType.INV_ParkFull;  //车位已满
                case 14:
                    return EventInvalidType.IVN_NotPaid; //未缴费
                case 15:
                    return EventInvalidType.INV_OverTime;  //超时补交费
                case 16:
                    return EventInvalidType.INV_OverDate; //此卡已过期
                default:
                    return EventInvalidType.INV_Invalid;
            }
        }
        #endregion 私有方法

        #region 解析包
        private ReportBase DeformatPacket(Packet packet)
        {
            try
            {
                switch (packet.Order)
                {
                    case OrderCode .Comm_OnLine:         //工作方式+1字节参数,参数=1=软件复位/5=在线查询
                        return DeformatDeviceResetPacket(packet);
                    case OrderCode.Comm_Sense:              //地感
                        return DeformatCarSensePacket(packet);
                    case OrderCode.CPMT_CardButt:          //按取卡按钮
                        return DeformatCardButtonPacket(packet);
                    case OrderCode.CCMT_Cap:               //收卡机收卡一张
                        return DeformatCardCapturePacket(packet);
                    case OrderCode.CPMT_CardOut:   //出卡机出卡一张
                        return DeformatCardTakeoutPacket(packet);
                    case OrderCode.RWMT_CardID:           //控制器读到卡片
                        return DeformatCardReadReportPacket(packet);
                    case OrderCode.Comm_CarPortReport:     //上报车位余数
                        return DeformatParkVacantPacket(packet);
                    case OrderCode.Comm_CardWaitingEvent:        //预处理卡片事件 0x50
                        return DeformatCardWaitingPacket(packet);
                    case OrderCode.Comm_CardPermitedEvent:       //刷卡事件明细 0x51
                        return DeformatCardPermitedPacket(packet);
                    case OrderCode.Comm_CardInvalid:        //无效刷卡 0x64
                        return DeformatCardInvalidPacket(packet);
                    case OrderCode .COMT_CommandEcho :
                        return DeformatCommandEchoPakcet(packet);
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        private ReportBase DeformatDeviceResetPacket(Packet packet)
        {
            byte para = packet.ReadByteFromParameter(0);
            if (para == 1)
            {
                DeviceResetReport report = new DeviceResetReport();
                report.Address = (byte)(packet.Address == 0 ? 1 : packet.Address);
                return report;
            }
            else if (para == 5) //查询状态
            {
                EntranceStatusReport report1 = new EntranceStatusReport();
                report1.Address = (byte)(packet.Address == 0 ? 1 : packet.Address);
                report1.Status = EntranceStatus.Ok;
                return report1;
            }
            return null;
        }

        private ReportBase DeformatCarSensePacket(Packet packet)
        {
            CarSenseReport report = new CarSenseReport();
            report.Address = packet.Address;
            report.Loop = 1;
            report.InOrOutFlag = (byte)(packet.Parameters[0] == 0 ? 0 : 1);
            return report;
        }

        private ReportBase DeformatCardButtonPacket(Packet packet)
        {
            ButtonClickedReport report = new ButtonClickedReport();
            report.Address = packet.Address;
            if (packet.Order == OrderCode.CPMT_CardButt) report.Button = 1;
            if (packet.Order == OrderCode.CPMT_CardButt2) report.Button = 2;
            return report;
        }

        private ReportBase DeformatCardCapturePacket(Packet packet)
        {
            CardCaptureReport report = new CardCaptureReport();
            report.Address = packet.Address;
            return report;
        }

        private ReportBase DeformatCardTakeoutPacket(Packet packet)
        {
            CardTakeoutReport report = new CardTakeoutReport();
            report.Address = packet.Address;
            return report;
        }

        private ReportBase DeformatCardReadReportPacket(Packet packet)
        {
            //上传卡号+CardKind+4bytesID
            CardReadReport report = new CardReadReport();
            report.Address = packet.Address;
            if (packet.Source == 0xb1) report.Reader = EntranceReader.Reader1;
            if (packet.Source == 0xb2) report.Reader = EntranceReader.Reader2;
            report.CardType = packet.ReadByteFromParameter(0);
            report.CardID = ((uint)(SEBinaryConverter.BytesToInt(packet.ReadDataFromParameter(1, 4)))).ToString();
            return report;
        }

        private ReportBase DeformatCardWaitingPacket(Packet packet)
        {
            //正在等待处理的事件
            //口地址[1]+上次地址[1]+事件状态[1]+停车状态[1]+事件参数[1]+发生卡内码[4]+上次动作日期时间秒分时日月年[6]+发生日期时间秒分时日月年[6]+
            //操作员编号[1]+小数点位数[1]+交易金额[2]+储值卡余额[4]
            OfflineCardReadReport report = new OfflineCardReadReport();
            report.Address = packet.ReadByteFromParameter(0);
            report.CardID = ((uint)SEBinaryConverter .BytesToInt ( packet.ReadDataFromParameter(5, 4))).ToString ();
            report.EventStatus = CardEventStatus.Pending;
            report.LastDateTime = BytesToDateTime(packet.ReadDataFromParameter(9, 6));
            report.EventDateTime = BytesToDateTime(packet.ReadDataFromParameter(15, 6));
            return report;
        }

        private ReportBase DeformatCardPermitedPacket(Packet packet)
        {
            //已经确认的事件
            //格式：事件序列号（4byte）＋口地址(1byte)＋上次地址（1byte）＋事件状态（1byte）＋停车状态（1byte）＋事件参数（1byte）＋发生卡内码（4byte）＋
            //发生日期时间秒分时日月年（6byte）＋操作员编号(1byte)＋ 交易金额(2byte)＋储值卡余额(4byte)＋CRC
            OfflineCardReadReport report = new OfflineCardReadReport();
            report.Address = packet.ReadByteFromParameter(4);
            report.CardID = ((uint)SEBinaryConverter.BytesToInt(packet.ReadDataFromParameter(9, 4))).ToString();
            report.EventStatus = CardEventStatus.Valid;
            report.EventDateTime = BytesToDateTime(packet.ReadDataFromParameter(13, 6));
            return report;
        }

        private ReportBase DeformatCardInvalidPacket(Packet packet)
        {
            CardInvalidEventReport report = new CardInvalidEventReport();
            report.Address = packet.Address;
            report.InvalidType = GetInvalidType(packet.Parameters[0]);
            try
            {
                if (report.InvalidType == EventInvalidType.INV_UnRegister) //卡片未注册
                {
                    report.CardID = ((uint)SEBinaryConverter.BytesToInt(packet.ReadDataFromParameter(1, 4))).ToString();
                }
            }
            catch
            {
            }
            return report;
        }

        private ReportBase DeformatParkVacantPacket(Packet packet)
        {
            ParkVacantReport report = new ParkVacantReport();
            report.Address = packet.Address;
            if (packet.Parameters.Count % 2 == 0)
            {
                for (int i = 0; i < packet.Parameters.Count; i += 2)
                {
                    byte[] data = packet.ReadDataFromParameter(i, 2);
                    report.ParkVacant += SEBinaryConverter.BytesToShort(data);
                }
            }
            return report;
        }

        private ReportBase DeformatCommandEchoPakcet(Packet packet)
        {
            CommandEchoReport report = new CommandEchoReport();
            report.Address = packet.Address;
            return report;
        }
        #endregion

        #region 公开方法与属性
        /// <summary>
        /// 串口状态
        /// </summary>
        public bool IsOpened
        {
            get { return _commPort.PortOpened; }
        }

        /// <summary>
        /// 把消息发送到硬件
        /// </summary>
        /// <param name="packet"></param>
        public void SendPacket(Packet packet)
        {
            byte[] data = packet.ToCommandBytes();
            if (data != null)
            {
                lock (_commportLock)
                {
                    this._commPort.SendData(data);
                }
            }
        }

        /// <summary>
        /// 打开串口通讯设备
        /// </summary>
        /// <returns></returns>
        public void Open()
        {
            _commPort.Open();
        }

        /// <summary>
        /// 关闭串口通讯设备
        /// </summary>
        public void Close()
        {
            if (this._commPort != null)
            {
                this._commPort.Close();
            }
        }
        #endregion 公开方法
    }
}

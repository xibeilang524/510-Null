using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.Park.BusinessModel.Model
{
    [Serializable()]
    [DataContract]
    public class EntranceInfo
    {
        #region 重载操作符
        public static bool operator ==(EntranceInfo e1, EntranceInfo e2)
        {
            return object.Equals(e1, e2);
        }

        public static bool operator !=(EntranceInfo e1, EntranceInfo e2)
        {
            return !object.Equals(e1, e2);
        }
        #endregion

        #region 构造函数
        public EntranceInfo()
        {
            _VideoSources = new List<VideoSourceInfo>();
        }
        #endregion

        #region 私有变量
        [DataMember(Name = "VideoSources")]
        private List<VideoSourceInfo> _VideoSources;
        /// <summary>
        /// 卡片类型属性
        /// </summary>
        [DataMember(Name = "CardTypeProperty")]
        private string _CardTypeProperty;
        /// <summary>
        /// 此通道车牌识别结果需发送的控制器IP
        /// </summary>
        [DataMember(Name = "CarPlateNotifyIP")]
        public string _CarPlateNotifyIP;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置控制器ID
        /// </summary>
        [DataMember(Name = "EntranceID")]
        public int EntranceID { get; set; }

        /// <summary>
        /// 获取或设置控制器名
        /// </summary>
        [DataMember(Name = "EntranceName")]
        public string EntranceName { get; set; }

        /// <summary>
        /// 获取或设置通道的地址(仅用于CAN控制板)
        /// </summary>
        [DataMember]
        public int Address { get; set; }

        /// <summary>
        /// 获取或设置控制器IP地址(用于网络控制板)
        /// </summary>
        [DataMember(Name = "IPAddress")]
        public string IPAddress { get; set; }

        /// <summary>
        /// 获取或设置子网掩码(用于网络控制板)
        /// </summary>
        [DataMember(Name = "IPMask")]
        public string IPMask { get; set; }

        /// <summary>
        /// 获取或设置网关地址(用于网络控制板)
        /// </summary>
        [DataMember(Name = "Gateway")]
        public string Gateway { get; set; }

        /// <summary>
        /// 获取或设置主控制器IP(用于网络控制板)
        /// </summary>
        [DataMember(Name = "MasterIP")]
        public string MasterIP { get; set; }

        /// <summary>
        /// 获取或设置控制端口号(用于网络控制板)
        /// </summary>
        [DataMember(Name = "ControlPort")]
        public int ControlPort { get; set; }

        /// <summary>
        /// 获取或设置事件端口号(用于网络控制板)
        /// </summary>
        [DataMember(Name = "EventPort")]
        public int EventPort { get; set; }

        /// <summary>
        /// 获取或设置MAC地址(用于网络控制板)
        /// </summary>
        [DataMember(Name = "MACAddress")]
        public string MACAddress { get; set; }

        /// <summary>
        /// 获取或设置读卡/取卡间隔
        /// </summary>
        [DataMember(Name = "ReadCardInterval")]
        public int ReadCardInterval { get; set; }

        /// <summary>
        /// 获取或设置控制器的停车场ID
        /// </summary>
        [DataMember(Name = "ParkID")]
        public int ParkID { get; set; }

        /// <summary>
        /// 获取或设置通道的最上层停车场ID
        /// </summary>
        [DataMember(Name = "RootParkID")]
        public int RootParkID { get; set; }

        /// <summary>
        /// 获取或设置条码打印机的串口号
        /// </summary>
        [DataMember(Name = "TicketPrinterCOMPort")]
        public byte TicketPrinterCOMPort { get; set; }

        /// <summary>
        /// 获取或设置条码枪的串口号
        /// </summary>
        [DataMember(Name = "TicketReaderCOMPort")]
        public byte TicketReaderCOMPort { get; set; }

        /// <summary>
        /// 获取或设置控制器的状态
        /// </summary>
        [DataMember(Name = "Status")]
        public EntranceStatus Status { get; set; }

        /// <summary>
        /// 获取或设置控制器中的临时卡数量
        /// </summary>
        [DataMember(Name = "TempCard")]
        public int TempCard { get; set; }

        /// <summary>
        /// 获取或设置引起系统报警的发卡机临时卡临界数量(临时卡数量等于或低于此数量时系统发出语音报警提示）
        /// </summary>
        [DataMember]
        public int MinTempCard { get; set; }

        /// <summary>
        /// 获取或设置工作模式
        /// </summary>
        [DataMember(Name = "WorkMode")]
        public EntranceWorkmodeOption WorkMode;

        /// <summary>
        /// 获取或设置控制板已读取到的缴费事件记录的流水号
        /// </summary>
        [DataMember(Name = "PaymentEventIndex")]
        public int PaymentEventIndex { get; set; }

        /// <summary>
        /// 获取或设置此通道的车牌识别一体机IP
        /// </summary>
        [DataMember]
        public string CarPlateIP { get; set; }

        /// <summary>
        /// 获取或设置此通道的车牌识别一体机视频号
        /// </summary>
        [DataMember]
        public int? VideoID { get; set; }

        /// <summary>
        /// 获取或设置此通道车牌识别结果需发送的控制器IP组
        /// </summary>
        public List<string> CarPlateNotifyIPs
        {
            get
            {
                List<string> ips = _CarPlateNotifyIP.Split(',').ToList();
                if (ips == null) ips = new List<string>();
                return ips;
            }
            set
            {
                string ips = string.Empty;
                if (value != null) ips = string.Join(",", value);
                _CarPlateNotifyIP = ips;
            }
        }

        /// <summary>
        /// 获取所有属于此控制器的视频的列表
        /// </summary>
        public List<VideoSourceInfo> VideoSources
        {
            get
            {
                return _VideoSources;
            }
        }

        /// <summary>
        /// 获取卡片类型属性字符串形式
        /// </summary>
        public string CardTypePropertyString
        {
            get
            {
                return _CardTypeProperty;
            }
        }
        /// <summary>
        /// 获取或设置卡片类型属性
        /// </summary>
        public ushort[] CardTypeProperty
        {
            get
            {
                //卡类型包括：0免费卡；1业主卡；2月租卡；3储值卡；4临时卡；5自定义卡1；6自定义卡2；其余预留。总共16种卡型
                ushort[] propertyBytes = new ushort[16];
                string[] propertyStr = null;
                if (!string.IsNullOrEmpty(_CardTypeProperty))
                {
                    propertyStr = _CardTypeProperty.Split(',');
                }
                for (int i = 0; i < 16; i++)
                {
                    ushort p = 0xFFFF;//默认设为全1
                    if (i == 3)//储值卡默认不允许在韦根读卡器刷卡，其余卡类型默认允许在韦根读卡器刷卡。
                    {
                        p ^= (byte)EntranceCardTypeProperty.EnabledWiegandReader;
                    }
                    else if (i == 4)//临时卡默认不允许在韦根读卡器刷卡，入口车牌写卡，其余卡类型默认允许在韦根读卡器刷卡，入口车牌不写卡。
                    {
                        p ^= (byte)EntranceCardTypeProperty.EnabledWiegandReader;
                        p ^= (byte)EntranceCardTypeProperty.EnterNotWriteCarPlate;
                    }
                    if (propertyStr != null
                        && i < propertyStr.Length)
                    {
                        ushort.TryParse(propertyStr[i], out p);
                    }
                    propertyBytes[i] = p;
                }
                return propertyBytes;
            }
            set
            {
                if (value != null && value.Length == 16)
                {
                    var pstr = from p in value
                               select p.ToString();
                    _CardTypeProperty = string.Join(",", pstr);
                }
            }
        }
        #endregion

        #region 工作模式属性
        /// <summary>
        /// 获取或设置是否是主控制器
        /// </summary>
        public bool IsMaster
        {
            get { return (WorkMode & EntranceWorkmodeOption.OPT_IsMaster) == EntranceWorkmodeOption.OPT_IsMaster; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_IsMaster;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_IsMaster;
            }
        }
        /// <summary>
        /// 是否是出口控制器
        /// </summary>
        public bool IsExitDevice
        {
            get
            {
                if (Address > 0) return Address % 2 == 1;  //ADDRESS>0表明是CAN控制板
                return (WorkMode & EntranceWorkmodeOption.OPT_IsExitDevice) == EntranceWorkmodeOption.OPT_IsExitDevice;
            }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_IsExitDevice;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_IsExitDevice;
            }
        }
        /// <summary>
        /// 获取或设置读/取卡是否要求压地感
        /// </summary>
        public bool ReadAndTakeCardNeedCarSense
        {
            get { return (WorkMode & EntranceWorkmodeOption.OPT_TakeCardNeedCarSense) == EntranceWorkmodeOption.OPT_TakeCardNeedCarSense; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_TakeCardNeedCarSense;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_TakeCardNeedCarSense;
            }
        }
        /// <summary>
        /// 获取或设置是否启用补光灯
        /// </summary>
        public bool LightEnable
        {
            get { return (WorkMode & EntranceWorkmodeOption.OPT_LightEnable) == EntranceWorkmodeOption.OPT_LightEnable; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_LightEnable;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_LightEnable;
            }
        }
        /// <summary>
        /// 允许取卡后刷卡,即发卡机吐卡后如果未读到卡片，允许用户自行刷卡，而不用再收回
        /// </summary>
        public bool AllowEjectCardWhithoutRead
        {
            get
            {
                return (WorkMode & EntranceWorkmodeOption.OPT_AllowEjectCardWhithoutRead) == EntranceWorkmodeOption.OPT_AllowEjectCardWhithoutRead;
            }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_AllowEjectCardWhithoutRead;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_AllowEjectCardWhithoutRead;
            }
        }
        /// <summary>
        /// 获取或设置是否禁止临时卡进出
        /// </summary>
        public bool DisableTempCard
        {
            get { return (WorkMode & EntranceWorkmodeOption.OPT_DisableTempCard) == EntranceWorkmodeOption.OPT_DisableTempCard; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_DisableTempCard;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_DisableTempCard;
            }
        }
        /// <summary>
        /// 车场满时禁止进场
        /// </summary>
        public bool ForbidWhenFull
        {
            get { return (WorkMode & EntranceWorkmodeOption.OPT_ForbidEnterWhenFull) == EntranceWorkmodeOption.OPT_ForbidEnterWhenFull; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_ForbidEnterWhenFull;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_ForbidEnterWhenFull;
            }
        }
        /// <summary>
        /// 禁止过期卡片使用
        /// </summary>
        public bool ForbidWhenCardExpired
        {
            get { return (WorkMode & EntranceWorkmodeOption.OPT_ForbidExitWhenCardExpired) == EntranceWorkmodeOption.OPT_ForbidExitWhenCardExpired; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_ForbidExitWhenCardExpired;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_ForbidExitWhenCardExpired;
            }
        }
        /// <summary>
        /// 出口收费
        /// </summary>
        public bool ExportCharge
        {
            get { return (WorkMode & EntranceWorkmodeOption.OPT_ExportCharge) == EntranceWorkmodeOption.OPT_ExportCharge; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_ExportCharge;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_ExportCharge;
            }
        }
        /// <summary>
        /// 不进行车位计数
        /// </summary>
        public bool NoParkingCount
        {
            get { return (WorkMode & EntranceWorkmodeOption.OPT_NoParkingCount) == EntranceWorkmodeOption.OPT_NoParkingCount; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_NoParkingCount;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_NoParkingCount;
            }
        }
        /// <summary>
        /// 控制器有效
        /// </summary>
        public bool Valid
        {
            get { return (WorkMode & EntranceWorkmodeOption.OPT_Valid) == EntranceWorkmodeOption.OPT_Valid; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OPT_Valid;
                if (!value) WorkMode -= EntranceWorkmodeOption.OPT_Valid;
            }
        }
        /// <summary>
        /// 获取或设置车场是否有余位显示屏
        /// </summary>
        public bool EnableParkvacantLed
        {
            get { return (WorkMode & EntranceWorkmodeOption.EnableParkvacantLed) == EntranceWorkmodeOption.EnableParkvacantLed; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.EnableParkvacantLed;
                if (!value) WorkMode -= EntranceWorkmodeOption.EnableParkvacantLed;
            }
        }
        /// <summary>
        /// 获取或设置出口收卡机内是否没有读卡器
        /// </summary>
        public bool NoReaderOnCardCaptuer
        {
            get { return (WorkMode & EntranceWorkmodeOption.NoReaderOnCardCaptuer) == EntranceWorkmodeOption.NoReaderOnCardCaptuer; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.NoReaderOnCardCaptuer;
                if (!value) WorkMode -= EntranceWorkmodeOption.NoReaderOnCardCaptuer;
            }
        }
        /// <summary>
        /// 获取或设置当按下取卡按钮时是否只响应临时卡读头的读卡事件
        /// </summary>
        public bool OnlyTempReaderAfterButtonClick
        {
            get { return (WorkMode & EntranceWorkmodeOption.OnlyTempReaderAfterButtonClick) == EntranceWorkmodeOption.OnlyTempReaderAfterButtonClick; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.OnlyTempReaderAfterButtonClick;
                if (!value) WorkMode -= EntranceWorkmodeOption.OnlyTempReaderAfterButtonClick;
            }
        }
        /// <summary>
        /// 卡片有效命令需等待控制板返回执行结果（使用此选项的原因是CAN总线版停车场旧的硬件卡片有效命令没有返回消息）
        /// </summary>
        public bool CardValidNeedResponse
        {
            get { return (WorkMode & EntranceWorkmodeOption.CardValidNeedResponse) == EntranceWorkmodeOption.CardValidNeedResponse; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.CardValidNeedResponse;
                if (!value) WorkMode -= EntranceWorkmodeOption.CardValidNeedResponse;
            }
        }
        /// <summary>
        /// 获取或设置月卡类出场需要确认
        /// </summary>
        public bool MonthCardWaitWhenOut
        {
            get { return (WorkMode & EntranceWorkmodeOption.MonthCardWaitWhenOut) == EntranceWorkmodeOption.MonthCardWaitWhenOut; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.MonthCardWaitWhenOut;
                if (!value) WorkMode -= EntranceWorkmodeOption.MonthCardWaitWhenOut;
            }
        }
        /// <summary>
        ///获取或设置储值卡类出场需要确认
        /// </summary>
        public bool PrepayCardWaitWhenOut
        {
            get { return (WorkMode & EntranceWorkmodeOption.PrepayCardWaitWhenOut) == EntranceWorkmodeOption.PrepayCardWaitWhenOut; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.PrepayCardWaitWhenOut;
                if (!value) WorkMode -= EntranceWorkmodeOption.PrepayCardWaitWhenOut;
            }
        }
        /// <summary>
        /// 获取或设置是否将此通道当成门禁方式来用，启用这一选项时，卡片进出场不会改变卡片的状态，只记录卡片的进出记录
        /// </summary>
        public bool UseAsAcs
        {
            get { return (WorkMode & EntranceWorkmodeOption.UseAsACS) == EntranceWorkmodeOption.UseAsACS; }
            set
            {
                WorkMode |= EntranceWorkmodeOption.UseAsACS;
                if (!value) WorkMode -= EntranceWorkmodeOption.UseAsACS;
            }
        }
        #endregion

        #region 重载基类方法
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is EntranceInfo)
                {
                    EntranceInfo entrance = obj as EntranceInfo;
                    return (entrance.EntranceID == this.EntranceID);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取某种卡片类型的控制器属性
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public EntranceCardTypeProperty GetCardTypeProperty(CardType cardType)
        {
            ushort[] propertyBytes = CardTypeProperty;

            if (CardType.GetBaseCardType(cardType) == CardType.VipCard)
            {
                return (EntranceCardTypeProperty)propertyBytes[0];
            }
            else if(CardType.GetBaseCardType(cardType)==CardType.OwnerCard)
            {
                return (EntranceCardTypeProperty)propertyBytes[1];
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.MonthRentCard)
            {
                return (EntranceCardTypeProperty)propertyBytes[2];
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.PrePayCard)
            {
                return (EntranceCardTypeProperty)propertyBytes[3];
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.TempCard)
            {
                return (EntranceCardTypeProperty)propertyBytes[4];
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.UserDefinedCard1)
            {
                return (EntranceCardTypeProperty)propertyBytes[5];
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.UserDefinedCard2)
            {
                return (EntranceCardTypeProperty)propertyBytes[6];
            }

            return EntranceCardTypeProperty.Default;
        }

        /// <summary>
        /// 设置某种卡片类型的控制器属性
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="property"></param>
        public bool SetCardTypeProperty(CardType cardType, EntranceCardTypeProperty property)
        {
            ushort[] propertyBytes = CardTypeProperty;

            ushort p = 0;

            try
            {
                p = (ushort)property;
            }
            catch
            {
                return false;
            }

            if (CardType.GetBaseCardType(cardType) == CardType.VipCard)
            {
                propertyBytes[0] = p;
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.OwnerCard)
            {
                propertyBytes[1] = p;
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.MonthRentCard)
            {
                propertyBytes[2] = p;
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.PrePayCard)
            {
                propertyBytes[3] = p;
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.TempCard)
            {
                propertyBytes[4] = p;
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.UserDefinedCard1)
            {
                propertyBytes[5] = p;
            }
            else if (CardType.GetBaseCardType(cardType) == CardType.UserDefinedCard2)
            {
                propertyBytes[6] = p;
            }

            CardTypeProperty = propertyBytes;

            return true;
        }
        #endregion

        #region SQL语句

        /// <summary>
        /// 获取插入包括主键值的记录的SQL语句
        /// </summary>
        public string SQLInsertWithPrimaryCmd
        {
            get
            {
                string cmd = string.Format(@"INSERT INTO [Entrance](
           [EntranceID]
           ,[EntranceName]
           ,[ParkID]
           ,[Address]
           ,[IPAddress]
           ,[IPMask]
           ,[Gateway]
           ,[ControlPort]
           ,[EventPort]
           ,[MasterIP]
           ,[MACAddress]
           ,[WorkMode]
           ,[ReadCardInterval]
           ,[TicketPrinterCOMPort]
           ,[TicketReaderCOMPort]
           ,[TempCard]
           ,[CarPlateIP]
           ,[VideoID]
           ,[PaymentEventIndex]
           ,[CardTypeProperty]
           ,[CarPlateNotifyIP])
VALUES
({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20});",
                   SQLStringHelper.FromInt(this.EntranceID),
                   SQLStringHelper.FromString(this.EntranceName),
                   SQLStringHelper.FromInt(this.ParkID),
                   SQLStringHelper.FromInt(this.Address),
                   SQLStringHelper.FromString(this.IPAddress),
                   SQLStringHelper.FromString(this.IPMask),
                   SQLStringHelper.FromString(this.Gateway),
                   SQLStringHelper.FromInt(this.ControlPort),
                   SQLStringHelper.FromInt(this.EventPort),
                   SQLStringHelper.FromString(this.MasterIP),
                   SQLStringHelper.FromString(this.MACAddress),
                   SQLStringHelper.FromInt((int)this.WorkMode),
                   SQLStringHelper.FromInt(this.ReadCardInterval),
                   SQLStringHelper.FromByte(this.TicketPrinterCOMPort),
                   SQLStringHelper.FromByte(this.TicketReaderCOMPort),
                   SQLStringHelper.FromInt(this.TempCard),
                   SQLStringHelper.FromString(this.CarPlateIP),
                   SQLStringHelper.FromInt(this.VideoID),
                   SQLStringHelper.FromInt(this.PaymentEventIndex),
                   SQLStringHelper.FromString(this._CardTypeProperty),
                   SQLStringHelper.FromString(this._CarPlateNotifyIP));
                return cmd;
            }
        }
        #endregion
    }
}

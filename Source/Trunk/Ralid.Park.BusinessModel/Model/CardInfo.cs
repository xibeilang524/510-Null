using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    [DataContract]
    public class CardInfo
    {
        #region 构造函数
        public CardInfo()
        {
            this.CardNum = 1000;
            this.ActivationDate = new DateTime(2011, 1, 1);
            this.ValidDate = new DateTime(2011, 1, 1);
            this.LastDateTime = new DateTime(2011, 1, 1);
            this.CardVersion = GlobalVariables.CurrentCardVersion;
            this.OnlineHandleWhenOfflineMode = false;
            this.IndexNumber = 0xFFFFFF;
        }
        #endregion

        #region 私有变量
        public byte _CardType;//Modify by Jan 2015-05-11 ,将private改为public是因为需要使用_CardType作为查询条件
        #endregion

        #region 实体字段
        ///<summary>
        /// 记录序号
        /// </summary>
        [DataMember]
        public short Index { get; set; }

        /// <summary>
        /// 获取或设置卡片编号
        /// </summary>
        [DataMember]
        public short CardNum { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        /// <summary>
        /// 卡片类型
        /// </summary>
        [DataMember]
        public CardType CardType
        {
            get
            {
                return CardType.GetSystemCardType(_CardType);
            }
            set
            {
                _CardType = (byte)value;
            }
        }
        /// <summary>
        /// 收费车型
        /// </summary>
        [DataMember]
        public Byte CarType { get; set; }
        /// <summary>
        /// 卡片状态
        /// </summary>
        [DataMember]
        public CardStatus Status { get; set; }
        /// <summary>
        /// 获取或设置卡片的生效时间
        /// </summary>
        [DataMember]
        public DateTime ActivationDate { get; set; }
        /// <summary>
        /// 有效日期
        /// </summary>
        [DataMember]
        public DateTime ValidDate { get; set; }
        /// <summary>
        /// 获取或设置储值余额,写卡模式最大金额167772.15元
        /// </summary>
        [DataMember]
        public decimal Balance { get; set; }
        /// <summary>
        /// 获取或设置卡片的押金
        /// </summary>
        [DataMember]
        public decimal Deposit { get; set; }
        /// <summary>
        /// 获取或设置停车标志
        /// </summary>
        [DataMember]
        public ParkingStatus ParkingStatus { get; set; }
        /// <summary>
        /// 获取或设置上一次刷卡时间
        /// </summary>
        [DataMember]
        public DateTime LastDateTime { get; set; }
        /// <summary>
        /// 获取或设置上次刷卡的通道
        /// </summary>
        [DataMember]
        public int LastEntrance { get; set; }
        /// <summary>
        /// 获取或设置最近一次识别到的车牌
        /// </summary>
        [DataMember]
        public string LastCarPlate { get; set; }
        /// <summary>
        /// 获取或设置嵌套车场的入场时间
        /// </summary>
        [DataMember]
        public DateTime? LastNestParkDateTime { get; set; }
        /// <summary>
        /// 获取或设置权限组号
        /// </summary>
        [DataMember]
        public byte AccessID { get; set; }
        /// <summary>
        /// 获取或设置卡片选项
        /// </summary>
        [DataMember]
        public CardOptions Options { get; set; }

        /// <summary>
        /// 获取或设置卡片入场后的最近一次收费记录
        /// </summary>
        [DataMember]
        public CardPaymentInfo LastPayment { get; set; }

        /// <summary>
        /// 获取或设置卡片的总优惠时数(0~255，注意：数据库存储使用的是byte类型)
        /// </summary>
        [DataMember]
        public int DiscountHour { get; set; }

        /// <summary>
        /// 获取或设置卡片的编号，主要用于存储用户在卡片印刷时打印的一个卡片标识或用于其它目的
        /// </summary>
        [DataMember]
        public string CardCertificate { get; set; }
        /// <summary>
        /// 获取或设置卡片拥有者姓名
        /// </summary>
        [DataMember]
        public string OwnerName { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        [DataMember]
        public string CarPlate { get; set; }
        /// <summary>
        /// 获取或设置部门
        /// </summary>
        [DataMember]
        public string Department { get; set; }
        /// <summary>
        /// 获取或设置卡片描述
        /// </summary>
        [DataMember]
        public string Memo { get; set; }
        /// <summary>
        /// 获取或设置是否已上传到主数据库标识
        /// </summary>
        [DataMember]
        public bool? UpdateFlag { get; set; }
        /// <summary>
        /// 获取或设置免费时间点
        /// </summary>
        [DataMember]
        public DateTime? FreeDateTime { get; set; }
        /// <summary>
        /// 获取或设置优惠信息的录入时间
        /// </summary>
        [DataMember]
        public DateTime? PreferentialTime { get; set; }
        /// <summary>
        /// 获取或设置名单类型
        /// </summary>
        [DataMember]
        public CardListType ListType { get; set; }
        #endregion

        #region 卡片选项
        /// <summary>
        /// 获取或设置卡片是否可以重复入场
        /// </summary>
        public bool CanRepeatIn
        {
            get
            {
                return (Options & CardOptions.ForbidRepeatIn) != CardOptions.ForbidRepeatIn;
            }
            set
            {
                Options |= CardOptions.ForbidRepeatIn;
                if (value) Options -= CardOptions.ForbidRepeatIn;
            }
        }

        /// <summary>
        /// 操作员卡专用，收费时允许切换车型(即为授权卡)
        /// </summary>
        public bool OperatorAllowSwitchCarType
        {
            get
            {
                return CanRepeatIn;
            }
            set
            {
                CanRepeatIn = value;
            }
        }

        /// <summary>
        /// 获取或设置卡片是否可以重复出场
        /// </summary>
        public bool CanRepeatOut
        {
            get
            {
                return (Options & CardOptions.ForbidRepeatOut) != CardOptions.ForbidRepeatOut;
            }
            set
            {
                Options |= CardOptions.ForbidRepeatOut;
                if (value) Options -= CardOptions.ForbidRepeatOut;
            }
        }
        /// <summary>
        /// 获取或设置卡片在节假日允许进入
        /// </summary>
        public bool HolidayEnabled
        {
            get
            {
                return (Options & CardOptions.HolidayEnable) == CardOptions.HolidayEnable;
            }
            set
            {
                Options |= CardOptions.HolidayEnable;
                if (!value) Options -= CardOptions.HolidayEnable;
            }
        }
        /// <summary>
        /// 获取或设置卡片进出时是否参加车位计数
        /// </summary>
        public bool WithCount
        {
            get
            {
                return (Options & CardOptions.WithCount) == CardOptions.WithCount;
            }
            set
            {
                Options |= CardOptions.WithCount;
                if (!value) Options -= CardOptions.WithCount;
            }
        }
        /// <summary>
        /// 获取或设置车场满位时是否允许入场
        /// </summary>
        public bool CanEnterWhenFull
        {
            get { return (Options & CardOptions.ForbidWhenFull) != CardOptions.ForbidWhenFull; }
            set
            {
                Options |= CardOptions.ForbidWhenFull;
                if (value) Options -= CardOptions.ForbidWhenFull;
            }
        }
        /// <summary>
        /// 获取或设置卡片过期后卡片是否有效
        /// </summary>
        public bool EnableWhenExpired
        {
            get { return (Options & CardOptions.ForbidWhenExpired) != CardOptions.ForbidWhenExpired; }
            set
            {
                Options |= CardOptions.ForbidWhenExpired;
                if (value) Options -= CardOptions.ForbidWhenExpired;
            }
        }
        /// <summary>
        /// 获取或设置脱机模式时是否按在线模式处理
        /// </summary>
        public bool OnlineHandleWhenOfflineMode
        {
            get { return (Options & CardOptions.OfflineHandleWhenOfflineMode) != CardOptions.OfflineHandleWhenOfflineMode; }
            set
            {
                Options |= CardOptions.OfflineHandleWhenOfflineMode;
                if (value) Options -= CardOptions.OfflineHandleWhenOfflineMode;
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置识别到的车牌号码
        /// </summary>
        [DataMember]
        public string RegCarPlate { get; set; }
        /// <summary>
        /// 获取或设置自增序号,珠海长隆停车场4.0.20140127~4.0.20140131版本需要用到
        /// </summary>
        [DataMember]
        public int IndexNumber { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 获取卡片是否可以发行
        /// </summary>
        public bool ReleasAble
        {
            get
            {
                if (CardType == CardType.TempCard) return true;
                if (Status == CardStatus.Deleted || Status == CardStatus.Recycled || Status == CardStatus.UnRegister) return true;
                return false;
            }
        }
        /// <summary>
        /// 卡片是否可以充值
        /// </summary>
        public bool Chargable
        {
            get
            {
                return (this.CardType.IsPrepayCard && this.Status == CardStatus.Enabled);
            }
        }
        /// <summary>
        /// 卡片是否可以延期
        /// </summary>
        public bool Deferable
        {
            get
            {
                return (CardType.IsMonthCard && this.Status == CardStatus.Enabled);
            }
        }

        /// <summary>
        /// 卡片是否可以更换
        /// </summary>
        public bool Changable
        {
            get
            {
                return (!CardType.IsTempCard && Status == CardStatus.Enabled);
            }
        }

        /// <summary>
        /// 卡片是否可回收
        /// </summary>
        public bool Recycleable
        {
            get
            {
                return (!CardType.IsTempCard && !CardType.IsManagedCard && Status == CardStatus.Enabled);
            }
        }

        /// <summary>
        /// 卡片是否是临时性卡
        /// </summary>
        public bool IsTempCard
        {
            get
            {
                return (CardType.IsTempCard);
            }
        }

        /// <summary>
        /// 卡片是否是管理类卡
        /// </summary>
        public bool IsManagedCard
        {
            get
            {
                return (CardType.IsManagedCard);
            }
        }

        /// <summary>
        /// 获取或设置卡片是否已经进入停车场
        /// </summary>
        public bool IsInPark
        {
            get
            {
                return (ParkingStatus & ParkingStatus.In) == ParkingStatus.In;
            }
            set
            {
                ParkingStatus |= ParkingStatus.In;
                if (!value) ParkingStatus ^= ParkingStatus.In;
            }
        }
        /// <summary>
        /// 获取或设置是否已经进入内嵌车场
        /// </summary>
        public bool IsInNestedPark
        {
            get
            {
                return (ParkingStatus & ParkingStatus.IndoorIn) == ParkingStatus.IndoorIn;
            }
            set
            {
                ParkingStatus |= ParkingStatus.IndoorIn;
                if (!value) ParkingStatus ^= ParkingStatus.IndoorIn;
            }
        }

        /// <summary>
        /// 获取或设置是否标定已停过室内车场
        /// </summary>
        public bool IsMarkNestedPark
        {
            get
            {
                return (ParkingStatus & ParkingStatus.NestedParkMarked) == ParkingStatus.NestedParkMarked;
            }
            set
            {
                ParkingStatus |= ParkingStatus.NestedParkMarked;
                if (!value) ParkingStatus ^= ParkingStatus.NestedParkMarked; 
            }
        }

        /// <summary>
        /// 获取或设置是否启用酒店应用
        /// </summary>
        public bool EnableHotelApp
        {
            get
            {
                return (ParkingStatus & ParkingStatus.HotelApp) == ParkingStatus.HotelApp;
            }
            set
            {
                ParkingStatus |= ParkingStatus.HotelApp;
                if (!value) ParkingStatus ^= ParkingStatus.HotelApp;
            }
        }

        /// <summary>
        /// 获取或设置是否已退房
        /// </summary>
        public bool HotelCheckOut
        {
            get
            {
                return (ParkingStatus & ParkingStatus.NotCheckOut) != ParkingStatus.NotCheckOut;
            }
            set
            {
                ParkingStatus |= ParkingStatus.NotCheckOut;
                if (value) ParkingStatus ^= ParkingStatus.NotCheckOut;
            }
        }
        /// <summary>
        /// 获取是否卡片名单
        /// </summary>
        public bool IsCardList
        {
            get
            {
                return ListType == CardListType.Card;
            }
        }
        /// <summary>
        /// 获取是否车牌名单
        /// </summary>
        public bool IsCarPlateList
        {
            get
            {
                return ListType == CardListType.CarPlate;
            } 
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 卡片入场
        /// </summary>
        /// <param name="entranceID"></param>
        /// <param name="enterDateTime"></param>
        public void Enter(int entranceID, DateTime enterDateTime)
        {
            ParkingStatus = ParkingStatus.In;
            this.LastDateTime = enterDateTime;
            this.LastEntrance = entranceID;
        }
        /// <summary>
        /// 卡片出场
        /// </summary>
        /// <param name="entranceID"></param>
        /// <param name="exitDateTime"></param>
        public void Exit(int entrnaceID, DateTime exitDateTime)
        {
            ParkingStatus = ParkingStatus.Out;
            this.LastEntrance = entrnaceID;
            this.LastDateTime = exitDateTime;
        }

        /// <summary>
        /// 挂失
        /// </summary>
        public void Lost()
        {
            Status = CardStatus.Loss;
        }
        /// <summary>
        /// 恢复
        /// </summary>
        public void Restore()
        {
            Status = CardStatus.Enabled;
        }
        /// <summary>
        /// 禁用
        /// </summary>
        public void Disable()
        {
            Status = CardStatus.Disabled;
        }
        /// <summary>
        /// 启用
        /// </summary>
        public void Enable()
        {
            Status = CardStatus.Enabled;
        }
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="money"></param>
        public void Charge(decimal money)
        {
            Balance += money;
        }
        /// <summary>
        /// 延期
        /// </summary>
        /// <param name="validDate"></param>
        public void Defer(DateTime validDate)
        {
            ValidDate = validDate;
        }
        /// <summary>
        /// 发行
        /// </summary>
        public void Release()
        {
            LastDateTime = DateTime.Now;
            LastEntrance = 0;
            LastCarPlate = string.Empty;
            ParkingStatus = IsInPark ? ParkingStatus.In : ParkingStatus.Out;
            PaidDateTime = null;
            TotalPaidFee = 0;
            ParkFee = 0;
            FreeDateTime = null;
            DiscountHour = 0;
            PreferentialTime = null;
            Status = CardStatus.Enabled;
        }
        /// <summary>
        /// 回收
        /// </summary>
        public void Recycle()
        {
            Status = CardStatus.Recycled;
            this.Deposit = 0;
            this.Balance = 0;
            this.ValidDate = new DateTime(2011, 1, 1);
            this.OwnerName = CardType.ToString();
            this.CarPlate = string.Empty;
            this.Memo = string.Empty;
        }
        /// <summary>
        /// 获取自身的一个克隆复本
        /// </summary>
        /// <returns></returns>
        public  CardInfo Clone()
        {
            CardInfo card = (CardInfo)base.MemberwiseClone();
            return card;
        }

        /// <summary>
        /// 对比两张卡片用于收费的信息是否一致
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool CompareChargeInfo(CardInfo card)
        {
            if (card == null
                || CardID != card.CardID
                || ParkingStatus != card.ParkingStatus   //停车状态
                || LastDateTime != card.LastDateTime     //最近一次刷卡时间
                || PaidDateTime != card.PaidDateTime        //缴费时间
                || ParkFee != card.ParkFee                  //停车费用
                || TotalPaidFee != card.TotalPaidFee)       //已缴费用
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 检查时间是否未过免费时间点
        /// </summary>
        /// <param name="dateTime">检查的时间</param>
        /// <returns></returns>
        public bool IsInFreeTime(DateTime dateTime)
        {
            if (FreeDateTime.HasValue)
            {
                return dateTime <= FreeDateTime.Value;
            }
            return false;
        }
        #endregion


        #region 写卡模式相关
        #region 实体字段
        /// <summary>
        /// 获取或设置卡格式版本
        /// </summary>
        [DataMember]
        public byte CardVersion { get; set; }

        ///// <summary>
        ///// 获取或设置室内停车场的进入时间
        ///// </summary>
        //[DataMember]
        //public DateTime? IndoorInDateTime { get; set; }

        ///// <summary>
        ///// 获取或设置室内停车场累计停车时间，单位分钟
        ///// </summary>
        //[DataMember]
        //public int IndoorTimeInterval { get; set; }

        /// <summary>
        /// 获取或设置缴费时间
        /// </summary>
        [DataMember]
        public DateTime? PaidDateTime { get; set; }

        /// <summary>
        /// 获取或设置停车费用（外车场收费后写入）/累计已收费用（收费卡时）
        /// </summary>
        [DataMember]
        public decimal ParkFee { get; set; }

        ///// <summary>
        ///// 获取或设置停车场累计停车费用
        ///// （缴费时写入，缴费后减去收取的费用，出口时判断是否为0作为缴费是否完成的标识，主要是考虑到自助缴费机会分多次完成缴费的情况出现）
        ///// </summary>
        //[DataMember]
        //public decimal TotalFee { get; set; }

        /// <summary>
        /// 获取或设置累计已缴的费用（包括支付的费用和折扣费用）
        /// </summary>
        [DataMember]
        public decimal TotalPaidFee { get; set; }
        #endregion

        #region 公共属性
        /// <summary>
        /// 卡片外车场是否已缴过费（有可能未完成缴费，只缴纳了部分费用）
        /// </summary>
        public bool IsPaid
        {
            get
            {
                return (ParkingStatus & ParkingStatus.PaidBill) == ParkingStatus.PaidBill
                    && PaidDateTime.HasValue;
            }
            set
            {
                ParkingStatus |= ParkingStatus.PaidBill;
                if (!value) ParkingStatus ^= ParkingStatus.PaidBill;
            }
        }


        ///// <summary>
        ///// 卡片内车场是否已缴费（目前出内车场会将费用累加到累计停车费用，标记为已缴费，但实际收费会在外车场收费时一并收取）
        ///// </summary>
        //public bool IsIndoorPaid
        //{
        //    get
        //    {
        //        return (ParkingStatus & ParkingStatus.IndoorPaid) == ParkingStatus.IndoorPaid
        //            && PaidDateTime.HasValue;
        //    }
        //    set
        //    {
        //        ParkingStatus |= ParkingStatus.IndoorPaid;
        //        if (!value) ParkingStatus ^= ParkingStatus.IndoorPaid;
        //    }
        //}


        #endregion

        #region 公共只读属性

        /// <summary>
        /// 卡片是否已缴纳完停车费用
        /// </summary>
        public bool IsCompletedPaid
        {
            get
            {
                //return IsPaid && TotalFee == 0;
                return IsPaid && TotalPaidFee >= ParkFee;
            }
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 设置卡片缴费数据（中央收费或出口收费）
        /// </summary>
        /// <param name="payment">缴费记录</param>
        public void SetPaidData(CardPaymentInfo payment)
        {
            if (!IsCompletedPaid || !TariffSetting.Current.IsInFreeTime(PaidDateTime.Value, payment.ChargeDateTime))
            {
                //未完成缴费或已过缴费后免费时间的
                ParkFee = payment.ParkFee;//更新外车场的停车费用
                PaidDateTime = payment.ChargeDateTime;//更新缴费时间
            }
            IsPaid = true;//更新缴费标识
            //TotalFee = payment.Accounts;
            //TotalFee -= payment.Paid + payment.Discount;
            TotalPaidFee += payment.Paid + payment.Discount;//加上已缴费用
        }

        /// <summary>
        /// 清除卡片缴费数据
        /// </summary>
        public void ClearPaidData()
        {
            //IndoorInDateTime = null;
            //IndoorTimeInterval = 0;
            //TotalFee = 0;
            IsPaid = false;
            PaidDateTime = null;
            ParkFee = 0;
            TotalPaidFee = 0;
        }

        /// <summary>
        /// 清除免费优惠授权信息
        /// </summary>
        public void ClearFreeAuthorization()
        {
            EnableHotelApp = false;
            HotelCheckOut = true;
            FreeDateTime = null;
        }

        /// <summary>
        /// 清除优惠录入信息
        /// </summary>
        public void ClearDiscount()
        {
            DiscountHour = 0;
            PreferentialTime = null;
        }

        ///// <summary>
        ///// 更新室内停车场累计停车时间
        ///// </summary>
        ///// <param name="dateTime">出场时间</param>
        //public bool UpdateIndoorTimeInterval(DateTime dateTime)
        //{
        //    if (IndoorInDateTime.HasValue && dateTime > IndoorInDateTime.Value)
        //    {
        //        if (IsIndoorPaid)//已缴费，只计算多出的停车时间
        //        {
        //            if (dateTime > PaidDateTime.Value)
        //            {
        //                TimeSpan ts = new TimeSpan(dateTime.Ticks - PaidDateTime.Value.Ticks);
        //                IndoorTimeInterval += (int)ts.TotalMinutes;
        //            }
        //        }
        //        else//未缴费
        //        {
        //            TimeSpan ts = new TimeSpan(dateTime.Ticks - IndoorInDateTime.Value.Ticks);
        //            IndoorTimeInterval += (int)ts.TotalMinutes;
        //        }

        //        return true;
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// 获取外车场实际的计费时间，缴费时间-内车场累计停车时间长
        ///// </summary>
        ///// <param name="chargeDateTime"></param>
        ///// <returns></returns>
        //public DateTime GetOuterParkChargeDateTime(DateTime chargeDateTime)
        //{
        //    return chargeDateTime.AddMinutes(0 - IndoorTimeInterval);
        //}

        /// <summary>
        /// 通过刷卡事件设置卡片的相关信息
        /// </summary>
        /// <param name="report"></param>
        public void SetEventReportData(CardEventReport report)
        {
            if (report != null)
            {
                ParkingStatus = report.ParkingStatus;//使用事件的状态
                //如果启用了酒店应用，保留免费时间点，否则清空免费时间点
                FreeDateTime = report.EnableHotelApp ? report.FreeDateTime : null;
            }
        }
        #endregion
        #endregion
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Parking.POS.Model
{
    [Serializable]
    public class CardInfo
    {
        #region 构造函数
        public CardInfo()
        {
        }
        #endregion

        #region 实体字段

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardID { get; set; }

        /// <summary>
        /// 卡片类型
        /// </summary>
        public CardType  CardType { get; set; }

        /// <summary>
        /// 收费车型
        /// </summary>
        
        public Byte CarType { get; set; }
        /// <summary>
        /// 卡片状态
        /// </summary>
        public CardStatus Status { get; set; }
        /// <summary>
        /// 获取或设置卡片的生效时间
        /// </summary>
        
        public DateTime ActivationDate { get; set; }
        /// <summary>
        /// 有效日期
        /// </summary>
        
        public DateTime ValidDate { get; set; }
        /// <summary>
        /// 获取或设置储值余额,写卡模式最大金额167772.15元
        /// </summary>
        
        public decimal Balance { get; set; }
        /// <summary>
        /// 获取或设置停车标志
        /// </summary>
        
        public ParkingStatus ParkingStatus { get; set; }
        /// <summary>
        /// 获取或设置上一次刷卡时间
        /// </summary>
        public DateTime LastDateTime { get; set; }
        /// <summary>
        /// 获取或设置权限组号
        /// </summary>
        public byte AccessID { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        public string CarPlate { get; set; }
        /// <summary>
        /// 获取或设置卡格式版本
        /// </summary>
        public byte CardVersion { get; set; }

        /// <summary>
        /// 获取或设置缴费时间
        /// </summary>
        public DateTime? PaidDateTime { get; set; }
        /// <summary>
        /// 获取或设置停车费用（外车场收费后写入）/累计已收费用（收费卡时）
        /// </summary>
        public decimal ParkFee { get; set; }

        /// <summary>
        /// 获取或设置累计已缴的费用（包括支付的费用和折扣费用）
        /// </summary>
        public decimal TotalPaidFee { get; set; }
        /// <summary>
        /// 获取或设置卡片选项
        /// </summary>
        public CardOptions Options { get; set; }

        /// <summary>
        /// 免费时间点
        /// </summary>
        public DateTime? FreeDateTime { get; set; }
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
        /// 获取或设置卡片在节假日允许进出
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
        #endregion

        #region 只读属性
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

        #region 公共方法
        /// <summary>
        /// 获取自身的一个克隆复本
        /// </summary>
        /// <returns></returns>
        public  CardInfo Clone()
        {
            CardInfo card = (CardInfo)base.MemberwiseClone();
            return card;
        }

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

        #endregion

        #region 公共只读属性
        /// <summary>
        /// 卡片是否已缴纳完停车费用
        /// </summary>
        public bool IsCompletedPaid
        {
            get
            {
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
            if (!IsCompletedPaid || !MySetting.Current.IsInFreeTime(PaidDateTime.Value, payment.ChargeDateTime))
            {
                //未完成缴费或已过缴费后免费时间的
                ParkFee = payment.ParkFee;//更新外车场的停车费用
                PaidDateTime = payment.ChargeDateTime;//更新缴费时间
            }
            IsPaid = true;//更新缴费标识
            TotalPaidFee += payment.Paid + payment.Discount;//加上已缴费用
        }

        /// <summary>
        /// 清除卡片缴费数据
        /// </summary>
        public void ClearPaidData()
        {
            IsPaid = false;
            PaidDateTime = null;
            ParkFee = 0;
            TotalPaidFee = 0;
        }
        #endregion
    }
}



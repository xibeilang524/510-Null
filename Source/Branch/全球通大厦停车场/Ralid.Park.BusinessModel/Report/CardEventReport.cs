﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq ;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Factory;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 表示停车场的刷卡事件
    /// </summary>
    [Serializable]
    [DataContract]
    public class CardEventReport : ReportBase
    {
        #region 静态方法
        public static CardEventReport CreateEnterEvent(CardInfo card, int parkID, int entranceID, string entranceName, DateTime eventDatetime)
        {
            CardEventReport report = new CardEventReport();
            report.ID = Guid.NewGuid();
            report.ParkID = parkID;
            report.EntranceID = entranceID;
            report.IsExitEvent = false;
            report.SourceName = entranceName;
            report.CardType = card.CardType;
            report.CarType = card.CarType;
            report.CardID = card.CardID;
            report.OwnerName = card.OwnerName;
            report.CarPlate = card.CarPlate;
            report.CardCertificate = card.CardCertificate;
            report.EventDateTime = eventDatetime;
            report.EventStatus = 0;
            report.LastDateTime = card.LastDateTime;
            report.ValidDate = card.ValidDate;
            report.Balance = card.Balance;
            report.OnlineHandleWhenOfflineMode = card.OnlineHandleWhenOfflineMode;
            report.ParkingStatus = ParkingStatus.In;
            if (card.EnableLimitation && UserSetting.Current != null && UserSetting.Current.LimitationPerMonth > 0)
            {
                report.Limitation = 0;

                if (!card.LimitationTimestamp.HasValue ||
                    card.LimitationTimestamp.Value.Date.Year != report.EventDateTime.Year ||
                    card.LimitationTimestamp.Value.Month != report.EventDateTime.Month)  //卡片累计的剩余时长非本月的,重新计算
                {
                    report.LimitationRemain = UserSetting.Current.LimitationPerMonth - report.Limitation;
                }
                else
                {
                    report.LimitationRemain = card.LimitationRemain.Value - report.Limitation;
                }
            }
            return report;
        }

        public static CardEventReport CreateExitEvent(CardInfo card, int parkID, int entranceID, string entranceName, Byte carType, TariffSetting ts, DateTime eventDateTime)
        {
            CardEventReport report = new CardEventReport();
            report.ID = Guid.NewGuid();
            report.ParkID = parkID;
            report.EventDateTime = eventDateTime;
            report.EntranceID = entranceID;
            report.SourceName = entranceName;
            report.IsExitEvent = true;
            report.CardID = card.CardID;
            report.OwnerName = card.OwnerName;
            report.CarPlate = card.CarPlate;
            report.CardCertificate = card.CardCertificate;
            report.CardType = card.CardType;
            report.CarType = carType;
            report.EventStatus = CardEventStatus.Pending;
            report.LastDateTime = card.LastDateTime;
            report.LastCarPlate = card.LastCarPlate;
            report.CardPaymentInfo = CardPaymentInfoFactory.CreateCardPaymentRecord(card, ts, carType, eventDateTime);
            report.Balance = card.Balance;
            report.ValidDate = card.ValidDate;
            report.OnlineHandleWhenOfflineMode = card.OnlineHandleWhenOfflineMode;
            report.ParkingStatus = ParkingStatus.Out;

            if (card.EnableLimitation && UserSetting.Current != null && UserSetting.Current.LimitationPerMonth > 0)
            {
                DateTime s = new DateTime(report.EventDateTime.Year, report.EventDateTime.Month, 1);
                if (report.LastDateTime > s) s = report.LastDateTime.Value;
                decimal hour = UserSetting.Current.CalculateLimitation(s, report.EventDateTime); //只计算本月分的限时停车时长
                report.Limitation = hour;

                if (!card.LimitationTimestamp.HasValue ||
                    card.LimitationTimestamp.Value.Date.Year != report.EventDateTime.Year ||
                    card.LimitationTimestamp.Value.Month != report.EventDateTime.Month)  //卡片累计的剩余时长非本月的,重新计算
                {
                    report.LimitationRemain = UserSetting.Current.LimitationPerMonth - report.Limitation;
                }
                else
                {
                    report.LimitationRemain = card.LimitationRemain.Value - report.Limitation;
                }
            }
            return report;
        }
        #endregion

        #region 构造函数
        public CardEventReport()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置事件记录的ID
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置事件卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置持卡人姓名
        /// </summary>
        [DataMember]
        public string OwnerName { get; set; }

        /// <summary>
        /// 获取或设置卡片编号
        /// </summary>
        [DataMember]
        public string CardCertificate { get; set; }
        /// <summary>
        /// 获取卡片类型
        /// </summary>
        [DataMember]
        public CardType CardType { get; set; }

        /// <summary>
        /// 获取或设置刷卡车辆的车型
        /// </summary>
        [DataMember]
        public byte CarType { get; set; }

        /// <summary>
        /// 获取或设置卡片上次活动时间,出口事件则用它来表示入口时间
        /// </summary>
        [DataMember]
        public DateTime? LastDateTime { get; set; }

        /// <summary>
        /// 获取或设置事件状态
        /// </summary>
        [DataMember]
        public CardEventStatus EventStatus { get; set; }

        /// <summary>
        /// 获取或设置是否是出口刷卡事件
        /// </summary>
        [DataMember]
        public bool IsExitEvent { get; set; }

        /// <summary>
        /// 获取或设置停车状态(用于更新卡片的停车状态和判断卡片此次是否当成临时卡来收费)
        /// </summary>
        [DataMember]
        public ParkingStatus ParkingStatus { get; set; }

        /// <summary>
        /// 获取或设置事件类型,0表示控制器生成的事件,1表示人工出入场事件
        /// </summary>
        [DataMember]
        public byte EventType { get; set; }

        /// <summary>
        /// 获取或设置本事件是在控制板的哪个读头上刷卡产生的
        /// </summary>
        [DataMember]
        public EntranceReader Reader { get; set; }

        /// <summary>
        /// 获取或设置卡片余额
        /// </summary>
        [DataMember]
        public decimal Balance { get; set; }

        /// <summary>
        /// 获取或设置卡片的过期时间
        /// </summary>
        [DataMember]
        public DateTime ValidDate { get; set; }

        /// <summary>
        /// 获取或设置上次事件时识别到的车牌(出场事件中为入场识别到的车牌,入场事件为空)
        /// </summary>
        [DataMember]
        public string LastCarPlate { get; set; }

        /// <summary>
        /// 获取或获取车牌对比结果
        /// </summary>
        [DataMember]
        public CarPlateComparisonResult ComparisonResult { get; set; }

        /// <summary>
        /// 获取或设置识别到的车牌
        /// </summary>
        [DataMember]
        public string CarPlate { get; set; }

        /// <summary>
        /// 获取或设置事件在硬件中保存的索引号
        /// </summary>
        [DataMember]
        public int EventIndex { get; set; }

        /// <summary>
        /// 获取或设置处理此事件的操作员编号
        /// </summary>
        [DataMember]
        public string OperatorID { get; set; }
        /// <summary>
        /// 获取或设置处理此事件的工作站
        /// </summary>
        [DataMember]
        public string StationID { get; set; }

        /// <summary>
        /// 获取或设置事件的收费信息
        /// </summary>
        [DataMember]
        public CardPaymentInfo CardPaymentInfo { get; set; }

        /// <summary>
        /// 获取或设置脱机模式时事件是否按在线模式处理
        /// </summary>
        [DataMember]
        public bool OnlineHandleWhenOfflineMode { get; set; }

        /// <summary>
        /// 获取或设置事件停车场的工作模式
        /// </summary>
        [DataMember]
        public ParkWorkMode WorkMode { get; set; }

        /// <summary>
        /// 获取或设置限时停车时长
        /// </summary>
        [DataMember]
        public decimal? Limitation { get; set; }

        /// <summary>
        /// 获取或设置剩余限时停车时长
        /// </summary>
        [DataMember]
        public decimal? LimitationRemain { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 是否以临时卡方式来收费(固定卡,月卡过期,储值卡余额不足当成临时卡收费)
        /// </summary>
        public bool ChargeAsTempCard
        {
            get
            {
                if (this.CardPaymentInfo == null) return false;
                if (CardType.IsTempCard && this.CardPaymentInfo.Accounts > 0) return true;
                if (CardType.IsPrepayCard && this.Balance < this.CardPaymentInfo.Accounts) return true;
                else if (CardType.IsMonthCard && CardPaymentInfo.Accounts > 0) return true;
                return false;
            }
        }

        /// <summary>
        /// 获取停车时长
        /// </summary>
        public string TimeInterval
        {
            get
            {
                string ret = string.Empty;
                if (LastDateTime != null)
                {
                    DatetimeInterval di = new DatetimeInterval(LastDateTime.Value, EventDateTime);
                    return di.ToString();
                }
                return ret;
            }
        }
        /// <summary>
        /// 获取是否在线处理的事件
        /// </summary>
        public bool IsOnlineHandleEvent
        {
            get
            {
                return WorkMode == ParkWorkMode.Fool || OnlineHandleWhenOfflineMode;
            }
        }

        /// <summary>
        /// 获取当是储值卡出口事件时，是否需要扣费
        /// </summary>
        public bool PrepayCardExitNeedPay
        {
            get
            {
                if (!this.IsExitEvent) return false;
                if (this.CardPaymentInfo == null) return false;
                if (this.CardPaymentInfo.Accounts == 0) return false;
                if (CardType.IsPrepayCard && this.Balance >= this.CardPaymentInfo.Accounts) return true;
                return false;
            }
        }
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2} {3}  {4}",
                    EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    SourceName,
                    OwnerName,
                    IsExitEvent ? Resouce.Resource1.Report_Exit : Resouce.Resource1.Report_Enter,
                    Resouce.Resource1.Report_CarPlate + ":" + CarPlate
                );
            }
        }
        #endregion

        #region 公共方法
        public CardEventReport Clone()
        {
            CardEventReport report = this.MemberwiseClone() as CardEventReport;
            if (this.CardPaymentInfo != null)
            {
                report.CardPaymentInfo = this.CardPaymentInfo.Clone();
            }
            return report;
        }
        #endregion
    }
}

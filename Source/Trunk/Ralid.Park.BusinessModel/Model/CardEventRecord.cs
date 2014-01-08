using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示卡片的进出事件记录
    /// </summary>
    public class CardEventRecord
    {
        #region 构造函数
        public CardEventRecord()
        {
        }

        public CardEventRecord(CardEventReport cardReport)
        {
            this.CardID = cardReport.CardID;
            this.EventDateTime = cardReport.EventDateTime;
            this.EntranceID = cardReport.EntranceID;
            this.ParkID = cardReport.ParkID;
            this.OwnerName = cardReport.OwnerName;
            this.CardCertificate = cardReport.CardCertificate;
            this.CardType = cardReport.CardType;
            this.CarType = cardReport.CarType;
            this.CarPlate = cardReport.CarPlate;
            this.EntranceName = cardReport.SourceName;
            this.IsExitEvent = cardReport.IsExitEvent;
            this.EventType = cardReport.EventType;
            this.LastDateTime = cardReport.LastDateTime;
            this.ParkingStatus = cardReport.ParkingStatus;
            this.OperatorID = cardReport.OperatorID;
            this.StationID = cardReport.StationID;
        }
        #endregion

        #region 字段
        private byte _CardType;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置事件发生时间
        /// </summary>
        public DateTime EventDateTime { get; set; }

        /// <summary>
        /// 获取或设置持卡人姓名
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// 获取或设置卡片编号
        /// </summary>
        public string CardCertificate { get; set; }

        /// <summary>
        /// 获取或设置发生事件的停车场ID
        /// </summary>
        public int ParkID { get; set; }

        /// <summary>
        /// 获取或设置报告发生的停车场ID
        /// </summary>
        public int EntranceID { get; set; }

        /// <summary>
        /// 获取或设置通道名称
        /// </summary>
        public string EntranceName { get; set; }

        /// <summary>
        /// 获取或设置卡片上次活动时间,出口事件则用它来表示入口时间
        /// </summary>
        public DateTime? LastDateTime { get; set; }

        /// <summary>
        /// 是否是出口刷卡事件
        /// </summary>
        public bool IsExitEvent { get; set; }

        /// <summary>
        /// 获取或设置卡片类型
        /// </summary>
        public CardType CardType
        {
            get
            {
                return CardType.GetSystemCardType (_CardType);
            }
            set
            {
                _CardType = (byte)value;
            }
        }

        /// <summary>
        /// 获取或设置车型
        /// </summary>
        public byte CarType { get; set; }

        /// <summary>
        /// 获取或设置事件类型,0表示控制器生成的事件,1表示人工出入场事件
        /// </summary>
        public byte EventType { get; set; }

        /// <summary>
        /// 获取或设置事件状态
        /// </summary>
        public byte EventStatus { get; set; }

        /// <summary>
        /// 获取或设置停车状态(用于更新卡片的停车状态和判断卡片此次是否当成临时卡来收费)
        /// </summary>
        public ParkingStatus ParkingStatus { get; set; }

        /// <summary>
        /// 获取和设置事件发生时识别到的车牌号
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 获取或设置处理此事件的操作员编号
        /// </summary>
        [DataMember]
        public string OperatorID { get; set; }

        /// <summary>
        /// 获取或设置处理此事件的工作站
        /// </summary>
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置结算时间,没有进行结算时为空
        /// </summary>
        public DateTime? SettleDateTime { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 获取停车时长
        /// </summary>
        public string ParkInterval
        {
            get
            {
                if (LastDateTime != null)
                {
                    DatetimeInterval di = new DatetimeInterval(EventDateTime, LastDateTime.Value);
                    return di.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        #endregion

        public CardEventRecord Clone()
        {
            return this.MemberwiseClone() as CardEventRecord;
        }
    }
}

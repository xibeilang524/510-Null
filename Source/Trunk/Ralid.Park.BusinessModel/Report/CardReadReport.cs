﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 控制器读到卡片时的通知
    /// </summary>
    [DataContract]
    [Serializable]
    public class CardReadReport : ReportBase
    {
        #region 构造函数
        public CardReadReport()
            : base()
        {
            CarPlateComparisonResult = CarPlateComparisonResult.Noncontrastive;
        }

        public CardReadReport(int parkID, int entranceID, DateTime eventDatetime, string sourceName)
            :base(parkID,entranceID,eventDatetime,sourceName)
        {
            CarPlateComparisonResult = CarPlateComparisonResult.Noncontrastive;
        }
        #endregion

        /// <summary>
        /// 卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置车牌对比结果
        /// </summary>
        [DataMember]
        public CarPlateComparisonResult CarPlateComparisonResult { get; set; }

        /// <summary>
        /// 获取或设置识别到的车牌号码
        /// </summary>
        [DataMember]
        public string CarPlate { get; set; }

        /// <summary>
        /// 获取或设置上次识别到的车牌号码
        /// </summary>
        [DataMember]
        public string LastCarPlate { get; set; }

        /// <summary>
        /// 获取或设置卡片的停车场数据
        /// </summary>
        [DataMember]
        public byte[] ParkingData { get; set; }

        /// <summary>
        /// 获取或设置卡片类型
        /// </summary>
        [DataMember]
        public byte CardType { get; set; }
        /// <summary>
        /// 获取或设置控制板上读到卡号的读头
        /// </summary>
        [DataMember]
        public EntranceReader Reader { get; set; }

        /// <summary>
        /// 获取或设置刷卡事件是否可以被忽略（如果设置为TRUE，则表示刷卡事件一定会被处理，而不会在与上次刷卡时间间隔小于控制板读卡间隔时被丢弃）
        /// </summary>
        [DataMember]
        public bool CannotIgnored { get; set; }

        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2}{3} Reader:{4} 车牌号:{5}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName, Resouce.Resource1.Report_CardRead, CardID, Reader, CarPlate);
            }
        }
    }
}

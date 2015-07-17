using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 无效刷卡（包括：卡已入场、卡已过期、卡已过点、卡余额不足） 根据语音代码来区别指令的类型
    /// </summary>
    [DataContract]
    public class CardInvalidEventReport : ReportBase
    {
        #region 构造函数
        public CardInvalidEventReport()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 事件无效类型
        /// </summary>
        [DataMember]
        public EventInvalidType InvalidType { get; set; }

        /// <summary>
        /// 设置或获取卡号,只有此卡未登记无效事件才有卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置车牌号码
        /// </summary>
        [DataMember]
        public string CarPlate { get; set; }


        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2} {3} {4}",
                                     EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                     SourceName,
                                     Resouce.CardInvalidDescripition.GetDescription(InvalidType),
                                     CardID,
                                     CarPlate);
            }
        }
        #endregion
    }
}

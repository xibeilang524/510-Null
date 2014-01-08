using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.Report
{
    [System.Runtime .Serialization .DataContract]
    [Serializable ]
    public class EntranceRemainTempCardReport:ReportBase
    {
        #region 构造函数
        public EntranceRemainTempCardReport()
        {
        }

        public EntranceRemainTempCardReport(int parkID, int entranceID, DateTime eventDatetime, string sourceName, int remainTempCard)
            : base(parkID, entranceID, eventDatetime, sourceName)
        {
            RemainTempCard = remainTempCard;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置临时卡数量
        /// </summary>
        [DataMember]
        public int RemainTempCard { get; set; }
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get { return string.Format("【{0} ＠ {1}】:剩余临时卡数量 {2}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName, RemainTempCard); }
        }
        #endregion
    }
}

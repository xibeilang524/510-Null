using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 停车场车位余数上报通知
    /// </summary>
    [DataContract]
    public class ParkVacantReport:ReportBase 
    {
        #region 构造函数
        public ParkVacantReport(int parkID, int entranceID, DateTime eventDatetime, string sourceName, short parkVacant)
            : base(parkID, entranceID, eventDatetime, sourceName)
        {
            this.ParkVacant = parkVacant;
        }

        public ParkVacantReport()
        {

        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置停车场车位余数
        /// </summary>
        [DataMember]
        public short ParkVacant { get; set; }
        #endregion

        #region 重写基类方法 
        public override string Description
        {
            get { return string.Format("【{0} ＠ {1}】:{2} {3}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 
                SourceName,Resouce .Resource1 .Report_ParkVacant , ParkVacant); }
        }
        #endregion
    }
}

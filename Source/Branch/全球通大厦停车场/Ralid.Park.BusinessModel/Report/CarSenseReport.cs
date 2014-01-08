using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 地感检测
    /// </summary>
    [DataContract]
    public class CarSenseReport : ReportBase
    {
        #region 构造函数
        public CarSenseReport()
        {
        }

        public CarSenseReport(int parkID, int entranceID, DateTime eventDateTime, string sourceName, int inOrOutFlag)
            : base(parkID, entranceID, eventDateTime, sourceName)
        {
            this.InOrOutFlag = inOrOutFlag;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置产生事件的地感，1为1号地感，2为2号地感
        /// </summary>
        [DataMember]
        public int Loop { get; set; }
        /// <summary>
        /// 车到/走标记,1 车到 0 车走
        /// </summary>
        [DataMember]
        public int InOrOutFlag { get; set; }
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName,
                    InOrOutFlag == 1 ? Resouce.Resource1.Report_CarArrived : Resouce.Resource1.Report_CarLeave);
            }
        }
        #endregion
    }
}

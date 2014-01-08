using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Enum;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 设备状态上线状态实体对象
    /// </summary>
    [DataContract]
    public class EntranceStatusReport : ReportBase
    {
        #region 构造函数
        public EntranceStatusReport()
        {
        }

        public EntranceStatusReport(int parkID, int entranceID, DateTime eventDateTime, string sourceName, EntranceStatus status)
            : base(parkID, entranceID, eventDateTime, sourceName)
        {
            this.Status = status;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置控制器状态
        /// </summary>
        [DataMember]
        public EntranceStatus Status { get; set; }
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get
            {
                string msg = string.Format("【{0} ＠ {1}】:{2}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            SourceName, Resouce.EntranceStatusDescription.GetDescription(Status));
                return msg;
            }
        }
        #endregion
    }
}

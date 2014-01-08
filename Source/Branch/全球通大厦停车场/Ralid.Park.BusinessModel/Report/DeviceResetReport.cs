using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Enum;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Report
{
    [DataContract]
    public class DeviceResetReport : ReportBase
    {
        #region 构造函数
        public DeviceResetReport()
        {
        }

        public DeviceResetReport(int parkID, int entranceID, DateTime eventDateTime, string sourceName)
            : base(parkID, entranceID, eventDateTime, sourceName)
        {
        }
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName,
                   Resouce.Resource1.Report_DeviceReset);
            }
        }
        #endregion
    }
}

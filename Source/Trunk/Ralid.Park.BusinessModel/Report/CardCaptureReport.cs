using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 收卡机收卡一张通知
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CardCaptureReport : ReportBase
    {
        #region 构造函数
        public CardCaptureReport(int parkID, int entranceID, DateTime eventDatetime, string sourceName)
            : base(parkID, entranceID, eventDatetime, sourceName)
        {
        }

        public CardCaptureReport()
        {
        }
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName,
                    Resouce.Resource1.Report_CardCapture);
            }
        }
        #endregion
    }
}

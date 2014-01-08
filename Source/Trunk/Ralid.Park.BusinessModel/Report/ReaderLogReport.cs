using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 读卡器日志通知
    /// </summary>
    [DataContract]
    public class ReaderLogReport : ReportBase
    {
        #region 构造函数
        public ReaderLogReport(int parkID, int entranceID, DateTime eventDatetime, string sourceName)
            : base(parkID, entranceID, eventDatetime, sourceName)
        {
        }

        public ReaderLogReport()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置日志信息
        /// </summary>
        public string LogMsg { get; set; }
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName,
                    LogMsg);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Report
{
    public class CardTakeoutReport : ReportBase
    {
        #region 构造函数
        public CardTakeoutReport(int parkID, int entranceID, DateTime eventDatetime, string sourceName)
            : base(parkID, entranceID, eventDatetime, sourceName)
        {
        }

        public CardTakeoutReport()
        {
        }
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName, "吐卡一张");
            }
        }
        #endregion
    }
}

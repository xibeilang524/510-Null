using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 记录ID
    /// </summary>
    public class RecordID
    {
        /// <summary>
        /// 获取或设置记录的卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置记录发生时间
        /// </summary>
        public DateTime RecordDateTime { get; set; }

        public RecordID()
        {
        }

        public RecordID(string cardID, DateTime eventDateTime)
        {
            CardID = cardID;
            RecordDateTime = eventDateTime;
        }
    }
}

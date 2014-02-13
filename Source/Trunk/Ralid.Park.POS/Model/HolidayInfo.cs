using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.POS.Model
{
    [Serializable]
    public class HolidayInfo
    {
        #region 构造函数
        public HolidayInfo()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置节假日的开始日期
        /// </summary>
        public DateTime StartDate{get;set;}
        /// <summary>
        /// 获取或设置节假日的结束日期
        /// </summary>
        public DateTime EndDate{get;set;}
        /// <summary>
        /// 获取为此节假日调整的周末转工作日日期段
        /// </summary>
        public List<DatetimeInterval> WeekenToWorkDayInterval{get;set;}
        #endregion
    }
}

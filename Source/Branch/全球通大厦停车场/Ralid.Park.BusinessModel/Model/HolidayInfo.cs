using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Interface;

namespace Ralid.Park.BusinessModel.Model
{
    [DataContract]
    public class HolidayInfo
    {
        #region 构造函数
        public HolidayInfo()
        {
        }
        #endregion

        #region 私有变量
        [DataMember]
        private DateTime _StartDate;

        [DataMember]
        private DateTime _EndDate;

        [DataMember]
        private List<DateTime> _WeekendToWorkDays = new List<DateTime>();

        [DataMember]
        private List<DatetimeInterval> _WeekenToWorkDayInterval=new List<DatetimeInterval>();
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置节假日的开始日期
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value.Date;
            }
        }
        /// <summary>
        /// 获取或设置节假日的结束日期
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value.Date;
            }
        }

        ///// <summary>
        ///// 获取为此节假日调整的周末转工作日
        ///// </summary>
        //public List<DateTime> WeekendToWorkDays
        //{
        //    get { return _WeekendToWorkDays; }
        //}

        /// <summary>
        /// 获取为此节假日调整的周末转工作日日期段
        /// </summary>
        public List<DatetimeInterval> WeekenToWorkDayInterval
        {
            get { return _WeekenToWorkDayInterval; }
        }
        #endregion
    }
}

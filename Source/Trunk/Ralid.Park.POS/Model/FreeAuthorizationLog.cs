using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.POS.Model
{
    /// <summary>
    /// 免费授权记录
    /// </summary>
    public class FreeAuthorizationLog
    {

        #region  构造函数
        public FreeAuthorizationLog()
        {
        }
        #endregion

        #region 实体字段
        /// <summary>
        /// 获取或设置授权时间
        /// </summary>
        public DateTime LogDateTime { get; set; }
        /// <summary>
        /// 获取或设置授权卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置免费开始时间
        /// </summary>
        public DateTime BeginDateTime { get; set; }
        /// <summary>
        /// 获取或设置免费结束时间
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// 获取或设置卡片是否在场
        /// </summary>
        public bool InPark { get; set; }
        /// <summary>
        /// 获取或设置卡片是否不允许多次进出
        /// </summary>
        public bool CheckOut { get; set; }
        /// <summary>
        /// 获取或设置授权操作员
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 获取或设置授权工作站
        /// </summary>
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置备注信息
        /// </summary>
        public string Memo { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 获取免费小时数
        /// </summary>
        public int FreeHours
        {
            get
            {
                if (EndDateTime > BeginDateTime)
                {
                    TimeSpan ts = new TimeSpan(GetMyDateTime(EndDateTime).Ticks - GetMyDateTime(BeginDateTime).Ticks);
                    return (int)Math.Ceiling(ts.TotalHours);
                }
                return 0;
            }
        }
        /// <summary>
        /// 获取免费天数
        /// </summary>
        public int FreeDays
        {
            get
            {
                if (EndDateTime > BeginDateTime)
                {
                    TimeSpan ts = new TimeSpan(GetMyDateTime(EndDateTime).Ticks - GetMyDateTime(BeginDateTime).Ticks);
                    return (int)Math.Ceiling(ts.TotalDays);
                }
                return 0;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取只精确到分钟的日期时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DateTime GetMyDateTime(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    public class CarFlowStatistics
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置统计时长
        /// </summary>
        public string TimeInterval { get; set; }
        /// <summary>
        /// 获取或设置统计时间内入场的临时车
        /// </summary>
        public int TempCardIn { get; set; }
        /// <summary>
        /// 获取或设置统计时间内入场的月卡车
        /// </summary>
        public int MonthCardIn { get; set; }
        /// <summary>
        /// 获取或设置统计时间内入场的储值卡车
        /// </summary>
        public int PrepayCardIn { get; set; }
        /// <summary>
        /// 获取统计时间内入场的总车数量
        /// </summary>
        public int TotalIn
        {
            get
            {
                return TempCardIn + MonthCardIn + PrepayCardIn;
            }
        }
        /// <summary>
        /// 获取或设置统计时间内出场的临时卡车数量
        /// </summary>
        public int TempCardOut { get; set; }
        /// <summary>
        /// 获取或设置统计时间内出场的月卡车数量
        /// </summary>
        public int MonthCardOut { get; set; }
        /// <summary>
        /// 获取或设置统计时间内出场的储值卡车数量
        /// </summary>
        public int PrepayCardOut { get; set; }
        /// <summary>
        /// 获取统计时间内出场的总车数量
        /// </summary>
        public int TotalOut
        {
            get { return TempCardOut + MonthCardOut + PrepayCardOut; }
        }
        #endregion
    }

    /// <summary>
    /// 统计类型
    /// </summary>
    public enum CarFlowStatisticsType
    {
        /// <summary>
        /// 按小时统计
        /// </summary>
        perHour=1,
        /// <summary>
        /// 按天统计
        /// </summary>
        perDay=2,
        /// <summary>
        /// 按月统计
        /// </summary>
        perMonth=3
    }
}

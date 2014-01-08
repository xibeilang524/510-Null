using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 通道权限的有效时间段
    /// </summary>
    [DataContract]
    public class AccessTimeZone
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置权限开始时间点
        /// </summary>
        [DataMember]
        public TimeEntity BeginTime { get; set; }
        /// <summary>
        /// 获取或设置权限结束时间点
        /// </summary>
        [DataMember]
        public TimeEntity EndTime { get; set; }

        /// <summary>
        /// 获取或设置节假日是否有效
        /// </summary>
        [DataMember]
        public bool IncludeHoliday { get; set; }

        /// <summary>
        /// 获取或设置通行通道
        /// </summary>
        [DataMember]
        public List<int> AccessEntrances { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 权限组是否有在某个时间通过某个通道的权限
        /// </summary>
        /// <param name="entranceID"></param>
        /// <param name="accessDT"></param>
        /// <returns></returns>
        public bool CanAccess(int entranceID, DateTime accessDT)
        {
            bool ret = false;
            if (!IncludeHoliday && HolidaySetting.Current != null && HolidaySetting.Current.IsHoliday(accessDT))  //首先判断节假日
            {
                ret = false;
            }
            else
            {
                int tm = (new TimeEntity(accessDT.Hour, accessDT.Minute)).TotalMinutes;
                if (BeginTime.TotalMinutes < EndTime.TotalMinutes)  // 
                {
                    if (BeginTime.TotalMinutes <= tm && tm <= EndTime.TotalMinutes)
                    {
                        ret = AccessEntrances.Exists(id => id == entranceID);
                    }
                }
                else if (BeginTime.TotalMinutes == EndTime.TotalMinutes) //全天有效
                {
                    ret = AccessEntrances.Exists(id => id == entranceID);
                }
                else
                {
                    if ((BeginTime.TotalMinutes <= tm && tm <= (new TimeEntity(23, 59)).TotalMinutes) ||
                        (tm <= EndTime.TotalMinutes))
                    {
                        ret = AccessEntrances.Exists(id => id == entranceID);
                    }
                }
            }
            return ret;
        }
        #endregion
    }
}

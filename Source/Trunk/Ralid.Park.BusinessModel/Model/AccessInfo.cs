using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 通道权限
    /// </summary>
    [DataContract]
    public class AccessInfo
    {
        #region 公共属性
        /// <summary>
        /// 权限组ID
        /// </summary>
        [DataMember]
        public byte ID { get; set; }

        /// <summary>
        ///权限组名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///获取权限组的时区信息
        /// </summary>
        [DataMember]
        public List<AccessTimeZone> AccessTimeZones { get; set; }
        #endregion

        #region 公共只读属性
        /// <summary>
        /// 获取通道权限中的所有通道控制器及时区信息
        /// </summary>
        public Dictionary<int,List<AccessTimeZone>> EntrancesAndTimeZones
        {
            get
            {
                Dictionary<int, List<AccessTimeZone>> entrances = new Dictionary<int, List<AccessTimeZone>>();
                foreach (AccessTimeZone zone in AccessTimeZones)
                {
                    foreach (int id in zone.AccessEntrances)
                    {
                        if (!entrances.ContainsKey(id))
                        {
                            entrances.Add(id, new List<AccessTimeZone>());
                        }
                        entrances[id].Add(zone);
                    }
                }
                return entrances;
            }
        }

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
            if (AccessTimeZones != null && AccessTimeZones.Count > 0)
            {
                foreach (AccessTimeZone tz in AccessTimeZones)
                {
                    if (tz.CanAccess(entranceID, accessDT))
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取某个控制器的时区信息
        /// </summary>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        public List<AccessTimeZone> GetAccessTimeZones(int entranceID)
        {
            List<AccessTimeZone> timezones = new List<AccessTimeZone>();
            foreach (AccessTimeZone zone in AccessTimeZones)
            {
                if (zone.AccessEntrances.Any(item => item == entranceID))
                {
                    timezones.Add(zone);
                }
            }
            return timezones;
        }
        #endregion
    }
}

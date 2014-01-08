using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    [DataContract]
    public class AccessSetting
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置系统当前的权限设置
        /// </summary>
        public static AccessSetting Current { get; set; }
        #endregion

        #region 构造函数
        public AccessSetting()
        {

        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 权限列表
        /// </summary>
        [DataMember]
        public List<AccessInfo> Accesses { get; set; }
        #endregion

        #region 公共只读属性
        /// <summary>
        /// 获取权限列表的所有通道控制器及权限
        /// </summary>
        public Dictionary<int, List<AccessInfo>> EntrancesAndAccesses
        {
            get
            {
                return GetEntrancesAndAccesses(Accesses);
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取权限列表中的所有通道控制器及权限
        /// </summary>
        /// <param name="accesses"></param>
        /// <returns></returns>
        public Dictionary<int, List<AccessInfo>> GetEntrancesAndAccesses(List<AccessInfo> accesses)
        {
            Dictionary<int, List<AccessInfo>> entrances = new Dictionary<int, List<AccessInfo>>();
            foreach (AccessInfo access in accesses)
            {
                Dictionary<int, List<AccessTimeZone>> timezones = access.EntrancesAndTimeZones;
                foreach (int id in timezones.Keys)
                {
                    if (!entrances.ContainsKey(id))
                    {
                        entrances.Add(id, new List<AccessInfo>());
                    }
                    entrances[id].Add(access);
                }
            }
            return entrances;
        }

        /// <summary>
        /// 获取权限列表中相关的通道
        /// </summary>
        /// <param name="accesses"></param>
        /// <returns></returns>
        public List<int> GetEntrancesIDs(List<AccessInfo> accesses)
        {
            List<int> entrances = new List<int>();
            foreach (AccessInfo access in accesses)
            {
                foreach (AccessTimeZone zone in access.AccessTimeZones)
                {
                    entrances.AddRange(zone.AccessEntrances);
                }
            }
            entrances = entrances.Distinct().ToList();
            return entrances;
        }
        
        /// <summary>
        /// 获取某通道的权限列表
        /// </summary>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        public List<AccessInfo> GetAccesses(int entranceID)
        {
            List<AccessInfo> accesses = new List<AccessInfo>();
            foreach (AccessInfo access in Accesses)
            {
                foreach (AccessTimeZone zone in access.AccessTimeZones)
                {
                    if (zone.AccessEntrances.Any(item => item == entranceID))
                    {
                        accesses.Add(access);
                        break;
                    }
                }
            }
            accesses = (from item in accesses 
                        orderby item.ID ascending 
                        select item).ToList();
            return accesses;
        }
        #endregion
    }
}

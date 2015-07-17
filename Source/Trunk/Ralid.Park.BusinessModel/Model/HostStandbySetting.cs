using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 服务器切换设置
    /// </summary>
    [DataContract]
    public class HostStandbySetting
    {
        #region 构造函数
        public HostStandbySetting()
        {
            SMSItems = new List<SMSItem>();
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置停车场ID
        /// </summary>
        [DataMember]
        public int ParkID { get; set; }
        /// <summary>
        /// 获取或设置主机IP
        /// </summary>
        [DataMember]
        public string HostIP { get; set; }
        /// <summary>
        /// 获取或设置从机IP
        /// </summary>
        [DataMember]
        public string StandbyIP { get; set; }
        /// <summary>
        /// 获取或设置最近一次的服务器IP
        /// </summary>
        [DataMember]
        public string LastIP { get; set; }
        /// <summary>
        /// 获取或设置最近一次的服务器使用状态
        /// </summary>
        [DataMember]
        public HostStandbyStatus LastStatus { get; set; }
        /// <summary>
        /// 获取或设置最近一次的双机热备使用时间
        /// </summary>
        [DataMember]
        public DateTime? LastStart { get; set; }
        /// <summary>
        /// 获取或设置是否发送短信提醒
        /// </summary>
        [DataMember]
        public bool SendSMS { get; set; }
        /// <summary>
        /// 获取或设置主从机切换时需要发送短信提醒的设置列表
        /// </summary>
        [DataMember]
        public List<SMSItem> SMSItems { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 是否主机
        /// </summary>
        /// <param name="ips"></param>
        /// <returns></returns>
        public bool IsHost(IPAddress ip)
        {
            if (!string.IsNullOrEmpty(HostIP) && ip != null)
            {
                return ip.ToString() == HostIP;
            }
            return false;
        }
        /// <summary>
        /// 是否主机
        /// </summary>
        /// <param name="ips"></param>
        /// <returns></returns>
        public bool IsHost(IPAddress[] ips)
        {
            if (!string.IsNullOrEmpty(HostIP) && ips != null)
            {
                return ips.Any(item => item.ToString() == HostIP);
            }
            return false;
        }
        /// <summary>
        /// 是否从机
        /// </summary>
        /// <param name="ips"></param>
        /// <returns></returns>
        public bool IsStandby(IPAddress ip)
        {
            if (!string.IsNullOrEmpty(StandbyIP) && ip != null)
            {
                return ip.ToString() == StandbyIP;
            }
            return false;
        }
        /// <summary>
        /// 是否从机
        /// </summary>
        /// <param name="ips"></param>
        /// <returns></returns>
        public bool IsStandby(IPAddress[] ips)
        {
            if (!string.IsNullOrEmpty(StandbyIP) && ips != null)
            {
                return ips.Any(item => item.ToString() == StandbyIP);
            }
            return false;
        }
        /// <summary>
        /// 是否切换到主服务器
        /// </summary>
        /// <param name="oldStatus">旧状态</param>
        /// <param name="newStatus">新状态</param>
        /// <returns></returns>
        public bool IsHostSwitch(HostStandbyStatus oldStatus,HostStandbyStatus newStatus)
        {
            if (newStatus != HostStandbyStatus.UnKnown)
            {
                if (oldStatus != newStatus && newStatus == HostStandbyStatus.Host)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 是否切换到从服务器
        /// </summary>
        /// <param name="oldStatus">旧状态</param>
        /// <param name="newStatus">新状态</param>
        /// <returns></returns>
        public bool IsStandbySwitch(HostStandbyStatus oldStatus, HostStandbyStatus newStatus)
        {
            if (newStatus != HostStandbyStatus.UnKnown)
            {
                if (oldStatus != newStatus && newStatus == HostStandbyStatus.Standby)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取一个复制体
        /// </summary>
        /// <returns></returns>
        public HostStandbySetting Clone()
        {
            return this.MemberwiseClone() as HostStandbySetting;
        }
        #endregion
    }

    /// <summary>
    /// 表示一个短信发送设置
    /// </summary>
    [DataContract]
    public class SMSItem
    {
        /// <summary>
        /// 获取或设置姓名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置电话号码
        /// </summary>
        [DataMember]
        public string Telephone { get; set; }
    }

    /// <summary>
    /// 服务器主从使用状态枚举
    /// </summary>
    [DataContract]
    public enum HostStandbyStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        [EnumMember]
        UnKnown = 0,
        /// <summary>
        /// 主机
        /// </summary>
        [EnumMember]
        Host = 1,
        /// <summary>
        /// 从机
        /// </summary>
        [EnumMember]
        Standby = 2
    }

    /// <summary>
    /// 服务器切换提醒信息类
    /// </summary>
    public class ServerSwitchRemind
    {
        /// <summary>
        /// 获取或设置停车场
        /// </summary>
        public string Park { get; set; }
        /// <summary>
        /// 获取或设置切换时间
        /// </summary>
        public string SwitchTime{get;set;}
        /// <summary>
        /// 获取或设置切换主从状态
        /// </summary>
        public string SwitchStatus { get; set; }
        /// <summary>
        /// 获取或设置切换IP
        /// </summary>
        public string SwitchIP{get;set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Interface;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel .Model ;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 描述服务器返回给客户端数据实体 发送方式：服务器－>客户端
    /// Transmitter:上传指令的设备地址，和设备
    /// </summary>
    [DataContract]
    public abstract class ReportBase
    {
        #region 构造函数
        public ReportBase(int parkID, int entranceID, DateTime eventDatetime,string sourceName)
        {
            this.ParkID = parkID;
            this.EntranceID = entranceID;
            this.EventDateTime = eventDatetime;
            this.SourceName = sourceName;
        }

        public ReportBase()
        {

        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置发生时间
        /// </summary>
        [DataMember]
        public DateTime EventDateTime { get; set; }

        /// <summary>
        /// 获取或设置停车场ID
        /// </summary>
        [DataMember]
        public int ParkID { get; set; }

        /// <summary>
        /// 获取或设置事件源通道的地址(这个只用于CAN总线控制板)
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// 获取或设置事件源的通道地址
        /// </summary>
        [DataMember]
        public int EntranceID { get; set; }

        /// <summary>
        /// 获取或设置事件源的名称
        /// </summary>
        [DataMember]
        public string SourceName { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取事件的文字描述
        /// </summary>
        public abstract string Description { get; }
        #endregion
    }
}

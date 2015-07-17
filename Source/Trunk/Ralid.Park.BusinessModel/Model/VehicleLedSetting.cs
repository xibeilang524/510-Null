using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 车辆信息显示LED屏设置
    /// </summary>
    [DataContract]
    public class VehicleLedSetting
    {
        #region 构造函数
        public VehicleLedSetting()
        {
            Items = new List<VehicleLedItem>();
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置车辆信息显示LED屏集合
        /// </summary>
        [DataMember]
        public List<VehicleLedItem> Items { get; set; }

        /// <summary>
        /// 获取或设置设置中是否存在名称为指定值的LED屏
        /// </summary>
        /// <param name="readerIP"></param>
        /// <returns></returns>
        public bool HasLED(string name)
        {
            if (Items != null) return Items.Exists(item => item.Name == name);
            return false;
        }
        /// <summary>
        /// 获取名称为指定值的车辆信息显示LED屏
        /// </summary>
        /// <param name="readerIP"></param>
        /// <returns></returns>
        public VehicleLedItem GetLED(string name)
        {
            if (Items != null) return Items.FirstOrDefault(item => item.Name == name);
            return null;
        }
        /// <summary>
        /// 通过通信工作站ID获取LED集合
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        public List<VehicleLedItem> GetLEDs(string stationID)
        {
            if (Items != null)
            {
                return Items.FindAll(item => item.StationID == stationID);
            }
            return null;
        }
        /// <summary>
        /// 通过通信工作站ID和通道ID获取LED集合
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        public List<VehicleLedItem> GetLEDsFromEntranceID(string stationID, int entranceID)
        {
            if (Items != null)
            {
                return Items.FindAll(item => item.StationID == stationID && item.EntranceID == entranceID);
            }
            return null;
        }
        /// <summary>
        /// 通过通信工作站ID和停车场ID获取LED集合
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="parkID"></param>
        /// <returns></returns>
        public List<VehicleLedItem> GetLEDsFromParkID(string stationID, int parkID)
        {
            if (Items != null)
            {
                return Items.FindAll(item => item.StationID == stationID && item.ParkID == parkID);
            }
            return null;
        }
        #endregion
    }

    /// <summary>
    /// 表示一个车辆信息显示LED屏
    /// </summary>
    [DataContract]
    public class VehicleLedItem
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置LED屏名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置停车场ID，如果没有指定的话为0
        /// </summary>
        [DataMember]
        public int ParkID { get; set; }
        /// <summary>
        /// 获取或设置通道ID，如果没有指定的话为0
        /// </summary>
        [DataMember]
        public int EntranceID { get; set; }
        /// <summary>
        /// 获取或设置通信的工作站ID，如果没有指定的话为空
        /// </summary>
        [DataMember]
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置通信的COM端口号
        /// </summary>
        [DataMember]
        public byte ComPort { get; set; }
        /// <summary>
        /// 获取或设置是否显示标题
        /// </summary>
        [DataMember]
        public bool ShowTitle { get; set; }
        /// <summary>
        /// 获取或设置是启用显示时长
        /// </summary>
        [DataMember]
        public bool EnabledInterval { get; set; }
        /// <summary>
        /// 获取或设置子屏1地址，如果没有指定的话为0
        /// </summary>
        [DataMember]
        public byte SubAddress1 { get; set; }
        /// <summary>
        /// 获取或设置子屏1标题
        /// </summary>
        [DataMember]
        public string SubTitle1 { get; set; }
        /// <summary>
        /// 获取或设置子屏1显示信息
        /// </summary>
        [DataMember]
        public VehicleLEDMessageType SubMessage1 { get; set; }
        /// <summary>
        /// 获取或设置子屏1显示时长，单位秒，为0时表示永久显示
        /// </summary>
        [DataMember]
        public int SubInterval1 { get; set; }
        /// <summary>
        /// 获取或设置子屏2地址，如果没有指定的话为0
        /// </summary>
        [DataMember]
        public byte SubAddress2 { get; set; }
        /// <summary>
        /// 获取或设置子屏2标题
        /// </summary>
        [DataMember]
        public string SubTitle2 { get; set; }
        /// <summary>
        /// 获取或设置子屏2显示信息
        /// </summary>
        [DataMember]
        public VehicleLEDMessageType SubMessage2 { get; set; }
        /// <summary>
        /// 获取或设置子屏2显示时长，单位秒，为0时表示永久显示
        /// </summary>
        [DataMember]
        public int SubInterval2 { get; set; }
        /// <summary>
        /// 获取或设置子屏3地址，如果没有指定的话为0
        /// </summary>
        [DataMember]
        public byte SubAddress3 { get; set; }
        /// <summary>
        /// 获取或设置子屏3标题
        /// </summary>
        [DataMember]
        public string SubTitle3 { get; set; }
        /// <summary>
        /// 获取或设置子屏3显示信息
        /// </summary>
        [DataMember]
        public VehicleLEDMessageType SubMessage3 { get; set; }
        /// <summary>
        /// 获取或设置子屏3显示时长，单位秒，为0时表示永久显示
        /// </summary>
        [DataMember]
        public int SubInterval3 { get; set; }
        /// <summary>
        /// 获取或设置说明信息
        /// </summary>
        [DataMember]
        public string Memo { get; set; }

        #endregion

        #region 公共方法
        /// <summary>
        /// 获取一个复制体
        /// </summary>
        /// <returns></returns>
        public VehicleLedItem Clone()
        {
            return this.MemberwiseClone() as VehicleLedItem;
        }
        #endregion

    }
}

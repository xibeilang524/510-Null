using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示停车场参数设置数据缓存的一个类
    /// </summary>
    [DataContract]
    public class ParkingParameterDataBuffer
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置当前停车场参数设置数据缓存
        /// </summary>
        public static ParkingParameterDataBuffer Current { get; set; }
        #endregion

        #region 构造函数
        public ParkingParameterDataBuffer()
        {
            Operators = new List<OperatorInfo>();
            WorkStations = new List<WorkStationInfo>();
            APMs = new List<APM>();
        }
        #endregion

        #region 私有属性
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置停车场的常规设置
        /// </summary>
        [DataMember]
        public UserSetting UserSetting { get; set; }
        /// <summary>
        /// 获取或设置停车场的节假日设置
        /// </summary>
        [DataMember]
        public HolidaySetting HolidaySetting { get; set; }
        /// <summary>
        /// 获取或设置停车场的通道权限设置
        /// </summary>
        [DataMember]
        public AccessSetting AccessSetting { get; set; }
        /// <summary>
        /// 获取或设置停车场的费率设置
        /// </summary>
        [DataMember]
        public TariffSetting TariffSetting { get; set; }
        /// <summary>
        /// 获取或设置停车场的自定义车型设置
        /// </summary>
        [DataMember]
        public CarTypeSetting CarTypeSetting { get; set; }
        /// <summary>
        /// 获取或设置停车场的自定义卡类设置
        /// </summary>
        [DataMember]
        public CustomCardTypeSetting CustomCardTypeSetting { get; set; }
        /// <summary>
        /// 获取或设置停车场的基本卡类设置
        /// </summary>
        [DataMember]
        public BaseCardTypeSetting BaseCardTypeSetting { get; set; }
        /// <summary>
        /// 获取或设置停车场的密钥设置
        /// </summary>
        [DataMember]
        public KeySetting KeySetting { get; set; }
        /// <summary>
        /// 获取或设置停车场的操作员
        /// </summary>
        [DataMember]
        public List<OperatorInfo> Operators { get; set; }
        /// <summary>
        /// 获取或设置停车场的工作站
        /// </summary>
        [DataMember]
        public List<WorkStationInfo> WorkStations { get; set; }
        /// <summary>
        /// 获取或设置自助缴费机
        /// </summary>
        [DataMember]
        public List<APM> APMs { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Runtime.Serialization;
using Ralid.GeneralLibrary;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    /// <summary>
    /// 表示车位引导系统的中央控制器
    /// </summary>
    [DataContract]
    public class CentralController : DeviceBase
    {
        #region 构造函数
        public CentralController()
        {
            ControlPort = 4001;
        }
        #endregion

        #region 私有变量
        private ParkingGuideBus[] _Buses;  //
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置IP地址
        /// </summary>
        [DataMember]
        public string IPAddress{get;set;}
        /// <summary>
        /// 获取或设置端口号
        /// </summary>
        [DataMember]
        public int ControlPort { get; set; }

        /// <summary>
        /// 获取或设置子网掩码
        /// </summary>
        [DataMember]
        public string IPMask { get; set; }

        /// <summary>
        /// 获取或设置网关地址
        /// </summary>
        [DataMember]
        public string GateWay{get;set;}

        /// <summary>
        /// 获取或设置MAC地址
        /// </summary>
        [DataMember]
        public string MACAddress { get; set; }

        /// <summary>
        /// 获取硬件下的所有总线
        /// </summary>
        public override ParkingGuideBus[] Buses
        {
            get
            {
                if (_Buses == null)
                {
                    _Buses = new ParkingGuideBus[3];
                    for (int i = 0; i < _Buses.Length; i++)
                    {
                        _Buses[i] = new ParkingGuideBus(i, 64);
                        _Buses[i].Parent = this;
                    }
                }
                return _Buses.ToArray();
            }
        }

        /// <summary>
        /// 获取中央控制器下所有的区域控制器
        /// </summary>
        public List<RegionController> RegionControllers
        {
            get
            {
                List<RegionController> items = new List<RegionController>();
                foreach (ParkingGuideBus bus in Buses)
                {
                    foreach (SubDevice device in bus.SubDevices)
                    {
                        if (device is RegionController)
                        {
                            items.Add(device as RegionController);
                        }
                    }
                }
                return items;
            }
        }

        /// <summary>
        /// 获取中央控制器下的所有引导屏
        /// </summary>
        public List<ParkingScreen> Screens
        {
            get
            {
                List<ParkingScreen> items = new List<ParkingScreen>();
                foreach (RegionController rc in RegionControllers)
                {
                    items.AddRange(rc.Screens);
                }
                foreach (ParkingGuideBus bus in Buses)
                {
                    foreach (SubDevice device in bus.SubDevices)
                    {
                        if (device is ParkingScreen)
                        {
                            items.Add(device as ParkingScreen);
                        }
                    }
                }
                return items;
            }
        }

        /// <summary>
        /// 获取中央控制器下的所有车位探测器
        /// </summary>
        public List<ParkingDetector> Detectors
        {
            get
            {
                List<ParkingDetector> items = new List<ParkingDetector>();
                List<RegionController> rcs = RegionControllers;
                foreach (RegionController rc in rcs)
                {
                    items.AddRange(rc.Detectors);
                }
                return items;
            }
        }
        #endregion
    }
}

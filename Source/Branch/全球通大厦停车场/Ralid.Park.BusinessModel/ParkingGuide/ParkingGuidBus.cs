using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    /// <summary>
    /// 表示车位引导系统设备的485总线
    /// </summary>
    public class ParkingGuideBus
    {
        #region 构造函数
        public ParkingGuideBus(int num, int capacity)
        {
            _BusNum = num;
            _SubDevices = new SubDevice[capacity];
        }
        #endregion

        #region 私有变量
        private int _BusNum;
        private SubDevice[] _SubDevices;  //要访问它请直接使用SubDevices属性
        #endregion

        #region 私有方法
        #endregion

        #region 公共属性
        /// <summary>
        /// 或取总线的最大子设备数量
        /// </summary>
        public int Capacity { get { return _SubDevices.Length; } }

        /// <summary>
        /// 获取总线编号
        /// </summary>
        public int BusNum { get { return _BusNum; } }

        /// <summary>
        /// 获取总线上的所有子设备
        /// </summary>
        public SubDevice[] SubDevices
        {
            get
            {
                List<SubDevice> items = new List<SubDevice>();
                foreach (SubDevice d in _SubDevices)
                {
                    if (d != null) items.Add(d);
                }
                return items.ToArray();
            }
        }

        /// <summary>
        /// 获取总线上的所有区域控制器子设备
        /// </summary>
        public List<RegionController> RegionControllers
        {
            get
            {
                return (from d in SubDevices
                        where d is RegionController
                        select d as RegionController).ToList();
            }
        }

        /// <summary>
        /// 获取一个总线上还没有设备占用的拨码地址,如果所有地址上都有设备，返回-1
        /// </summary>
        public int GetAnEmptyDialCode()
        {
            int ret=-1;
            for (int i = 0; i < Capacity; i++)
            {
                if (_SubDevices[i] == null)
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取或设置总线所属的设备
        /// </summary>
        public DeviceBase Parent { get; set; }

        /// <summary>
        /// 获取总线某拨码地址的设备,没有找到返回空，如果查询的拨码地址大于总线容量，抛出IndexOutOfRangeException异常
        /// </summary>
        /// <param name="dialCode"></param>
        /// <returns></returns>
        public SubDevice this[int dialCode]
        {
            get
            {
                if (dialCode >= 0 && dialCode < _SubDevices.Length)
                {
                    return _SubDevices[dialCode];
                }
                else
                {
                    throw new IndexOutOfRangeException("下标超出总线的最大容量");
                }
            }
        }

        /// <summary>
        /// 获取设备所属的区域控制器
        /// </summary>
        public CentralController CentralController
        {
            get
            {
                DeviceBase parent = this.Parent;
                while (parent != null && !(parent is CentralController))
                {
                    parent = parent.Parent;
                }
                return parent as CentralController;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 增加子设备
        /// </summary>
        /// <param name="device"></param>
        public void Add(SubDevice device)
        {
            if (device.DialCode >= 0 && device.DialCode < _SubDevices.Length)
            {
                if (_SubDevices[device.DialCode] == null)
                {
                    _SubDevices[device.DialCode] = device;
                    device.Parent = this.Parent;
                    device.BusNum = this._BusNum;
                    if (this.Parent != null)
                    {
                        device.ParentID = this.Parent.ID;
                    }
                }
                else
                {
                    throw new InvalidOperationException(string.Format("总线地址{0}下已经存在设备", device.DialCode));
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format("无效的总线地址{0}", device.DialCode));
            }
        }
        /// <summary>
        /// 增加子设备，不成功时返回false,而不是抛出异常
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public bool TryAdd(SubDevice device)
        {
            try
            {
                Add(device);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 移除某个总线地址的子设备
        /// </summary>
        /// <param name="device"></param>
        public void Remove(int dialCode)
        {
            SubDevice d = _SubDevices[dialCode];
            if (d != null)
            {
                _SubDevices[dialCode] = null;
            }
        }

        /// <summary>
        /// 改变子设备的拨码地址改
        /// </summary>
        /// <param name="device"></param>
        /// <param name="newDialCode"></param>
        public void Move(SubDevice device, int newDialCode)
        {
            int dialCode = device.DialCode;
            if (newDialCode >= 0 && newDialCode < _SubDevices.Length)
            {
                if (_SubDevices[newDialCode] == null)
                {
                    Remove(device.DialCode);
                    device.DialCode = newDialCode;
                    _SubDevices[newDialCode] = device;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("总线地址{0}下已经存在设备", device.DialCode));
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format("无效的总线地址{0}", device.DialCode));
            }
        }
        /// <summary>
        /// 改变子设备的拨码地址改,不成功时返回false，而不是抛出异常
        /// </summary>
        /// <param name="device"></param>
        /// <param name="newDialCode"></param>
        /// <returns></returns>
        public bool TryMove(SubDevice device, int newDialCode)
        {
            try
            {
                Move(device, newDialCode);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 清除所有的总线设备
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < _SubDevices.Length; i++)
            {
                _SubDevices[i] = null;
            }
        }
        #endregion
    }
}

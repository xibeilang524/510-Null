using System;
using System.Collections.Generic;
using System.Linq;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel .Notify;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.ParkAdapter
{
    /// <summary>
    /// 用于管理系统所有的停车场通信客户端
    /// </summary>
    public class ParkingAdapterManager
    {
        #region 静态方法和属性
        private static ParkingAdapterManager _instance;
        public static ParkingAdapterManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ParkingAdapterManager();
                }
                return _instance;
            }
        }
        #endregion

        private Dictionary<int, IParkingAdapter> _parkAdapters;

        #region 构造函数
        private ParkingAdapterManager()
        {
            _parkAdapters = new Dictionary<int, IParkingAdapter>();
        }
        #endregion

        #region 私有变量
        private object _ListLock = new object();
        #endregion

        #region 公共方法
        /// <summary>
        /// 增加一个停车场转换器
        /// </summary>
        /// <param name="pa"></param>
        public void Add(int parkID,IParkingAdapter pad)
        {
            lock (_ListLock)
            {
                _parkAdapters.Add(parkID, pad);
            }
        }

        /// <summary>
        /// 获取停车场的通信转换器
        /// </summary>
        /// <param name="parkID">停车场ID</param>
        /// <returns></returns>
        public IParkingAdapter this[int parkID]
        {
            get
            {
                lock (_ListLock)
                {
                    foreach (var ad in _parkAdapters)
                    {
                        if (ad.Key == parkID)
                        {
                            return ad.Value;
                        }
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取所有的停车场通信转换器
        /// </summary>
        public IParkingAdapter[] ParkAdapters
        {
            get
            {
                lock (_ListLock)
                {
                    return _parkAdapters.Values.ToArray();
                }
            }
        }
        
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示读取硬件设备收费记录的指示器
    /// </summary>
    [DataContract]
    public class DevicePaymentRecordPointer
    {

        #region 静态属性
        /// <summary>
        /// 系统当前读取硬件设备收费记录的指示器
        /// </summary>
        public static DevicePaymentRecordPointer Current { get; set; }
        #endregion

        #region 构造函数
        public DevicePaymentRecordPointer()
        {
            _Pointer = new Dictionary<int, int>();
        }
        #endregion

        #region 私有变量
        [DataMember]
        private Dictionary<int, int> _Pointer;

        private object _ListLock = new object();
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置上次读取设备收费记录的最后一条记录流水号
        /// </summary>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        public int this[int entranceID]
        {
            get
            {
                lock (_ListLock)
                {
                    if (!_Pointer.ContainsKey(entranceID))
                    {
                        _Pointer.Add(entranceID, 0);
                    }
                    return _Pointer[entranceID];
                }
            }
            set
            {
                lock (_ListLock)
                {
                    if (!_Pointer.ContainsKey(entranceID))
                    {
                        _Pointer.Add(entranceID, 0);
                    }
                    _Pointer[entranceID] = value;
                }
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.LocalDataBase.Model
{
    /// <summary>
    /// 表示停车场数据缓存的一个类
    /// </summary>
    [DataContract]
    public class LDB_ParkingDataBuffer
    {
        #region 静态属性
        public static LDB_ParkingDataBuffer Current
        {
            get
            {
                if (_instance == null) _instance = new LDB_ParkingDataBuffer();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        #endregion

        #region 构造函数
        public LDB_ParkingDataBuffer()
        {
            Operators = new List<OperatorInfo>();
            WorkStations = new List<WorkStationInfo>();
        }
        #endregion

        #region 私有属性
        private static LDB_ParkingDataBuffer _instance;
        #endregion

        #region 公共属性
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
        #endregion
    }
}

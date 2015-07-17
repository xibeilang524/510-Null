using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 通道路口信息类
    /// </summary>
    public class RoadWayInfo
    {
        #region 私有变量
        private List<int> _EntranceList = new List<int>();
        private string _EntranceIDs
        {
            get
            {
                if (EntranceList != null && EntranceList.Count > 0)
                {
                    string[] list = _EntranceList.Select(en => en.ToString()).ToArray();
                    return string.Join(",", list);
                }
                return string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _EntranceList = new List<int>();
                }
                else
                {
                    _EntranceList = value.Split(',').Select(str => int.Parse(str)).ToList();
                }
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置通道路口ID
        /// </summary>
        public int RoadID { get; set; }
        /// <summary>
        /// 获取或设置通道路口名称
        /// </summary>
        public string RoadName { get; set; }
        /// <summary>
        /// 获取或设置通道路口控制的控制器列表
        /// </summary>
        public List<int> EntranceList
        {
            get
            {
                return _EntranceList;
            }
            set
            {
                _EntranceList = value;
            }
        }
        /// <summary>
        /// 获取或设置通道路口的模式
        /// </summary>
        public RoadMode Mode { get; set; }
        #endregion
    }
}

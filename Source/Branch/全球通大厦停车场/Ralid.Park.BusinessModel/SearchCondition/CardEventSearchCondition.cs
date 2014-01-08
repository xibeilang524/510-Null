using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class CardEventSearchCondition : RecordSearchCondition
    {
        /// <summary>
        /// 获取或设置查询条件中的事件发生的停车场
        /// </summary>
        public int? ParkID { get; set; }
        /// <summary>
        /// 获取或设置查询条件中的事件发生通道
        /// </summary>
        public List<EntranceInfo> Source { get; set; }  //
        /// <summary>
        /// 获取或设置查询条件中的是否只查询出场事件
        /// </summary>
        public bool OnlyExitEvent { get; set; } //
    }
}

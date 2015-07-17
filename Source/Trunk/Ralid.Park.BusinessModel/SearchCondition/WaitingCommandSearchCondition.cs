using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class WaitingCommandSearchCondition : SearchCondition
    {
        /// <summary>
        /// 设置或获取控制板ID
        /// </summary>
        public int EntranceID { get; set; }
        /// <summary>
        /// 设置或获取命令类型
        /// </summary>
        public CommandType? CommandType { get; set; }
        /// <summary>
        /// 设置或获取卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 设置或获取下发状态
        /// </summary>
        public WaitingCommandStatus? Status { get; set; }
    }
}

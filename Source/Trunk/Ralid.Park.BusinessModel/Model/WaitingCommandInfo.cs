using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示等待下载的命令
    /// </summary>
    public class WaitingCommandInfo
    {
        /// <summary>
        /// 获取或设置卡号或车牌名单的车牌号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置控制板ID
        /// </summary>
        public int EntranceID { get; set; }
        /// <summary>
        /// 获取或设置命令动作
        /// </summary>
        public CommandType Command { get; set; }
        /// <summary>
        /// 获取或设置命令下发状态
        /// </summary>
        public WaitingCommandStatus Status { get; set; }
        /// <summary>
        /// 获取或设置CardID类型，为空时为默认0,
        /// 0：名单的卡号
        /// 1：名单的车牌号
        /// </summary>
        public byte? CardIDType { get; set; }

        public WaitingCommandInfo()
        {
            CardID = string.Empty;
        }
    }
}

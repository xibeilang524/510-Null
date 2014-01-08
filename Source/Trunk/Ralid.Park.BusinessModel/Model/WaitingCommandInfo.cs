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
        /// 获取或设置卡号
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

        public WaitingCommandInfo()
        {
            CardID = string.Empty;
        }
    }
}

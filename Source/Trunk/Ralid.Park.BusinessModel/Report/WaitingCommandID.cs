using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 表示等待下载的命令主键
    /// </summary>
    public class WaitingCommandID
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

        public WaitingCommandID()
        {
            CardID = string.Empty;
        }

        public WaitingCommandID(int entranceid,CommandType command,string cardid)
        {
            EntranceID = entranceid;
            Command = command;
            CardID = cardid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.UserControls.VideoPanels
{
    /// <summary>
    /// 视频状态
    /// </summary>
    public enum  VideoStatus
    {
        /// <summary>
        /// 未连接
        /// </summary>
        Disconnected=0,
        /// <summary>
        /// 已连接
        /// </summary>
        Connected=1,
        /// <summary>
        /// 播放
        /// </summary>
        Playing=2,
        /// <summary>
        /// 暂停
        /// </summary>
        Paused=3
    }
}

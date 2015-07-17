using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UI
{
    public class SwitchRoadModeArgs : EventArgs
    {
        /// <summary>
        /// 获取或设置模式
        /// </summary>
        public RoadMode Mode { get; set; }

        /// <summary>
        /// 获取或设置通道路口
        /// </summary>
        public RoadWayInfo RoadWay { get; set; }

        public SwitchRoadModeArgs()
        { 
        }
        public SwitchRoadModeArgs(RoadMode mode, RoadWayInfo info)
        {
            this.Mode = mode;
            this.RoadWay = info;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.UserControls.VideoPanels
{
    /// <summary>
    /// 视频控件创建工厂,此工厂根据不同的视频设备产家生成不同的视频控件
    /// </summary>
    public class VideoPanelFactory
    {
        public static VideoPanel CreatePanel()
        {
            if (UserSetting.Current != null && UserSetting.Current.VideoType == 1)
            {
                return new XingLuTongVideoPanel();
            }
            else if (UserSetting.Current != null && UserSetting.Current.VideoType == 2)
            {
                return new JingYangVideoPanel();
            }
            else if (UserSetting.Current != null && UserSetting.Current.VideoType == 3)
            {
                return new DaHuaVideoPanel();
            }
            return new ACTIVideoControl();
        }


        public static VideoPanel CreatePanel(int videoType)
        {
            if (videoType == (int)VideoServerType.XinLuTong)
            {
                return new XingLuTongVideoPanel();
            }
            else if (videoType == (int)VideoServerType.JingYang)
            {
                return new JingYangVideoPanel();
            }
            else if (videoType == (int)VideoServerType.DaHua)
            {
                return new DaHuaVideoPanel();
            }
            //默认返回ACTi
            return new ACTIVideoControl();
        }
    }
}

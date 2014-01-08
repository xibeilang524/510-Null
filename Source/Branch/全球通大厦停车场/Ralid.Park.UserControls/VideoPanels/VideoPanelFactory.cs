using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
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
            return new ACTIVideoControl();
        }
    }
}

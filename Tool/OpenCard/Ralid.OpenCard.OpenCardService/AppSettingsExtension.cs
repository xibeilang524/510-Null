using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Configuration ;

namespace Ralid.OpenCard.OpenCardService
{
    public static class AppSettingsExtension
    {
        public static void GetYiTingConfig(this AppSettings aps, YiTingShanFuSetting yt)
        {
            yt.IP = "127.0.0.1";
            string temp = aps.GetConfigContent("YiTingIP");
            if (!string.IsNullOrEmpty(temp))
            {
                yt.IP = temp;
            }
            else
            {
                System.Net.IPAddress addr = Ralid.GeneralLibrary.NetTool.GetFirstIP();
                if (addr != null) yt.IP = addr.ToString();
            }
            int port = 0;
            temp = aps.GetConfigContent("YiTingPort");
            if (!string.IsNullOrEmpty(temp)) int.TryParse(temp, out port);
            if (port == 0) port = 16171;
            yt.Port = port;
        }

        public static void SaveYiTingConfig(this AppSettings aps, YiTingShanFuSetting yt)
        {
            aps.SaveConfig("YiTingIP", yt.IP);
            aps.SaveConfig("YiTingPort", yt.Port.ToString());
        }

        public static int GetShowBalanceInterval(this AppSettings aps)
        {
            int ret = 0;
            string temp = aps.GetConfigContent("ShowBalanceInterval");
            if (!string.IsNullOrEmpty(temp)) int.TryParse(temp, out ret);
            return ret <= 0 ? 3 : ret;
        }

        public static void SetShowBalanceInterval(this AppSettings aps, int val)
        {
            aps.SaveConfig("ShowBalanceInterval", val.ToString());
        }
    }
}

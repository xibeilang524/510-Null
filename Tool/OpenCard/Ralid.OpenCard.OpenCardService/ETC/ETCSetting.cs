using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    [DataContract]
    public class ETCSetting
    {
        #region 工厂方法
        private static ETCSetting _CurSetting = null;

        public static ETCSetting GetSetting()
        {
            try
            {
                if (_CurSetting == null)
                {
                    _CurSetting = new ETCSetting();
                    _CurSetting.Devices = new List<ETCDeviceInfo>();
                    var items = ETCDevice.GetAllDevices();
                    if (items != null && items.Length > 0)
                    {
                        _CurSetting.Devices.AddRange(items);
                        var original = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect).GetSetting<ETCSetting>();
                        if (original != null && original.Devices != null)
                        {
                            foreach (var item in items)
                            {
                                var o = original.Devices.SingleOrDefault(it => it.LaneNo == item.LaneNo);
                                item.EntranceID = o != null ? o.EntranceID : 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return _CurSetting;
        }
        #endregion

        public static readonly string CardTyte = "粤通卡";

        #region 构造函数
        private ETCSetting()
        {
        }
        #endregion

        #region 公共属性
        [DataMember]
        public List<ETCDeviceInfo> Devices { get; set; }
        #endregion
    }
}

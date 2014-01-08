using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.OutdoorLEDSetting
{
    /// <summary>
    /// 表示一个停车场户外屏配置存储类
    /// </summary>
    public class ParkOutDoorLedSettingsStorage
    {
        #region 公共方法
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public static ParkOutDoorLedManager Get(int parkID)
        {
            SysParaSettingsBll bll = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);
            ParkOutDoorLedManager man = bll.GetSetting<ParkOutDoorLedManager>("OutdoorLed_Park" + parkID.ToString());
            return man;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static bool Save(ParkOutDoorLedManager setting)
        {
            try
            {
                SysParaSettingsBll bll = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);
                CommandResult ret = bll.SaveSetting<ParkOutDoorLedManager>(setting, "OutdoorLed_Park" + setting.ParkID.ToString());

                ParkCarPortSearchCondition con = new ParkCarPortSearchCondition() { ParkID = setting.ParkID };
                IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(AppSettings.CurrentSetting.ParkConnect);
                List<ParkCarPort> items = ProviderFactory.Create<IParkCarPortProvider>(AppSettings.CurrentSetting.ParkConnect).GetItems(con).QueryObjects;
                foreach (OutDoorLedArea area in setting.Areas)
                {
                    if (area.CardType != null)
                    {
                        ParkCarPort item = items.FirstOrDefault(it => it.CardType.Value == area.CardType && it.CarType.Value == area.CarType);
                        if (item == null)
                        {
                            ParkCarPort pcp = new ParkCarPort()
                            {
                                ParkID = setting.ParkID,
                                CardType = area.CardType,
                                CarType = area.CarType,
                                CarPort = (short)area.CarPort,
                                Vacant = (short)area.Vacant
                            };
                            ProviderFactory.Create<IParkCarPortProvider>(AppSettings.CurrentSetting.ParkConnect).Insert(pcp, unitWork);
                        }
                        else
                        {
                            ParkCarPort pcp = item.Clone();
                            item.CarPort = (short)area.CarPort;
                            item.Vacant = (short)area.Vacant;
                            ProviderFactory.Create<IParkCarPortProvider>(AppSettings.CurrentSetting.ParkConnect).Update(item, pcp, unitWork);
                        }
                    }
                }
                ret = unitWork.Commit();
                return ret.Result == ResultCode.Successful;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return false;
            }
        }
        #endregion
    }
}

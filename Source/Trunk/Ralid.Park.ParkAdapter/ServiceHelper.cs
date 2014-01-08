using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Net;
using System.Threading;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.GeneralLibrary;
using Ralid.GeneralLibrary.WCF;

namespace Ralid.Park.ParkAdapter
{
    public class ServiceHelper : MarshalByRefObject
    {
        public ServiceHelper()
        {
            ////当使用不同程序域创建时，需要重新获取系统参数和停车场硬件缓存
            //SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.AvailableParkConnect);
            //AccessSetting.Current = ssb.GetOrCreateSetting<AccessSetting>();
            //TariffSetting.Current = ssb.GetOrCreateSetting<TariffSetting>();
            //UserSetting.Current = ssb.GetOrCreateSetting<UserSetting>();
            //HolidaySetting.Current = ssb.GetOrCreateSetting<HolidaySetting>();
            //CarTypeSetting.Current = ssb.GetOrCreateSetting<CarTypeSetting>();
            //CustomCardTypeSetting.Current = ssb.GetSetting<CustomCardTypeSetting>();
            //ParkBuffer.Current = new ParkBuffer(AppSettings.CurrentSetting.ParkConnect);
            //ParkBuffer.Current.InValid(AppSettings.CurrentSetting.AvailableParkConnect);
        }

        #region 私有方法
        private void UpdateParkAdapterUri(ParkInfo park)
        {
            string select = string.Empty;//选择登录验证的数据库
            string another = string.Empty;//另外一个数据库

            if (AppSettings.CurrentSetting.SelectedPark == DataBaseType.Master)
            {
                select = AppSettings.CurrentSetting.MasterParkConnect;
                another = AppSettings.CurrentSetting.StandbyParkConnect;
            }
            else
            {
                select = AppSettings.CurrentSetting.StandbyParkConnect;
                another = AppSettings.CurrentSetting.MasterParkConnect; 
            }

            //先更新选择登录验证的数据库,因为登录验证的数据库是可连接的
            UpdatePark(select,park);
            
            //再更新另外一个数据库
            if (!string.IsNullOrEmpty(another))
            {
                Action action = delegate()
                {
                    UpdatePark(another, park);
                };
                Thread t = new Thread(new ThreadStart(action));
                t.Start();//由于不知道另外一个数据库是否能连接，所以这里使用单独线程去更新
            }
        }

        private void UpdatePark(string constr, ParkInfo park)
        {
            ParkBll bll = new ParkBll(constr);
            CommandResult result = bll.UpdateParkAdapterUri(park.ParkID, park.ParkAdapterUri);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 启动工作站的所有停车场服务
        /// </summary>
        /// <param name="stationID"></param>
        public void StartServer(string workstationID)
        {
            int port = 13000;

            IPAddress ipa = GlobalVariables.CurrentParkingCommunicationIP;
            if (ipa != null)
            {
                foreach (ParkInfo park in ParkBuffer.Current.Parks)
                {
                    if (park.HostWorkstation == workstationID && park.ParentID == null)
                    {
                        if (park.DeviceType != EntranceDeviceType.CANEntrance || park.CommPort > 0) //如果是CAN总线停车场必须设置串口号才有效
                        {
                            string address = "net.tcp://" + ipa.ToString() + ":" + (port + park.ParkID).ToString() + "/ParkAdapter";
                            park.ParkAdapterUri = address;
                            UpdateParkAdapterUri(park);
                            IParkingAdapter ad = new ParkingAdapterServer(park);
                            Uri uri = new Uri(address);
                            Binding binding = BindingFactory.CreateDualBinding(address);
                            ServiceHost host = new ServiceHost(ad);
                            host.AddServiceEndpoint(typeof(IParkingAdapter), binding, address);
                            host.Open();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 启动所有服务
        /// </summary>
        public void StartAllServer()
        {
            int port = 13000;

            IPAddress ipa = GlobalVariables.CurrentParkingCommunicationIP;
            if (ipa != null)
            {
                foreach (ParkInfo park in ParkBuffer.Current.Parks)
                {
                    if (park.ParentID == null)
                    {
                        if (park.DeviceType != EntranceDeviceType.CANEntrance || park.CommPort > 0) //如果是CAN总线停车场必须设置串口号才有效
                        {
                            string address = "net.tcp://" + ipa.ToString() + ":" + (port + park.ParkID).ToString() + "/ParkAdapter";
                            park.ParkAdapterUri = address;
                            UpdateParkAdapterUri(park);
                            IParkingAdapter ad = new ParkingAdapterServer(park);
                            Uri uri = new Uri(address);
                            Binding binding = BindingFactory.CreateDualBinding(address);
                            ServiceHost host = new ServiceHost(ad);
                            host.AddServiceEndpoint(typeof(IParkingAdapter), binding, address);
                            host.Open();
                        }
                    }
                }
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Net;
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
            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);
            AccessSetting.Current = ssb.GetOrCreateSetting<AccessSetting>();
            TariffSetting.Current = ssb.GetOrCreateSetting<TariffSetting>();
            UserSetting.Current = ssb.GetOrCreateSetting<UserSetting>();
            HolidaySetting.Current = ssb.GetOrCreateSetting<HolidaySetting>();
            CarTypeSetting.Current = ssb.GetOrCreateSetting<CarTypeSetting>();
            CustomCardTypeSetting.Current = ssb.GetSetting<CustomCardTypeSetting>();
            ParkBuffer.Current = new ParkBuffer(AppSettings.CurrentSetting.ParkConnect);
            ParkBuffer.Current.InValid();
        }


        /// <summary>
        /// 启动工作站的所有停车场服务
        /// </summary>
        /// <param name="stationID"></param>
        public void StartServer(string workstationID)
        {
            int port = 13000;

            //IPAddress[] ips = NetTool.GetLocalIPS();
            //if (ips != null && ips.Length > 0)
            //{
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
                            (new ParkBll(AppSettings.CurrentSetting.ParkConnect)).Update(park);
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
            //}
        }

        /// <summary>
        /// 启动所有服务
        /// </summary>
        public void StartAllServer()
        {
            int port = 13000;
            //IPAddress[] ips = NetTool.GetLocalIPS();
            //if (ips != null && ips.Length > 0)
            //{
            //    IPAddress ipa = ips[0];

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
                            (new ParkBll(AppSettings.CurrentSetting.ParkConnect)).Update(park);
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
    }
}

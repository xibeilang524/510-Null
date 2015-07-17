using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Serialization;
using Ralid.GeneralLibrary.LED;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.Park.OutdoorLEDSetting
{
    /// <summary>
    /// 表示停车场户外屏配置信息
    /// </summary>
    [DataContract]
    public class ParkOutDoorLedManager
    {
        #region 构造函数
        public ParkOutDoorLedManager()
        {
            _Areas = new OutDoorLedArea[10];
        }
        #endregion

        #region 私有变量
        [DataMember]
        private OutDoorLedArea[] _Areas;

        private static List<BX4KLEDControler> _Controllers;
        private static object _ControllersLocker;
        #endregion

        #region 私有方法
        private List<BX4KDynamicArea> GetAreas(byte carType)
        {
            int offset = 108;
            List<OutDoorLedArea> areas = _Areas.Where(item => item != null && item.CarType == carType).ToList();
            if (areas.Count > 0 && areas.Count >= 3) //3表示目前的屏只能显示三种卡类型的车位数
            {
                List<BX4KDynamicArea> items = new List<BX4KDynamicArea>();
                for (int i = 0; i < 3; i++)
                {
                    BX4KDynamicArea item = new BX4KDynamicArea()
                    {
                        X = (short)(offset + (6 + 1) * i),
                        Y = 0,
                        Width = 6,
                        Height = 16,
                        SingleLine = false,
                        NewLine = false,
                        FontCode = 0x1,
                        DisplayMode = BX4KDisplayMode.Static,
                        StayTime = 0xA,
                        AreaType = 0x00,
                        Text = string.Format(@"\FE000\C{0}{1}", areas[i].Vacant == 0 ? areas[i].FullColor : areas[i].VacantColor, (areas[i].Vacant > 999 ? 999 : areas[i].Vacant).ToString("D3")),
                    };
                    items.Add(item);
                }
                return items;
            }
            else
            {
                return null;
            }
        }

        private BX4KLEDControler GetAController(byte commport, long baudRate)
        {
            if (_ControllersLocker == null) _ControllersLocker = new object();
            lock (_ControllersLocker)
            {
                if (_Controllers != null && _Controllers.Count > 0)
                {
                    foreach (BX4KLEDControler ctrl in _Controllers)
                    {
                        if (ctrl.Port == commport)
                        {
                            return ctrl;
                        }
                    }
                }
                BX4KLEDControler bx = new BX4KLEDControler(commport, baudRate);
                if (_Controllers == null) _Controllers = new List<BX4KLEDControler>();
                _Controllers.Add(bx);
                return bx;
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置停车场ID
        /// </summary>
        [DataMember]
        public int ParkID { get; set; }
        /// <summary>
        /// 获取或设置停车场中的电单车进出通道
        /// </summary>
        [DataMember]
        public List<int> MotorEntrances { get; set; }
        /// <summary>
        /// 获取或设置停车场的所有户外屏
        /// </summary>
        [DataMember]
        public List<OutDoorLed> OutDoorLeds { get; set; }
        /// <summary>
        /// 获取或设置最近一次更新时间
        /// </summary>
        [DataMember]
        public DateTime? LastUpdate { get; set; }
        /// <summary>
        /// 获取所有的车位显示区域
        /// </summary>
        public OutDoorLedArea[] Areas
        {
            get { return _Areas; }
            set
            {
                _Areas = value;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 在所有户外屏上显示车位信息
        /// </summary>
        public void ShowLed()
        {
            //这里使用新的OutDoorLed集合是因为OutDoorLeds在发送数据过程中有可能被修改，导致foreach错误
            List<OutDoorLed> leds = new List<OutDoorLed>();
            leds.AddRange(OutDoorLeds);

            //这个方法采用每次都打开和关闭串口，则如果两个屏串口一样时也可以依次更新。
            //如果采用打开串口后不关闭，则下一个屏属于同一个串口的就会打开此串口失败，导致不能更新屏。
            foreach (OutDoorLed led in leds)
            {
                BX4KLEDControler ledController = GetAController(led.Commport, led.Baud);
                try
                {
                    ledController.Open();
                    List<BX4KDynamicArea> areas = GetAreas(0);
                    if (areas != null && areas.Count > 0)
                    {
                        ledController.DynamicDisplay((short)led.CarLedAddress, 0x51, true, areas);
                        if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log("户外屏", string.Format("发屏更新指令给小车屏 地址{0} 车位{1} {2} {3}",
                            led.CarLedAddress, areas[0].Text, areas[1].Text, areas[2].Text));
                        Thread.Sleep(2000);
                    }

                    areas = GetAreas(1);
                    if (areas != null && areas.Count > 0)
                    {
                        ledController.DynamicDisplay((short)led.MotorLedAddress, 0x51, true, areas);
                        if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log("户外屏", string.Format("发屏更新指令给电单车屏 地址{0} 车位{1} {2} {3}",
                            led.MotorLedAddress, areas[0].Text, areas[1].Text, areas[2].Text));
                        Thread.Sleep(2000);
                    }
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }
        }
        /// <summary>
        /// 更新所有户外屏的亮度
        /// </summary>
        public void SetBrightness()
        {
            //这里使用新的OutDoorLed集合是因为OutDoorLeds在发送数据过程中有可能被修改，导致foreach错误
            List<OutDoorLed> leds = new List<OutDoorLed>();
            leds.AddRange(OutDoorLeds);
            //这个方法采用每次都打开和关闭串口，则如果两个屏串口一样时也可以依次更新。
            //如果采用打开串口后不关闭，则下一个屏属于同一个串口的就会打开此串口失败，导致不能更新屏。
            foreach (OutDoorLed led in leds)
            {
                BX4KLEDControler ledController = GetAController(led.Commport, led.Baud);
                try
                {
                    ledController.Open();
                    ledController.SetBrightness((short)led.CarLedAddress, 0x51, led.Brightness);
                    if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log("户外屏", string.Format("发屏亮度指令给小车屏 地址{0}", led.CarLedAddress));
                    Thread.Sleep(2000);
                    ledController.SetBrightness((short)led.MotorLedAddress, 0x51, led.Brightness);
                    if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log("户外屏", string.Format("发屏亮度指令给电单车屏 地址{0}", led.MotorLedAddress));
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }
        }

        /// <summary>
        /// 处理卡片进出记录，即根据卡片进出记录确定各区域车位数,如果事件被处理则返回真
        /// </summary>
        /// <param name="records"></param>
        public bool ProcessCardEvent(CardEventReport record)
        {
            if (_Areas != null
                && _Areas.Length > 0
                && this.MotorEntrances != null)
            {
                OutDoorLedArea ar;
                if (this.MotorEntrances.Contains(record.EntranceID)) //如果是从电车通道进出
                {
                    ar = _Areas.SingleOrDefault(area => area.CarType == 1 && area.CardType == (byte)(record.CardType));
                }
                else
                {
                    ar = _Areas.SingleOrDefault(area => area.CarType == 0 && area.CardType == (byte)(record.CardType));
                }
                if (ar != null)
                {
                    if (record.IsExitEvent && ar.Vacant < ar.CarPort) ar.Vacant += 1;
                    if (!record.IsExitEvent && ar.Vacant > 0) ar.Vacant -= 1;
                    LastUpdate = DateTime.Now;
                    return true;
                }
            }
            return false;
        }

        public int? GetVacant(CardType cardType, int entranceID)
        {
            int? vacant = null;
            if (_Areas != null && _Areas.Length > 0)
            {
                foreach (OutDoorLedArea area in _Areas)
                {
                    byte carType = 0;
                    if (MotorEntrances != null && MotorEntrances.Contains(entranceID))
                    {
                        carType = 1;
                    }
                    if ((area.CardType != null && area.CardType.Value == cardType.ID) && area.CarType == carType)
                    {
                        vacant = area.Vacant;
                        break;
                    }
                }
            }
            return vacant;
        }
        #endregion
    }
}

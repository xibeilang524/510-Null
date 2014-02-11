using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ZLG;

namespace BatteryDemo_CSharp
{
    public partial class BatteryDemo : Form
    {
        System.Threading.Timer timer;
        SYSTEM_POWER_STATUS_EX2 powerInfo;

        public BatteryDemo()
        {
            InitializeComponent();
            TimerCallback callback = new TimerCallback(Callback);
            timer = new System.Threading.Timer(callback, null, 0, 1000);
        }
        delegate void somedle();
        public void Callback(Object obj)
        {
            somedle sd = new somedle(Update);
            Invoke(sd);
        }
        public void Update()
        {
            if (Battery.GetSystemPowerStatusEx2(ref powerInfo, System.Runtime.InteropServices.Marshal.SizeOf(powerInfo), 0) != 0)
            {
                txtVoltage.Text = string.Format("电压 {0:D}mV", powerInfo.BatteryVoltage);
                txtPercent.Text = string.Format("电量 {0:D}%", powerInfo.BatteryLifePercent);
                if (powerInfo.ACLineStatus == 0xFF)
                {
                    lblStatus.Text = "未知电源";
                }
                else if ((powerInfo.ACLineStatus & 0x01) == 0x01/*AC_LINE_ONLINE*/)
                {
                    lblStatus.Text = "正在使用外部电源";
                }
                else if (powerInfo.ACLineStatus == 0x00/*AC_LINE_OFFLINE*/)
                {
                    lblStatus.Text = "正在使用电池电源";
                }
                else
                {
                    lblStatus.Text = "未知电源";
                }
                if (powerInfo.BatteryFlag == 0xFF/*BATTERY_FLAG_UNKNOWN*/)
                {
                    lblLevel.Text = "未知的电池级别";
                }
                else if ((powerInfo.BatteryFlag & 0x01) == 0x01/*BATTERY_FLAG_HIGH*/)
                {
                    lblLevel.Text = "电池电量高";
                }
                else if ((powerInfo.BatteryFlag & 0x02) == 0x02/*BATTERY_FLAG_LOW*/)
                {
                    lblLevel.Text = "电池电量不足";
                }
                else if ((powerInfo.BatteryFlag & 0x04) == 0x04/*BATTERY_FLAG_CRITICAL*/)
                {
                    lblLevel.Text = "电池电量严重不足";
                }
                else
                {
                    lblLevel.Text = "未知的电池级别";
                }
                if ((powerInfo.BatteryFlag & 0x08) == 0x08/*BATTERY_FLAG_CHARGING*/)
                {
                    lblCharge.Text = "正在充电";
                }
                else
                {
                    if ((powerInfo.ACLineStatus & 0x01) == 0x01/*AC_LINE_ONLINE*/)
                    {
                        lblCharge.Text = "使用外部电源";
                    }
                    else
                    {
                        lblCharge.Text = "使用电池";
                    }
                }
            }
        }
    }
}
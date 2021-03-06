﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ZLG
{

    public struct SYSTEM_POWER_STATUS_EX2
    {  //c# Windows CE读取电池电量的实现
        public byte ACLineStatus;
        public byte BatteryFlag;
        public byte BatteryLifePercent;
        public byte Reserved1;
        public uint BatteryLifeTime;
        public uint BatteryFullLifeTime;
        public byte Reserved2;
        public byte BackupBatteryFlag;
        public byte BackupBatteryLifePercent;
        public byte Reserved3;
        public uint BackupBatteryLifeTime;
        public uint BackupBatteryFullLifeTime;
        public uint BatteryVoltage;
        public uint BatteryCurrent;
        public uint BatteryAverageCurrent;
        public uint BatteryAverageInterval;
        public uint BatterymAHourConsumed;
        public uint BatteryTemperature;
        public uint BackupBatteryVoltage;
        public byte BatteryChemistry;
    }

    class Battery
    {
        [System.Runtime.InteropServices.DllImport("coredll.dll")]
        public static extern uint GetSystemPowerStatusEx2(
            ref SYSTEM_POWER_STATUS_EX2 pSystemPowerStatusEx2,
            int dwLen,
            int fUpdate
        );
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Parking.POS.Model
{
     /// <summary>
    /// 表示时间刻度
    /// </summary>
    [Serializable]
    public class TimeEntity
    {
        #region 构造函数
        public TimeEntity()
            : this(0, 0)
        {
        }

        public TimeEntity(int hour, int minute)
        {
            if (hour < 0 || hour > 24)
            {
                throw new InvalidOperationException("无效小时数");
            }
            if (minute < 0 || minute > 60)
            {
                throw new InvalidOperationException("无效分钟数");
            }
            Hour = (byte)hour;
            Minute = (byte)minute;
        }

        public TimeEntity(DateTime time)
        {
            Hour = (byte)time.Hour;
            Minute = (byte)time.Minute;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置小时数
        /// </summary>
        public byte Hour { get; set; }
        /// <summary>
        /// 分钟数
        /// </summary>
        public byte Minute { get; set; }
        /// <summary>
        /// 获取计算出的总分钟数
        /// </summary>
        public int TotalMinutes
        {
            get { return Hour * 60 + Minute; }
        }
        #endregion

        #region 公共方法
        public TimeEntity AddMinutes(int minutes)
        {
            int total = (Hour * 60 + Minute + minutes);
            if (total < 0)
            {
                while (true)
                {
                    total += 24 * 60;
                    if (total >= 0) break;
                }
            }
            total %= (24 * 60);
            byte h = (byte)(total / 60);
            byte m = (byte)(total % 60);
            return new TimeEntity(h, m);
        }

        public TimeEntity AddHours(int hours)
        {
            return AddMinutes(hours * 60);
        }

        public override string ToString()
        {
            return Hour.ToString("00") + ":" + Minute.ToString("00");
        }
        #endregion
    }
}

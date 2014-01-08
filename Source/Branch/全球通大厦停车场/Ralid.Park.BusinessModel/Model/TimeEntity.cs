using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
     /// <summary>
    /// 表示时间刻度
    /// </summary>
    [DataContract]
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
                throw new InvalidOperationException(Resouce.Resource1.InvalidHour);
            }
            if (minute < 0 || minute > 60)
            {
                throw new InvalidOperationException(Resouce.Resource1.InvalidMinute);
            }
            _Hour = (byte)hour;
            _Minute = (byte)minute;
        }

        public TimeEntity(DateTime time)
        {
            _Hour = (byte)time.Hour;
            _Minute = (byte)time.Minute;
        }
        #endregion

        #region 私有变量
        [DataMember]
        private byte _Hour;

        [DataMember]
        private byte _Minute;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置小时数
        /// </summary>
        public byte Hour { get { return _Hour; } }
        /// <summary>
        /// 分钟数
        /// </summary>
        public byte Minute { get { return _Minute; } }
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

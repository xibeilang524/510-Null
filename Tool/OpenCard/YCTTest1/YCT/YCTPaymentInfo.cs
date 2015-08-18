using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.GeneralLibrary.CardReader.YCT
{
    public class YCTPaymentInfo
    {
        #region 构造函数
        public YCTPaymentInfo()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 
        /// </summary>
        public string 本次交易设备编号 { get; set; }

        public int 终端交易流水号 { get; set; }

        public DateTime 本次交易日期时间 { get; set; }

        public string 逻辑卡号 { get; set; }

        public string 物理卡号 { get; set; }

        public int 交易金额 { get; set; }

        public int 票价 { get; set; }

        public int 本次余额 { get; set; }

        public byte 交易类型 { get; set; }

        public byte 附加交易类型 { get; set; }

        public int 票卡充值交易计数 { get; set; }

        public int 票卡消费交易计数 { get; set; }

        public string 累计门槛月份 { get; set; }

        public int 公交门槛计数 { get; set; }

        public int 地铁门槛计数 { get; set; }

        public int 联乘门槛计数 { get; set; }

        public string 本次交易入口设备编号 { get; set; }

        public string 本次交易入口日期时间 { get; set; }

        public string 上次交易设备编号 { get; set; }

        public string 上次交易日期时间 { get; set; }

        public string 区域代码 { get; set; }

        public string 区域卡类型 { get; set; }

        public string 区域子码 { get; set; }

        public string 交易认证码 { get; set; }
        #endregion
    }
}

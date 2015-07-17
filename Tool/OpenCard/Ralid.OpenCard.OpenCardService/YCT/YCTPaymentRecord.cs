using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    public class YCTPaymentRecord
    {
        #region 构造函数
        public YCTPaymentRecord()
        {
        }
        #endregion

        #region 公共属性
        public Guid ID { get; set; }
        /// <summary>
        /// 获取或设置停车消费的入场时间
        /// </summary>
        public DateTime EnterDateTime { get; set; }
        /// <summary>
        /// 获取或设置消费的设备ID
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// 获取或设置消费流水号
        /// </summary>
        public string SerialNum { get; set; }
        /// <summary>
        /// 获取或设置消费时间
        /// </summary>
        public DateTime PaymentDateTime { get; set; }
        /// <summary>
        /// 获取或设置物理卡号
        /// </summary>
        public string PhysicalCardID { get; set; }
        /// <summary>
        /// 获取或设置逻辑卡号
        /// </summary>
        public string LogicCardID { get; set; }
        /// <summary>
        /// 获取或设置交易金额(分为单位)
        /// </summary>
        public int Paid { get; set; }
        /// <summary>
        /// 获取或设置交易后卡片的余额(以分为单位)
        /// </summary>
        public int Balance { get; set; }
        /// <summary>
        /// 获取或设置消费交易计数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 获取或设置交易验证码
        /// </summary>
        public string TAC { get; set; }
        /// <summary>
        /// 获取或设置记录状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 获取或设置上传字符串
        /// </summary>
        public string UploadString { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    [Serializable]
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
        /// 获取或设置钱包类型 1表示M1钱包 2表示CPU钱包
        /// </summary>
        public int WalletType { get; set; }
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
        public YCTPaymentRecordState State { get; set; }
        /// <summary>
        /// 获取或设置上传字符串
        /// </summary>
        public string UploadString { get; set; }
        #endregion

        #region 公共方法
        public YCTPaymentRecord Clone()
        {
            return this.MemberwiseClone() as YCTPaymentRecord;
        }
        #endregion
    }

    public enum YCTPaymentRecordState
    {
        /// <summary>
        /// 表示未处理完成
        /// </summary>
        Uncompleted = 0,
        /// <summary>
        /// 处理完成
        /// </summary>
        Completed = 1,
        /// <summary>
        /// 羊城通服务器接受其为有效记录
        /// </summary>
        ServiceAccepted = 2
    }
}

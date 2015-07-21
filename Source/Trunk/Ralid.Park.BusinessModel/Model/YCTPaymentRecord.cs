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
        public int ID { get; set; }
        /// <summary>
        /// 获取或设置消费的设备ID
        /// </summary>
        public string PID { get; set; }
        /// <summary>
        /// 获取或设置消费流水号
        /// </summary>
        public string PSN { get; set; }
        /// <summary>
        /// 获取或设置消费时间
        /// </summary>
        public DateTime TIM { get; set; }
        /// <summary>
        /// 获取或设置物理卡号
        /// </summary>
        public string FCN { get; set; }
        /// <summary>
        /// 获取或设置逻辑卡号
        /// </summary>
        public string LCN { get; set; }
        /// <summary>
        /// 获取或设置票价(分为单位)
        /// </summary>
        public int TF { get; set; }
        /// <summary>
        /// 获取或设置交易金额(分为单位)
        /// </summary>
        public int FEE { get; set; }
        /// <summary>
        /// 获取或设置交易后卡片的余额(以分为单位)
        /// </summary>
        public int BAL { get; set; }
        /// <summary>
        /// 获取或设置交易类型
        /// </summary>
        public byte TT { get; set; }
        /// <summary>
        /// 获取或设置附加交易类型
        /// </summary>
        public byte ATT { get; set; }
        /// <summary>
        /// 获取或设置充值交易计数
        /// </summary>
        public int CRN { get; set; }
        /// <summary>
        /// 获取或设置消费交易计数
        /// </summary>
        public int XRN { get; set; }
        /// <summary>
        /// 获取或设置累计月份门槛
        /// </summary>
        public string DMON { get; set; }
        /// <summary>
        /// 获取或设置公交门槛计数
        /// </summary>
        public int BDCT { get; set; }
        /// <summary>
        /// 获取或设置地铁门槛计数
        /// </summary>
        public int MDCT { get; set; }
        /// <summary>
        /// 获取或设置联乘门槛计数
        /// </summary>
        public int UDCT { get; set; }
        /// <summary>
        /// 获取或设置入口交易设备编号
        /// </summary>
        public string EPID { get; set; }
        /// <summary>
        /// 获取或设置入口交易时间
        /// </summary>
        public string ETIM { get; set; }
        /// <summary>
        /// 获取或设置上次交易设备编号
        /// </summary>
        public string LPID { get; set; }
        /// <summary>
        /// 获取或设置上次交易时间
        /// </summary>
        public string LTIM { get; set; }
        /// <summary>
        /// 获取或设置区域代码
        /// </summary>
        public string AREA { get; set; }
        /// <summary>
        /// 获取或设置区域卡类型
        /// </summary>
        public string ACT { get; set; }
        /// <summary>
        /// 获取或设置区域子码
        /// </summary>
        public string SAREA { get; set; }
        /// <summary>
        /// 获取或设置交易验证码
        /// </summary>
        public string TAC { get; set; }
        /// <summary>
        /// 获取或设置说明信息
        /// </summary>
        public string MEM { get; set; }
        /// <summary>
        /// 获取或设置钱包类型 1表示M1钱包 2表示CPU钱包
        /// </summary>
        public int WalletType { get; set; }
        /// <summary>
        /// 获取或设置停车消费的入场时间
        /// </summary>
        public DateTime EnterDateTime { get; set; }
        /// <summary>
        /// 获取或设置记录状态 
        /// </summary>
        public YCTPaymentRecordState State { get; set; }
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

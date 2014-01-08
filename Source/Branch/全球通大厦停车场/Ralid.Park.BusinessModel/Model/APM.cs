using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示自助缴费机
    /// </summary>
    public class APM
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置自助缴机ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 获取或设置自助缴机编号
        /// </summary>
        public string SerialNum { get; set; }
        /// <summary>
        /// 获取或设置自助缴机IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 获取或设置自助缴机MAC地址
        /// </summary>
        public string MAC { get; set; }
        /// <summary>
        /// 获取或设置自助缴机当前状态
        /// </summary>
        public APMStatus Status { get; set; }
        /// <summary>
        /// 硬币数量
        /// </summary>
        public int Coin { get; set; }
        /// <summary>
        /// 纸币总金额
        /// </summary>
        public decimal CashAmount { get; set; }
        /// <summary>
        /// 上次结账时间
        /// </summary>
        public DateTime CheckOutTime { get; set; }
        /// <summary>
        /// 上次结账余额
        /// </summary>
        public decimal LastBalance { get; set; }
        /// <summary>
        /// 获取或设置自助缴费机最近一次标记服务器的时间
        /// (缴费机定期更新此时间，其它系统可以根据这个时间与当前时间对比来确定缴费机是否已经停止工作或连接不上数据库)
        /// </summary>
        public DateTime? ActiveDateTime { get; set; }
        /// <summary>
        /// 获取或设置自助缴机说明描述
        /// </summary>
        public string Memo { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 克隆一个副本对象
        /// </summary>
        /// <returns></returns>
        public APM Clone()
        {
            return this.MemberwiseClone() as APM;
        }
        #endregion
    }
}

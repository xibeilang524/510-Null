using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示羊城通卡黑名单
    /// </summary>
    public class YCTBlacklist
    {
        #region 构造函数
        public YCTBlacklist()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置逻辑卡号
        /// </summary>
        public string LCN { get; set; }
        /// <summary>
        /// 获取或设置物理卡号
        /// </summary>
        public string FCN { get; set; }
        /// <summary>
        /// 获取或设置进入黑名单的原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 获取或设置钱包类型 1表示M1钱包 2表示CPU钱包
        /// </summary>
        public int? WalletType { get; set; }
        /// <summary>
        /// 获取或设置设置成黑名单的日期
        /// </summary>
        public DateTime? AddDateTime { get; set; }
        /// <summary>
        /// 获取或设置捕捉黑名单的日期
        /// </summary>
        public DateTime? CatchAt { get; set; }
        /// <summary>
        /// 获取或设置记录的上传文件
        /// </summary>
        public string UploadFile { get; set; }
        #endregion

        #region 公共方法
        public YCTBlacklist Clone()
        {
            return this.MemberwiseClone() as YCTBlacklist;
        }
        #endregion
    }
}

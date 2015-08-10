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
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置进入黑名单的原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 获取或设置设置成黑名单的日期时间
        /// </summary>
        public DateTime? AddDateTime { get; set; }
        #endregion
    }
}

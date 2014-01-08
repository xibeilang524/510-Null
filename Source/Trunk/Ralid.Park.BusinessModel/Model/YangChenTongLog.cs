using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示羊城通收费记录
    /// </summary>
    public class YangChenTongLog
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置ID
        /// </summary>
        public int LogID { get; set; }
        /// <summary>
        /// 获取或设置保存时间
        /// </summary>
        public DateTime LogDateTime { get; set; }
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置逻辑卡号
        /// </summary>
        public string LogicalID { get; set; }
        /// <summary>
        /// 获取或设置交易金额
        /// </summary>
        public decimal Payment { get; set; }
        /// <summary>
        /// 获取或设置余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 获取或设置交易数据(此数据要上传到羊城通公司)
        /// </summary>
        public string Data { get; set; }
        #endregion
    }
}

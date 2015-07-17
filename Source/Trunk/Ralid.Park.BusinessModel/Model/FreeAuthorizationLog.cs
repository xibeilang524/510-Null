using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 免费授权记录
    /// </summary>
    public class FreeAuthorizationLog
    {
        #region 实体字段
        /// <summary>
        /// 获取或设置记录ID
        /// </summary>
        public int LogID { get; set; }
        /// <summary>
        /// 获取或设置授权时间
        /// </summary>
        public DateTime LogDateTime { get; set; }
        /// <summary>
        /// 获取或设置授权卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置免费开始时间
        /// </summary>
        public DateTime BeginDateTime { get; set; }
        /// <summary>
        /// 获取或设置免费结束时间
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// 获取或设置卡片是否在场
        /// </summary>
        public bool InPark { get; set; }
        /// <summary>
        /// 获取或设置卡片是否不允许多次进出
        /// </summary>
        public bool CheckOut { get; set; }
        /// <summary>
        /// 获取或设置授权操作员
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 获取或设置授权工作站
        /// </summary>
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置备注信息
        /// </summary>
        public string Memo { get; set; }
        #endregion
    }
}

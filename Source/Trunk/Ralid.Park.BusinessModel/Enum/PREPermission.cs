using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 优惠录入系统操作员的权限枚举
    /// </summary>
    public enum PREPermission
    {
        #region 系统
        /// <summary>
        /// 系统设置
        /// </summary>
        [OperatorRight(Catalog = "System", Description = "系统设置")]
        SystemSetting = 11,
        /// <summary>
        /// 优惠录入
        /// </summary>
        [OperatorRight(Catalog = "System", Description = "优惠录入")]
        PreferentialCore = 12,
        /// <summary>
        /// 优惠取消
        /// </summary>
        [OperatorRight(Catalog = "System", Description = "优惠取消")]
        PreferentialCancel = 13,
        #endregion

        #region 数据-工作站&&商家信息设置
        /// <summary>
        /// 查看工作站信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "查看工作站信息")]
        ReadWorkstations = 21,
        /// <summary>
        /// 修改工作站信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "修改工作站信息")]
        EditWorkstations = 22,
        /// <summary>
        /// 查看商家信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "查看商家信息")]
        ReadBusiness = 23,
        /// <summary>
        /// 修改商家信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "修改商家信息")]
        EditBusiness = 24,
        #endregion

        #region 安全
        /// <summary>
        /// 操作员管理
        /// </summary>
        [OperatorRight(Catalog = "Safe", Description = "操作员管理")]
        OperatorManager = 31,
        /// <summary>
        /// 角色管理
        /// </summary>
        [OperatorRight(Catalog = "Safe", Description = "角色管理")]
        RoleManager = 32,
        #endregion

        #region 报表
        /// <summary>
        /// 优惠记录
        /// </summary>
        [OperatorRight(Catalog = "Reprot", Description = "优惠记录")]
        PreferentialReport = 41,
        #endregion
    }

}

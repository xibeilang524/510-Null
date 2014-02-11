﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Parking.POS.Tool;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 代表系统操作员
    /// </summary>
    [Serializable]
    public class OperatorInfo
    {
        private static DSEncrypt DES = new DSEncrypt();

        private static OperatorInfo currentOperator;
        /// <summary>
        /// 获取或设置当前的操作员
        /// </summary>
        public static OperatorInfo CurrentOperator
        {
            get { return currentOperator; }
            set { currentOperator = value; }
        }

        #region 公共属性
        /// <summary>
        /// 操作员登录名
        /// </summary>

        public string OperatorID { get; set; }
        /// <summary>
        /// 操作员名
        /// </summary>

        public string OperatorName { get; set; }
        /// <summary>
        /// 操作员登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public byte OperatorNum { get; set; }
        #endregion

        public OperatorInfo()
        {

        }

        /// <summary>
        /// 操作员是否可以删除,系统内置的ADMIN操作员不可删除
        /// </summary>
        public bool CanDelete
        {
            get
            {
                return (OperatorID.ToUpper() != "ADMIN");
            }
        }

        public bool CanEdit
        {
            get
            {
                return (OperatorID.ToUpper() != "ADMIN");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    public class DeptInfo
    {
        #region 私有变量
        private Guid _deptID;
        private String _deptName;
        private String _descrption;
        private Guid _parentID;
        #endregion

        #region 公共属性
        /// <summary>
        /// 部门编号
        /// </summary>
        public Guid DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public String DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }

        /// <summary>
        /// 部门描述
        /// </summary>
        public String Descrption
        {
            get { return _descrption; }
            set { _descrption = value; }
        }

        /// <summary>
        /// 预留字段(组织架构用)
        /// </summary>
        public Guid? ParentID
        {
            get;
            set;
        }
        #endregion
    }
}

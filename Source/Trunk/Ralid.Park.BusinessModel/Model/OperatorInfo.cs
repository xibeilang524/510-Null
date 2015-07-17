using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.GeneralLibrary.SoftDog;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 代表系统操作员
    /// </summary>
    [Serializable()]
    [DataContract]
    public class OperatorInfo
    {
        private static Ralid.GeneralLibrary .SoftDog .DSEncrypt DES=new GeneralLibrary.SoftDog.DSEncrypt ();
        private static OperatorInfo currentOperator;
        /// <summary>
        /// 获取或设置当前的操作员
        /// </summary>
        public static OperatorInfo CurrentOperator
        {
            get { return currentOperator; }
            set { currentOperator = value; }
        }

        #region 私有变量
        [DataMember]
        private string _Password;
        #endregion

        #region 公共属性
        /// <summary>
        /// 操作员登录名
        /// </summary>
        [DataMember]
        public string OperatorID { get; set; }
        /// <summary>
        /// 操作员名
        /// </summary>
        [DataMember]
        public string OperatorName { get; set; }
        /// <summary>
        /// 操作员登录密码
        /// </summary>
        public string Password
        {
            get
            {
                return (new DTEncrypt()).DSEncrypt(_Password);
            }
            set
            {
                _Password = (new DTEncrypt()).Encrypt(value);
            }
        }
        /// <summary>
        /// 操作员角色ID
        /// </summary>
        [DataMember]
        public string RoleID { get; set; }

        /// <summary>
        /// 操作员角色
        /// </summary>
        [DataMember]
        public RoleInfo Role { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        [DataMember]
        public byte OperatorNum { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [DataMember]
        public Guid? DeptID { get; set; }
        /// <summary>
        /// 操作员部门
        /// </summary>
        [DataMember]
        public DeptInfo Dept { get; set; }

        #endregion

        public OperatorInfo()
        {

        }

        public override string ToString()
        {
            return string.Format("logName={0} , OperatorName={1} , PWD={2} , RoleID={3} , SN={4}", this.OperatorID,
                this.OperatorName, this.Password, this.RoleID, this.OperatorNum.ToString());
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

         /// <summary>
        /// 查看此操作员是否被授予此权限
        /// </summary>
        public bool Permit(Permission right)
        {
            if (this.Role != null)
            {
                return Role.Permit(right);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 操作员是否是需要交接班操作的操作员(一个工作站有一个交接班操作员登录后,其它交接班操作员就不能再登录该工作站,除非该操作员交班)
        /// </summary>
        public bool NeedShift
        {
            get
            {
                return Role.IsIoOperator || Role.IsCardManager;
            }
        }
    }
}

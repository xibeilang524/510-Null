using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    [Serializable()]
    [DataContract]
    public class PRERoleInfo
    {
        #region 私有变量
        //系统预定义的角色
        [DataMember]
        private readonly string Admin = "ADMIN"; //系统管理员
        
        [DataMember]
        private List<PREPermission> _rights;
        [DataMember]
        private string _myRights;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置角色ID
        /// </summary>
        [DataMember]
        public string RoleID { get; set; }
        /// <summary>
        /// 获取或设置角色名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置角色描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public string Permission
        {
            get
            {
                return _myRights;
            }
            set
            {
                _myRights = value.ToLower();
                _rights = null;
            }
        }

        /// <summary>
        /// 是否可以删除,系统默认的角色(系统管理员,卡片操作员,进出口操作员)不能删除
        /// </summary>
        public bool CanDelete
        {
            get
            {
                return (RoleID.ToUpper() != Admin);
            }
        }

        /// <summary>
        /// 是否可以编辑,系统默认的角色(系统管理员)不能编辑
        /// </summary>
        public bool CanEdit
        {
            get
            {
                return RoleID.ToUpper() != Admin;
            }
        }

        /// <summary>
        /// 是否是系统管理员
        /// </summary>
        public bool IsAdmin
        {
            get { return RoleID.ToUpper() == Admin; }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 查看此角色是否被授予此权限
        /// </summary>
        public bool Permit(PREPermission right)
        {
            if (_myRights == "all")
            {
                return true;
            }
            else
            {
                if (_rights == null)
                {
                    _rights = new List<PREPermission>();
                    if (!string.IsNullOrEmpty(_myRights))
                    {
                        foreach (string str in _myRights.Split(','))
                        {
                            int i;
                            if (int.TryParse(str, out i))
                            {
                                _rights.Add((PREPermission)i);
                            }
                        }
                    }
                }
                return _rights.Exists(r => r == right);
            }
        }
        #endregion

    }
}

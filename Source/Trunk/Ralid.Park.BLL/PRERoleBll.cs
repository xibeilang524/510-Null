using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.BLL
{
    public class PRERoleBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public PRERoleBll(string repoUri)
        {
            _RepoUri = repoUri;
            provider = ProviderFactory.Create<IPRERoleProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private IPRERoleProvider provider;
        private string _RepoUri;
        #endregion

        #region 公共方法
        public QueryResult<PRERoleInfo> GetRoleInfoByID(string roleID)
        {
            return provider.GetByID(roleID);
        }

        public QueryResultList<PRERoleInfo> GetAllRoles()
        {
            return provider.GetAll();
        }

        public CommandResult Update(PRERoleInfo newVal)
        {
            PRERoleInfo original = GetRoleInfoByID(newVal.RoleID).QueryObject;
            if (original != null)
            {
                return provider.Update(newVal, original);
            }
            else
            {
                return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
            }
        }

        public CommandResult Insert(PRERoleInfo info)
        {
            return provider.Insert(info);
        }

        public CommandResult Delete(PRERoleInfo info)
        {
            if (!info.CanDelete)
            {
                throw new InvalidOperationException(string.Format(Resource1.RoleBll_CannotDelete, info.Name));
            }
            else
            {
                OperatorBll bll = new OperatorBll(_RepoUri);
                OperatorSearchCondition search = new OperatorSearchCondition { RoleID = info.RoleID };
                QueryResultList<OperatorInfo> result = bll.GetOperators(search);
                if (result.Result == ResultCode.Successful && result.QueryObjects.Count > 0)
                {
                    throw new InvalidOperationException(string.Format(Resource1.RoleBll_RoleBeUsed, info.RoleID, info.RoleID));
                }
            }
            return provider.Delete(info);
        }

        public CommandResult DeleteAllRoles()
        {
            return provider.DeleteAllRoles();
        }

        public CommandResult UpdateOrInsert(PRERoleInfo info)
        {
            PRERoleInfo original = provider.GetByID(info.RoleID).QueryObject;
            if (original != null)
            {
                return provider.Update(info, original);
            }
            else
            {
                return provider.Insert(info);
            }
        }
        #endregion
    }
}

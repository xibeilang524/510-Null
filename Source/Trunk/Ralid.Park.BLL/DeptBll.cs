using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.BLL
{
    public class DeptBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public DeptBll(string repoUri)
        {
            _RepoUri = repoUri;
            provider = ProviderFactory.Create<IDeptProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private IDeptProvider provider;
        private string _RepoUri;
        #endregion

        #region 公共方法
        public QueryResult<DeptInfo> GetDeptInfoByID(string deptID)
        {
            return provider.GetByID(deptID);
        }

        public QueryResultList<DeptInfo> GetAllDepts()
        {
            return provider.GetAll();
        }

        public CommandResult Update(DeptInfo newVal)
        {
            DeptInfo original = GetDeptInfoByID(newVal.DeptID.ToString()).QueryObject;
            if (original != null)
            {
                return provider.Update(newVal, original);
            }
            else
            {
                return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
            }
        }

        public CommandResult Insert(DeptInfo info)
        {
            return provider.Insert(info);
        }

        public CommandResult Delete(DeptInfo info)
        {
            return provider.Delete(info);
        }

        public CommandResult UpdateOrInsert(DeptInfo info)
        {
            DeptInfo original = provider.GetByID(info.DeptID.ToString()).QueryObject;
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

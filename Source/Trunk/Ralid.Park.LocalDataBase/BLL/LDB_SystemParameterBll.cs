using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.LocalDataBase.DAL.IDAL;
using Ralid.Park.LocalDataBase.Model;

namespace Ralid.Park.LocalDataBase.BLL
{
    public class LDB_SystemParameterBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public LDB_SystemParameterBll(string repoUri)
        {
            provider = LDB_ProviderFactory.Create<LDB_ISysParameterProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private LDB_ISysParameterProvider provider;
        #endregion

        #region 公共方法
        public QueryResult<LDB_SysparameterInfo> GetSysParameterByID(string id)
        {
            return provider.GetByID(id);
        }

        public CommandResult Insert(LDB_SysparameterInfo info)
        {
            return provider.Insert(info);
        }

        public List<LDB_SysparameterInfo> GetAll()
        {
            return provider.GetAll().QueryObjects;
        }
        #endregion
    }
}

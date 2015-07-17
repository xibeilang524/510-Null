using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.BLL
{
    public class SystemParameterBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public SystemParameterBll(string repoUri)
        {
            provider = ProviderFactory.Create<ISysParameterProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private ISysParameterProvider provider;
        #endregion

        #region 公共方法
        public QueryResult<SysparameterInfo> GetSysParameterByID(string id)
        {
            return provider.GetByID(id);
        }

        public CommandResult Insert(SysparameterInfo info)
        {
            return provider.Insert(info);
        }

        public CommandResult Delete(SysparameterInfo info)
        {
            return provider.Delete(info);
        }

        public List<SysparameterInfo> GetAll()
        {
            return provider.GetAll().QueryObjects;
        }
        #endregion
    }
}

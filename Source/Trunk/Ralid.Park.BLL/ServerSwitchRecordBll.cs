using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.BLL
{
    /// <summary>
    /// 服务器切换记录的应用层类
    /// </summary>
    public class ServerSwitchRecordBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public ServerSwitchRecordBll(string repoUri)
        {
            provider = ProviderFactory.Create<IServerSwitchRecordProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IServerSwitchRecordProvider provider;
        #endregion

        #region 公共方法
        public CommandResult Insert(ServerSwitchRecord info)
        {
            return provider.Insert(info);
        }

        public QueryResultList<ServerSwitchRecord> GetServerSwitchRecords(SearchCondition search)
        {
            return provider.GetItems(search);
        }
        #endregion
    }
}

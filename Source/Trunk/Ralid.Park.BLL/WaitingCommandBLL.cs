using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.BLL
{
    public class WaitingCommandBLL
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public WaitingCommandBLL(string repoUri)
        {
            provider = ProviderFactory.Create<IWaitingCommandProvider>(repoUri);
        }
        #endregion

        #region 成员变量
        IWaitingCommandProvider provider = null;
        #endregion 成员变量

        #region 公共方法
        public QueryResultList<WaitingCommandInfo> GetCommands(int entranceID)
        {
            WaitingCommandSearchCondition search = new WaitingCommandSearchCondition();
            search.EntranceID = entranceID;
            return provider.GetItems(search);
        }

        public QueryResultList<WaitingCommandInfo> GetAllCommands()
        {
            return provider.GetAll();
        }

        public CommandResult Insert(WaitingCommandInfo t)
        {
            return provider.Insert(t);
        }

        public CommandResult Delete(WaitingCommandInfo t)
        {
            return provider.Delete(t);
        }
        #endregion
    }
}

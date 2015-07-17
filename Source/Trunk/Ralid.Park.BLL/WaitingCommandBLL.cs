using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Report;
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
        /// <summary>
        /// 获取控制器ID为entranceID的所有等待下发命令
        /// </summary>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        public QueryResultList<WaitingCommandInfo> GetCommands(int entranceID)
        {
            WaitingCommandSearchCondition search = new WaitingCommandSearchCondition();
            search.EntranceID = entranceID;
            return provider.GetItems(search);
        }

        /// <summary>
        /// 获取等待下发的命令
        /// </summary>
        /// <returns></returns>
        public QueryResultList<WaitingCommandInfo> GetWaitingCommands()
        {
            WaitingCommandSearchCondition search = new WaitingCommandSearchCondition();
            search.Status = WaitingCommandStatus.Waiting;
            return provider.GetItems(search);
        }

        public QueryResultList<WaitingCommandInfo> GetCommands(WaitingCommandSearchCondition search)
        {
            return provider.GetItems(search);
        } 

        public QueryResultList<WaitingCommandInfo> GetAllCommands()
        {
            return provider.GetAll();
        }

        public CommandResult DeleteAndInsert(WaitingCommandInfo t)
        {
            CommandResult result = provider.Delete(t);
            if (result.Result == ResultCode.Successful)
            {
                result = provider.Insert(t);
            }
            return result;
        }

        public CommandResult Delete(WaitingCommandInfo t)
        {
            return provider.Delete(t);
        }

        public CommandResult Update(WaitingCommandInfo newInfo)
        {
            WaitingCommandInfo original = provider.GetByID(new WaitingCommandID(newInfo.EntranceID, newInfo.Command, newInfo.CardID)).QueryObject;
            if (original != null)
            {
                return provider.Update(newInfo, original);
            }
            return new CommandResult(ResultCode.NoRecord);
        }
        #endregion
    }
}

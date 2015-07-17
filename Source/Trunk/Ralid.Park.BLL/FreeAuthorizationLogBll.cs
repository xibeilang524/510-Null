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
    /// 免费优惠记录的业务逻辑类
    /// </summary>
    public class FreeAuthorizationLogBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public FreeAuthorizationLogBll(string repoUri)
        {
            provider = ProviderFactory.Create<IFreeAuthorizationLogProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IFreeAuthorizationLogProvider provider;
        #endregion

        #region 公共方法
        public CommandResult Insert(FreeAuthorizationLog info)
        {
            return provider.Insert(info);
        }

        public QueryResultList<FreeAuthorizationLog> GetFreeAuthorizationLogs(RecordSearchCondition search)
        {
            return provider.GetItems(search);
        }

        public CommandResult Delete(FreeAuthorizationLog info)
        {
            return provider.Delete(info);
        }

        public CommandResult InsertRecordWithCheck(FreeAuthorizationLog info)
        {
            RecordSearchCondition searchCondition = new RecordSearchCondition();
            searchCondition.RecordDateTimeRange = new DateTimeRange(info.LogDateTime, info.LogDateTime);
            searchCondition.CardID = info.CardID;

            List<FreeAuthorizationLog> check = provider.GetItems(searchCondition).QueryObjects;
            if (check == null || check.Count == 0)
            {
                return provider.Insert(info);
            }
            //已存在该记录，可认为插入成功
            return new CommandResult(ResultCode.Successful, string.Empty);
        }
        #endregion
    }
}

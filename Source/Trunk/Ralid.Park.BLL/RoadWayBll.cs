using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.BLL
{
    /// <summary>
    /// 通道路口逻辑处理类
    /// </summary>
    public class RoadWayBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public RoadWayBll(string repoUri)
        {
            provider = ProviderFactory.Create<IRoadWayProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IRoadWayProvider provider;
        #endregion

        #region 公共方法
        public QueryResult<RoadWayInfo> GetRoadWayByID(int roadID)
        {
            return provider.GetByID(roadID);
        }

        public QueryResultList<RoadWayInfo> GetAllRoadWays()
        {
            return provider.GetAll();
        }

        public CommandResult Insert(RoadWayInfo info)
        {
            return provider.Insert(info);
        }

        public CommandResult Update(RoadWayInfo curVal)
        {
            QueryResult<RoadWayInfo> original = provider.GetByID(curVal.RoadID);
            if (original.Result == ResultCode.Successful)
            {
                if (original.QueryObject != null)
                {
                    return provider.Update(curVal, original.QueryObject);
                }
                else
                {
                    return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
                }
            }
            else
            {
                return new CommandResult(original.Result);
            }
        }

        public CommandResult Delete(RoadWayInfo info)
        {
            return provider.Delete(info);
        }
        #endregion
    }
}

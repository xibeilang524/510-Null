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
    public class WorkstationBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public WorkstationBll(string repoUri)
        {
            provider = ProviderFactory.Create<IWorkstationProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IWorkstationProvider provider;
        #endregion

        #region 公共方法
        public WorkStationInfo GetWorkStationByID(string stationID)
        {
            QueryResult<WorkStationInfo> result = provider.GetByID(stationID);
            if (result.Result == ResultCode.Successful)
            {
                return result.QueryObject;
            }
            else
            {
                return null;
            }
        }

        public QueryResultList<WorkStationInfo> GetAllWorkstations()
        {
            return provider.GetAll();
        }

        public CommandResult Insert(WorkStationInfo info)
        {
            return provider.Insert(info);
        }

        public CommandResult Update(WorkStationInfo curVal)
        {
            WorkStationInfo original = GetWorkStationByID(curVal.StationID);
            if (original != null)
            {
                return provider.Update(curVal, original);
            }
            else
            {
                return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
            }
        }

        public CommandResult Delete(WorkStationInfo info)
        {
            if (info.CanDelete)
            {
                return provider.Delete(info);
            }
            else
            {
                throw new InvalidOperationException(string.Format(Resource1.WorkstationBll_CannotDelete, info.StationName));
            }
        }

        public CommandResult DeleteAllWorkStations()
        {
            return provider.DeleteAllWorkStations();
        }

        public CommandResult UpdateOrInsert(WorkStationInfo info)
        {
            WorkStationInfo original = provider.GetByID(info.StationID).QueryObject;
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

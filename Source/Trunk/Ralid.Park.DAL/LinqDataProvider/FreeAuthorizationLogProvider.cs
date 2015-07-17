using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class FreeAuthorizationLogProvider : ProviderBase<FreeAuthorizationLog, int>, IFreeAuthorizationLogProvider
    {
        #region 构造函数
        public FreeAuthorizationLogProvider()
        {
        }
        public FreeAuthorizationLogProvider(string connStr)
            :base(connStr)
        {
        }
        #endregion

        #region 重写基类方法
        protected override FreeAuthorizationLog GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.FreeAuthorizationLog.SingleOrDefault(item => item.LogID == id);
        }

        protected override List<FreeAuthorizationLog> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                IQueryable<FreeAuthorizationLog> result = parking.FreeAuthorizationLog;
                RecordSearchCondition condition = search as RecordSearchCondition;
                result = result.Where(item => item.LogDateTime >= condition.RecordDateTimeRange.Begin &&
                    item.LogDateTime <= condition.RecordDateTimeRange.End);
                if (!string.IsNullOrEmpty(condition.CardID))
                {
                    result = result.Where(item => item.CardID == condition.CardID);
                }
                if (condition.Operator != null)
                {
                    result = result.Where(item => item.OperatorID == condition.Operator.OperatorName);
                }
                if (!string.IsNullOrEmpty(condition.StationID))
                {
                    result = result.Where(item => item.StationID == condition.StationID);
                }
                return result.ToList();
            }
            return new List<FreeAuthorizationLog>();
        }
        #endregion
    }
}

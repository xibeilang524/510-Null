using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class OperatorLogProvider : ProviderBase<OperatorLogInfo, OperatorLogID>, IOperatorLogProvider
    {
        public OperatorLogProvider()
        {
        }

        public OperatorLogProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写基类方法
        protected override OperatorLogInfo GetingItemByID(OperatorLogID id, ParkDataContext parking)
        {
            return parking.OperatorLog.SingleOrDefault(o => o.OperatorID == id.OperatorID && o.OnDutyDateTime == id.OnDutyDateTime);
        }

        protected override List<OperatorLogInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;
                IQueryable<OperatorLogInfo> result = parking.OperatorLog;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(o => o.OffDutyDateTime >= condition.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(o => o.OffDutyDateTime <= condition.RecordDateTimeRange.End).AsQueryable();
                }
                if (condition.Operator != null)
                {
                    result = result.Where(c => c.OperatorID == condition.Operator.OperatorID).AsQueryable();
                }
                if (condition.StationID != null && condition.StationID != "")
                {
                    result = result.Where(o => o.StationID == condition.StationID).AsQueryable();
                }
                return result.ToList();
            }
            return new List<OperatorLogInfo>();
        }
        #endregion
    }
}

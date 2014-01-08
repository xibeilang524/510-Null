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
    public class OperatorLogProvider : ProviderBase<OperatorSettleLog, DateTime>, IOperatorLogProvider
    {
        public OperatorLogProvider()
        {
        }

        public OperatorLogProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写基类方法
        protected override OperatorSettleLog GetingItemByID(DateTime id, ParkDataContext parking)
        {
            return parking.OperatorLog.SingleOrDefault(o => o.SettleDateTime == id);
        }

        protected override List<OperatorSettleLog> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;
                IQueryable<OperatorSettleLog> result = parking.OperatorLog;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(o => o.SettleDateTime >= condition.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(o => o.SettleDateTime <= condition.RecordDateTimeRange.End).AsQueryable();
                }
                if (condition.Operator != null)
                {
                    result = result.Where(c => c.OperatorID == condition.Operator.OperatorName).AsQueryable();
                }
                if (condition.StationID != null && condition.StationID != "")
                {
                    result = result.Where(o => o.StationID == condition.StationID).AsQueryable();
                }
                return result.ToList();
            }
            return new List<OperatorSettleLog>();
        }
        #endregion
    }
}

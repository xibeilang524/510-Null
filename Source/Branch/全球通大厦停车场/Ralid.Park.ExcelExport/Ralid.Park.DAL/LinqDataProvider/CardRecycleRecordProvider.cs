using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class CardRecycleRecordProvider : ProviderBase<CardRecycleRecord, string>, ICardRecycleRecordProvider
    {
        public CardRecycleRecordProvider()
        {
        }

        public CardRecycleRecordProvider(string connStr)
            : base(connStr)
        {
        }

        #region  重写基类方法
        protected override List<CardRecycleRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;
                IQueryable<CardRecycleRecord> result = parking.CardRecycle;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.RecycleDateTime >= condition.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.RecycleDateTime <= condition.RecordDateTimeRange.End).AsQueryable();
                }
                if (condition.Operator != null)
                {
                    result = result.Where(c => c.OperatorID == condition.Operator.OperatorID).AsQueryable();
                }
                if (condition.StationID != null && condition.StationID != "")
                {
                    result = result.Where(c => c.StationID == condition.StationID).AsQueryable();
                }
                if (condition.CardID != null && condition.CardID != "")
                {
                    result = result.Where(c => c.CardID == condition.CardID).AsQueryable();
                }
                return result.ToList();
            }
            return new List<CardRecycleRecord>();
        }
        #endregion
    }
}

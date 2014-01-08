using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class CardLostRestoreRecordProvider :ProviderBase<CardLostRestoreRecord,string>,
        ICardLostRestoreRecordProvider
    {
        public CardLostRestoreRecordProvider()
        {
        }

        public CardLostRestoreRecordProvider(string connStr):base(connStr )
        {

        }

        #region 重写基类方法
        protected override List<CardLostRestoreRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;
                IQueryable<CardLostRestoreRecord> result;
                result = parking.CardLostRestore;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.LostDateTime >= condition.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.LostDateTime <= condition.RecordDateTimeRange.End).AsQueryable();
                }
                if (condition.Operator != null)
                {
                    result = result.Where(c => c.LostOperator == condition.Operator.OperatorID).AsQueryable();
                }
                if (condition.StationID != null && condition.StationID != "")
                {
                    result = result.Where(c => c.LostStation == condition.StationID).AsQueryable();
                }
                if (condition.CardID != null && condition.CardID != "")
                {
                    result = result.Where(c => c.CardID == condition.CardID).AsQueryable();
                }
                return result.ToList();
            }
            return new List<CardLostRestoreRecord>();
        }

        protected override void InsertingItem(CardLostRestoreRecord info, ParkDataContext parking)
        {
            if (info.RestoreDateTime == null) //挂失
            {
                if (parking.CardLostRestore.Count(c => c.CardID == info.CardID && c.RestoreDateTime == null) == 0)
                {
                    parking.CardLostRestore.InsertOnSubmit(info);
                }
            }
            else
            {
                CardLostRestoreRecord record = parking.CardLostRestore.Single(
                    c => c.CardID == info.CardID && c.RestoreDateTime == null);
                record.RestoreDateTime = info.RestoreDateTime;
                record.RestoreOperator = info.RestoreOperator;
                record.RestoreStation = info.RestoreStation;
                record.RestoreMemo = info.RestoreMemo;
            }
        }
        #endregion
    }
}

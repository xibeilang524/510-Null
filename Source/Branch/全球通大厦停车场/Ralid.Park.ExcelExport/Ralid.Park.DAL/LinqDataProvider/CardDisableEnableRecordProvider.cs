using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class CardDisableEnableRecordProvider : ProviderBase<CardDisableEnableRecord, string>,
        ICardDisableEnableRecordProvider
    {
        public CardDisableEnableRecordProvider()
        {
        }

        public CardDisableEnableRecordProvider(string connStr)
            : base(connStr)
        {
        }


        #region 重写基类方法
        protected override List<CardDisableEnableRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;
                IQueryable<CardDisableEnableRecord> result = parking.CardDisableEnable;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.DisableDateTime >= condition.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.DisableDateTime <= condition.RecordDateTimeRange.End).AsQueryable();
                }
                if (condition.Operator != null)
                {
                    result = result.Where(c => c.DisableOperator == condition.Operator.OperatorID).AsQueryable();
                }
                if (condition.StationID != null && condition.StationID != "")
                {
                    result = result.Where(c => c.DisableStationID == condition.StationID).AsQueryable();
                }
                if (condition.CardID != null && condition.CardID != "")
                {
                    result = result.Where(c => c.CardID == condition.CardID).AsQueryable();
                }
                return result.ToList();
            }
            return new List<CardDisableEnableRecord>();
        }

        protected override void InsertingItem(CardDisableEnableRecord info, ParkDataContext parking)
        {
            if (info.EnableDateTime == null) //禁用
            {
                if (parking.CardDisableEnable.Count(c => c.CardID == info.CardID && c.EnableDateTime == null) == 0)
                {
                    parking.CardDisableEnable.InsertOnSubmit(info);
                }
            }
            else //启用
            {
                CardDisableEnableRecord record = parking.CardDisableEnable.Single(c => c.CardID == info.CardID && c.EnableDateTime == null);
                record.EnableDateTime = info.EnableDateTime;
                record.EnableOperator = info.EnableOperator;
                record.EnableStationID = info.EnableStationID;
                record.EnableMemo = info.EnableMemo;
            }
        }
        #endregion
    }
}

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
                RecordSearchCondition con = search as RecordSearchCondition;
                IQueryable<CardDisableEnableRecord> result = parking.CardDisableEnable;
                if (con.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.DisableDateTime >= con.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.DisableDateTime <= con.RecordDateTimeRange.End).AsQueryable();
                }
                if (con.Operator != null) result = result.Where(c => c.DisableOperator == con.Operator.OperatorName).AsQueryable();
                if (con.StationID != null && con.StationID != "") result = result.Where(c => c.DisableStationID == con.StationID).AsQueryable();
                if (!string.IsNullOrEmpty(con.CardID)) result = result.Where(c => c.CardID.Contains(con.CardID));
                if (!string.IsNullOrEmpty(con.CardCertificate)) result = result.Where(c => c.CardCertificate.Contains(con.CardCertificate));
                if (!string.IsNullOrEmpty(con.OwnerName)) result = result.Where(c => c.OwnerName.Contains(con.OwnerName));
                if (!string.IsNullOrEmpty(con.CarPlate)) result = result.Where(c => c.CarPlate.Contains(con.CarPlate));
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

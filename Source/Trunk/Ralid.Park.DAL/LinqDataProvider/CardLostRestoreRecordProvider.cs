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
                RecordSearchCondition con = search as RecordSearchCondition;
                IQueryable<CardLostRestoreRecord> result;
                result = parking.CardLostRestore;
                if (con.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.LostDateTime >= con.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.LostDateTime <= con.RecordDateTimeRange.End).AsQueryable();
                }
                if (con.Operator != null) result = result.Where(c => c.LostOperator == con.Operator.OperatorName).AsQueryable();
                if (con.StationID != null && con.StationID != "") result = result.Where(c => c.LostStation == con.StationID).AsQueryable();
                if (!string.IsNullOrEmpty(con.CardID)) result = result.Where(c => c.CardID.Contains(con.CardID));
                if (!string.IsNullOrEmpty(con.CardCertificate)) result = result.Where(c => c.CardCertificate.Contains(con.CardCertificate));
                if (!string.IsNullOrEmpty(con.OwnerName)) result = result.Where(c => c.OwnerName.Contains(con.OwnerName));
                if (!string.IsNullOrEmpty(con.CarPlate)) result = result.Where(c => c.CarPlate.Contains(con.CarPlate));
                if (con.IsUnSettled != null)
                {
                    if (con.IsUnSettled.Value) result = result.Where(c => c.SettleDateTime == null);
                    else result = result.Where(c => c.SettleDateTime != null);
                }
                if (con.SettleDateTime != null) result = result.Where(c => c.SettleDateTime == con.SettleDateTime.Value);
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

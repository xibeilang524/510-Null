using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class YCTPaymentRecordProvider : ProviderBase<YCTPaymentRecord, Guid>, IYCTPaymentRecordProvider
    {
        #region 构造函数
        public YCTPaymentRecordProvider()
        {
        }

        public YCTPaymentRecordProvider(string connStr)
            : base(connStr)
        {
        }
        #endregion

        #region 重写基类方法
        protected override YCTPaymentRecord GetingItemByID(Guid id, ParkDataContext parking)
        {
            return parking.GetTable<YCTPaymentRecord>().SingleOrDefault(item => item.ID == id);
        }

        protected override List<YCTPaymentRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                IQueryable<YCTPaymentRecord> result = parking.GetTable<YCTPaymentRecord>();
                RecordSearchCondition condition = search as RecordSearchCondition;
                //result = result.Where(item => item.LogDateTime >= condition.RecordDateTimeRange.Begin &&
                //    item.LogDateTime <= condition.RecordDateTimeRange.End);
                //if (!string.IsNullOrEmpty(condition.CardID))
                //{
                //    result = result.Where(item => item.CardID == condition.CardID);
                //}
                return result.ToList();
            }
            return new List<YCTPaymentRecord>();
        }
        #endregion
    }
}

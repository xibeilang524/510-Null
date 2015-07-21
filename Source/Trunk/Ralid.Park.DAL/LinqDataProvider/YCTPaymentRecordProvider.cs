using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class YCTPaymentRecordProvider : ProviderBase<YCTPaymentRecord, int>, IYCTPaymentRecordProvider
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
        protected override YCTPaymentRecord GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.GetTable<YCTPaymentRecord>().SingleOrDefault(item => item.ID == id);
        }

        protected override List<YCTPaymentRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is YCTPaymentRecordSearchCondition)
            {
                IQueryable<YCTPaymentRecord> result = parking.GetTable<YCTPaymentRecord>();
                YCTPaymentRecordSearchCondition condition = search as YCTPaymentRecordSearchCondition;
                result = result.Where(item => item.TIM >= condition.PaymentDateTimeRange.Begin && item.TIM <= condition.PaymentDateTimeRange.End);
                if (!string.IsNullOrEmpty(condition.PID)) result = result.Where(item => item.PID == condition.PID);
                if (!string.IsNullOrEmpty(condition.CardID)) result = result.Where(item => item.FCN == condition.CardID);
                if (condition.State.HasValue) result = result.Where(item => (int)item.State == condition.State.Value);
                return result.ToList();
            }
            return new List<YCTPaymentRecord>();
        }
        #endregion
    }
}

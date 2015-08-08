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
            IQueryable<YCTPaymentRecord> result = parking.GetTable<YCTPaymentRecord>();
            if (search is YCTPaymentRecordSearchCondition)
            {
                YCTPaymentRecordSearchCondition con = search as YCTPaymentRecordSearchCondition;
                if (con.PaymentDateTimeRange != null) result = result.Where(item => item.TIM >= con.PaymentDateTimeRange.Begin && item.TIM <= con.PaymentDateTimeRange.End);
                if (!string.IsNullOrEmpty(con.PID)) result = result.Where(item => item.PID == con.PID);
                if (con.PSN.HasValue) result = result.Where(item => item.PSN == con.PSN.Value);
                if (con.TIM.HasValue) result = result.Where(item => item.TIM == con.TIM.Value);
                if (!string.IsNullOrEmpty(con.LCN)) result = result.Where(item => item.LCN == con.LCN);
                if (con.WalletType.HasValue) result = result.Where(item => item.WalletType == con.WalletType.Value);
                if (con.State.HasValue) result = result.Where(item => (int)item.State == con.State.Value);
                if (!string.IsNullOrEmpty(con.UploadFile)) result = result.Where(item => item.UploadFile == con.UploadFile);
                if (con.UnUploaded) result = result.Where(item => item.UploadFile == null);
                return result.ToList();
            }
            return new List<YCTPaymentRecord>();
        }
        #endregion
    }
}

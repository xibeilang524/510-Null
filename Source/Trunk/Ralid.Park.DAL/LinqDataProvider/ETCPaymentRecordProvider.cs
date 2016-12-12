using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class ETCPaymentRecordProvider : ProviderBase<ETCPaymentRecord, string>, IETCPaymentRecordProvider
    {
        #region 构造函数
        public ETCPaymentRecordProvider()
        {
        }

        public ETCPaymentRecordProvider(string connStr)
            : base(connStr)
        {

        }
        #endregion

        #region 重写基类方法
        protected override ETCPaymentRecord GetingItemByID(string id, ParkDataContext parking)
        {
            return parking.GetTable<ETCPaymentRecord>().SingleOrDefault(it => it.ListNo == id);
        }

        protected override List<ETCPaymentRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is ETCPaymentRecordSearchCondition)
            {
                ETCPaymentRecordSearchCondition con = search as ETCPaymentRecordSearchCondition;

                IQueryable<ETCPaymentRecord> result = parking.GetTable<ETCPaymentRecord>();
                if (con.CreateTime != null) result = result.Where(it => it.CreateTime >= con.CreateTime.Begin && it.CreateTime <= con.CreateTime.End);
                if (!string.IsNullOrEmpty(con.ListNoLike)) result = result.Where(it => it.ListNo.Contains(con.ListNoLike));
                if (con.WaitingUpload.HasValue)
                {
                    if (con.WaitingUpload.Value) result = result.Where(it => it.UploadTime == null);
                    else result = result.Where(it => it.UploadTime != null);
                }
                return result.ToList();
            }
            return new List<ETCPaymentRecord>();
        }
        #endregion
    }
}

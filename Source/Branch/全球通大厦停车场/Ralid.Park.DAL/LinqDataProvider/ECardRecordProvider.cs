using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class ECardRecordProvider : ProviderBase<ECardRecord, int>, IECardRecordProvider
    {
        #region 构造函数
        public ECardRecordProvider(string connStr)
            : base(connStr)
        {
        }

        public ECardRecordProvider()
        {
        }
        #endregion

        #region 重写基类方法
        protected override ECardRecord GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.GetTable<ECardRecord>().SingleOrDefault(c => c.ID == id);
        }

        protected override List<ECardRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            List<ECardRecord> items = new List<ECardRecord>();
            IQueryable<ECardRecord> result = parking.GetTable<ECardRecord>();
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;
                if (!string.IsNullOrEmpty(condition.CardID)) result = result.Where(c => c.CardID == condition.CardID);
                items = result.ToList();
            }
            return items;
        }
        #endregion
    }
}

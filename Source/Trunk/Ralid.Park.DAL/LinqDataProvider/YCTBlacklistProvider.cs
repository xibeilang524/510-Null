using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class YCTBlacklistProvider : ProviderBase<YCTBlacklist, string>, IYCTBlacklistProvider
    {
        #region 构造函数
        public YCTBlacklistProvider()
        {
        }

        public YCTBlacklistProvider(string connStr)
            : base(connStr)
        {
        }
        #endregion

        #region 重写基类方法
        protected override YCTBlacklist GetingItemByID(string id, ParkDataContext parking)
        {
            return parking.GetTable<YCTBlacklist>().SingleOrDefault(item => item.ID == id);
        }

        protected override List<YCTBlacklist> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                IQueryable<YCTBlacklist> result = parking.GetTable<YCTBlacklist>();
                RecordSearchCondition condition = search as RecordSearchCondition;
                //result = result.Where(item => item.LogDateTime >= condition.RecordDateTimeRange.Begin &&
                //    item.LogDateTime <= condition.RecordDateTimeRange.End);
                //if (!string.IsNullOrEmpty(condition.CardID))
                //{
                //    result = result.Where(item => item.CardID == condition.CardID);
                //}
                return result.ToList();
            }
            return new List<YCTBlacklist>();
        }
        #endregion
    }
}

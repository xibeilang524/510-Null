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
            return parking.GetTable<YCTBlacklist>().SingleOrDefault(item => item.LCN == id);
        }

        protected override List<YCTBlacklist> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            IQueryable<YCTBlacklist> result = parking.GetTable<YCTBlacklist>();
            if (search is YCTBlacklistSearchCondition)
            {
                YCTBlacklistSearchCondition con = search as YCTBlacklistSearchCondition;
                if (con.WalletType != null) result = result.Where(item => item.WalletType == con.WalletType.Value);
                if (con.OnlyCatched) result = result.Where(item => item.CatchAt != null);
                if (con.OnlyUnUploaded) result = result.Where(item =>item.CatchAt != null && item.UploadFile == null);
            }
            return result.ToList();
        }
        #endregion
    }
}

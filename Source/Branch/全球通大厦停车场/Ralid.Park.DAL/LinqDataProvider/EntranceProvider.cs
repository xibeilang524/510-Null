using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class EntranceProvider : ProviderBase<EntranceInfo, int>, IEntranceProvider
    {
        public EntranceProvider()
        {
        }

        public EntranceProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override EntranceInfo GetingItemByID(int id, ParkDataContext Parking)
        {
            return Parking.Entrance.SingleOrDefault(e => e.EntranceID == id);
        }

        protected override List<EntranceInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is EntranceSearchCondition)
            {
                EntranceSearchCondition con = search as EntranceSearchCondition;
                var result = parking.GetTable<EntranceInfo>() as IQueryable<EntranceInfo>;
                if (con.ParkID > 0)
                {
                    result = result.Where(e => e.ParkID == con.ParkID);
                }
                if (con.EntranceID > 0)
                {
                    result = result.Where(e => e.EntranceID == con.EntranceID);
                }
                if (!string.IsNullOrEmpty(con.EntranceName))
                {
                    result = result.Where(e => e.EntranceName.Contains(con.EntranceName));
                }
                return result.ToList();
            }
            else
            {
                return new List<EntranceInfo>();
            }
        }

        protected override void DeletingItem(EntranceInfo info, ParkDataContext Parking)
        {
            var vs = Parking.VideoSource.Where(v => v.EntranceID == info.EntranceID);
            Parking.VideoSource.DeleteAllOnSubmit(vs);
            Parking.Entrance.Attach(info);
            Parking.Entrance.DeleteOnSubmit(info);
        }
        #endregion
    }
}

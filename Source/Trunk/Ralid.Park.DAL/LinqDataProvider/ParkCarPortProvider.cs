using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class ParkCarPortProvider : ProviderBase<ParkCarPort, int>, IParkCarPortProvider
    {
        #region 构造函数
        public ParkCarPortProvider(string connStr)
            : base(connStr)
        {
        }
        #endregion

        #region 重写基类方法
        protected override ParkCarPort GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.GetTable<ParkCarPort>().SingleOrDefault(item => item.ID == id);
        }

        protected override List<ParkCarPort> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            IQueryable<ParkCarPort> ret = parking.GetTable<ParkCarPort>();
            if (search is ParkCarPortSearchCondition)
            {
                ParkCarPortSearchCondition con = search as ParkCarPortSearchCondition;
                if (con.ParkID > 0) ret = ret.Where(item => item.ParkID == con.ParkID);
            }
            return ret.ToList();
        }
        #endregion
    }
}

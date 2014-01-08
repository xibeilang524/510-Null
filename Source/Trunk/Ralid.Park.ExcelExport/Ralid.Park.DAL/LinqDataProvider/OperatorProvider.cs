using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class OperatorProvider : ProviderBase<OperatorInfo,string>, IOperatorProvider
    {
        public OperatorProvider()
        {
        }

        public OperatorProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override OperatorInfo GetingItemByID(string id, ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<OperatorInfo>(o => o.Role);
            parking.LoadOptions = opt;
            return parking.Operator.SingleOrDefault(o => o.OperatorID == id);
        }

        protected override List<OperatorInfo> GetingAllItems(ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<OperatorInfo>(o => o.Role);
            parking.LoadOptions = opt;
            return parking.Operator.ToList();
        }


        protected override List<OperatorInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is OperatorSearchCondition)
            {
                OperatorSearchCondition con = search as OperatorSearchCondition;
                DataLoadOptions opt = new DataLoadOptions();
                opt.LoadWith<OperatorInfo>(o => o.Role);
                parking.LoadOptions = opt;
                IQueryable<OperatorInfo> result = parking.Operator.AsQueryable();
                if (!string.IsNullOrEmpty(con.RoleID))
                {
                    result = result.Where(o => o.RoleID == con.RoleID);
                }
                if (con.OperatorNum != null)
                {
                    result = result.Where(o => o.OperatorNum == con.OperatorNum.Value);
                }
                return result.ToList();
            }
            else
            {
                return new List<OperatorInfo>();
            }
        }
        #endregion
    }
}

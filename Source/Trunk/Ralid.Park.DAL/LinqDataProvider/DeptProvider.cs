using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class DeptProvider : ProviderBase<DeptInfo, string>, IDeptProvider
    {
        public DeptProvider()
        {
        }

        public DeptProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override DeptInfo GetingItemByID(string id, ParkDataContext parking)
        {
            return parking.Dept.SingleOrDefault(r => r.DeptID.ToString() == id);
        }

        protected override void DeletingItem(DeptInfo info, ParkDataContext parking)
        {
            parking.Dept.Attach(info);
            parking.Dept.DeleteOnSubmit(info);
        }
        #endregion
    }
}

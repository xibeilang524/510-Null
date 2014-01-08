using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Linq;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model ;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class SysparameterProvider : ProviderBase<SysparameterInfo,string>, ISysParameterProvider
    {
        public SysparameterProvider()
        {
        }

        public SysparameterProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override SysparameterInfo GetingItemByID(string id, ParkDataContext parking)
        {
            return parking.Sysparameter.SingleOrDefault(s => s.Parameter == id);
        }

        protected override void InsertingItem(SysparameterInfo info, ParkDataContext parking)
        {
            string cmd = @"delete Sysparameter where Parameter=@p0 " +
                       "insert into SysParameter (Parameter,ParameterValue,Description) values (@p1,@p2,@p3)";
            parking.ExecuteCommand(cmd, info.Parameter, info.Parameter, info.ParameterValue, info.Description);
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.LocalDataBase.DAL.IDAL;
using Ralid.Park.LocalDataBase.Model;

namespace Ralid.Park.LocalDataBase.DAL.LinqDataProvider
{
    public class LDB_SysparameterProvider : LDB_ProviderBase<LDB_SysparameterInfo, string>, LDB_ISysParameterProvider
    {
        public LDB_SysparameterProvider()
        {
        }

        public LDB_SysparameterProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override LDB_SysparameterInfo GetingItemByID(string id, LDB_DataContext parking)
        {
            return parking.Sysparameter.SingleOrDefault(s => s.Parameter == id);
        }

        protected override void InsertingItem(LDB_SysparameterInfo info, LDB_DataContext parking)
        {
            string cmd = @"delete from Sysparameter where Parameter=@p0 ; " +
                       "insert into SysParameter (Parameter,ParameterValue,Description) values (@p1,@p2,@p3) ;";
            parking.ExecuteCommand(cmd, info.Parameter, info.Parameter, info.ParameterValue, info.Description);
        }
        #endregion
    }
}

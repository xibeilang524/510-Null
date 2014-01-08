using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using Ralid.Park.LocalDataBase.Model;

namespace Ralid.Park.LocalDataBase.DAL.LinqDataProvider
{

    public class LDB_DataContext:DataContext
    {
        public LDB_DataContext(IDbConnection connection) :
            base(connection)
        {
        }

        public Table<LDB_SysparameterInfo> Sysparameter
        {
            get
            {
                return this.GetTable<LDB_SysparameterInfo>();
            }
        }

        public Table<LDB_CardPaymentInfo> CardPaymentRecord
        {
            get
            {
                return this.GetTable<LDB_CardPaymentInfo>();
            }
        }
    }
}

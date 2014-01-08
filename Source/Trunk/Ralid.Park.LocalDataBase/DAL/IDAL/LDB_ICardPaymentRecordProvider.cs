using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.LocalDataBase.Model;

namespace Ralid.Park.LocalDataBase.DAL.IDAL
{
    public interface LDB_ICardPaymentRecordProvider : IProvider<LDB_CardPaymentInfo, int>
    {
    }
}

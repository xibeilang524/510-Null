using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.LocalDataBase.Model;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.LocalDataBase.DAL.IDAL
{
    public interface LDB_ISysParameterProvider : IProvider<LDB_SysparameterInfo, string>
    {
    }
}

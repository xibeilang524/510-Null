using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.IDAL
{
    public interface IPRERoleProvider : IProvider<PRERoleInfo, string>
    {
        CommandResult DeleteAllRoles();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.IDAL
{
    public interface ICardProvider:IProvider <CardInfo,string>
    {
        CommandResult DeleteAllItems();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class CardChangeRecordProvider:ProviderBase<CardChangeRecord,string>,ICardChangeRecordProvider
    {
    }

}

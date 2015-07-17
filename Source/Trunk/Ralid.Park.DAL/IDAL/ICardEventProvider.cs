using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.Park.DAL.IDAL
{
    public interface ICardEventProvider : IProvider<CardEventRecord,RecordID>
    {
        void DeleteAllCardEventBefore(DateTime eventDatetime);
    }
}

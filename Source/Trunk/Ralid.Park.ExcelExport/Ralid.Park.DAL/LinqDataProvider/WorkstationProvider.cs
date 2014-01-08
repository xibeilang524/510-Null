using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class  WorkstationProvider:ProviderBase<WorkStationInfo,string>,IWorkstationProvider 
    {
        public WorkstationProvider()
        {
        }

        public WorkstationProvider(string connStr):base(connStr)
        {
        }

        protected override WorkStationInfo GetingItemByID(string id, ParkDataContext parking)
        {
            return parking.GetTable<WorkStationInfo>().SingleOrDefault(w => w.StationID == id);
        }
    }
}


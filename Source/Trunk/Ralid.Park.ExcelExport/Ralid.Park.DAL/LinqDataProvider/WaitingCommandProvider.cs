using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class WaitingCommandProvider : ProviderBase<WaitingCommandInfo, int>, IWaitingCommandProvider
    {
        public WaitingCommandProvider()
        {
        }
        public WaitingCommandProvider(string connStr)
            : base(connStr)
        {

        }

        protected override void InsertingItem(WaitingCommandInfo info, ParkDataContext parking)
        {
            if (parking.WaitingCommand.Count(w => w.CardID == info.CardID && w.ParkID == info.ParkID) == 0)
            {
                base.InsertingItem(info, parking);
            }
        }

        protected override List<WaitingCommandInfo> GetingItems(ParkDataContext parking,SearchCondition search)
        {
            if (search is WaitingCommandSearchCondition)
            {
                WaitingCommandSearchCondition con = search as WaitingCommandSearchCondition;
                return parking.WaitingCommand.Where(w => w.ParkID == con.ParkID).ToList();
            }
            else
            {
                return new List<WaitingCommandInfo>();
            }
        }
    }
}

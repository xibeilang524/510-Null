using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class WaitingCommandProvider : ProviderBase<WaitingCommandInfo, WaitingCommandID>, IWaitingCommandProvider
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
            if (parking.WaitingCommand.Count(w => w.CardID == info.CardID && w.EntranceID == info.EntranceID) == 0)
            {
                base.InsertingItem(info, parking);
            }
        }

        protected override List<WaitingCommandInfo> GetingItems(ParkDataContext parking,SearchCondition search)
        {
            if (search is WaitingCommandSearchCondition)
            {
                WaitingCommandSearchCondition con = search as WaitingCommandSearchCondition;
                return parking.WaitingCommand.Where(w => w.EntranceID == con.EntranceID).ToList();
            }
            else
            {
                return new List<WaitingCommandInfo>();
            }
        }

        protected override void DeletingItem(WaitingCommandInfo info, ParkDataContext parking)
        {
            string cmd = @"delete WaitingCommand where EntranceID=@p0 and  Command=@p1 and CardID=@p2";
            parking.ExecuteCommand(cmd, info.EntranceID, info.Command, info.CardID);
        }
    }
}

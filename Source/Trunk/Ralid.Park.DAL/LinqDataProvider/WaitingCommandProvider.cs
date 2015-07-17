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

        protected override WaitingCommandInfo GetingItemByID(WaitingCommandID id, ParkDataContext parking)
        {
            return parking.GetTable<WaitingCommandInfo>().SingleOrDefault(w => w.EntranceID == id.EntranceID && w.Command == id.Command && w.CardID == id.CardID);
        }

        protected override List<WaitingCommandInfo> GetingAllItems(ParkDataContext parking)
        {
            return parking.GetTable<WaitingCommandInfo>().Select(t => t).OrderBy(t => t.EntranceID).ToList();
        }

        protected override List<WaitingCommandInfo> GetingItems(ParkDataContext parking,SearchCondition search)
        {
            if (search is WaitingCommandSearchCondition)
            {
                WaitingCommandSearchCondition con = search as WaitingCommandSearchCondition;
                IQueryable<WaitingCommandInfo> result = parking.WaitingCommand.AsQueryable();
                if (con.EntranceID > 0)
                {
                    result = result.Where(w => w.EntranceID == con.EntranceID);
                }
                if (con.CommandType != null)
                {
                    result = result.Where(w => w.Command == con.CommandType.Value);
                }
                if (!string.IsNullOrEmpty(con.CardID))
                {
                    result = result.Where(w => w.CardID == con.CardID);
                }
                if (con.Status != null)
                {
                    result = result.Where(w => w.Status == con.Status);
                }
                result = result.OrderBy(w => w.EntranceID);
                return result.ToList();
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

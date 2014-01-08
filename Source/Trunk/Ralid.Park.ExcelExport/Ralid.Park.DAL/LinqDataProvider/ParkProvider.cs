using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel .Model ;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class ParkProvider : ProviderBase<ParkInfo, int>, IParkProvider
    {
        public ParkProvider()
        {
        }

        public ParkProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写基类方法
        protected override ParkInfo GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.Park.Single(p => p.ParkID == id);
        }

        protected override void DeletingItem(ParkInfo info, ParkDataContext parking)
        {
            List<EntranceInfo> entrances = parking.Entrance.Where(e => e.ParkID == info.ParkID).ToList();
            List<VideoSourceInfo> videos = (from e in parking.Entrance
                                            join v in parking.VideoSource on e.EntranceID equals v.EntranceID
                                            where e.ParkID == info.ParkID
                                            select v).ToList();
            parking.Park.Attach(info);
            parking.Park.DeleteOnSubmit(info);
            parking.Entrance.DeleteAllOnSubmit(entrances);
            parking.VideoSource.DeleteAllOnSubmit(videos);
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class SnapShotProvider : ProviderBase<SnapShot, SnapShotID>, ISnapShotProvider
    {
        public SnapShotProvider()
        {
        }

        public SnapShotProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写基类成员
        protected override SnapShot GetingItemByID(SnapShotID id, ParkDataContext parking)
        {
            return parking.SnapShot.SingleOrDefault(s => s.ShotAt == id.ShotAt && s.VideoSourceID == id.VideoSourceID);
        }

        protected override List<SnapShot> GetingItems(ParkDataContext parking, Ralid.Park.BusinessModel.SearchCondition.SearchCondition search)
        {
            List<SnapShot> items = new List<SnapShot>();
            if (search is SnapShotSearchCondition)
            {
                SnapShotSearchCondition con = search as SnapShotSearchCondition;
                IQueryable<SnapShot> result = parking.SnapShot;
                if (con.ShotDateTime != null)
                {
                    result = result.Where(s => s.ShotAt == con.ShotDateTime.Value);
                }
                items = result.ToList();
            }
            return items;
        }
        #endregion

        public void DeleteAllSnapShotBefore(DateTime shotDatetime)
        {
            ParkDataContext parking = ParkDataContextFactory.CreateParking(base.ConnectStr);
            string cmd = "delete SnapShot where ShotAt < '" + shotDatetime.ToString("yyyy-MM-dd") + "'";
            parking.CommandTimeout = 5 * 60 * 60 * 1000;
            parking.ExecuteCommand(cmd);
        }
    }
}

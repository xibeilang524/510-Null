using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.SqlClient;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel.Result;

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

            //循环删除子车场及其下面的所有设备
            List<ParkInfo> subParks = parking.Park.Where(p => p.ParentID == info.ParkID).ToList ();
            foreach (ParkInfo park in subParks)
            {
                DeletingSubPark(park, parking);
            }
        }

        private void DeletingSubPark(ParkInfo info, ParkDataContext parking)
        {
            List<EntranceInfo> entrances = parking.Entrance.Where(e => e.ParkID == info.ParkID).ToList();
            List<VideoSourceInfo> videos = (from e in parking.Entrance
                                            join v in parking.VideoSource on e.EntranceID equals v.EntranceID
                                            where e.ParkID == info.ParkID
                                            select v).ToList();
            parking.Park.DeleteOnSubmit(info);
            parking.Entrance.DeleteAllOnSubmit(entrances);
            parking.VideoSource.DeleteAllOnSubmit(videos);

            //循环删除子车场及其下面的所有设备
            List<ParkInfo> subParks = parking.Park.Where(p => p.ParentID == info.ParkID).ToList();
            foreach (ParkInfo park in subParks)
            {
                DeletingSubPark(park, parking);
            }
        }

        public CommandResult InsertWithPrimaryKey(ParkInfo info)
        {
            CommandResult result = null;
            try
            {
                ParkDataContext parking = ParkDataContextFactory.CreateParking(base.ConnectStr);
                if (parking != null)
                {
                    string cmdtext = string.Empty;
                    cmdtext += string.Format("SET IDENTITY_INSERT Park ON;");
                    cmdtext += info.SQLInsertWithPrimaryCmd;
                    cmdtext += string.Format("SET IDENTITY_INSERT Park OFF;");
                    parking.ExecuteCommand(cmdtext);
                    result = new CommandResult(ResultCode.Successful, string.Empty);
                }
                else
                {
                    result= new CommandResult(ResultCode.Fail, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result = new CommandResult(ResultCode.Fail, string.Empty);
            }
            return result;
        }

        public CommandResult DeleteAllItems()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from Park", con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return new CommandResult(ResultCode.Successful, string.Empty);
                    }
                }
            }
            catch (SqlException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCodeResolver.GetFromSqlExceptionNumber(ex.Number), ex.Message);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }
        #endregion

    }
}

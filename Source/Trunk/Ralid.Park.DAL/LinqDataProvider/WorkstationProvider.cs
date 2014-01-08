using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
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

        public CommandResult DeleteAllWorkStations()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from WorkStation", con))
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
    }
}


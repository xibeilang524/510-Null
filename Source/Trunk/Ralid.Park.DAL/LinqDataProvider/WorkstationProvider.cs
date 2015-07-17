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
using Ralid.Park.BusinessModel.SearchCondition;

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

        protected override void InsertingItem(WorkStationInfo info, ParkDataContext parking)
        {
            if (info.Dept != null) parking.GetTable<DeptInfo>().Attach(info.Dept);//不需要在插入工作站时同时插入工作站的部门
            base.InsertingItem(info, parking);
        }

        protected override List<WorkStationInfo> GetingAllItems(ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<WorkStationInfo>(o => o.Dept);
            parking.LoadOptions = opt;
            return parking.WorkStation.ToList();
        }

        protected override List<WorkStationInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is WorkstationSearchCondition)
            {
                WorkstationSearchCondition con = search as WorkstationSearchCondition;
                IQueryable<WorkStationInfo> result = parking.WorkStation.AsQueryable();
                if (con.DeptID != null)
                {
                    result = result.Where(w => w.DeptID == con.DeptID);
                }
                result = result.OrderBy(w => w.StationID);
                return result.ToList();
            }
            else
            {
                return new List<WorkStationInfo>();
            }
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.SqlClient;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class EntranceProvider : ProviderBase<EntranceInfo, int>, IEntranceProvider
    {
        public EntranceProvider()
        {
        }

        public EntranceProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override EntranceInfo GetingItemByID(int id, ParkDataContext Parking)
        {
            return Parking.Entrance.SingleOrDefault(e => e.EntranceID == id);
        }

        protected override List<EntranceInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is EntranceSearchCondition)
            {
                EntranceSearchCondition con = search as EntranceSearchCondition;
                var result = parking.GetTable<EntranceInfo>() as IQueryable<EntranceInfo>;
                if (con.ParkID > 0)
                {
                    result = result.Where(e => e.ParkID == con.ParkID);
                }
                if (con.EntranceID > 0)
                {
                    result = result.Where(e => e.EntranceID == con.EntranceID);
                }
                if (!string.IsNullOrEmpty(con.EntranceName))
                {
                    result = result.Where(e => e.EntranceName.Contains(con.EntranceName));
                }
                return result.ToList();
            }
            else
            {
                return new List<EntranceInfo>();
            }
        }

        protected override void DeletingItem(EntranceInfo info, ParkDataContext Parking)
        {
            var vs = Parking.VideoSource.Where(v => v.EntranceID == info.EntranceID);
            Parking.VideoSource.DeleteAllOnSubmit(vs);
            Parking.Entrance.Attach(info);
            Parking.Entrance.DeleteOnSubmit(info);
        }

        public CommandResult InsertWithPrimaryKey(EntranceInfo info)
        {
            CommandResult result = null;
            try
            {
                ParkDataContext parking = ParkDataContextFactory.CreateParking(base.ConnectStr);
                if (parking != null)
                {
                    string cmdtext = string.Empty;
                    cmdtext += string.Format("SET IDENTITY_INSERT Entrance ON;");
                    cmdtext += info.SQLInsertWithPrimaryCmd;
                    cmdtext += string.Format("SET IDENTITY_INSERT Entrance OFF;");
                    parking.ExecuteCommand(cmdtext);
                    result = new CommandResult(ResultCode.Successful, string.Empty);
                }
                else
                {
                    result = new CommandResult(ResultCode.Fail, string.Empty);
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
                    using (SqlCommand cmd = new SqlCommand("delete from Entrance", con))
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

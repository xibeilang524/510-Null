using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Data.SqlClient;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class PREPreferentialLogProvider : ProviderBase<PREPreferentialLog, string>, IPREPreferentialLogProvider
    {
        public PREPreferentialLogProvider()
        {
        }

        public PREPreferentialLogProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override PREPreferentialLog GetingItemByID(string id, ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<PREPreferentialLog>(o => o.Operator);
            parking.LoadOptions = opt;
            return parking.PREPreferentialLog.SingleOrDefault(o => o.PreferentialID.ToString() == id);
        }

        protected override List<PREPreferentialLog> GetingAllItems(ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<PREPreferentialLog>(o => o.Operator);
            parking.LoadOptions = opt;
            return parking.PREPreferentialLog.ToList();
        }


        protected override List<PREPreferentialLog> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is PreferentialReportSearchCondition)
            {
                PreferentialReportSearchCondition con = search as PreferentialReportSearchCondition;

                DataLoadOptions opt = new DataLoadOptions();
                opt.LoadWith<PREPreferentialLog>(o => o.Operator);
                parking.LoadOptions = opt;

                IQueryable<PREPreferentialLog> result = parking.PREPreferentialLog.AsQueryable();
                if (con.RecordDateTimeRange != null)
                {
                    result = result.Where(o => o.OperatorTime >= con.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(o => o.OperatorTime <= con.RecordDateTimeRange.End).AsQueryable();
                }
                if (!string.IsNullOrEmpty(con.CardID))
                {
                    result = result.Where(o => o.CardID.IndexOf(con.CardID) != -1);
                }
                if(!string.IsNullOrEmpty(con.BusinessName))
                {
                    result = result.Where(o => o.BusinessesName1 == con.BusinessName || o.BusinessesName2 == con.BusinessName || o.BusinessesName3 == con.BusinessName);
                }
                if (!string.IsNullOrEmpty(con.CancelReason))
                {
                    result = result.Where(o => o.CancelReason.IndexOf(con.CancelReason) != -1);
                }
                if (con.Hour != null)
                {
                    result = result.Where(o => o.PreferentialHour == con.Hour);
                }
                if (con.StationIDs != null && con.StationIDs.Count > 0) result = result.Where(c => con.StationIDs.Contains(c.WorkstationName));
                if (con.OperatorNames != null && con.OperatorNames.Count > 0) result = result.Where(c => con.OperatorNames.Contains(c.Operator.OperatorName));
                return result.ToList();
            }
            else
            {
                return new List<PREPreferentialLog>();
            }
        }

        protected override void InsertingItem(PREPreferentialLog info, ParkDataContext parking)
        {
            if (info.Operator != null)
            {
                if (!parking.GetTable<PREOperatorInfo>().Contains(info.Operator))
                    parking.GetTable<PREOperatorInfo>().Attach(info.Operator);//不需要在插入优惠信息时同时插入操作员的角色

            }
            base.InsertingItem(info, parking);
        }

        public CommandResult DeleteAllItems()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from PREPreferentialLog", con))
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

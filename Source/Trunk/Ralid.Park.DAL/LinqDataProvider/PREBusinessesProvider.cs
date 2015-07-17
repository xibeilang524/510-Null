using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using System.Data.SqlClient;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class PREBusinessesProvider : ProviderBase<PREBusinesses, string>, IPREBusinessesProvider
    {
        public PREBusinessesProvider()
        {
        }

        public PREBusinessesProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override PREBusinesses GetingItemByID(string id, ParkDataContext parking)
        {
            return parking.PREBusinesses.SingleOrDefault(r => r.BusinessesID.ToString() == id);
        }

        protected override void DeletingItem(PREBusinesses info, ParkDataContext parking)
        {
            parking.PREBusinesses.Attach(info);
            parking.PREBusinesses.DeleteOnSubmit(info);
        }

        #endregion

        public CommandResult DeleteAllItems()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from PREBusinesses", con))
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

        protected override List<PREBusinesses> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is PreferentialReportSearchCondition)
            {
                PreferentialReportSearchCondition con = search as PreferentialReportSearchCondition;
                IQueryable<PREBusinesses> result = parking.PREBusinesses.AsQueryable();
                if (!string.IsNullOrEmpty(con.BusinessName))
                {
                    result = result.Where(o => o.BusinessesName.IndexOf(con.BusinessName) != -1);//相当于%A%
                }
                return result.ToList();
            }
            else
            {
                return new List<PREBusinesses>();
            }
        }

    }
}

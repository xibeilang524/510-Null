using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;


namespace Ralid.Park.DAL.LinqDataProvider
{
    public class PRERoleProvider : ProviderBase<PRERoleInfo, string>, IPRERoleProvider
    {
        public PRERoleProvider()
        {
        }

        public PRERoleProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override PRERoleInfo GetingItemByID(string id, ParkDataContext parking)
        {
            return parking.PRERole.SingleOrDefault(r => r.RoleID == id);
        }

        protected override void DeletingItem(PRERoleInfo info, ParkDataContext parking)
        {
            parking.PRERole.Attach(info);
            parking.PRERole.DeleteOnSubmit(info);
        }
        #endregion

        public CommandResult DeleteAllRoles()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from PRERole", con))
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

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
    public class RoleProvider:ProviderBase <RoleInfo,string> ,IRoleProvider
    {
        public RoleProvider()
        {
        }

        public RoleProvider(string connStr):base(connStr)
        {
        }

        #region 重写模板方法
        protected override RoleInfo GetingItemByID(string id, ParkDataContext parking)
        {
            return parking.Role.SingleOrDefault(r => r.RoleID == id);
        }

        protected override void DeletingItem(RoleInfo info, ParkDataContext parking)
        {
            parking.Role.Attach(info);
            parking.Role.DeleteOnSubmit(info);
        }
        #endregion

        public CommandResult DeleteAllRoles()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from Role", con))
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

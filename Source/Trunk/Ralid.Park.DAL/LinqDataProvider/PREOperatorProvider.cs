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
    public class PREOperatorProvider : ProviderBase<PREOperatorInfo, string>, IPREOperatorProvider
    {
        public PREOperatorProvider()
        {
        }

        public PREOperatorProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override PREOperatorInfo GetingItemByID(string id, ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<PREOperatorInfo>(o => o.Role);
            parking.LoadOptions = opt;
            return parking.PREOperator.SingleOrDefault(o => o.OperatorID == id);
        }

        protected override List<PREOperatorInfo> GetingAllItems(ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<PREOperatorInfo>(o => o.Role);
            parking.LoadOptions = opt;
            return parking.PREOperator.ToList();
        }


        protected override List<PREOperatorInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is OperatorSearchCondition)
            {
                OperatorSearchCondition con = search as OperatorSearchCondition;
                DataLoadOptions opt = new DataLoadOptions();
                opt.LoadWith<PREOperatorInfo>(o => o.Role);
                parking.LoadOptions = opt;
                IQueryable<PREOperatorInfo> result = parking.PREOperator.AsQueryable();
                if (!string.IsNullOrEmpty(con.RoleID))
                {
                    result = result.Where(o => o.RoleID == con.RoleID);
                }
                if (con.OperatorNum != null)
                {
                    result = result.Where(o => o.OperatorNum == con.OperatorNum.Value);
                }
                if (!string.IsNullOrEmpty(con.OperatorID))
                {
                    result = result.Where(o => o.OperatorID.IndexOf(con.OperatorID) != -1);
                }
                if (!string.IsNullOrEmpty(con.OperatorName))
                {
                    result = result.Where(o => o.OperatorName.IndexOf(con.OperatorName) != -1);//相当于%A%匹配
                }
                return result.ToList();
            }
            else
            {
                return new List<PREOperatorInfo>();
            }
        }

        protected override void InsertingItem(PREOperatorInfo info, ParkDataContext parking)
        {
            if (info.Role != null) parking.GetTable<PRERoleInfo>().Attach(info.Role);//不需要在插入操作员时同时插入操作员的角色
            base.InsertingItem(info, parking);
        }

        public CommandResult DeleteAllItems()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from PREOperator", con))
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

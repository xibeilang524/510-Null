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
    public class OperatorProvider : ProviderBase<OperatorInfo,string>, IOperatorProvider
    {
        public OperatorProvider()
        {
        }

        public OperatorProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override OperatorInfo GetingItemByID(string id, ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<OperatorInfo>(o => o.Role);
            opt.LoadWith<OperatorInfo>(o => o.Dept);
            parking.LoadOptions = opt;
            return parking.Operator.SingleOrDefault(o => o.OperatorID == id);
        }

        protected override List<OperatorInfo> GetingAllItems(ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<OperatorInfo>(o => o.Role);
            opt.LoadWith<OperatorInfo>(o => o.Dept);
            parking.LoadOptions = opt;
            return parking.Operator.ToList();
        }


        protected override List<OperatorInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is OperatorSearchCondition)
            {
                OperatorSearchCondition con = search as OperatorSearchCondition;
                DataLoadOptions opt = new DataLoadOptions();
                opt.LoadWith<OperatorInfo>(o => o.Role);
                opt.LoadWith<OperatorInfo>(o => o.Dept);
                parking.LoadOptions = opt;
                IQueryable<OperatorInfo> result = parking.Operator.AsQueryable();
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
                    result = result.Where(o => o.OperatorID == con.OperatorID);
                }
                if (!string.IsNullOrEmpty(con.OperatorName))
                {
                    result = result.Where(o => o.OperatorName == con.OperatorName);
                }
                if (!(con.DeptID == null || con.DeptID == Guid.Empty))
                {
                    result = result.Where(o => o.DeptID == con.DeptID);
                }
                return result.ToList();
            }
            else
            {
                return new List<OperatorInfo>();
            }
        }

        protected override void InsertingItem(OperatorInfo info, ParkDataContext parking)
        {
            if (info.Role != null) parking.GetTable<RoleInfo>().Attach(info.Role);//不需要在插入操作员时同时插入操作员的角色
            if (info.Dept != null) parking.GetTable<DeptInfo>().Attach(info.Dept);//不需要在插入操作员时同时插入操作员的部门
            base.InsertingItem(info, parking);
        }

        public CommandResult DeleteAllItems()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from Operator", con))
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

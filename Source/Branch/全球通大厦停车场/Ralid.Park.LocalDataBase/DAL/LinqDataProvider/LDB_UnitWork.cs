using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.LocalDataBase.DAL.LinqDataProvider
{
    /// <summary>
    /// 单元操作
    /// </summary>
    public class LDB_UnitWork : IUnitWork
    {
        private LDB_DataContext _LDB;

        public LDB_UnitWork(string connStr)
        {
            _LDB = LDB_DataContextFactory.CreateLDB(connStr);
        }

        public LDB_DataContext LDB
        {
            get
            {
                return _LDB;
            }
        }

        public CommandResult Commit()
        {
            try
            {
                LDB.SubmitChanges();
                return new CommandResult(ResultCode.Successful,
                    ResultCode.Successful.ToString());

            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "LDB_UnitWork.Commit()");
                return new CommandResult(ResultCode.Fail , ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    /// <summary>
    /// 多个事务操作
    /// </summary>
    public class TransactionsWork : ITransactionsWork
    {
        private UnitWork _Master;
        private UnitWork _Standby;

        public TransactionsWork(string masterStr, string standbyStr)
        {
            _Master = new UnitWork(masterStr);
            _Standby = new UnitWork(standbyStr);
        }

        public UnitWork Master
        {
            get
            {
                return _Master;
            }
        }
        public UnitWork Standby
        {
            get
            {
                return _Standby;
            }
        }

        public CommandResult Commit()
        {
            CommandResult result = null;
            bool masterFlag = false;
            bool standbyFlag = false;
            try
            {
                if (Master.Parking == null || Standby.Parking == null)
                {
                    result = new CommandResult(Ralid.Park.BusinessModel.Result.ResultCode.CannotConnectServer, string.Empty);
                    return result;
                }

                Master.Parking.Connection.Open();
                masterFlag = true;
                Master.Parking.Transaction = Master.Parking.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                Standby.Parking.Connection.Open();
                standbyFlag = true;
                Standby.Parking.Transaction = Standby.Parking.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                try
                {
                    Master.Parking.SubmitChanges();
                    Master.Parking.Transaction.Commit();

                    Standby.Parking.SubmitChanges();
                    Standby.Parking.Transaction.Commit();


                    result = new CommandResult(Ralid.Park.BusinessModel.Result.ResultCode.Successful,
                        Ralid.Park.BusinessModel.Result.ResultCode.Successful.ToString());
                }
                catch (Exception ex)
                {
                    if (Master.Parking.Transaction != null) Master.Parking.Transaction.Rollback();
                    if (Standby.Parking.Transaction != null) Standby.Parking.Transaction.Rollback();

                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "TransactionsWork.Transaction.Commit()");
                    result = new CommandResult(Ralid.Park.BusinessModel.Result.ResultCode.Fail, ex.Message);
                }
                finally
                {
                    Master.Parking.Transaction = null;
                    Standby.Parking.Transaction = null;

                    if(masterFlag) Master.Parking.Connection.Close();
                    if(standbyFlag) Standby.Parking.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "TransactionsWork.Commit()");
                result = new CommandResult(Ralid.Park.BusinessModel.Result.ResultCode.Fail, ex.Message);
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.LocalDataBase.DAL.IDAL;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.GeneralLibrary.ExceptionHandling;


namespace Ralid.Park.LocalDataBase.DAL.LinqDataProvider
{
    public abstract class LDB_ProviderBase<TInfo, TID> : IProvider<TInfo, TID> where TInfo : class,new()
    {
        protected readonly string successMsg = "ok";

        public string ConnectStr { get; set; }

        public  LDB_ProviderBase(string connectStr)
        {
            this.ConnectStr = connectStr;
        }

        public LDB_ProviderBase()
        {
        }

        #region IProvider <TInfo> 成员
        
        public QueryResult<TInfo> GetByID(TID id)
        {

            QueryResult<TInfo> result;
            try
            {
                LDB_DataContext ldb = LDB_DataContextFactory.CreateLDB(ConnectStr);
                TInfo info = GetingItemByID(id, ldb);
                if (info != null)
                {
                    result = new QueryResult<TInfo>(ResultCode.Successful, successMsg, info);
                }
                else
                {
                    result = new QueryResult<TInfo>(ResultCode.Fail, string.Format("没有找到ID={0}的数据!", id.ToString()), info);
                }
            }
            catch (Exception ex)
            {
                result = new QueryResult<TInfo>(ResultCode.Fail, "从数据库获取数据时发生错误!", null);
                ExceptionPolicy.HandleException(ex, this.GetType().Name + "." + "GetByID()");
            }
            return result;
        }

        public QueryResultList<TInfo> GetAll()
        {
            QueryResultList<TInfo> result;
            try
            {
                LDB_DataContext ldb = LDB_DataContextFactory.CreateLDB(ConnectStr);
                List<TInfo> infoes = GetingAllItems(ldb);
                result = new QueryResultList<TInfo>(ResultCode.Successful, successMsg, infoes);
            }
            catch (Exception ex)
            {
                result = new QueryResultList<TInfo>(ResultCode.Fail, "从数据库获取数据时发生错误!", new List<TInfo>());
                ExceptionPolicy.HandleException(ex, this.GetType().Name + "." + "GetAll()");
            }
            return result;
        }

        public QueryResultList<TInfo> GetItems(SearchCondition search)
        {
            QueryResultList<TInfo> result;
            try
            {
                LDB_DataContext ldb = LDB_DataContextFactory.CreateLDB(ConnectStr);
                List<TInfo> infoes;
                if (search != null)
                {
                    infoes = GetingItems(ldb, search);
                }
                else
                {
                    infoes = GetingAllItems(ldb);
                }
                result = new QueryResultList<TInfo>(ResultCode.Successful, successMsg, infoes);
            }
            catch (Exception ex)
            {
                result = new QueryResultList<TInfo>(ResultCode.Fail, "从数据库获取数据时发生错误!", new List<TInfo>());
                ExceptionPolicy.HandleException(ex, this.GetType().Name + "." + "GetItems()");
            }
            return result;
        }

        public CommandResult Insert(TInfo info)
        {
            CommandResult result;
            try
            {
                LDB_DataContext ldb = LDB_DataContextFactory.CreateLDB(ConnectStr);
                InsertingItem(info, ldb);
                ldb.SubmitChanges();
                result = new CommandResult(ResultCode.Successful, successMsg);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    result = new CommandResult(ResultCode.Fail, "不能插入重复ID的数据!");
                }
                else
                {
                    result = new CommandResult(ResultCode.Fail, "数据在写入数据库时发生错误!");
                }
                ExceptionPolicy.HandleException(ex, this.GetType().Name + "." + "Insert()");
            }
            catch (Exception ex)
            {
                result = new CommandResult(ResultCode.Fail, "数据在写入数据库时发生错误!");
                ExceptionPolicy.HandleException(ex, this.GetType().Name + "." + "Insert()");
            }
            return result;
        }

        public CommandResult Update(TInfo newVal, TInfo original)
        {
            CommandResult result;
            try
            {
                LDB_DataContext ldb = LDB_DataContextFactory.CreateLDB(ConnectStr);
                UpdatingItem(newVal, original, ldb);
                ldb.SubmitChanges();
                result = new CommandResult(ResultCode.Successful, successMsg);
            }
            catch (Exception ex)
            {
                result = new CommandResult(ResultCode.Fail, "数据在写入数据库时发生错误!");
                ExceptionPolicy.HandleException(ex, this.GetType().FullName + "." + "Update()");
            }
            return result;
        }

        public CommandResult Delete(TInfo info)
        {
            CommandResult result;
            try
            {
                LDB_DataContext ldb = LDB_DataContextFactory.CreateLDB(ConnectStr);
                DeletingItem(info, ldb);
                ldb.SubmitChanges();
                result = new CommandResult(ResultCode.Successful, successMsg);
            }
            catch (Exception ex)
            {
                result = new CommandResult(ResultCode.Fail, "从数据库删除数据时发生错误!");
                ExceptionPolicy.HandleException(ex, this.GetType().FullName + "." + "Delete()");
            }
            return result;
        }

        public void Insert(TInfo info, IUnitWork unitWork)
        {
            if (unitWork == null)
            {
                throw new NullReferenceException("参数unitWork为空!");
            }
            LDB_UnitWork trans = unitWork as LDB_UnitWork;
            if (trans != null)
            {
                LDB_DataContext ldb = trans.LDB;
                InsertingItem(info, ldb);
            }
            else
            {
                throw new InvalidCastException("参数unitWork不能转换成类型Ralid.LinqDataProvider.UnitWork," +
                    "请检查参数是否是一个Ralid.LinqDataProvider.UnitWork实例!");
            }
        }

        public void Update(TInfo newVal, TInfo originalVal, IUnitWork unitWork)
        {
            if (unitWork == null)
            {
                throw new NullReferenceException("参数unitWork为空!");
            }
            LDB_UnitWork trans = unitWork as LDB_UnitWork;
            if (trans != null)
            {
                LDB_DataContext ldb = trans.LDB;
                UpdatingItem(newVal, originalVal, ldb);
            }
            else
            {
                throw new InvalidCastException("参数unitWork不能转换成类型Ralid.LinqDataProvider.UnitWork," +
                    "请检查参数是否是一个Ralid.LinqDataProvider.UnitWork实例!");
            }
        }

        public void Delete(TInfo info, IUnitWork unitWork)
        {
            if (unitWork == null)
            {
                throw new NullReferenceException("参数unitWork为空!");
            }
            LDB_UnitWork trans = unitWork as LDB_UnitWork;
            if (trans != null)
            {
                LDB_DataContext ldb = trans.LDB;
                DeletingItem(info, ldb);
            }
            else
            {
                throw new InvalidCastException("参数unitWork不能转换成类型Ralid.LinqDataProvider.UnitWork," +
                    "请检查参数是否是一个Ralid.LinqDataProvider.UnitWork实例!");
            }
        }
        #endregion

        #region 模板方法
        protected virtual TInfo GetingItemByID(TID id, LDB_DataContext ldb)
        {
            //每一个子类都要重写这个方法
            throw new Exception("子类没有重写GetingItemByID方法!");
        }
        protected virtual List<TInfo> GetingAllItems(LDB_DataContext ldb)
        {
            //如果实体类要加载其关联数据,就重写此方法
            return ldb.GetTable<TInfo>().Select(t => t).ToList();
        }
        protected virtual List<TInfo> GetingItems(LDB_DataContext ldb, SearchCondition search)
        {
            //如果要实现这个功能,子类一定要重写这个方法
            throw new NotImplementedException("子类没有重写GetingItems方法");
        }
        protected virtual void InsertingItem(TInfo info, LDB_DataContext ldb)
        {
            ldb.GetTable<TInfo>().InsertOnSubmit(info);
        }
        protected virtual void UpdatingItem(TInfo newVal, TInfo original, LDB_DataContext ldb)
        {
            //所有实体都可以用这个方法来更新数据
            ldb.GetTable<TInfo>().Attach(newVal, original);
        }
        protected virtual void DeletingItem(TInfo info, LDB_DataContext ldb)
        {
            //如果删除实体时要删除其关联数据,就得重写这个方法
            ldb.GetTable<TInfo>().Attach(info);
            ldb.GetTable<TInfo>().DeleteOnSubmit(info);
        }
        #endregion
    }
}

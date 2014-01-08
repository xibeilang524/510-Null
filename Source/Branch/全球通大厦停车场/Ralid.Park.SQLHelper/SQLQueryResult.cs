using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.SQLHelper
{
    /// <summary>
    /// 查询数据返回单个对象
    /// </summary>
    public class SQLQueryResult<T>
    {
        private SQLResultCode _result;
        private T _queryObject;


        public SQLQueryResult()
        {
        }

        public SQLQueryResult(SQLResultCode code, T obj)
        {
            this._result = code;
            this._msg = SQLResultCodeDecription.GetDescription(code);
            this._queryObject = obj;
        }

        public SQLQueryResult(SQLResultCode code, string msg, T obj)
        {
            this._result = code;
            this._msg = msg;
            this._queryObject = obj;
        }

        #region ISQLQueryResult<T> 成员

        public SQLResultCode Result
        {
            get
            {
                return this._result;
            }

            set
            {
                this._result = value;
            }
        }

        public T QueryObject
        {
            get
            {
                return this._queryObject;
            }
            set
            {
                this._queryObject = value;
            }
        }

        private string _msg;
        public string Message
        {
            get { return _msg; }
            set
            {
                _msg = value;
            }
        }

        #endregion
    }
}

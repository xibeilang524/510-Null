using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.BusinessModel.Result
{
    /// <summary>
    /// 查询数据返回单个对象
    /// </summary>
    [DataContract]
    public class QueryResult<T>
    {
        [DataMember]
        private ResultCode _result;
        [DataMember]
        private T _queryObject;


        public QueryResult()
        {
        }

        public QueryResult(ResultCode code, T obj)
        {
            this._result = code;
            this._msg = ResultCodeDecription.GetDescription(code);
            this._queryObject = obj;
        }

        public QueryResult(ResultCode code, string msg, T obj)
        {
            this._result = code;
            this._msg = msg;
            this._queryObject = obj;
        }

        #region IQueryResult<T> 成员
        [DataMember]
        public ResultCode Result
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
        [DataMember]
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
        [DataMember]
        private string _msg;
        [DataMember]
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

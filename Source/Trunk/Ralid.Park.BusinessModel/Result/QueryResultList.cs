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
    /// 查询数据返回结果集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable()]
    [DataContract]
    public class QueryResultList<T>
    {
        public QueryResultList()
        {
        }

        public QueryResultList(ResultCode code, List<T> list)
        {
            this._result = code;
            this._msg = ResultCodeDecription.GetDescription(code);
            this.QueryObjects = list;
        }

        public QueryResultList(ResultCode code, string msg, List<T> list)
        {
            this._result = code;
            this._msg = msg;
            this._queryList = list;
        }

        #region IQueryResultList<T> 成员

        [DataMember]
        private ResultCode _result;
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
        private List<T> _queryList;
        [DataMember]
        public List<T> QueryObjects
        {
            get
            {
                return this._queryList;
            }
            set
            {
                this._queryList = value;
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

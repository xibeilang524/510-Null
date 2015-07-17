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
    /// 命令操作结果类
    /// </summary>
    [DataContract]
    public class CommandResult
    {
        public CommandResult()
        {
        }

        public CommandResult(ResultCode code)
        {
            this._ret = code;
            this._msg = ResultCodeDecription.GetDescription(code);
        }

        public CommandResult(ResultCode code, string msg)
        {
            this._ret = code;
            this._msg = msg;
        }

        [DataMember]
        private ResultCode _ret;
        [DataMember]
        public ResultCode Result
        {
            get
            {
                return this._ret;
            }
            set
            {
                this._ret = value;
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
    }
}

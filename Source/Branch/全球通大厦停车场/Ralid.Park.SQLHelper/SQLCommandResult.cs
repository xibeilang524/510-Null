using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.SQLHelper
{
    public class SQLCommandResult
    {

        public SQLCommandResult()
        {
        }

        public SQLCommandResult(SQLResultCode code)
        {
            this._ret = code;
            this._msg = SQLResultCodeDecription.GetDescription(code);
        }

        public SQLCommandResult(SQLResultCode code, string msg)
        {
            this._ret = code;
            this._msg = msg;
        }

        private SQLResultCode _ret;
        public SQLResultCode Result
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

        private string _msg;
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

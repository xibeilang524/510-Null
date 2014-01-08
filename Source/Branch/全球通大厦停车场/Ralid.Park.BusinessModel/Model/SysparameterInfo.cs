using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    public class SysparameterInfo
    {
        #region 公共属性
        public string Parameter { get; set; }
        public string ParameterValue { get; set; }
        public string Description { get; set; }
        #endregion

        public SysparameterInfo(string para, string paraValue, string desc)
        {
            this.Parameter = para;
            this.ParameterValue = paraValue;
            this.Description = desc;
        }

        public SysparameterInfo()
        {
        }
    }
}

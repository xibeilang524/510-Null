using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ralid.Park.LocalDataBase.Model
{
    /// <summary>
    /// 系统参数设置表
    /// </summary>
    [Table(Name = "Sysparameter")]
    public class LDB_SysparameterInfo
    {
        #region 公共属性
        [Column(IsPrimaryKey = true, Name = "Parameter")]
        public string Parameter { get; set; }
        [Column(Name = "ParameterValue")]
        public string ParameterValue { get; set; }
        [Column(Name = "Description")]
        public string Description { get; set; }
        #endregion

        public LDB_SysparameterInfo(string para, string paraValue, string desc)
        {
            this.Parameter = para;
            this.ParameterValue = paraValue;
            this.Description = desc;
        }

        public LDB_SysparameterInfo()
        {
        }
    }
}

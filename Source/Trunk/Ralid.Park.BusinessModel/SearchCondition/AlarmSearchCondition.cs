using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class AlarmSearchCondition:RecordSearchCondition
    {
        public AlarmType? AlarmType { get; set; }        //报警类型
        public string AlarmSource { get; set; }      //报警来源
    }
}

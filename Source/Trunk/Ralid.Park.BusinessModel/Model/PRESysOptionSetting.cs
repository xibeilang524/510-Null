using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    [DataContract]
    public class PRESysOptionSetting
    {
        public static PRESysOptionSetting Current { get; set; }

        [DataMember]
        private PRESysOption _PRESysOption;

        public PRESysOptionSetting()
        {
            _PRESysOption = new PRESysOption();
        }

        public PRESysOption PRESysOption
        {
            get
            {
                return _PRESysOption;
            }
            set
            {
                _PRESysOption = value;
            }
        }

    }
}

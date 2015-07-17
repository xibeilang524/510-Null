using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    [DataContract]
    public class PREWorkstationSetting
    {
        public static PREWorkstationSetting Current { get; set; }

        [DataMember]
        private Dictionary<Guid,PREWorkstation> _WorkstationDictionary;

        public PREWorkstationSetting()
        {
            _WorkstationDictionary = new Dictionary<Guid,PREWorkstation>();
        }

        public Dictionary<Guid, PREWorkstation> WorkstationDictionary
        {
            get
            {
                return _WorkstationDictionary;
            }
            set
            {
                _WorkstationDictionary = value;
            }
        }
    }
}

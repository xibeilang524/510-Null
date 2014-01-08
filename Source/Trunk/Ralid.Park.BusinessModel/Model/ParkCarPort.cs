using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    public class ParkCarPort
    {
        #region 公共属性
        public int ID { get; set; }

        public int ParkID { get; set; }

        public byte? CardType { get; set; }

        public byte? CarType { get; set; }

        public short CarPort { get; set; }

        public short Vacant { get; set; }
        #endregion

        public ParkCarPort Clone()
        {
            return this.MemberwiseClone() as ParkCarPort;
        }
    }
}

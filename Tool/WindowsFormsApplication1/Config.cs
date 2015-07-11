using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization ;

namespace WindowsFormsApplication1
{
    public class Config
    {
        public string ComputeName { get; set; }
        public string StartTime { get; set; }
        public string ExpireTime { get; set; }
        public string Lic { get; set; }
        public string StdTime { get; set; }
    }
}

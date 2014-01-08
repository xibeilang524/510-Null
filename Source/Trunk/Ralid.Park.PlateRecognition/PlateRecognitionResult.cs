using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.PlateRecognition
{
    /// <summary>
    /// 车牌识别结果
    /// </summary>
    [DataContract]
    public class PlateRecognitionResult
    {
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        [DataMember]
        public string CarPlate { get; set; }

        /// <summary>
        /// 获取或设置车牌颜色说明,如蓝底白字等
        /// </summary>
        [DataMember]
        public string Color{ get; set; }
    }
}

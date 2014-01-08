using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 文通车牌识别参数
    /// </summary>
    [DataContract ]
    public class WintoneCarPlateSetting
    {
        #region 静态属性
        public static WintoneCarPlateSetting Current { get; set; }
        #endregion

        public WintoneCarPlateSetting()
        {
        }
        #region 构造函数

        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置车牌最小宽度
        /// </summary>
        [DataMember ]
        public int MinPlateWidth { get; set; }
        /// <summary>
        /// 获取或设置车牌最大宽度
        /// </summary>
        [DataMember]
        public int MaxPlateWidth { get; set; }
        /// <summary>
        /// 获取或设置默认省份的简写,比如"粤,湘"
        /// </summary>
        [DataMember]
        public string DefaultProvince { get; set; }
        /// <summary>
        /// 获取或设置识别结果中最多输出车牌个数,停车场一般选1
        /// </summary>
        [DataMember]
        public int PlatesNum { get; set; }
        /// <summary>
        /// 车牌定位阀值
        /// </summary>
        [DataMember]
        public int ImagePlateThr { get; set; }
        /// <summary>
        /// 车牌识别阀值
        /// </summary>
        [DataMember ]
        public int ImageRecoThr { get; set; }
        /// <summary>
        /// 获取或设置识别运动或静止图像，为假表示识别静止图像
        /// </summary>
        [DataMember]
        public bool ForMovingImage { get; set; }
        /// <summary>
        /// 获取或设置是否识别双层黄牌
        /// </summary>
        [DataMember]
        public bool EnableYellow2 { get; set; }
        /// <summary>
        /// 获取或设置是否识别个性化车牌
        /// </summary>
        [DataMember]
        public bool EnableCustomize { get; set; }
        /// <summary>
        /// 获取或设置是否识别军牌
        /// </summary>
        [DataMember]
        public bool EnableArmPol { get; set; }
        /// <summary>
        /// 获取或设置是否识别双层军牌
        /// </summary>
        [DataMember]
        public bool EnableAarm2 { get; set; }
        /// <summary>
        /// 获取或设置是否识别农用车牌
        /// </summary>
        [DataMember]
        public bool EnableTractor { get; set; }
        /// <summary>
        /// 获取或设置是否识别使馆车牌
        /// </summary>
        [DataMember]
        public bool EnableEmbassy { get; set; }
        /// <summary>
        /// 获取或设置是否识别双层武警车牌
        /// </summary>
        [DataMember]
        public bool EnablePolice2 { get; set; }
        /// <summary>
        /// 获取或设置是否是夜间模式
        /// </summary>
        [DataMember]
        public bool IsNightMode { get; set; }
        #endregion
    }
}

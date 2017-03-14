using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    public class ETCPaymentRecord
    {
        #region 构造函数
        public ETCPaymentRecord()
        {
        }
        #endregion

        #region 公共属性
        public int ID { get; set; }
        /// <summary>
        ///  获取或设置记录产生的车道号
        /// </summary>
        public string LaneNo { get; set; }
        /// <summary>
        ///  获取或设置记录产生的设备，0表示天线，1表示ETC读卡器，2表示第三方读卡器
        /// </summary>
        public int Device { get; set; }
        /// <summary>
        /// 获取或设置记录产生的时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 获取或设置记录的数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 获取或设置记录上传的时间
        /// </summary>
        public DateTime? UploadTime { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public ETCPaymentRecord Clone()
        {
            return this.MemberwiseClone() as ETCPaymentRecord;
        }
        #endregion
    }
}

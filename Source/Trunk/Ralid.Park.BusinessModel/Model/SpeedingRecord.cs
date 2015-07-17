using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 未处理超速记录实体类
    /// </summary>
    public class SpeedingRecord
    {
        /// <summary>
        /// 记录编号
        /// </summary>
        public Guid SpeedingID { get; set; }
        /// <summary>
        /// 超速发生时间
        /// </summary>
        public DateTime SpeedingDateTime { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// 地点
        /// </summary>
        public string Place { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public string Speed { get; set; }
        /// <summary>
        /// 超速所拍照片的照片地址
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Memo { get; set; }

    }
}

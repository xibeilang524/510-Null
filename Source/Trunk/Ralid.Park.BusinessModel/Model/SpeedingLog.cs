using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 已处理超速记录实体类
    /// </summary>
    public class SpeedingLog
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
        /// <summary>
        /// 处理人员ID
        /// </summary>
        public string DealOperatorID { get; set; }
        /// <summary>
        /// 处理人员
        /// </summary>
        public OperatorInfo DealOperator { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? DealDateTime { get; set; }
        /// <summary>
        /// 处理备注信息
        /// </summary>
        public string DealMemo { get; set; }
    }
}

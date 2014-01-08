using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class SnapShotSearchCondition:SearchCondition 
    {
        /// <summary>
        /// 获取或设置抓拍时间
        /// </summary>
        public DateTime? ShotDateTime { get; set; }
        /// <summary>
        /// 获取或设置抓拍时的车牌号码
        /// </summary>
        public string CardID { get; set; }
    }
}

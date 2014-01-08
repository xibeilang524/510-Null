using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park .BusinessModel.Enum;
using Ralid.GeneralLibrary.Printer;

namespace Ralid.Park.BusinessModel.Model
{
    //add by Jan 2012-3-18
    /// <summary>
    /// 优惠信息记录
    /// </summary>
    [Serializable]
    [DataContract]
    public class DiscountRecordInfo
    {
        #region 构造函数
        public DiscountRecordInfo()
        {
            //this.shopInfo = new ShopInfo();
            this.ShopName = "";
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 记录ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// 商家编码
        /// </summary>
        [DataMember]
        public string ShopID { get; set; }

        /// <summary>
        /// 优惠写入时间
        /// </summary>
        [DataMember]
        public DateTime DiscountDateTime { get; set; }

        /// <summary>
        /// 优惠时数
        /// </summary>
        [DataMember]
        public int DiscountHour { get; set; }

        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置操作员编号
        /// </summary>
        [DataMember]
        public string OperatorID { get; set; }

        /// <summary>
        /// 获取或设置工作站
        /// </summary>
        [DataMember]
        public string StationID { get; set; }

        /// <summary>
        /// 优惠结算标识
        /// </summary>
        [DataMember]
        public bool Pay { get; set; }

        /// <summary>
        /// 获取商店名称
        /// </summary>
        [DataMember]
        public string ShopName { get; set; }


        #endregion
    }
    //end 
}

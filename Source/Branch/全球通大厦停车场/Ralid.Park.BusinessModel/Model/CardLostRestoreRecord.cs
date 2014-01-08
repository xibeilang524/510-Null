using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Model
{
    public class CardLostRestoreRecord
    {
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置持卡人姓名
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        public string CarPlate { get; set; }
        /// <summary>
        /// 获取或设置卡片序列号
        /// </summary>
        public string CardCertificate { get; set; }
        /// <summary>
        /// 获取或设置挂失操作员
        /// </summary>
        public string LostOperator { get; set; }
        /// <summary>
        /// 获取或设置挂失时间
        /// </summary>
        public DateTime LostDateTime { get; set; }
        /// <summary>
        /// 获取或设置挂失工作站
        /// </summary>
        public string LostStation { get; set; }
        /// <summary>
        /// 获取或设置挂失备注
        /// </summary>
        public string LostMemo { get; set; }
        /// <summary>
        /// 获取或设置卡片挂失前状态
        /// </summary>
        public byte CardStatus { get; set; }
        /// <summary>
        /// 获取或设置收费方式
        /// </summary>
        public PaymentMode? PaymentMode { get; set; }
        /// <summary>
        /// 获取或设置遗失工本费
        /// </summary>
        public decimal? LostCardCost { get; set; }
        /// <summary>
        /// 获取或设置结算时间
        /// </summary>
        public DateTime? SettleDateTime { get; set; }
        /// <summary>
        /// 获取或设置卡片恢复操作员
        /// </summary>
        public string RestoreOperator { get; set; }
        /// <summary>
        /// 获取或设置卡片恢复时间
        /// </summary>
        public DateTime? RestoreDateTime { get; set; }
        /// <summary>
        /// 获取或设置卡片恢复工作站
        /// </summary>
        public string RestoreStation { get; set; }
        /// <summary>
        /// 获取或设置卡片恢复备注
        /// </summary>
        public string RestoreMemo { get; set; }

        public CardLostRestoreRecord Clone()
        {
            return this.MemberwiseClone() as CardLostRestoreRecord;
        }
    }
}

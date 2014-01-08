using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    public class CardDisableEnableRecord
    {
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置持卡人姓名
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        public string CarPlate { get; set; }
        public string CardCertificate { get; set; }
        public string DisableOperator { get; set; }
        public DateTime  DisableDateTime { get; set; }
        public string DisableStationID { get; set; }
        public string DisableMemo { get; set; }
        public string EnableOperator { get; set; }
        public DateTime? EnableDateTime { get; set; }
        public string EnableStationID { get; set; }
        public string EnableMemo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.LR280
{
    public class LR280Setting
    {
        #region 构造函数
        public LR280Setting()
        {
            Items = new List<LR280Item>();
        }
        #endregion

        public static readonly string CardTyte = "银联闪付卡";

        #region 公共属性
        /// 获取或设置服务器管理的所有POS机
        /// </summary>
        [DataMember]
        public List<LR280Item> Items { get; set; }
        #endregion
    }

    /// <summary>
    /// 表示羊城通读头
    /// </summary>
    [DataContract]
    public class LR280Item
    {
        /// <summary>
        /// 获取或设置串口
        /// </summary>
        [DataMember]
        public byte Comport { get; set; }

        [DataMember]
        public int Baud { get; set; }
        /// <summary>
        /// 获取或设置通道ID，如果没有指定的话为0
        /// </summary>
        [DataMember]
        public int? EntranceID { get; set; }
        /// <summary>
        /// 获取或设置读卡器
        /// </summary>
        public LR280POS Reader { get; set; }
        /// <summary>
        /// 获取或设置说明信息
        /// </summary>
        [DataMember]
        public string Memo { get; set; }
    }
}

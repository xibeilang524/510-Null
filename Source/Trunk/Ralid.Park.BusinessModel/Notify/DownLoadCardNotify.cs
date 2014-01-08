using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Interface;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 下载卡片通知
    /// </summary>
    [DataContract]
    public class DownLoadCardNotify
    {
        /// <summary>
        /// 获取或设置准备下载记录起始序号
        /// </summary>
        [DataMember]
        public int StartIndex { get; set; }

        /// <summary>
        /// 获取或设置下载卡片的数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }



        public DownLoadCardNotify(int index, int count)
        {
            this.StartIndex = index;
            this.Count = count;
        }

        public DownLoadCardNotify()
        {
        }
    }
}

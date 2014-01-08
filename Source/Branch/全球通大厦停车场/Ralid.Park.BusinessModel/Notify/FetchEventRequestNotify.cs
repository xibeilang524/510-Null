using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Interface;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 获取控制器事件通知
    /// </summary>
   [DataContract]
    public class FetchEventRequestNotify
    {
        /// <summary>
        /// 起始序号
        /// </summary>
        private int _startIndex;
        [DataMember]
        public int StartIndex
        {
            get { return _startIndex; }
            set { _startIndex = value; }
        }

        /// <summary>
        /// 提取数量
        /// </summary>
        private int _count;
        [DataMember]
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }


        public FetchEventRequestNotify(int _index, int _count)
        {
            this._startIndex = _index;
            this._count = _count;
        }

         public FetchEventRequestNotify()
        {
        }
    }
}

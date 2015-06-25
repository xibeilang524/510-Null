using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ralid.OpenCard.OpenCardService
{
    public class YiTingBuffer
    {
        #region 构造函数
        public YiTingBuffer() { }
        #endregion

        #region 私有变量
        private List<byte> _Buffer = new List<byte>();
        private object _BufferLocker = new object();
        #endregion

        #region 公共方法
        public void Write(IEnumerable<byte> data)
        {
            lock (_BufferLocker)
            {
                _Buffer.AddRange(data);
            }
        }

        public YiTingPacket Read()
        {
            return null;
        }
        #endregion
    }
}

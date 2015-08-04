using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    public class YCTBuffer
    {
        #region 构造函数
        public YCTBuffer()
        {
        }
        #endregion

        #region 私有变量
        private List<byte> _Buffer = new List<byte>();
        private object _BufferLocker = new object();

        private byte _Header = 0xBD;
        #endregion

        #region 公共方法
        /// <summary>
        /// 将数据写入缓存中
        /// </summary>
        /// <param name="data"></param>
        public void Write(IEnumerable<byte> data)
        {
            lock (_BufferLocker)
            {
                _Buffer.AddRange(data);
            }
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            lock (_BufferLocker)
            {
                _Buffer.Clear();
            }
        }
        /// <summary>
        /// 从缓存中读取一个包
        /// </summary>
        /// <returns></returns>
        public YCTPacket Read()
        {
            try
            {
                lock (_BufferLocker)
                {
                    for (int i = 0; i < _Buffer.Count - 1; i++) //减1表示找到头后,紧跟的一个字节是数据长度
                    {
                        if (_Buffer[i] == _Header)
                        {
                            int dlen = _Buffer[i + 1];
                            if (_Buffer.Count >= i + 2 + dlen)
                            {
                                byte[] packet = new byte[2 + dlen];
                                _Buffer.CopyTo(i, packet, 0, packet.Length);
                                _Buffer.RemoveRange(0, i + 2 + dlen);
                                return new YCTPacket(packet);
                            }
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }
        #endregion
    }
}

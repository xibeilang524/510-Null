using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ralid.GeneralLibrary;

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

        //包的结构 头(2byte) + 命令(2byte) + 通讯方向(1byte) + 位置(1byte) + 校验和(1byte) + 数据长度(2byte) + 数据(nbyte) + 尾(2byte)
        private byte[] _Header = new byte[] { 0x78, 0xB6 };
        private byte[] _Tail = new byte[] { 0x21, 0xD3 };
        private int _PLengthBeforeData = 9; //
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
            try
            {
                lock (_BufferLocker)
                {
                    int end = FirstOf(_Buffer, _Tail);
                    if (end < 0) return null;
                    List<byte> temp = _Buffer.Take(end + _Tail.Length).ToList();
                    _Buffer.RemoveRange(0, temp.Count);
                    int begin = LastOf(temp, _Header);
                    if (begin < 0) return null;
                    temp.RemoveRange(0, begin);
                    if (temp.Count < _PLengthBeforeData + _Tail.Length) return null;
                    return new YiTingPacket(temp.ToArray());
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        private int FirstOf(List<byte> source, byte[] data)
        {
            int ret = -1;
            for (int i = 0; i < source.Count - data.Length + 1; i++)
            {
                bool ok = true;
                for (int j = 0; j < data.Length; j++)
                {
                    if (source[i + j] != data[j])
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok) return i;
            }
            return ret;
        }

        private int LastOf(List<byte> source, byte[] data)
        {
            int ret = -1;
            for (int i = 0; i < source.Count - data.Length + 1; i++)
            {
                bool ok = true;
                for (int j = 0; j < data.Length; j++)
                {
                    if (source[i + j] != data[j])
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok) ret = i;
            }
            return ret;
        }
        #endregion
    }
}

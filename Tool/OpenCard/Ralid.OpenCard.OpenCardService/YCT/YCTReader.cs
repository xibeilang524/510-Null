using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    public class YCTReader
    {
        #region 构造函数
        public YCTReader(byte comport, int baud)
        {
            _Port = new CommPort(comport, baud);
        }
        #endregion

        #region 私有变量
        private CommPort _Port;
        private YCTBuffer _buffer = new YCTBuffer();
        private System.Threading.AutoResetEvent _Responsed = new System.Threading.AutoResetEvent(false);
        private YCTPacket _Response = null;
        #endregion

        #region 私有方法
        private void _Port_OnDataArrivedEvent(object sender, byte[] data)
        {
            _buffer.Write(data);
            var p = _buffer.Read();
            if (p != null)
            {
                _Response = p;
                _Responsed.Set();
            }
        }

        private byte[] CreateRequest(YCTCommandType cmd, byte[] data)
        {
            ////包结构 头(1byte) + 包长(2byte) + Command(1byte) + data(nbyte) + checksum32(4byte)
            List<byte> ret = new List<byte>();
            ret.Add(0xFC); //头
            byte[] temp = BEBinaryConverter.ShortToBytes((short)(5 + (data != null ? data.Length : 0))); //命令+数据+crc的长度
            ret.AddRange(temp);
            ret.Add((byte)cmd);
            if (data != null) ret.AddRange(data);
            ret.AddRange(CRC32Helper.CRC32(ret));
            return ret.ToArray();
        }
        #endregion

        #region 公共方法(串口管理)
        /// <summary>
        /// 打开
        /// </summary>
        public void Open()
        {
            _Port.Open();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            _Port.Close();
        }
        /// <summary>
        /// 获取读卡器是否已经打开
        /// </summary>
        public bool IsOpened { get { return _Port.PortOpened; } }
        #endregion

        #region 公共方法
        /// <summary>
        /// 向读卡器请求命令，并取得返回包
        /// </summary>
        /// <param name="cmd">请求的命令</param>
        /// <param name="data">请求中包含的数据</param>
        /// <returns></returns>
        public YCTPacket Request(YCTCommandType cmd, byte[] data)
        {
            byte[] request = CreateRequest(cmd, data);
            _Port.OnDataArrivedEvent -= _Port_OnDataArrivedEvent;
            _buffer.Clear();
            _Responsed.Reset();
            _Response = null;
            _Port.OnDataArrivedEvent += _Port_OnDataArrivedEvent;
            _Port.SendData(request);
            if (_Responsed.WaitOne(2000))
            {
                if (_Response != null && _Response.Command == (byte)cmd) return _Response;
            }
            return null;
        }
        /// <summary>
        /// 设置商家编号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool SetServiceCode(int code)
        {
            code = code % 10000; //取数值的低四位
            byte[] bcd = new byte[] { BCDConverter.IntToBCD(code / 100), BCDConverter.IntToBCD(code % 100) };
            var response = Request(YCTCommandType.SetServiceCode, bcd);
            return (response != null && response.IsCommandExcuteOk);
        }
        /// <summary>
        /// 初始化消费模式
        /// </summary>
        /// <returns></returns>
        public bool InitPaidMode()
        {
            var response = Request(YCTCommandType.InitPaidPara, new byte[] { 0x80 }); //0x80表示正常消费
            return (response != null && response.IsCommandExcuteOk);
        }
        /// <summary>
        /// 询卡
        /// </summary>
        /// <returns></returns>
        public YCTWallet Poll()
        {
            var response = Request(YCTCommandType.Poll, null);
            if (response != null && response.IsCommandExcuteOk)
            {

            }
            return null;
        }
        #endregion
    }
}

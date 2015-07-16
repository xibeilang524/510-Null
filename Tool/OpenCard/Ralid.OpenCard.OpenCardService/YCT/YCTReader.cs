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

        private YCTPacket Send(byte cmd, byte[] data)
        {
            _Port.OnDataArrivedEvent -= _Port_OnDataArrivedEvent;
            _buffer.Clear();
            _Responsed.Reset();
            _Response = null;
            _Port.OnDataArrivedEvent += _Port_OnDataArrivedEvent;
            _Port.SendData(data);
            if (_Responsed.WaitOne(3000))
            {
                if (_Response != null && _Response.Command == cmd) return _Response;
            }
            return null;
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
        
        #endregion
    }
}

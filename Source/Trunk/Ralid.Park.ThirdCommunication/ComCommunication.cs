using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary;

namespace Ralid.Park.ThirdCommunication
{
    public class ComCommunication:ICommunication
    {
        #region 构造函数
        public ComCommunication(byte comport, int baudRate)
        {
            _Commport = new CommPort(comport, baudRate);
        }
        #endregion

        #region 私有成员
        private CommPort _Commport;
        #endregion

        #region ICommunication 接口实现
        public bool IsConnected()
        {
            return _Commport.PortOpened;
        }

        public void Connect()
        {
            _Commport.Open();
        }

        public void Close()
        {
            _Commport.Close();
        }

        public void SendMessage(byte[] msg)
        {
            _Commport.SendData(msg);
        }
        #endregion

    }
}

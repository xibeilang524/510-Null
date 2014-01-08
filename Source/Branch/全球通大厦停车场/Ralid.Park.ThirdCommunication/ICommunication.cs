using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.ThirdCommunication
{
    /// <summary>
    /// 表示通讯的接口
    /// </summary>
    public interface ICommunication
    {
        bool IsConnected();

        void Connect();

        void Close();

        void SendMessage(byte[] msg);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Ralid.Park.ThirdCommunication
{
    /// <summary>
    /// 表示网络通讯的类同
    /// </summary>
    public class NetCommunication:ICommunication
    {
        #region 构造函数
        public NetCommunication(string ip, int port)
        {
            _RemoteIP = ip;
            _RemotePort = port;
        }
        #endregion

        #region 私有变量
        TcpClient _Client;
        string _RemoteIP;
        int _RemotePort;
        #endregion

        #region 私有方法
        private void Recieve_Thread()
        {
            
        }
        #endregion

        #region ICommunication接口
        public bool IsConnected()
        {
            return _Client.Connected;
        }

        public void Connect()
        {
            try
            {
                string[] strs = _RemoteIP.Split('.');
                System.Diagnostics.Debug.Assert(strs.Length == 4);

                byte[] ipBytes = new byte[4];
                for (int i = 0; i < strs.Length; i++)
                {
                    ipBytes[i] = Convert.ToByte(strs[i]);
                }
                IPEndPoint iep = new IPEndPoint(new IPAddress(ipBytes), _RemotePort );
                _Client = new TcpClient();
                _Client.ReceiveBufferSize = 1024 * 1024;
                _Client.SendBufferSize = 1024 * 1024;
                _Client.SendTimeout = 5;
                _Client.Connect(iep);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        public void Close()
        {
            _Client.Close();
        }

        public void SendMessage(byte[] msg)
        {
            try
            {
                _Client.GetStream().Write(msg, 0, msg.Length);
            }
            catch 
            {

            }
        }
        #endregion
    }
}

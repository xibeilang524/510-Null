using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Ralid.GeneralLibrary;

namespace Ralid.OpenCard.OpenCardService
{
    public class LJHSocket
    {
        #region 构造函数
        public LJHSocket()
        {
        }

        public LJHSocket(string ip, int port)
        {
            this.IP = ip;
            this.Port = port;
        }
        #endregion

        #region 私有变量
        private Socket _Client = null;
        private Thread _ReadDataTread = null;
        #endregion

        #region 私有方法
        private void ReadDataTask()
        {
            byte[] buffer = new byte[1024];
            try
            {
                while (true)
                {
                    int count = _Client.Receive(buffer);
                    if (count > 0)
                    {
                        byte[] data = new byte[count];
                        Array.Copy(buffer, 0, data, 0, count); //将每次收到的数据放到
                        if (this.OnDataArrivedEvent != null)
                        {
                            this.OnDataArrivedEvent(this, data);
                        }
                    }
                    Thread.Sleep(100);
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 公共属性
        public string IP { get; set; }

        public int Port { get; set; }

        public bool IsConnected
        {
            get { return _Client != null && _Client.Connected; }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 数据到达事件
        /// </summary>
        public event DataArrivedDelegate OnDataArrivedEvent;
        #endregion

        #region 公开方法
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public void Open()
        {
            try
            {
                Close(); //打开之前先关闭之前的连接,如果有的话
                if (!string.IsNullOrEmpty(IP) && Port > 0)
                {
                    string[] strs = IP.Split('.');
                    byte[] ipBytes = new byte[4];
                    for (int i = 0; i < strs.Length; i++)
                    {
                        ipBytes[i] = Convert.ToByte(strs[i]);
                    }
                    IPEndPoint iep = new IPEndPoint(new IPAddress(ipBytes), Port);
                    _Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    _Client.Connect(iep);
                    _ReadDataTread = new Thread(new ThreadStart(ReadDataTask));
                    _ReadDataTread.Start();
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        public void SendData(byte[] data)
        {
            try
            {
                if (IsConnected)
                {
                    _Client.Send(data);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            try
            {
                if (IsConnected) _Client.Close();
                if (_ReadDataTread != null)
                {
                    _ReadDataTread.Abort();
                    _ReadDataTread = null;
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion 公开方法
    }
}

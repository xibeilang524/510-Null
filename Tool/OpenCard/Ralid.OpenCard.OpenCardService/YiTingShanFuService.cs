using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.OpenCard.OpenCardService
{
    public class YiTingShanFuService : IOpenCardService
    {
        #region 构造函数
        public YiTingShanFuService()
        {

        }

        public YiTingShanFuService(YiTingShanFuSetting setting)
        {
            Setting = setting;
        }
        #endregion

        #region 私有变量
        private TcpListener _Listener = null;
        private Dictionary<LJHSocket, YiTingBuffer> _Buffers = new Dictionary<LJHSocket, YiTingBuffer>();
        private object _BuffersLocker = new object();
        private Thread _ListenThread = null;
        private byte _OK = 0x59;
        #endregion

        #region 私有方法
        private void TCPListen()
        {
            try
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(Setting.IP), Setting.Port);
                _Listener = new TcpListener(iep);
                _Listener.Start();
                while (true)
                {
                    Socket socket = _Listener.AcceptSocket();
                    LJHSocket s = new LJHSocket(socket);
                    s.OnDataArrivedEvent += socket_OnDataArrivedEvent;
                    s.OnClosed += new EventHandler(s_OnClosed);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void s_OnClosed(object sender, EventArgs e)
        {
            lock (_BuffersLocker)
            {
                _Buffers.Remove(sender as LJHSocket);
            }
        }

        private void socket_OnDataArrivedEvent(object sender, byte[] data)
        {
            lock (_BuffersLocker)
            {
                if (!_Buffers.ContainsKey(sender as LJHSocket)) _Buffers[sender as LJHSocket] = new YiTingBuffer();
                _Buffers[sender as LJHSocket].Write(data);
            }
            ExtraData(sender as LJHSocket, _Buffers[sender as LJHSocket]);
        }

        private void ExtraData(LJHSocket socket, YiTingBuffer buffer)
        {
            try
            {
                YiTingPacket packet = buffer.Read();
                while (packet != null)
                {
                    HandlePacket(socket, packet);
                    packet = buffer.Read();
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void HandlePacket(LJHSocket socket, YiTingPacket packet)
        {
            if (!packet.IsValid) return;
            Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", "收到数据 " + Ralid.GeneralLibrary.HexStringConverter.HexToString(packet.ToBytes(), " "));
            if (packet.IsHearbeat) //心跳包
            {
                HandleHeartBeat(socket, packet);
            }
            else if (packet.IsEnterRead)
            {
                HandleEnterRead(socket, packet);
            }
            else if (packet.IsPayingRequest)
            {
                HandlePayingRequst(socket, packet);
            }
            else if (packet.IsPayingState)
            {
                HandlePayingState(socket, packet);
            }
        }

        private void HandleHeartBeat(LJHSocket socket, YiTingPacket packet)
        {
            List<byte> d = new List<byte>();
            d.AddRange(packet.Data);
            d.Add(_OK); //正常处理
            YiTingPacket response = packet.CreateResponse(d.ToArray());
            byte[] r = response.ToBytes();
            socket.SendData(r);
            Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", "发送数据 " + Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
        }

        private void HandleEnterRead(LJHSocket socket, YiTingPacket packet)
        {
            if (Setting == null) return;
            byte[] data = packet.Data;
            if (data == null || data.Length < 26) return;
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = YiTingPacket.GetCardID(data.Take(19).ToArray()),
                CardType = data[19] == 0x01 ? "闪付卡" : "临时IC卡",
            };
            string device = YiTingPacket.ConvertToAsc(new byte[] { data[20], data[21], data[22], data[23], data[24], data[25] });
            YiTingPOS pos = Setting.GetReader(device);
            if (pos != null) args.EntranceID = pos.EntranceID;
            if (this.OnReadCard != null) this.OnReadCard(this, args);
            List<byte> temp = new List<byte>();
            temp.AddRange(data);
            byte[] carPlate = UnicodeEncoding.Unicode.GetBytes("粤A24M55");
            byte[] t = new byte[27];
            Array.Copy(carPlate, t, carPlate.Length);
            temp.AddRange(t);
            temp.AddRange(new byte[2]);
            temp.AddRange(YiTingPacket.GetDateBytes(DateTime.Now));
            temp.Add(_OK);
            YiTingPacket response = packet.CreateResponse(temp.ToArray());
            byte[] r = response.ToBytes();
            socket.SendData(r);
            Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", "发送数据 " + Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
        }

        private void HandlePayingRequst(LJHSocket socket, YiTingPacket packet)
        {
            if (Setting == null) return;
            byte[] data = packet.Data;
            if (data == null || data.Length < 26) return;
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = YiTingPacket.GetCardID(data.Take(19).ToArray()),
            };
            string device = YiTingPacket.ConvertToAsc(new byte[] { data[20], data[21], data[22], data[23], data[24], data[25] });
            YiTingPOS pos = Setting.GetReader(device);
            if (pos != null) args.EntranceID = pos.EntranceID;
            if (this.OnPaying != null) this.OnPaying(this, args);

            if (args.Payment != null)
            {
                if (args.Payment.Accounts == 0)
                {
                    OpenCardEventArgs args1 = new OpenCardEventArgs()
                    {
                        CardID = args.CardID,
                        Paid = 0,
                        PaymentCode = Park.BusinessModel.Enum.PaymentCode.Computer,
                        PaymentMode = Park.BusinessModel.Enum.PaymentMode.Pos,
                    };
                    if (this.OnPaidOk != null) this.OnPaidOk(this, args);
                }
                List<byte> temp = new List<byte>();
                temp.AddRange(data.Take(26)); //取包的前26字节
                temp.AddRange(new byte[5]); //车位号
                temp.AddRange(YiTingPacket.GetDateBytes(args.Payment.EnterDateTime.Value)); //入场时间
                temp.AddRange(YiTingPacket.GetIntervalBytes(args.Payment.EnterDateTime.Value, args.Payment.ChargeDateTime));
                temp.AddRange(YiTingPacket.GetMoneyBytes(args.Payment.Accounts)); //金额
                temp.Add(0x00);  //未出场
                YiTingPacket response = packet.CreateResponse(temp.ToArray());
                byte[] r = response.ToBytes();
                socket.SendData(r);
                Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", "发送数据 " + Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
            }
        }

        private void HandlePayingState(LJHSocket socket, YiTingPacket packet)
        {
            byte[] data = packet.Data;
            if (data == null || data.Length < 42) return;
            OpenCardEventArgs args = new OpenCardEventArgs();
            args.CardID = YiTingPacket.GetCardID(data.Take(19).ToArray());
            if (data[41] == 0x01)
            {
                byte[] paid = new byte[6];
                Array.Copy(data, 34, paid, 0, paid.Length);
                args.Paid = YiTingPacket.GetMoney(paid);
                args.PaymentCode = Park.BusinessModel.Enum.PaymentCode.Computer;
                args.PaymentMode = Park.BusinessModel.Enum.PaymentMode.Pos;
                if (this.OnPaidOk != null) this.OnPaidOk(this, args);
            }
            else if (data[41] == 0x02)
            {
                if (this.OnPaidFail != null) this.OnPaidFail(this, args);
            }

            List<byte> temp = new List<byte>();
            temp.AddRange(data.Take(19)); //取包的前19字节
            temp.Add(_OK);
            temp.Add(0x00);  //未出场
            YiTingPacket response = packet.CreateResponse(temp.ToArray());
            byte[] r = response.ToBytes();
            socket.SendData(r);
            Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", "发送数据 " + Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置参数
        /// </summary>
        public YiTingShanFuSetting Setting { get; set; }
        #endregion

        #region 实现接口 IOpenCardService
        /// <summary>
        /// 读卡时产生此事件
        /// </summary>
        public event EventHandler<OpenCardEventArgs> OnReadCard;
        /// <summary>
        /// 出口或中央收费处读卡时产生此事件
        /// </summary>
        public event EventHandler<OpenCardEventArgs> OnPaying;
        /// <summary>
        /// 收费成功时产生此事件
        /// </summary>
        public event EventHandler<OpenCardEventArgs> OnPaidOk;
        /// <summary>
        /// 收费失败时产生此事件
        /// </summary>
        public event EventHandler<OpenCardEventArgs> OnPaidFail;
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            if (Setting != null)
            {
                if (_ListenThread == null || _ListenThread.ThreadState == ThreadState.Stopped)
                {
                    _ListenThread = new Thread(new ThreadStart(TCPListen));
                    _ListenThread.IsBackground = true;
                    _ListenThread.Start();
                }
            }
        }
        /// <summary>
        /// 收回资源
        /// </summary>
        public void Dispose()
        {
            if (_Listener != null) _Listener.Stop();
            if (_ListenThread != null)
            {
                _ListenThread.Abort();
                _ListenThread = null;
            }
        }
        #endregion
    }
}

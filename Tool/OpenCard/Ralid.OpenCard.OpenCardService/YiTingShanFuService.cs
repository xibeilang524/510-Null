using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading ;
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
        private LJHSocket _Socket = null;
        private YiTingBuffer _Buffer = new YiTingBuffer();
        private AutoResetEvent _DataReceivedNotify = new AutoResetEvent(false);
        private Thread _ExtraDataThread = null;
        private Thread _ReconnectThread = null;
        private byte _OK = 0x59;
        #endregion

        #region 私有方法
        private void ReconnectTask()
        {
            try
            {
                while (true)
                {
                    if (_Socket != null && !_Socket.IsConnected)
                    {
                        _Socket.Open();
                    }
                    Thread.Sleep(5000);
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

        private void _Socket_OnDataArrivedEvent(object sender, byte[] data)
        {
            _Buffer.Write(data);
            _DataReceivedNotify.Set();
        }

        private void DoExtraData()
        {
            try
            {
                while (true)
                {
                    if (_DataReceivedNotify.WaitOne(int.MaxValue))
                    {
                        YiTingPacket packet = _Buffer.Read();
                        while (packet != null)
                        {
                            HandlePacket(packet);
                            packet = _Buffer.Read();
                        }
                    }
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

        private void HandlePacket(YiTingPacket packet)
        {
            if (!packet.IsValid) return;
            Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", "收到数据 " + Ralid.GeneralLibrary.HexStringConverter.HexToString(packet.ToBytes(), " "));
            if (packet.IsHearbeat) //心跳包
            {
                HandleHeartBeat(packet);
            }
            else if (packet.IsEnterRead)
            {
                HandleEnterRead(packet);
            }
            else if (packet.IsPayingRequest)
            {
                HandlePayingRequst(packet);
            }
            else if (packet.IsPayingState)
            {
                HandlePayingState(packet);
            }
        }

        private void HandleHeartBeat(YiTingPacket packet)
        {
            List<byte> d = new List<byte>();
            d.AddRange(packet.Data);
            d.Add(_OK); //正常处理
            YiTingPacket response = packet.CreateResponse(d.ToArray());
            byte[] r = response.ToBytes();
            _Socket.SendData(r);
            Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", "发送数据 " + Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
        }

        private void HandleEnterRead(YiTingPacket packet)
        {
            if (Setting == null) return;
            byte[] data = packet.Data;
            if (data == null || data.Length < 26) return;
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = YiTingPacket.ConvertToAsc(data.Take(19).ToArray()),
                CardType = "闪付卡",
                DeviceID = YiTingPacket.ConvertToAsc(new byte[] { data[20], data[21], data[22], data[23], data[24], data[25] }),
            };
            YiTingPOS pos = Setting.GetReader(args.DeviceID);
            if (pos == null) return;
            args.EntranceID = pos.EntranceID;
            if (this.OnReadCard != null) this.OnReadCard(this, args);
            List<byte> temp = new List<byte>();
            temp.AddRange(data);
            temp.AddRange(new byte[27]);
            temp.AddRange(new byte[2]);
            temp.AddRange(YiTingPacket.GetDateBytes(DateTime.Now));
            temp.Add(_OK);
            YiTingPacket response = packet.CreateResponse(temp.ToArray());
            byte[] r = response.ToBytes();
            _Socket.SendData(r);
            Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", "发送数据 " + Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
        }

        private void HandlePayingRequst(YiTingPacket packet)
        {
            if (Setting == null) return;
            byte[] data = packet.Data;
            if (data == null || data.Length < 26) return;
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = YiTingPacket.ConvertToAsc(data.Take(19).ToArray()),
                DeviceID = YiTingPacket.ConvertToAsc(new byte[] { data[20], data[21], data[22], data[23], data[24], data[25] }),
            };
            YiTingPOS pos = Setting.GetReader(args.DeviceID);
            if (pos == null) return;
            args.EntranceID = pos.EntranceID;
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
                _Socket.SendData(r);
                Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", "发送数据 " + Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
            }
        }

        private void HandlePayingState(YiTingPacket packet)
        {
            byte[] data = packet.Data;
            if (data == null || data.Length < 42) return;
            OpenCardEventArgs args = new OpenCardEventArgs();
            args.CardID = YiTingPacket.ConvertToAsc(data.Take(19).ToArray());
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
            _Socket.SendData(r);
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
                if (_Socket != null) _Socket.Close();
                _Socket = new LJHSocket(Setting.IP, Setting.Port);
                _Socket.OnDataArrivedEvent += new GeneralLibrary.DataArrivedDelegate(_Socket_OnDataArrivedEvent);
                _Socket.Open();

                if (_ExtraDataThread == null)
                {
                    _ExtraDataThread = new Thread(new ThreadStart(DoExtraData));
                    _ExtraDataThread.IsBackground = true;
                    _ExtraDataThread.Start();
                }

                if (_ReconnectThread == null)
                {
                    _ReconnectThread = new Thread(new ThreadStart(ReconnectTask));
                    _ReconnectThread.IsBackground = true;
                    _ReconnectThread.Start();
                }
            }
        }
        /// <summary>
        /// 收回资源
        /// </summary>
        public void Dispose()
        {
            if (_Socket != null && _Socket.IsConnected)
            {
                _Socket.Close();
                _Socket.OnDataArrivedEvent -= new GeneralLibrary.DataArrivedDelegate(_Socket_OnDataArrivedEvent);
            }
            if (_ExtraDataThread != null)
            {
                _ExtraDataThread.Abort();
                _ExtraDataThread = null;
            }
            if (_ReconnectThread != null)
            {
                _ReconnectThread.Abort();
                _ReconnectThread = null;
            }
        }
        #endregion
    }
}

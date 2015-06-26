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
        #endregion

        #region 私有变量
        private LJHSocket _Socket = null;
        private YiTingBuffer _Buffer = new YiTingBuffer();
        private AutoResetEvent _DataReceivedNotify = new AutoResetEvent(false);
        private Thread _ExtraDataThread = null;
        private byte _OK = 0x59;
        #endregion

        #region 私有方法
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
            Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", Ralid.GeneralLibrary.HexStringConverter.HexToString(packet.ToBytes(), " "));
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
            Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
        }

        private void HandleEnterRead(YiTingPacket packet)
        {
            byte[] data = packet.Data;
            if (data == null || data.Length < 26) return;
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = ASCIIEncoding.ASCII.GetString(data.Take(19).ToArray()),
                CardType = "闪付卡",
                DeviceID = Ralid.GeneralLibrary.HexStringConverter.HexToString(new byte[] { data[20], data[21], data[22], data[23], data[24], data[25] }, string.Empty),
            };
            YiTingShanFuSetting yt = GlobalSettings.Current.Get<YiTingShanFuSetting>();
            if (yt == null) return;
            YiTingPOS pos = yt.GetReader(args.DeviceID);
            if (pos == null) return;
            args.EntranceID = pos.EntranceID;
            if (this.OnReadCard != null) this.OnReadCard(this, args);

            List<byte> temp = new List<byte>();
            temp.AddRange(data);
            temp.AddRange(new byte[27]);
            temp.AddRange(new byte[2]);
            temp.AddRange(GetDateBytes(DateTime.Now));
            temp.Add(_OK);
            YiTingPacket response = packet.CreateResponse(temp.ToArray());
            byte[] r = response.ToBytes();
            _Socket.SendData(r);
            Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
        }

        private void HandlePayingRequst(YiTingPacket packet)
        {
            byte[] data = packet.Data;
            if (data == null || data.Length < 26) return;
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = ASCIIEncoding.ASCII.GetString(data.Take(19).ToArray()),
                DeviceID = Ralid.GeneralLibrary.HexStringConverter.HexToString(new byte[] { data[20], data[21], data[22], data[23], data[24], data[25] }, string.Empty),
            };
            YiTingShanFuSetting yt = GlobalSettings.Current.Get<YiTingShanFuSetting>();
            if (yt == null) return;
            YiTingPOS pos = yt.GetReader(args.DeviceID);
            if (pos == null) return;
            args.EntranceID = pos.EntranceID;
            if (this.OnPaying != null) this.OnPaying(this, args);

            if (args.Payment != null)
            {
                List<byte> temp = new List<byte>();
                temp.AddRange(data.Take(26)); //取包的前26字节
                temp.AddRange(new byte[5]); //车位号
                temp.AddRange(GetDateBytes(args.Payment.EnterDateTime.Value)); //入场时间
                temp.AddRange(GetIntervalBytes(args.Payment.EnterDateTime.Value, args.Payment.ChargeDateTime));
                temp.AddRange(ASCIIEncoding.ASCII.GetBytes(args.Payment.Accounts.ToString("F2").Replace(".", string.Empty).PadLeft(6, '0').Substring(0, 6))); //金额
                temp.Add(0x00);  //未出场
                YiTingPacket response = packet.CreateResponse(temp.ToArray());
                byte[] r = response.ToBytes();
                _Socket.SendData(r);
                Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
            }
        }

        private void HandlePayingState(YiTingPacket packet)
        {
            byte[] data = packet.Data;
            if (data == null || data.Length < 42) return;
            OpenCardEventArgs args = new OpenCardEventArgs();
            args.CardID = ASCIIEncoding.ASCII.GetString(data.Take(19).ToArray());
            if (data[41] == 0x01)
            {
                args.PaymentCode = Park.BusinessModel.Enum.PaymentCode.Computer; 
                args.PaymentMode =Park.BusinessModel.Enum.PaymentMode.Pos;
                if (this.OnPaidOk != null) this.OnPaidOk(this, args);
            }
            else if (data[41] == 0x02)
            {
                if (this.OnPaidFail != null) this.OnPaidFail(this, args);
            }

            if (args.Payment != null)
            {
                List<byte> temp = new List<byte>();
                temp.AddRange(data.Take(19)); //取包的前19字节
                temp.Add(_OK);
                temp.Add(0x00);  //未出场
                YiTingPacket response = packet.CreateResponse(temp.ToArray());
                byte[] r = response.ToBytes();
                _Socket.SendData(r);
                Ralid.GeneralLibrary.LOG.FileLog.Log("驿停闪付", Ralid.GeneralLibrary.HexStringConverter.HexToString(r, " "));
            }
        }

        private byte[] GetDateBytes(DateTime dt)
        {
            return ASCIIEncoding.ASCII.GetBytes(dt.ToString("yyyyMMddHHmmss"));
        }

        private byte[] GetIntervalBytes(DateTime begin, DateTime end)
        {
            TimeSpan span = new TimeSpan(end.Ticks - begin.Ticks);
            return ASCIIEncoding.ASCII.GetBytes(string.Format("{0:D2}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}", 0, 0, span.Days, span.Hours, span.Minutes, span.Seconds));
        }
        #endregion

        #region 实现接口 IOpenCardService
        public event EventHandler<OpenCardEventArgs> OnReadCard;

        public event EventHandler<OpenCardEventArgs> OnPaying;

        public event EventHandler<OpenCardEventArgs> OnPaidOk;

        public event EventHandler<OpenCardEventArgs> OnPaidFail;
        #endregion

        #region 公共方法
        public void Init()
        {
            YiTingShanFuSetting yt = GlobalSettings.Current.Get<YiTingShanFuSetting>();
            if (yt != null)
            {
                if (_ExtraDataThread == null)
                {
                    _ExtraDataThread = new Thread(new ThreadStart(DoExtraData));
                    _ExtraDataThread.IsBackground = true;
                    _ExtraDataThread.Start();
                }
                if (_Socket != null) _Socket.Close();
                _Socket = new LJHSocket(yt.IP, yt.Port);
                _Socket.OnDataArrivedEvent += new GeneralLibrary.DataArrivedDelegate(_Socket_OnDataArrivedEvent);
                _Socket.Open();
            }
        }
        #endregion
    }
}

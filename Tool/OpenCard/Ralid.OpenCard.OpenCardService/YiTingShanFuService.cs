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
        }
        #endregion

        #region 实现接口 IOpenCardService
        public string CardType
        {
            get { return "闪付卡"; }
        }

        public Park.BusinessModel.Enum.PaymentCode PaymentCode
        {
            get { return Park.BusinessModel.Enum.PaymentCode.Computer; }
        }

        public Park.BusinessModel.Enum.PaymentMode PaymentMode
        {
            get { return Park.BusinessModel.Enum.PaymentMode.Pos; }
        }

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
                if (_Socket != null) _Socket.Close();
                _Socket = new LJHSocket(yt.IP, yt.Port);
                _Socket.OnDataArrivedEvent += new GeneralLibrary.DataArrivedDelegate(_Socket_OnDataArrivedEvent);
                _Socket.Open();
            }
        }
        #endregion
    }
}

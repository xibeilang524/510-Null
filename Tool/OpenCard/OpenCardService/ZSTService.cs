using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.ParkAdapter;
using Ralid.Park.BLL;

namespace Ralid.OpenCard.OpenCardService
{
    public class ZSTService : IOpenCardService
    {
        #region 构造函数
        public ZSTService()
        {
        }
        #endregion

        #region 私有变量
        private ZSTReader _Reader = null;
        private Dictionary<string, CardPaymentInfo> _Paying = new Dictionary<string, CardPaymentInfo>();
        #endregion

        #region 私有方法
        private void reader_MessageRecieved(object sender, ZSTReaderEventArgs e)
        {
            if (e.MessageType == "1")
            {
                reader_CardReadHandler(sender, e);
            }
            else if (e.MessageType == "2")//扣款成功
            {
                reader_PaymentOk(sender, e);
            }
            else if (e.MessageType == "3") //扣款失败
            {
                reader_PaymentFail(sender, e);
            }
        }

        private void reader_CardReadHandler(object sender, ZSTReaderEventArgs e)
        {
            ZSTSetting zst = GlobalSettings.Current.Get<ZSTSetting>();
            if (zst == null)
            {
                _Reader.MessageConfirm(e.ReaderIP);
                return;
            }
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(zst.GetReader(e.ReaderIP).EntranceID);
            if (entrance != null && !entrance.IsExitDevice)  //入场时产生读卡事件
            {
                _Reader.MessageConfirm(e.ReaderIP);
                OpenCardEventArgs args = new OpenCardEventArgs()
                {
                    EntranceID = entrance.EntranceID,
                    CardID = e.CardID,
                    DeviceID = e.ReaderIP,
                    Balance = e.Balance,
                };
                if (this.OnReadCard != null) this.OnReadCard(this, args);
            }
            else  //中央收费和出口产生收费事件
            {
                OpenCardEventArgs args = new OpenCardEventArgs()
                {
                    CardID = e.CardID,
                    DeviceID = e.ReaderIP,
                };
                if (entrance != null) args.EntranceID = entrance.EntranceID;
                if (this.OnPaying != null)
                {
                    this.OnPaying(this, args); //产生收费事件
                    if (args.Payment == null) return;
                    if (args.Payment.Accounts == 0)
                    {
                        if (this.OnPaidOk != null) this.OnPaidOk(this, args);
                        return;
                    }
                    _Paying[e.ReaderIP] = args.Payment;
                    args.Payment.Paid = args.Payment.Accounts;
                    _Reader.Consumption(e.ReaderIP, args.Payment.Accounts);  //直接扣款
                }
            }
        }

        private void reader_PaymentFail(object sender, ZSTReaderEventArgs e)
        {
            _Reader.MessageConfirm(e.ReaderIP);
            ZSTSetting zst = GlobalSettings.Current.Get<ZSTSetting>();
            if (zst == null) return;
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(zst.GetReader(e.ReaderIP).EntranceID);
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = e.CardID,
                DeviceID = e.ReaderIP,
            };
            if (entrance != null) args.EntranceID = entrance.EntranceID;
            _Paying.Remove(e.ReaderIP); //移除当前收费记录
            if (this.OnPaidFail != null) this.OnPaidFail(this, args);
        }

        private void reader_PaymentOk(object sender, ZSTReaderEventArgs e)
        {
            _Reader.MessageConfirm(e.ReaderIP);
            ZSTSetting zst = GlobalSettings.Current.Get<ZSTSetting>();
            if (zst == null) return;
            if (!_Paying.ContainsKey(e.ReaderIP)) return; //如果没有等待的收费记录
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(zst.GetReader(e.ReaderIP).EntranceID);
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = e.CardID,
                DeviceID = e.ReaderIP,
                Payment = _Paying[e.ReaderIP],
            };
            if (entrance != null) args.EntranceID = entrance.EntranceID;
            _Paying.Remove(e.ReaderIP); //移除当前收费记录
            if (this.OnPaidOk != null) this.OnPaidOk(this, args);
        }
        #endregion

        #region 公共属性
        #endregion

        #region 实现IOpenCardService
        public string CardType
        {
            get { return "中山通"; }
        }

        public Park.BusinessModel.Enum.PaymentCode PaymentCode
        {
            get { return Park.BusinessModel.Enum.PaymentCode.Computer; }
        }

        public Park.BusinessModel.Enum.PaymentMode PaymentMode
        {
            get { return Park.BusinessModel.Enum.PaymentMode.ZhongShanTong; }
        }

        public event EventHandler<OpenCardEventArgs> OnReadCard;

        public event EventHandler<OpenCardEventArgs> OnPaying;

        public event EventHandler<OpenCardEventArgs> OnPaidOk;

        public event EventHandler<OpenCardEventArgs> OnPaidFail;
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            _Reader = GlobalSettings.Current.Get<ZSTReader>();
            if (_Reader == null)
            {
                _Reader = new ZSTReader();
                _Reader.Init();
                GlobalSettings.Current.Set<ZSTReader>(_Reader);
            }
            _Reader.MessageRecieved += reader_MessageRecieved;
        }
        #endregion
    }
}

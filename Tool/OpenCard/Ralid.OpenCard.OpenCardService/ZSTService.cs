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

        public ZSTService(ZSTSettings setting)
        {
            Setting = setting;
        }
        #endregion

        #region 私有变量
        private ZSTReader _Reader = null;
        private Dictionary<string, OpenCardEventArgs> _WaitingPayingCards = new Dictionary<string, OpenCardEventArgs>(); //等待交费的卡片
        private object _WaitingPayingCardsLocker = new object();
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
            if (Setting == null)
            {
                _Reader.MessageConfirm(e.ReaderIP);
                return;
            }
            ZSTItem item = Setting.GetReader(e.ReaderIP);
            if (item == null)
            {
                _Reader.MessageConfirm(e.ReaderIP);
                return;
            }
            EntranceInfo entrance = null;
            entrance = ParkBuffer.Current.GetEntrance(item.EntranceID);
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = e.CardID,
                CardType = ZSTSettings.CardType,
                Balance = e.Balance,
                Entrance = entrance,
            };
            if (entrance != null && !entrance.IsExitDevice)  //入场时产生读卡事件
            {
                _Reader.MessageConfirm(e.ReaderIP);
                if (this.OnReadCard != null) this.OnReadCard(this, args);
            }
            else  //中央收费和出口产生收费事件
            {
                if (this.OnPaying != null) this.OnPaying(this, args); //产生收费事件
                if (args.Payment == null) return;
                if (args.Payment.GetPaying() == 0) //不用收费直接返回收款成功事件
                {
                    args.Payment.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                    args.Payment.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.ZhongShanTong;
                    if (this.OnPaidOk != null) this.OnPaidOk(this, args);
                }
                else
                {
                    lock (_WaitingPayingCardsLocker)
                    {
                        _WaitingPayingCards[e.CardID] = args; //保存某个读卡器目前正在处理的收费记录
                    }
                    _Reader.Consumption(e.ReaderIP, args.Payment.GetPaying());  //直接扣款
                }
            }
        }

        private void reader_PaymentOk(object sender, ZSTReaderEventArgs e)
        {
            _Reader.MessageConfirm(e.ReaderIP);
            OpenCardEventArgs args = null;
            lock (_WaitingPayingCardsLocker)
            {
                if (_WaitingPayingCards.ContainsKey(e.CardID))
                {
                    args = _WaitingPayingCards[e.CardID];
                    _WaitingPayingCards.Remove(e.CardID);
                }
            }
            if (args != null)
            {
                args.Paid = args.Payment.GetPaying(); //默认是直接扣除应收款所以如果扣款成功,已交费用等于应收费用
                args.Payment.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                args.Payment.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.ZhongShanTong;
                args.Balance = e.Balance;
                if (this.OnPaidOk != null) this.OnPaidOk(this, args);
            }
        }

        private void reader_PaymentFail(object sender, ZSTReaderEventArgs e)
        {
            _Reader.MessageConfirm(e.ReaderIP);
            OpenCardEventArgs args = null;
            lock (_WaitingPayingCardsLocker)
            {
                if (_WaitingPayingCards.ContainsKey(e.CardID))
                {
                    args = _WaitingPayingCards[e.CardID];
                    _WaitingPayingCards.Remove(e.CardID);
                }
            }
            if (args != null && args.CardID == e.CardID)
            {
                if (this.OnPaidFail != null) this.OnPaidFail(this, args);
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置配置参数
        /// </summary>
        public ZSTSettings Setting { get; set; }
        #endregion

        #region 实现IOpenCardService
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

        public event EventHandler<OpenCardEventArgs> OnError;
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
        /// <summary>
        /// 收回资源
        /// </summary>
        public void Dispose()
        {
            _Reader = GlobalSettings.Current.Get<ZSTReader>();
            if (_Reader != null)
            {
                _Reader.Dispose();
                _Reader.MessageRecieved -= reader_MessageRecieved;
            }
        }
        #endregion
    }
}

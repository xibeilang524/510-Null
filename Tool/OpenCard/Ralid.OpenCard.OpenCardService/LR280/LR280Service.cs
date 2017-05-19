using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using Ralid.OpenCard.OpenCardService;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;

namespace Ralid.OpenCard.OpenCardService.LR280
{
    public class LR280Service : IOpenCardService
    {
        #region 构造函数
        public LR280Service(LR280Setting setting)
        {
            Setting = setting;
        }
        #endregion

        #region 私有变量
        private List<LR280Item> _LR280Items = new List<LR280Item>();
        private Dictionary<LR280Item, Thread> _PollRoutes = new Dictionary<LR280Item, Thread>();
        #endregion

        #region 私有事件
        private void PollRoute(object obj)
        {
            string lastCard = null;
            DateTime lastDT = DateTime.Now;
            LR280Item item = obj as LR280Item;
            if (item == null || item.Reader == null) return;
            while (true)
            {
                Thread.Sleep(500);
                try
                {
                    var w = item.Reader.ReadCard();
                    if (w != null)
                    {
                        if (w.返回码 == "00")
                        {
                            if (w.卡号 == lastCard && CalInterval(lastDT, DateTime.Now) < 3) continue; //同一张卡间隔至少要3秒才处理
                            lastCard = w.卡号;
                            lastDT = DateTime.Now;
                            HandleWallet(item, w);
                        }
                        else if (w.返回码 == "-1") item.Reader.Open();//串口打开失败
                        else if (w.返回码 == "A4") item.Reader.CheckIn();//没有签到
                        else if (w.返回码 == "XA" || w.返回码 == "XB") item.Reader.Clear();//没有结算
                        else HandleError(item, string.Format("错误{0}：{1}", w.返回码, w.错误说明));
                    }
                }
                catch (ThreadAbortException)
                {
                }
            }
        }

        private double CalInterval(DateTime dt1, DateTime dt2)
        {
            TimeSpan ts = new TimeSpan(dt2.Ticks - dt1.Ticks);
            return ts.TotalSeconds;
        }

        private void HandleWallet(LR280Item item, LR280Response w)
        {
            EntranceInfo entrance = item.EntranceID.HasValue ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = w.卡号,
                CardType = LR280Setting.CardTyte,
                Entrance = entrance,
                Balance = (decimal)w.金额 / 100,
            };
            if (args.CardType == LR280Setting.CardTyte)
            {
                ParkInfo p = ParkBuffer.Current.GetPark(entrance.ParkID);
                CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(w.卡号).QueryObject;
                if (card != null && card.CardType.Name == LR280Setting.CardTyte && (entrance == null || (!p.IsNested && entrance.IsExitDevice))) ////中央收费处和非嵌套车场的出口,则进行收费处理
                {
                    HandlePayment(item, w, args);
                }
                else
                {
                    if (this.OnReadCard != null) this.OnReadCard(this, args);
                }
            }
        }

        private void HandlePayment(LR280Item item, LR280Response w, OpenCardEventArgs args)
        {
            if (this.OnPaying != null) this.OnPaying(this, args); //产生收费事件
            if (args.Payment == null) return;
            if (args.Payment.GetPaying() <= 0) //不用收费直接返回收款成功事件
            {
                args.Payment.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                args.Payment.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.UnionPay;
                if (this.OnPaidOk != null) this.OnPaidOk(this, args);
            }
            else //扣费
            {
                //判断余额是否够扣费，否则返回"余额不足",注意钱包单位是分的，这里要转成分比较
                //因为CPU钱包里有一个余额下限，余额下限是不允许扣费的，如果不比较费用和余额，有可以会扣到余额下限
                int fee = (int)(args.Payment.GetPaying() * 100);
                if (fee <= w.金额)
                {
                    var r = item.Reader.Pay(w.卡号, fee);
                    if (r.返回码 == "00")
                    {
                        args.Paid = args.Payment.GetPaying();
                        args.Payment.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                        args.Payment.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.UnionPay;
                        args.Balance = (decimal)(w.金额 - fee) / 100;
                        if (this.OnPaidOk != null) this.OnPaidOk(this, args);
                    }
                    else
                    {
                        args.LastError = string.Format("错误{0}：{1}", r.返回码, r.错误说明);
                        if (this.OnPaidFail != null) this.OnPaidFail(this, args);
                    }
                }
                else
                {
                    args.LastError = "余额不足";
                    if (this.OnPaidFail != null) this.OnPaidFail(this, args);
                }
            }
        }

        private void HandleError(LR280Item item, string error)
        {
            EntranceInfo entrance = item.EntranceID.HasValue ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
            if (this.OnError != null) //
            {
                OpenCardEventArgs args = new OpenCardEventArgs()
                {
                    CardID = "0",
                    CardType = LR280Setting.CardTyte,
                    Entrance = entrance,
                    LastError = error,
                };
                this.OnError(this, args);
            }
        }
        #endregion

        #region 公共属性
        public LR280Setting Setting { get; set; }
        #endregion

        #region 事件
        public event EventHandler<OpenCardEventArgs> OnReadCard;

        public event EventHandler<OpenCardEventArgs> OnPaying;

        public event EventHandler<OpenCardEventArgs> OnPaidOk;

        public event EventHandler<OpenCardEventArgs> OnPaidFail;

        public event EventHandler<OpenCardEventArgs> OnError;
        #endregion

        #region 公共方法
        public void Init()
        {
            List<LR280Item> keys = _LR280Items.ToList();
            if (keys != null && keys.Count > 0)//将所有不在新设置中的读卡器删除
            {
                foreach (var key in keys)
                {
                    var item = Setting.Items != null ? Setting.Items.SingleOrDefault(it => it.Comport == key.Comport) : null;
                    if (item == null)
                    {
                        key.Reader.Close();
                        _LR280Items.Remove(key);
                        if (_PollRoutes.ContainsKey(key))
                        {
                            Thread t = _PollRoutes[key];
                            if (t.ThreadState != ThreadState.Stopped) t.Abort();
                            _PollRoutes.Remove(key);
                        }
                    }
                    else
                    {
                        key.EntranceID = item.EntranceID;
                    }
                }
            }
            if (Setting.Items != null)
            {
                foreach (var item in Setting.Items)
                {
                    if (_LR280Items == null || !_LR280Items.Exists(it => it.Comport == item.Comport))
                    {
                        var reader = new LR280POS((byte)item.Comport, 57600);
                        //reader.Log = AppSettings.CurrentSetting.Debug; //处理调试模式才启动日志功能
                        reader.Open();
                        item.Reader = reader;
                        _LR280Items.Add(item);
                        Thread t = new Thread(new ParameterizedThreadStart(PollRoute));
                        t.IsBackground = true;
                        _PollRoutes[item] = t;
                        t.Start(item);
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var item in _LR280Items)
            {
                if (item.Reader != null) item.Reader.Close();
                if (_PollRoutes.ContainsKey(item)) _PollRoutes[item].Abort();
            }
            _LR280Items.Clear();
        }
        #endregion
    }
}

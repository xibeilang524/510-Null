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

        public ZSTService(ZSTSetting setting)
        {
            Setting = setting;
        }
        #endregion

        #region 私有变量
        private ZSTReader _Reader = null;

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
            if (entrance != null && !entrance.IsExitDevice)  //入场时产生读卡事件
            {
                _Reader.MessageConfirm(e.ReaderIP);
                OpenCardEventArgs args = new OpenCardEventArgs()
                {
                    CardID = e.CardID,
                    CardType = "中山通",
                    DeviceID = e.ReaderIP,
                    EntranceID = entrance.EntranceID,
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
                    }
                    else
                    {
                        args.Payment.Paid = args.Payment.Accounts;
                        _Reader.Consumption(e.ReaderIP, args.Payment.Accounts);  //直接扣款
                    }
                }
            }
        }

        private void reader_PaymentOk(object sender, ZSTReaderEventArgs e)
        {
            _Reader.MessageConfirm(e.ReaderIP);
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = e.CardID,
                DeviceID = e.ReaderIP,
                PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer,
                PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.ZhongShanTong,
            };
            if (this.OnPaidOk != null) this.OnPaidOk(this, args);
        }

        private void reader_PaymentFail(object sender, ZSTReaderEventArgs e)
        {
            _Reader.MessageConfirm(e.ReaderIP);
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = e.CardID,
                DeviceID = e.ReaderIP,
            };
            if (this.OnPaidFail != null) this.OnPaidFail(this, args);
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置配置参数
        /// </summary>
        public ZSTSetting Setting { get; set; }
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

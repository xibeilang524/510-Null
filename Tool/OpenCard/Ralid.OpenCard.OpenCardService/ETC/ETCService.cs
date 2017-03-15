using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading ;
using Newtonsoft.Json;
using Ralid.OpenCard.OpenCardService;
using Ralid.OpenCard.OpenCardService.ETC.Response;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    public class ETCService : IOpenCardService
    {
        #region 私有变量
        private List<ETCDevice> _Devices { get; set; }
        private Thread _UploadList = null;
        #endregion

        public ETCSetting Setting { get; set; }

        #region 私有方法
        private void UploadListThread()
        {
            try
            {
                while (true)
                {
                    var bll = new ETCPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect);
                    Thread.Sleep(1000 * 60); //
                    var con = new ETCPaymentRecordSearchCondition() { WaitingUpload = true };
                    var items = bll.GetRecords(con).QueryObjects;
                    if (items != null && items.Count > 0)
                    {
                        foreach (var item in items)
                        {
                            var device = _Devices.SingleOrDefault(it => it.LaneNo == item.LaneNo);
                            if (device != null)
                            {
                                var list = JsonConvert.DeserializeObject<ETCPaymentList>(item.Data);
                                if (list != null)
                                {
                                    var res = device.ListUpLoad(list);
                                    if (res.ErrorCode == 0) bll.UpdateUploadTime(item, DateTime.Now);
                                }
                            }
                        }
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }
        #endregion

        #region 读卡事件处理程序
        private void device_OnReadOBUInfo(object sender, ReadOBUInfoEventArgs e)
        {
            ETCDevice device = sender as ETCDevice;
            if (device == null) return;
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(device.EntranceID);
            if (Setting.MonthCardFirst) //如果启用了优先使用车场卡功能
            {
                var con = new CardSearchCondition();
                con.CarPlate = e.OBUInfo.CardPlate.Trim();
                con.Status = Ralid.Park.BusinessModel.Enum.CardStatus.Enabled;
                var cards = new CardBll(AppSettings.CurrentSetting.ParkConnect).GetCards(con).QueryObjects;
                if (cards != null && cards.Count > 0 && cards.Exists(it => !it.CardType.IsTempCard))
                {
                    OpenCardEventArgs err = new OpenCardEventArgs()
                    {
                        Entrance = entrance,
                        LastError = "请读车场卡",
                    };
                    if (this.OnError != null) this.OnError(this, err);
                    return; //退出ETC读卡处理
                }
            }
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = e.OBUInfo.CardNo,
                CardType = ETCSetting.CardTyte,
                Entrance = entrance,
                Balance = (decimal)e.OBUInfo.Balance / 100,
            };
            if (entrance != null)
            {
                var p = ParkBuffer.Current.GetPark(entrance.ParkID);
                if (!entrance.IsExitDevice || (p != null && p.IsNested)) //入口或者嵌套车场，
                {
                    ETCPaymentList pr = null;
                    device.RSUWriteCard(e.OBUInfo, 0, false, out pr); //这里写卡主要是为了让卡片读卡时产生蜂鸣声
                    if (this.OnReadCard != null) this.OnReadCard(this, args);
                }
                else
                {
                    HandlePayment(device, args, e, null);
                }
            }
            else
            {
                HandlePayment(device, args, e, null);
            }
        }

        private void device_OnReadCardInfo(object sender, ReadCardInfoEventArgs e)
        {
            ETCDevice device = sender as ETCDevice;
            if (device == null) return;
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(device.EntranceID);
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = e.CardInfo.CardNo,
                CardType = ETCSetting.CardTyte,
                Entrance = entrance,
                Balance = (decimal)e.CardInfo.Balance / 100,
            };
            if (entrance != null)
            {
                var p = ParkBuffer.Current.GetPark(entrance.ParkID);
                if (!entrance.IsExitDevice || (p != null && p.IsNested)) //入口或者嵌套车场，
                {
                    if (this.OnReadCard != null) this.OnReadCard(this, args);
                }
                else
                {
                    HandlePayment(device, args, null, e);
                }
            }
            else
            {
                HandlePayment(device, args, null, e);
            }
        }

        private void HandlePayment(ETCDevice device, OpenCardEventArgs e, ReadOBUInfoEventArgs obuInfo, ReadCardInfoEventArgs cardInfo)
        {
            if (this.OnPaying != null) this.OnPaying(this, e); //产生收费事件
            if (e.Payment == null)
            {
                if (this.OnReadCard != null) this.OnReadCard(this, e);
                return;
            }
            if (e.Payment.GetPaying() <= 0) //不用收费直接返回收款成功事件
            {
                WriteCardResponse r = null;
                ETCPaymentList list = null;
                if (obuInfo != null) r = device.RSUWriteCard(obuInfo.OBUInfo, 0, true, out list);
                else r = device.CardReaderWriteCard(cardInfo.CardInfo, 0, true, out list);
                e.Payment.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                e.Payment.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.GDETC;
                if (this.OnPaidOk != null) this.OnPaidOk(this, e);
            }
            else //扣费
            {
                //判断余额是否够扣费，否则返回"余额不足",注意钱包单位是分的，这里要转成分比较
                if (e.Payment.GetPaying() <= e.Balance)
                {
                    int paid = (int)(e.Payment.GetPaying() * 100);
                    ETCPaymentList list = null;
                    WriteCardResponse r = null;
                    if (obuInfo != null) r = device.RSUWriteCard(obuInfo.OBUInfo, paid, true, out list);
                    else r = device.CardReaderWriteCard(cardInfo.CardInfo, paid, true, out list);
                    if (r.ErrorCode == 0)
                    {
                        var res = device.ListUpLoad(list); //上传流水
                        var record = new Ralid.Park.BusinessModel.Model.ETCPaymentRecord()
                        {
                            LaneNo = device.LaneNo,
                            Device = obuInfo != null ? 0 : 1,
                            AddTime = DateTime.Now,
                            Data = JsonConvert.SerializeObject(list),
                            UploadTime = res.ErrorCode == 0 ? (DateTime?)DateTime.Now : null,
                        };
                        new ETCPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect).Insert(record); //在数据库中保存流水记录
                        e.Paid = e.Payment.GetPaying();
                        e.Payment.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                        e.Payment.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.GDETC;
                        e.Balance = (decimal)r.Balance / 100;
                        if (this.OnPaidOk != null) this.OnPaidOk(this, e);
                    }
                    else
                    {
                        e.LastError = "扣款失败";
                        if (this.OnPaidFail != null) this.OnPaidFail(this, e);
                    }
                }
                else
                {
                    if (obuInfo != null)
                    {
                        e.LastError = "余额不足";
                        if (this.OnPaidFail != null) this.OnPaidFail(this, e);
                    }
                    else //ETC读卡器上余额不足时产生读卡事件，在收费电脑上显示明细
                    {
                        if (this.OnReadCard != null) this.OnReadCard(this, e);
                    }
                }
            }
        }

        private void device_OnError(object sender, OpenCardEventArgs e)
        {
            if (this.OnError != null) this.OnError(sender, e);
        }
        #endregion

        #region 实现接口IOpenCardService
        public event EventHandler<OpenCardEventArgs> OnReadCard;

        public event EventHandler<OpenCardEventArgs> OnPaying;

        public event EventHandler<OpenCardEventArgs> OnPaidOk;

        public event EventHandler<OpenCardEventArgs> OnPaidFail;

        public event EventHandler<OpenCardEventArgs> OnError;

        public void Init()
        {
            if (_Devices != null && _Devices.Count > 0)
            {
                foreach (var device in _Devices)
                {
                    device.Dispose();
                }
            }
            _Devices = new List<ETCDevice>();
            if (Setting.Devices != null && Setting.Devices.Count > 0)
            {
                foreach (var dinfo in Setting.Devices)
                {
                    var device = new ETCDevice(dinfo);
                    device.ETCCardReaderEnable = Setting.ETCCardReaderEnable;
                    device.OnReadCardInfo += device_OnReadCardInfo;
                    device.OnReadOBUInfo += device_OnReadOBUInfo;
                    device.OnError += device_OnError;
                    device.Init();
                    _Devices.Add(device);
                }
            }
            if (_UploadList == null)
            {
                _UploadList = new Thread(new ThreadStart(UploadListThread));
                _UploadList.IsBackground = true;
                _UploadList.Start();
            }
        }

        public void Dispose()
        {
            if (_UploadList != null)
            {
                _UploadList.Abort();
                _UploadList = null;
            }
            if (_Devices != null && _Devices.Count > 0)
            {
                foreach (var device in _Devices)
                {
                    device.Dispose();
                }
            }
            ETCInterop.Uninstall();
        }
        #endregion
    }
}

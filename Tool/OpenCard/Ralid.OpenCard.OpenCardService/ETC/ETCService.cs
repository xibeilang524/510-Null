using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        #region 公共属性
        public ETCSetting Setting { get; set; }

        private ETCDevice[] ETCDevices { get; set; }
        #endregion

        #region 读卡事件处理程序
        private void device_OnReadOBUInfo(object sender, ReadOBUInfoEventArgs e)
        {
            ETCDevice device = sender as ETCDevice;
            if (device == null) return;
            EntranceInfo entrance = GetEntranceOf(device.LaneNo);
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
            EntranceInfo entrance = GetEntranceOf(device.LaneNo);
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

        private void HandlePayment(ETCDevice device, OpenCardEventArgs args, ReadOBUInfoEventArgs obuInfo, ReadCardInfoEventArgs cardInfo)
        {
            if (this.OnPaying != null) this.OnPaying(this, args); //产生收费事件
            if (args.Payment == null) return;
            if (args.Payment.GetPaying() <= 0) //不用收费直接返回收款成功事件
            {
                args.Payment.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                args.Payment.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.YangChengTong;
                if (this.OnPaidOk != null) this.OnPaidOk(this, args);
            }
            else //扣费
            {
                int paid = (int)(args.Payment.GetPaying() * 100);
                //判断余额是否够扣费，否则返回"余额不足",注意钱包单位是分的，这里要转成分比较
                if (paid <= args.Balance)
                {
                    ETCPaymentRecord record = null;
                    WriteCardResponse r = null;
                    if (obuInfo != null) r = device.RSUWriteCard(obuInfo.OBUInfo, paid, true, out record);
                    else r = device.CardReaderWriteCard(cardInfo.CardInfo, paid, true, out record);
                    if (r.ErrorCode == 0)
                    {
                        device.ListUpLoad(record); //上传流水
                        args.Paid = paid;
                        args.Payment.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                        args.Payment.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.YangChengTong;
                        args.Balance = r.Balance;
                        if (this.OnPaidOk != null) this.OnPaidOk(this, args);
                    }
                    else
                    {
                        args.LastError = "扣款失败";
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

        private EntranceInfo GetEntranceOf(string laneNo)
        {
            if (Setting == null || Setting.ETCEntrancePairs == null) return null;
            if (!Setting.ETCEntrancePairs.ContainsKey(laneNo)) return null;
            return ParkBuffer.Current.GetEntrance(Setting.ETCEntrancePairs[laneNo]);
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
            try
            {
                ETCDevices = null;
                StringBuilder pRet = new StringBuilder(100 * 1000);
                StringBuilder err = new StringBuilder(1000);
                int count = 0;
                var n = ETCInterop.Initialize(pRet, ref count, err);
                if (count > 0)
                {
                    var str = pRet.ToString().Trim();
                    ETCDevices = JsonConvert.DeserializeObject<ETCDevice[]>(str);
                    if (ETCDevices != null && ETCDevices.Length > 0)
                    {
                        foreach (var device in ETCDevices)
                        {
                            device.OnReadCardInfo += new EventHandler<ReadCardInfoEventArgs>(device_OnReadCardInfo);
                            device.OnReadOBUInfo += new EventHandler<ReadOBUInfoEventArgs>(device_OnReadOBUInfo);
                            device.Init();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        public void Dispose()
        {
            if (ETCDevices != null && ETCDevices.Length > 0)
            {
                foreach (var device in ETCDevices)
                {
                    device.Dispose();
                }
            }
            ETCInterop.Uninstall();
        }
        #endregion
    }
}

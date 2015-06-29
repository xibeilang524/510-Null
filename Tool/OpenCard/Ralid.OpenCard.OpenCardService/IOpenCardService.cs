using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.OpenCard.OpenCardService
{
    public interface IOpenCardService
    {
        event EventHandler<OpenCardEventArgs> OnReadCard;

        event EventHandler<OpenCardEventArgs> OnPaying;

        event EventHandler<OpenCardEventArgs> OnPaidOk;

        event EventHandler<OpenCardEventArgs> OnPaidFail;

        void Init();

        void Dispose();
    }

    public class OpenCardEventArgs : EventArgs
    {
        #region 构造函数
        public OpenCardEventArgs()
        {
        }
        #endregion

        #region 公共属性
        public string DeviceID { get; set; }

        public string CardID { get; set; }

        public int? EntranceID { get; set; }

        public string EntranceName { get; set; }

        public string CardType { get; set; }

        public CardPaymentInfo Payment { get; set; }

        public decimal Paid { get; set; }

        public PaymentCode PaymentCode { get; set; }

        public PaymentMode PaymentMode { get; set; }
        #endregion
    }
}

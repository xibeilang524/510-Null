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
        string CardType { get; }

        PaymentCode PaymentCode { get; }

        PaymentMode PaymentMode { get;}

        event EventHandler<OpenCardEventArgs> OnReadCard;

        event EventHandler<OpenCardEventArgs> OnPaying;

        event EventHandler<OpenCardEventArgs> OnPaidOk;

        event EventHandler<OpenCardEventArgs> OnPaidFail;
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

        public decimal? Balance { get; set; }

        public CardPaymentInfo Payment { get; set; }
        #endregion
    }
}

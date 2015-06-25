using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        }
        #endregion
    }
}

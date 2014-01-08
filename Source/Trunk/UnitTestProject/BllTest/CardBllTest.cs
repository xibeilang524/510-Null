using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BLL;

namespace UnitTestProject.BllTest
{
    [TestClass]
    public class CardBllTest
    {
        [TestMethod]
        public void CanAddCardWithCustomCardType()
        {
            //string cardID = Guid.NewGuid().ToString();
            //CardType cardType = new CardType(22, "员工卡");
            //CardInfo card = new CardInfo()
            //{
            //    CardID = cardID,
            //    CardType = cardType,
            //    CarType = CarTypeSetting.DefaultCarType,
            //    ActivationDate = DateTime.Now,
            //    ValidDate = DateTime.Now,
            //    Status = CardStatus.Recycled,
            //    ParkingStatus = ParkingStatus.Out,
            //};
            //CommandResult ret = (new CardBll(StaticConnectString.ConnStr)).AddCard(card);
            //Assert.IsTrue(ret.Result == ResultCode.Successful);

            //CardInfo card1 = (new CardBll(StaticConnectString.ConnStr)).GetCardByID(cardID).QueryObject;
            //Assert.IsTrue(card1 != null);

            //ret = (new CardBll(StaticConnectString.ConnStr)).DeleteCard(card);
            //Assert.IsTrue(ret.Result == ResultCode.Successful);

            //card1 = (new CardBll(StaticConnectString.ConnStr)).GetCardByID(cardID).QueryObject;
            //Assert.IsTrue(card1 == null);
        }
    }
}

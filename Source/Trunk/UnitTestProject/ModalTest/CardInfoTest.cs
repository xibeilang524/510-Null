using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;

#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#endif

namespace TestProject1.ModalTest
{
    [TestClass]
    public class CardInfoTest
    {
        [TestMethod]
        public void CloneTest()
        {
            CardInfo card = new CardInfo();
            card.CardID = "12359";
            card.AccessID = 5;
            card.Balance = 15;
            card.CardType = Ralid.Park.BusinessModel.Enum.CardType.PrePayCard;
            card.CarType = CarTypeSetting.DefaultCarType;
            card.LastEntrance = 2;
            card.LastDateTime = new DateTime(2011, 3, 23, 12, 5, 0);
            card.ParkingStatus = Ralid.Park.BusinessModel.Enum.ParkingStatus.In;
            card.Status = Ralid.Park.BusinessModel.Enum.CardStatus.Enabled;
            card.CarPlate = "粤A15944";
            card.Memo = "sss";
            card.OwnerName = "李建华";
            CardInfo clone = card.Clone();
            AssertEqual(card,clone);
        }

        private void AssertEqual(CardInfo card,CardInfo clone)
        {
            Assert.IsFalse(object.ReferenceEquals(card, clone));
            Assert.IsTrue(card.CardID == clone.CardID);
            Assert.IsTrue(card.AccessID == clone.AccessID);
            Assert.IsTrue(card.Balance == clone.Balance);
            Assert.IsTrue(card.CardType == clone.CardType);
            Assert.IsTrue(card.CarType == clone.CarType);
            Assert.IsTrue(card.LastEntrance == clone.LastEntrance);
            Assert.IsTrue(card.LastDateTime == clone.LastDateTime);
            Assert.IsTrue(card.ParkingStatus == clone.ParkingStatus);
            Assert.IsTrue(card.Status == clone.Status);
            Assert.IsTrue(card.CarPlate == clone.CarPlate);
            Assert.IsTrue(card.Memo == clone.Memo);
            Assert.IsTrue(card.OwnerName == clone.OwnerName);
        }
    }
}

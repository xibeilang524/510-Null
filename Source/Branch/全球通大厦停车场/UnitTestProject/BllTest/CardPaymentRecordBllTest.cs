using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;

#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#endif
namespace UnitTestProject.BllTest
{
    [TestClass]
    public class CardPaymentRecordBllTest
    {
        [TestMethod]
        public void AddARecord()
        {
            CardPaymentInfo record = new CardPaymentInfo();
            record.CardID = "233955501";
            record.CardType = CardType.TempCard;
            record.CarType = CarTypeSetting.DefaultCarType;
            record.ChargeDateTime = new DateTime(2011, 3, 27, 12, 15, 20);
            record.EnterDateTime = new DateTime(2011, 3, 27, 9, 1, 15);
            record.Accounts = 30;
            record.Paid = 30;
            record.PaymentMode = PaymentMode.Cash;
            record.TariffType = TariffType.Normal;
            record.OperatorID = "test";
            record.StationID = "一号中央收费";

            CardPaymentRecordBll bll = new CardPaymentRecordBll(StaticConnectString.ConnStr);
            CommandResult result = bll.Insert(record);
            Assert.IsTrue(result.Result == ResultCode.Successful);

            CardPaymentInfo r = bll.GetByID(new Ralid.Park.BusinessModel.Report.RecordID(record.CardID, record.ChargeDateTime));
            Assert.IsTrue(r != null && r.CardID == record.CardID);

            bll.Delete(record);
            r = bll.GetByID(new Ralid.Park.BusinessModel.Report.RecordID(record.CardID, record.ChargeDateTime));
            Assert.IsTrue(r == null);
        }
    }
}

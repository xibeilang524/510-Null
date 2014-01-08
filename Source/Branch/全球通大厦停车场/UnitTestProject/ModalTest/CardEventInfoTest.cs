using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;

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
    public class CardEventInfoTest
    {
        [TestMethod]
        public void CarPlateComparisonTest()
        {
            CardEventReport report = new CardEventReport();
            report.LastCarPlate = "粤A12345";
            report.CarPlate = "粤A12345";
            for (int i = 0; i < 8; i++)
            {
                Assert.IsTrue(CarPlateComparer.CarPlateComparison(report.CarPlate, report.LastCarPlate, i));
            }

            report.CarPlate = "粤B12345";
            Assert.IsFalse(CarPlateComparer.CarPlateComparison(report.CarPlate, report.LastCarPlate, 0));
            for (int i = 1; i < 2; i++)
            {
                Assert.IsTrue(CarPlateComparer.CarPlateComparison(report.CarPlate, report.LastCarPlate, i));
            }

            report.CarPlate = "粤B22345";
            Assert.IsFalse(CarPlateComparer.CarPlateComparison(report.CarPlate, report.LastCarPlate, 0));
            Assert.IsFalse(CarPlateComparer.CarPlateComparison(report.CarPlate, report.LastCarPlate, 1));
            for (int i = 2; i < 8; i++)
            {
                Assert.IsTrue(CarPlateComparer.CarPlateComparison(report.CarPlate, report.LastCarPlate, i));
            }

            report.CarPlate = "粤B1234";
            Assert.IsFalse(CarPlateComparer.CarPlateComparison(report.CarPlate, report.LastCarPlate, 0));
            Assert.IsFalse(CarPlateComparer.CarPlateComparison(report.CarPlate, report.LastCarPlate, 1));

            report.CarPlate = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                Assert.IsFalse(CarPlateComparer.CarPlateComparison(report.CarPlate, report.LastCarPlate, i));
            }

            report.LastCarPlate = null;
            for (int i = 0; i < 8; i++)
            {
                Assert.IsFalse(CarPlateComparer.CarPlateComparison(report.CarPlate, report.LastCarPlate, i));
            }
        }
    }
}

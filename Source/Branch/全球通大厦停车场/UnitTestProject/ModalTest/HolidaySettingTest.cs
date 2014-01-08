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
    public class HolidaySettingTest
    {
        [TestMethod]
        public void TestHoliday()
        {
            HolidaySetting hs = new HolidaySetting();

            hs.SaturdayIsRest = true;
            hs.SundayIsRest = true;
            //2011-4-2 2011-4-3为周六日
            Assert.IsTrue(hs.IsInHoliday(new DateTime(2011, 4, 2, 1, 20, 15), new DateTime(2011, 4, 2, 12, 50, 50)));
            Assert.IsTrue(hs.IsInHoliday(new DateTime(2011, 4, 3, 1, 20, 15), new DateTime(2011, 4, 3, 12, 50, 50)));
            Assert.IsTrue(hs.IsInHoliday(new DateTime(2011, 4, 2, 1, 20, 15), new DateTime(2011, 4, 3, 12, 50, 50)));
            Assert.IsTrue(hs.IsInHoliday(new DateTime(2011, 4, 2, 0, 0, 0), new DateTime(2011, 4, 4, 0, 0, 0)));
            Assert.IsFalse(hs.IsInHoliday(new DateTime(2011, 4, 2, 0, 0, 0), new DateTime(2011, 4, 4, 1, 0, 0)));
            Assert.IsFalse(hs.IsInHoliday(new DateTime(2011, 4, 1, 23, 59, 59), new DateTime(2011, 4, 3, 12, 0, 0)));
            Assert.IsFalse(hs.IsInHoliday(new DateTime(2011, 4, 1, 11, 59, 59), new DateTime(2011, 4, 3, 12, 0, 0)));

            hs.SaturdayIsRest = false;
            hs.SundayIsRest = true;
            //2011-4-2 2011-4-3为周六日
            Assert.IsFalse(hs.IsInHoliday(new DateTime(2011, 4, 2, 1, 20, 15), new DateTime(2011, 4, 2, 12, 50, 50)));
            Assert.IsTrue(hs.IsInHoliday(new DateTime(2011, 4, 3, 1, 20, 15), new DateTime(2011, 4, 3, 12, 50, 50)));
            Assert.IsFalse(hs.IsInHoliday(new DateTime(2011, 4, 2, 1, 20, 15), new DateTime(2011, 4, 3, 12, 50, 50)));
            Assert.IsFalse(hs.IsInHoliday(new DateTime(2011, 4, 2, 0, 0, 0), new DateTime(2011, 4, 4, 0, 0, 0)));
            Assert.IsFalse(hs.IsInHoliday(new DateTime(2011, 4, 2, 0, 0, 0), new DateTime(2011, 4, 4, 1, 0, 0)));

            hs.SaturdayIsRest = true;
            hs.SundayIsRest = false;
            //2011-4-2 2011-4-3为周六日
            Assert.IsTrue(hs.IsInHoliday(new DateTime(2011, 4, 2, 1, 20, 15), new DateTime(2011, 4, 2, 12, 50, 50)));
            Assert.IsFalse(hs.IsInHoliday(new DateTime(2011, 4, 3, 1, 20, 15), new DateTime(2011, 4, 3, 12, 50, 50)));
            Assert.IsFalse(hs.IsInHoliday(new DateTime(2011, 4, 2, 1, 20, 15), new DateTime(2011, 4, 3, 12, 50, 50)));

            HolidayInfo holiday=new HolidayInfo ();
            //2011-4-3 到2011-4-5节假日
            holiday .StartDate =new DateTime (2011,4,3);
            holiday .EndDate =new DateTime (2011,4,5);
            hs.Holidays.Add(holiday);
            Assert.IsTrue(hs.IsInHoliday(new DateTime(2011, 4, 3), new DateTime(2011, 4, 5)));
            Assert.IsTrue (hs.IsInHoliday (new DateTime (2011,4,3,12,4,1),new DateTime (2011,4,5,15,0,0)));
            Assert.IsTrue(hs.IsInHoliday(new DateTime(2011, 4, 3), new DateTime(2011, 4, 6)));
            Assert.IsFalse(hs.IsInHoliday(new DateTime(2011, 4, 3), new DateTime(2011, 4, 6,0,1,0)));
        }
    }
}
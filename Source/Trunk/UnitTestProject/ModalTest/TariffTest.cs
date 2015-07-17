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
    public class TariffTest
    {
        [TestMethod]
        public void TariffPerTimeTest()
        {
            TariffPerTime tt = new TariffPerTime(30, 5);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 10, 0, 0)) == 0);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 9, 0, 0)) == 0);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 10, 10, 0)) == 0);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 10, 30, 0)) == 0);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 10, 31, 0)) == 5);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 12, 30, 0)) == 5);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 2, 10, 31, 0)) == 5);
        }

        [TestMethod]
        public void TariffPerDayTest()
        {
            TariffPerDay tt = new TariffPerDay(30, 5);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 9, 0, 0)) == 0);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 10, 0, 0)) == 0);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 10, 15, 0)) == 0);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 57), new DateTime(2011, 3, 1, 10, 30, 57)) == 0);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 57), new DateTime(2011, 3, 1, 10, 30, 58)) == 5);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 10, 31, 0)) == 5);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 12, 0, 0)) == 5);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 23, 59, 59)) == 5);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 2, 5, 59, 59)) == 5);  //没有超过24小时
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 2, 10, 1, 0)) == 10);  //起过24小时
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 3, 10, 59, 59)) == 15);
        }

        [TestMethod]
        public void TariffOfGuanZhouTest()
        {
            //广州收费时段,8:00-18:00 每小时收3元,18:00-8:00 每12小时收费10元(注意18点到8点有14个小时,所以在这个时段内超过10小时就收20元)
            TariffOfGuanZhou tt = new TariffOfGuanZhou();
            tt.DayTimezone = new TariffTimeZone(new TimeEntity(8, 0), new TimeEntity(18, 0), new ChargeUnit(60, 3));
            tt.NightTimezone = new TariffTimeZone(new TimeEntity(18, 0), new TimeEntity(8, 0), new ChargeUnit(720, 10));
            tt.FreeMinutes = 15;

            //没有每24小时限额
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 12, 16, 0)) == 3);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 18, 0, 0)) == 21);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 18, 1, 0)) == 31);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 8, 0, 0)) == 41);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 8, 2, 0)) == 44);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 11, 16, 0)) == 53);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 11, 18, 0)) == 53);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 18, 0, 0)) == 71);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 18, 1, 0)) == 81);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 11, 8, 0, 0)) == 91);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 11, 8, 2, 0)) == 94);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 11, 11, 16, 0)) == 103);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 11, 11, 18, 0)) == 103);

            tt.FeeOf24Hour = 40;  //每24小时限额40元
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 12, 16, 0)) == 3);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 18, 0, 0)) == 21);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 18, 1, 0)) == 31);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 8, 0, 0)) == 40);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 8, 2, 0)) == 40);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 11, 16, 0)) == 40);
            //从下面三个可以看出每24小时到点后重新开始收
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 11, 18, 0)) == 43);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 12, 16, 0)) == 43);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 12, 17, 0)) == 46);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 18, 0, 0)) == 61);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 18, 1, 0)) == 71);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 11, 8, 0, 0)) == 80);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 11, 8, 2, 0)) == 80);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 11, 11, 16, 0)) == 80);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 11, 11, 18, 0)) == 83);
        }

        [TestMethod]
        public void TariffOfTaiguhui()
        {
            TariffOfGuanZhou tt = new TariffOfGuanZhou();
            tt.DayTimezone = new TariffTimeZone(new TimeEntity(8, 0), new TimeEntity(22, 0), new ChargeUnit(30, 5));
            tt.NightTimezone = new TariffTimeZone(new TimeEntity(22, 0), new TimeEntity(8, 0), new ChargeUnit(30, 10));
            tt.NightTimezone.LimiteFee = 10;
            tt.FreeMinutes = 15;
            tt.FeeOf24Hour = 80;

            //2011-8-12 10:45	2011-8-12 8:08	16	0	0	0	0	30
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 8, 12, 8, 8, 0), new DateTime(2011, 8, 12, 10, 45, 0)) == 30);
            //2011-8-12 11:52	2011-8-12 7:59
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 8, 12, 7, 59, 0), new DateTime(2011, 8, 12, 11, 52, 0)) > 0);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 10, 30, 22, 3, 0), new DateTime(2011, 10, 31, 11, 03, 0)) > 0);
        }

        [TestMethod]
        public void TariffOfLimitationTest()
        {
            //有限额收费 入场30分钟免费,入场收5元
            TariffOfLimitation tt = new TariffOfLimitation();
            tt.FreeMinutes = 30;
            tt.FeeOf24Hour = 15;
            tt.FirstCharge = new ChargeUnit(60, 5);
            tt.RegularCharge = new ChargeUnit(60, 1);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 10, 0, 0)) == 0);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 10, 30, 0)) == 0);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 10, 31, 0)) == 5);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 12, 0, 0)) == 6);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 1, 12, 2, 0)) == 7);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 2, 0, 0, 0)) == 15);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 2, 0, 1, 0)) == 15);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 2, 10, 0, 0)) == 15);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 2, 10, 1, 0)) == 16);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 2, 15, 0, 0)) == 20);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 3, 0, 1, 0)) == 30);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 3, 10, 0, 0)) == 30);
            Assert.IsTrue(tt.CalculateFee(new DateTime(2011, 3, 1, 10, 0, 0), new DateTime(2011, 3, 3, 10, 1, 0)) == 31);
        }

        [TestMethod]
        public void TariffChanglongVIPTest()
        {
            TariffOfLimitation tariff = new TariffOfLimitation();
            tariff.FreeMinutes = 0;
            tariff.FirstCharge = new ChargeUnit(1439, 0);
            tariff.RegularCharge = new ChargeUnit(1440, 10);

            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 1, 8, 0, 0), new DateTime(2012, 1, 2, 7, 59, 0)) == 0);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 1, 8, 0, 0), new DateTime(2012, 1, 2, 8, 0, 0)) == 10);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 1, 8, 0, 0), new DateTime(2012, 1, 3, 7, 59, 0)) == 10);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 1, 8, 0, 0), new DateTime(2012, 1, 3, 8, 0, 0)) == 20);
        }

        [TestMethod]
        public void TariffOfTurningTest()
        {
            //免费0分钟，入场收取10元，过第二天6点改收30元，此后每过6点加收30元
            TariffOfTurning tariff = new TariffOfTurning();
            tariff.FreeMinutes = 0;
            tariff.FirstFee = 10;
            tariff.Turning = new TimeEntity(6, 0); //6点钟为转折点
            tariff.FeeOfTurning = 30;

            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 1, 8, 0, 0), new DateTime(2012, 1, 2, 5, 59, 0)) == 10);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 1, 8, 0, 0), new DateTime(2012, 1, 2, 6, 0, 0)) == 40);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 1, 8, 0, 0), new DateTime(2012, 1, 3, 5, 59, 0)) == 40);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 1, 8, 0, 0), new DateTime(2012, 1, 3, 6, 0, 0)) == 70);

            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 1, 3, 0, 0), new DateTime(2012, 1, 1, 5, 59, 0)) == 10);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 1, 3, 0, 0), new DateTime(2012, 1, 2, 6, 0, 0)) == 40);
        }

        [TestMethod]
        public void TariffOfTurningLimitedTest()
        {
            //免费0分钟，入场收取10元，过第二天6点改收30元，此后每过6点加收30元
            TariffOfTurningLimited tariff = new TariffOfTurningLimited();
            tariff.FreeMinutes = 0;
            tariff.FirstFee = 10;
            tariff.Turning = new TimeEntity(6, 0); //6点钟为转折点
            tariff.FeeOfTurning = 30;

            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 17, 2, 11, 13), new DateTime(2012, 1, 18, 6, 0, 0)) == 40);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 17, 2, 11, 13), new DateTime(2012, 1, 18, 6, 01, 43)) == 70);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 17, 6, 1, 50), new DateTime(2012, 1, 18, 6, 0, 0)) == 10);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 17, 6, 1, 50), new DateTime(2012, 1, 18, 6, 1, 50)) == 40);

            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 17, 6, 1, 50), new DateTime(2012, 1, 19, 6, 1, 50)) == 70);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2012, 1, 17, 6, 1, 50), new DateTime(2012, 1, 19, 6, 0, 13)) == 70);
        }

        [TestMethod]
        public void TariffOfDixiakongjian()  //地下空间费率测试
        {
            //停放3个小时内(含三个小时)收取2.5元/半小时,超过3个小时的部分按5元/半小时收取,每天22:00--8:00 2元/半小时,最高限价为10元,24小时限价65元
            TariffOfDixiakongjian tariff = new TariffOfDixiakongjian();
            tariff.FeeOf24Hour = 65;
            tariff.FreeMinutes = 15;
            tariff.FirstMinutes = 180;
            tariff.FirstFee = new ChargeUnit(30, 2.5m);
            tariff.RegularFee = new ChargeUnit(30, 5m);
            tariff.LimitationTimezone = new Ralid.Park.BusinessModel.Model.TimeZone(new TimeEntity(22, 0), new TimeEntity(8, 0));
            tariff.LimitationRegularFee = new ChargeUnit(30, 2);
            tariff.Limitation = 10;

            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 11, 18, 0)) == 0);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 11, 46, 0)) == 2.5m);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 12, 16, 0)) == 5);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 14, 16, 0)) == 15);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 9, 14, 17, 3)) == 20);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 11, 15, 0)) == 65);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 11, 16, 0), new DateTime(2011, 3, 10, 11, 17, 0)) == 67.5m);

            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 21, 16, 0), new DateTime(2011, 3, 9, 22, 0, 0)) == 5);
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 21, 16, 0), new DateTime(2011, 3, 9, 22, 16, 0)) == 7m); //跨时段分开计费
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 21, 16, 0), new DateTime(2011, 3, 9, 22, 17, 0)) == 7m); //跨时段分开计费
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 21, 16, 0), new DateTime(2011, 3, 10, 0, 16, 0)) == 15m); //跨时段分开计费
            Assert.IsTrue(tariff.CalculateFee(new DateTime(2011, 3, 9, 21, 46, 0), new DateTime(2011, 3, 9, 23, 46, 0)) == 10.5m); //跨时段分开计费

            Assert.IsTrue(tariff.CalculateFee(new DateTime(2013, 2, 4, 20, 26, 0), new DateTime(2013, 2, 5, 8, 16, 0)) == 22.5m); //跨时段分开计费

            Assert.IsTrue(tariff.CalculateFee(new DateTime(2013, 2, 4, 7, 35, 57), new DateTime(2013, 2, 4, 12, 58, 34)) == 37m); //跨时段分开计费
        }

        [TestMethod]
        public void TariffOfGuanZhouDiscountTest()
        {
            //太古汇收费时段入场免费30分钟,8:00-22:00 每15分钟收4元,22:00-8:00 每15分钟收1元 最高限价10元，24小时最高收费128元
            TariffOfGuanZhou tt = new TariffOfGuanZhou();
            tt.FreeMinutes = 30;
            tt.DayTimezone = new TariffTimeZone(new TimeEntity(8, 0), new TimeEntity(22, 0), new ChargeUnit(15, 4));
            tt.NightTimezone = new TariffTimeZone(new TimeEntity(22, 0), new TimeEntity(8, 0), new ChargeUnit(15, 1));
            tt.NightTimezone.LimiteFee = 10;
            tt.FeeOf24Hour = 128;

            int curHour = 0;

            //第一条为费用，第二条为优惠金额,第三条为使用优惠时数
            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 15, 12, 04), new DateTime(2014, 9, 12, 18, 23, 03)) == 180);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 15, 12, 04), new DateTime(2014, 9, 12, 18, 23, 03),3,out curHour) == 48);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 15, 12, 04), new DateTime(2014, 9, 12, 15, 15, 53)) == 132);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 15, 12, 04), new DateTime(2014, 9, 12, 15, 15, 53), 1, out curHour) == 4);
            Assert.IsTrue(curHour == 1);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 19, 15, 48, 47), new DateTime(2014, 9, 19, 17, 03, 12)) == 20);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 19, 15, 48, 47), new DateTime(2014, 9, 19, 17, 03, 12), 1, out curHour) == 16);
            Assert.IsTrue(curHour == 1);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 11, 10, 07, 00)) == 46);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 11, 10, 07, 00), 3, out curHour) == 36);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 11, 12, 55, 16)) == 84);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 11, 12, 55, 16), 3, out curHour) == 48);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 11, 09, 50, 08)) == 36);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 11, 09, 50, 08), 3, out curHour) == 36);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 12, 06, 02, 42)) == 128);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 12, 06, 02, 42), 3, out curHour) == 0);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 12, 12, 04, 29)) == 200);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 12, 12, 04, 29), 3, out curHour) == 48);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 12, 06, 10, 06)) == 128);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 12, 06, 10, 06), 3, out curHour) == 0);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 12, 03, 10, 12)) == 128);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 07, 04, 27), new DateTime(2014, 9, 12, 03, 10, 12), 3, out curHour) == 0);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 11, 12, 07, 48)) == 78);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 11, 12, 07, 48), 3, out curHour) == 48);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 12, 09, 08, 54)) == 158);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 12, 09, 08, 54), 3, out curHour) == 22);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 12, 09, 08, 54)) == 158);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 12, 09, 08, 54), 2, out curHour) == 20);
            Assert.IsTrue(curHour == 2);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 12, 12, 10, 14)) == 206);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 12, 12, 10, 14), 3, out curHour) == 48);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 12, 03, 09, 25)) == 128);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 12, 03, 09, 25), 3, out curHour) == 0);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 05, 06, 08)) == 1285);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 05, 06, 08), 3, out curHour) == 5);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 08, 06, 08)) == 1294);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 08, 06, 08), 3, out curHour) == 10);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 05, 06, 08)) == 1285);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 05, 06, 08), 27, out curHour) == 133);
            Assert.IsTrue(curHour == 27);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 08, 06, 08)) == 1294);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 08, 06, 08), 27, out curHour) == 138);
            Assert.IsTrue(curHour == 27);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 21, 10, 00), new DateTime(2014, 9, 11, 22, 09, 00)) == 17);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 21, 10, 00), new DateTime(2014, 9, 11, 22, 09, 00), 1, out curHour) == 17);
            Assert.IsTrue(curHour == 1);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 21, 28, 00), new DateTime(2014, 9, 11, 22, 27, 00)) == 14);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 21, 28, 00), new DateTime(2014, 9, 11, 22, 27, 00), 1, out curHour) == 14);
            Assert.IsTrue(curHour == 1);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 18, 10, 00), new DateTime(2014, 9, 11, 22, 11, 00)) == 65);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 18, 10, 00), new DateTime(2014, 9, 11, 22, 11, 00), 3, out curHour) == 49);
            Assert.IsTrue(curHour == 3);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 21, 28, 00), new DateTime(2014, 9, 11, 22, 29, 00)) == 14);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 21, 28, 00), new DateTime(2014, 9, 11, 22, 29, 00), 1, out curHour) == 14);
            Assert.IsTrue(curHour == 1);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 05, 06, 08)) == 1285);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 05, 06, 08), 23, out curHour) == 123);
            Assert.IsTrue(curHour == 23);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 08, 06, 08)) == 1294);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 11, 04, 05, 08), new DateTime(2014, 9, 21, 08, 06, 08), 23, out curHour) == 132);
            Assert.IsTrue(curHour == 23);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 19, 21, 34, 35), new DateTime(2014, 9, 20, 19, 35, 01)) == 128);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 19, 21, 34, 35), new DateTime(2014, 9, 20, 19, 35, 01), 20, out curHour) == 120);
            Assert.IsTrue(curHour == 20);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 9, 18, 07, 32, 10), new DateTime(2014, 9, 19, 08, 33, 06)) == 142);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 9, 18, 07, 32, 10), new DateTime(2014, 9, 19, 08, 33, 06), 20, out curHour) == 132);
            Assert.IsTrue(curHour == 20);

            Assert.IsTrue(tt.CalculateFee(new DateTime(2014, 11, 05, 21, 35, 51), new DateTime(2014, 11, 06, 14, 56, 48)) == 128);
            Assert.IsTrue(tt.CalculateDiscountFee(new DateTime(2014, 11, 05, 21, 35, 51), new DateTime(2014, 11, 06, 14, 56, 48), 3, out curHour) == 46);
            Assert.IsTrue(curHour == 3);

        }
    }
}

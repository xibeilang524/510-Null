using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary.LED;
using System.IO ;
#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#endif

namespace TestProject1.GeneralLibraryTest
{
    [TestClass]
    public class ParkFullLedTest
    {
        [TestMethod]
        public void SendDataTest()
        {
            ParkFullLed led = new ParkFullLed();
            led.ID = 1;
            led.Mark = 0x10;
            led.Width = 4;
            led.Height = 2;
            led.ShowMode = 4;
            led.MoveSpeed = 1;
            led.Pause = 0;
            led.Append = 0;
            led.Color = 1;

            byte[] data = led.GetSendData("  1234秒123456次");
            byte[] cdata = new byte[]{
                0x55, 0x1D, 0x00, 0x01, 0x00 ,0x10 ,
                0x04, 0x01 ,0x00, 0x00 ,0x08 ,0x02, 0x01, 0x00 ,
                0x20, 0x20 ,0x31, 0x32 ,0x33 ,0x34 ,0xC3, 0xEB ,0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0xB4, 0xCE, 
                0x0E ,0xF9, 0xaa
            };

            Assert.IsTrue(data.Length == cdata.Length);
            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] == cdata[i], string.Format("data[{0}]==cdata[{1}]出错,{2}!={3}", i, i, data[i], cdata[i]));
            }

            data = led.GetSendData("欢迎领导莅临指导");
            cdata = new byte[]
            {
                0x55 ,0x1D, 0x00, 0x01 ,0x00 ,0x10,
                0x04,0x01, 0x00, 0x00, 0x08 ,0x02, 0x01 ,0x00,
                0xBB, 0xB6, 0xD3 ,0xAD ,0xC1 ,0xEC ,0xB5, 0xBC ,0xDD ,0xB0 ,0xC1, 0xD9, 0xD6 ,0xB8, 0xB5, 0xBC ,
                0x3F ,0x95,0xAA
            };
            Assert.IsTrue(data.Length == cdata.Length);
            for (int i = 0; i < data.Length; i++)
            {
                Assert.IsTrue(data[i] == cdata[i], string.Format("data[{0}]==cdata[{1}]出错,{2}!={3}", i, i, data[i], cdata[i]));
            }
        }
    }
}

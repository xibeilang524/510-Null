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
    public class TimeEntityTest
    {
        [TestMethod]
        public void CanAddMinutes()
        {
            TimeEntity te = new TimeEntity();
            TimeEntity ret = te.AddMinutes(-30);
            Assert.IsTrue(ret.Hour == 23 && ret.Minute == 30);

            te = new TimeEntity(23, 30);
            ret = te.AddMinutes(40);  //0:10
            Assert.IsTrue(ret.Hour == 0 && ret.Minute == 10);
            ret = te.AddMinutes(-142535356);
            Assert.IsTrue(ret != null);

            ret = te.AddMinutes(0);
            Assert.IsTrue(ret.Hour == 23 && ret.Minute == 30);
            ret = te.AddMinutes(30);
            Assert.IsTrue(ret.Hour == 0 && ret.Minute == 0);
            ret = te.AddMinutes(24 * 60 + 30);
            Assert.IsTrue(ret.Hour == 0 && ret.Minute == 0);
            ret = te.AddMinutes(-(23 * 60 + 30));
            Assert.IsTrue(ret.Hour == 0 && ret.Minute == 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;

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
    
    /// <summary>
    /// 测试Ralid.BussinessLayer.ProviderFactory
    /// </summary>
    [TestClass]
    public class ProviderFactoryTest
    {
        private string connStr = "Data Source=tfs;Initial Catalog=netparking;Persist Security Info=True;User ID=sa;Password=ralid";

        [TestMethod]
        public void CreateProvider()
        {
            IAlarmProvider p1 = ProviderFactory.Create<IAlarmProvider>(connStr);
            Assert.IsTrue(p1 != null);

            ICardChargeRecordProvider p2 = ProviderFactory.Create<ICardChargeRecordProvider>(connStr);
            Assert.IsTrue(p2 != null);

            ICardDeferRecordProvider p3 = ProviderFactory.Create<ICardDeferRecordProvider>(connStr);
            Assert.IsTrue(p3 != null);

            ICardDisableEnableRecordProvider p4 = ProviderFactory.Create<ICardDisableEnableRecordProvider>(connStr);
            Assert.IsTrue(p4 != null);

            ICardEventProvider p5 = ProviderFactory.Create<ICardEventProvider>(connStr);
            Assert.IsTrue(p5 != null);

            ICardLostRestoreRecordProvider p6 = ProviderFactory.Create<ICardLostRestoreRecordProvider>(connStr);
            Assert.IsTrue(p6 != null);

            ICardProvider p9 = ProviderFactory.Create<ICardProvider>(connStr);
            Assert.IsTrue(p9 != null);

            ICardRecycleRecordProvider p10 = ProviderFactory.Create<ICardRecycleRecordProvider>(connStr);
            Assert.IsTrue(p10 != null);

            ICardReleaseRecordProvider p11 = ProviderFactory.Create<ICardReleaseRecordProvider>(connStr);
            Assert.IsTrue(p11 != null);

            IEntranceProvider p12 = ProviderFactory.Create<IEntranceProvider>(connStr);
            Assert.IsTrue(p12 != null);

            IOperatorLogProvider p13 = ProviderFactory.Create<IOperatorLogProvider>(connStr);
            Assert.IsTrue(p13 != null);

            IOperatorProvider p14 = ProviderFactory.Create<IOperatorProvider>(connStr);
            Assert.IsTrue(p14 != null);

            IParkProvider p16 = ProviderFactory.Create<IParkProvider>(connStr);
            Assert.IsTrue(p16 != null);

            IRoleProvider p17 = ProviderFactory.Create<IRoleProvider>(connStr);
            Assert.IsTrue(p17 != null);

            ISnapShotProvider p18 = ProviderFactory.Create<ISnapShotProvider>(connStr);
            Assert.IsTrue(p18 != null);

            ISysParameterProvider p19 = ProviderFactory.Create<ISysParameterProvider>(connStr);
            Assert.IsTrue(p19 != null);

            IVideoSourceProvider p21 = ProviderFactory.Create<IVideoSourceProvider>(connStr);
            Assert.IsTrue(p21 != null);

            IWaitingCommandProvider p24 = ProviderFactory.Create<IWaitingCommandProvider>(connStr);
            Assert.IsTrue(p24 != null);

            IWorkstationProvider p25 = ProviderFactory.Create<IWorkstationProvider>(connStr);
            Assert.IsTrue(p25 != null);
        }
    }
}

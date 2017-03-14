using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
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

namespace UnitTestProject.BllTest
{
    /// <summary>
    /// ETCPaymentBllTest 的摘要说明
    /// </summary>
    [TestClass]
    public class ETCPaymentBllTest
    {
        [TestMethod]
        public void CanInsertTest()
        {
            var bll = new ETCPaymentRecordBll(StaticConnectString.ConnStr);
            var record = new ETCPaymentRecord()
            {
                LaneNo ="12",
                Device =0,
                AddTime =DateTime.Now ,
                Data ="just test data",
            };
            CommandResult ret = bll.Insert(record);
            Assert.IsTrue(ret.Result == ResultCode.Successful);
            Assert.IsTrue(record.ID > 0);
            //var con=new
        }
    }
}

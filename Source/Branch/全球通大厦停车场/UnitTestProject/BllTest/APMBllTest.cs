using Ralid.Park.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;

namespace UnitTestProject.BllTest
{
    /// <summary>
    ///这是 APMBllTest 的测试类，旨在
    ///包含所有 APMBllTest 单元测试
    ///</summary>
    [TestClass()]
    public class APMBllTest
    {
        private TestContext testContextInstance;


        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///UpdateActiveDateTime 的测试
        ///</summary>
        [TestMethod()]
        public void UpdateActiveDateTimeTest()
        {
            APM apm = new APM()
            {
                SerialNum = "terogdge",
                Status = Ralid.Park.BusinessModel.Enum.APMStatus.Normal,
                CheckOutTime = new DateTime(2000, 1, 1)
            };

            APMBll bll = new APMBll(StaticConnectString.ConnStr);
            CommandResult ret = bll.Insert(apm);
            Assert.IsTrue(ret.Result == ResultCode.Successful);

            ret = bll.UpdateActiveDateTime(apm, new DateTime(2011, 1, 1));
            Assert.IsTrue(ret.Result == ResultCode.Successful);
            APM apm1 = bll.GetByID(apm.ID).QueryObject;
            Assert.IsTrue(apm1 != null && apm1.ActiveDateTime.Value == new DateTime(2011, 1, 1));

            ret = bll.Delete(apm1);
            Assert.IsTrue(ret.Result == ResultCode.Successful);
        }
    }
}

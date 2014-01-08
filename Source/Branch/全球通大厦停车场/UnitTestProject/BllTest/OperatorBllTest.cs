using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    [TestClass]
    public class OperatorBllTest
    {
        [TestMethod]
        public void GetOperatorByIDTest()
        {
            string optID = "admin";
            OperatorBll bll = new OperatorBll(StaticConnectString.ConnStr);
            optID = "notExistsOperator";
            QueryResult<OperatorInfo> result = bll.GetByID(optID);
            Assert.IsTrue(result.Result != ResultCode.Successful);
            Assert.IsTrue(result.QueryObject == null);
        }

        [TestMethod]
        public void InsertOperator()
        {
            OperatorBll bll = new OperatorBll(StaticConnectString.ConnStr);
            OperatorInfo op = new OperatorInfo
            {
                OperatorID = "testOperator55",
                OperatorName = "testOperator55",
                OperatorNum = 100,
                Password = "123",
                RoleID = "收费操作员"
            };
            CommandResult result = bll.Insert(op);
            Assert.IsTrue(result.Result == ResultCode.Successful);

            QueryResult<OperatorInfo> ret1 = bll.GetByID("testOperator55");
            Assert.IsTrue(ret1.Result == ResultCode.Successful);
            Assert.IsTrue(ret1.QueryObject != null);

            bll.Delete(op);

            ret1 = bll.GetByID("testOperator55");
            Assert.IsTrue(ret1.Result != ResultCode.Successful);
            Assert.IsTrue(ret1.QueryObject == null);
        }

        [TestMethod]
        public void ChangePassword()
        {
            OperatorBll bll = new OperatorBll(StaticConnectString.ConnStr);
            OperatorInfo op = new OperatorInfo
            {
                OperatorID = "testOperator55",
                OperatorName = "testOperator55",
                OperatorNum = 100,
                Password = "123",
                RoleID = "收费操作员"
            };

            CommandResult ret1 = bll.Insert(op);
            Assert.IsTrue(ret1.Result == ResultCode.Successful);

            OperatorInfo op1 = bll.GetByID("testOperator55").QueryObject;

            op1.Password = "123456";
            bll.Update(op1);

            OperatorInfo op2 = bll.GetByID("testOperator55").QueryObject;
            Assert.IsTrue(op.Password != op2.Password);
            Assert.AreEqual(op2.Password, "123456");
            bll.Delete(op2);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InsertNull()
        {
            OperatorBll bll = new OperatorBll(StaticConnectString.ConnStr);
            CommandResult ret = bll.Insert(null);
            Assert.IsTrue(ret.Result != ResultCode.Successful);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteNull()
        {
            OperatorBll bll = new OperatorBll(StaticConnectString.ConnStr);
            CommandResult ret = bll.Delete(null);
            Assert.IsTrue(ret.Result != ResultCode.Successful);
        }
    }
}

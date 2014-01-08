using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary.SoftDog ;

#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#endif

namespace TestProject1
{
    [TestClass]
    public class DSEncryptTest
    {
        [TestMethod]
        public void EncryptTest()
        {
            DSEncrypt ds = new DSEncrypt();
            string encrypted = ds.Encrypt("123");
            Assert.IsTrue(encrypted == "Y ;");
            string back = ds.Encrypt(encrypted);
            Assert.IsTrue(back == "123");

            encrypted = ds.Encrypt("ralid");
            Assert.IsTrue(encrypted == "c+4 G");
            back = ds.Encrypt(encrypted);
            Assert.IsTrue(back == "ralid");

            //采用不同的加密因子,加密后的结果不一样,但两次加密都会返回明文
            ds = new DSEncrypt("br");
            encrypted = ds.Encrypt("123");
            back = ds.Encrypt(encrypted);
            Assert.IsTrue(back == "123");

            encrypted = ds.Encrypt("ralid");
            back = ds.Encrypt(encrypted);
            Assert.IsTrue(back == "ralid");

            //加密中文字串不能再解密
            encrypted = ds.Encrypt("中文测试");
            back = ds.Encrypt(encrypted);
            Assert.IsTrue(back != "中文测试");

            DSEncrypt ds2 = new DSEncrypt("lijianhua");
            string str1 = ds.Encrypt("ralid");
            string str2 = ds2.Encrypt("ralid");
            Assert.IsTrue(str1 != str2);
        }
    }
}

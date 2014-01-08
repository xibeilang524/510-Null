using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary;

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
    public class BinaryConverterTest
    {
        [TestMethod ]
        public void TestIntConvert()
        {
            int i=2000;
            byte[] data=SEBinaryConverter .IntToBytes (i);
            int j=SEBinaryConverter .BytesToInt (data);

            Assert.IsTrue (i==j);

            i = -330;
            data = SEBinaryConverter.IntToBytes(i);
            j = SEBinaryConverter.BytesToInt(data);
            Assert.IsTrue(i == j);

            i = 94949491;
            data = SEBinaryConverter.IntToBytes(i);
            j = SEBinaryConverter.BytesToInt(data);
            Assert.IsTrue(i == j);
            Console.Write(i.ToString());
        }

        [TestMethod]
        public  void TestShortConvert()
        {
            short s1, s2;
            byte[] data = null;

            s1 = 3000;
            data = SEBinaryConverter.ShortToBytes(s1);
            s2 = SEBinaryConverter.BytesToShort(data);
            Assert.IsTrue(s1 == s2);

            s1 = -32768;
            data = SEBinaryConverter.ShortToBytes(s1);
            s2 = SEBinaryConverter.BytesToShort(data);
            Assert.IsTrue(s1 == s2);

            int i = 65535;
            data = SEBinaryConverter.ShortToBytes((short)i);
            s2 = SEBinaryConverter.BytesToShort(data);
            Assert.IsTrue(s2 == (short)i);
            Console.WriteLine(s2.ToString());
        }

        [TestMethod]
        public void TestLngConvert()
        {
            long a, b;
            byte[] data = null;

            a = 969419446969496;
            data = SEBinaryConverter.LongToBytes(a);
            b = SEBinaryConverter.BytesToLong(data);
            Assert.IsTrue(a == b);

            a = -494641949416;
            data = SEBinaryConverter.LongToBytes(a);
            b = SEBinaryConverter.BytesToLong(data);
            Assert.IsTrue(a == b);
        }
    }
}

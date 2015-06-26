using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ralid.OpenCard.OpenCardService;

namespace TestProject1
{
    [TestClass]
    public class YitingBufferTest
    {
        [TestMethod]
        public void CanRead()
        {
            YiTingBuffer buffer = new YiTingBuffer();
            YiTingPacket p = buffer.Read();
            Assert.IsNull(p);
            buffer.Write(new byte[] { 0x78, 0xB6 });
            p = buffer.Read();
            Assert.IsNull(p);
            buffer.Write(new byte[7]);
            p = buffer.Read();
            Assert.IsNull(p);
            buffer.Write(new byte[] { 0x21, 0xD3 });
            p = buffer.Read();
            Assert.IsNotNull(p);
            p = buffer.Read();
            Assert.IsNull(p);
        }

        [TestMethod]
        public void CanRead1()
        {
            YiTingBuffer buffer = new YiTingBuffer();
            YiTingPacket p = buffer.Read();
            Assert.IsNull(p);
            buffer.Write(new byte[] { 0x11, 0x55 });
            buffer.Write(new byte[] { 0x78, 0xB6 });
            p = buffer.Read();
            Assert.IsNull(p);
            buffer.Write(new byte[7]);
            p = buffer.Read();
            Assert.IsNull(p);
            buffer.Write(new byte[] { 0x21, 0xD3 });
            p = buffer.Read();
            Assert.IsNotNull(p);
            p = buffer.Read();
            Assert.IsNull(p);
        }
    }
}

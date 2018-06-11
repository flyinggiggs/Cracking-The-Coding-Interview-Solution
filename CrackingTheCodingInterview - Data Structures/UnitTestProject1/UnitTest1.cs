using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Chapter3___Trees_and_Graphs.Program;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var testQueue = new MyQueue();
            testQueue.Add(1);
            testQueue.Add(2);

            int actualFirstItem = testQueue.Peek();
            int expected = 1;

            Assert.AreEqual(expected, actualFirstItem, "Both are same");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.Mathematics;

namespace UnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddMethodTest()
        {
            double res = Mathematics.Add(10, 10);
            Assert.AreEqual(20, res);
        }
    }
}
namespace VendingSystemTests
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var myMath = new MyMath();
            int result = myMath.Plus(2, 2);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var myMath = new MyMath();
            int result = myMath.Plus(10, 5);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var myMath = new MyMath();
            int result = myMath.Plus(5, 0);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var myMath = new MyMath();
            int result = myMath.Minus(10, 2);
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var myMath = new MyMath();
            int result = myMath.Minus(7, 7);
            Assert.AreEqual(0, result);
        }
    }
}

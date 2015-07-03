using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.Backend.Controller;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.Backend.Tests.Controller
{
    [TestClass]
    public class BackendTimerTest : AssertTestClass
    {
        [TestMethod, TestCategory("Backend")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IBackendTimer), typeof (BackendTimer));
        }

        [TestMethod, TestCategory("Backend")]
        public void TestGetsBackendControllerPassedIn()
        {
            var mockController = new Mock<IBackendController>();

            var timer = new BackendTimer(mockController.Object);
            Assert.AreSame(mockController.Object, timer.Controller);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestDefaultIntervalIsSetupCorrectly_AndSetInterval()
        {
            var timer = new BackendTimer(null);
            var wrappedTimer = timer.Timer;

            Assert.AreEqual(60000, timer.Interval);
            Assert.AreEqual(60000, wrappedTimer.Interval);

            timer.Interval = 500;

            Assert.AreEqual(500, timer.Interval);
            Assert.AreEqual(500, wrappedTimer.Interval);

            timer.Interval = 6540;

            Assert.AreEqual(6540, timer.Interval);
            Assert.AreEqual(6540, wrappedTimer.Interval);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestStartEnablesTheTimer_StopDisables()
        {
            var mockController = new Mock<IBackendController>();

            var timer = new BackendTimer(mockController.Object) {Interval = 500};
            var wrappedTimer = timer.Timer;

            Assert.IsFalse(wrappedTimer.Enabled);

            timer.Start();

            Assert.IsTrue(wrappedTimer.Enabled);

            timer.Stop();

            Assert.IsFalse(wrappedTimer.Enabled);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestUpdateQuoteHistoryCalledOnBackendControllerAfterInterval()
        {
            var mockController = new Mock<IBackendController>();

            var timer = new BackendTimer(mockController.Object) {Interval = 50};

            mockController.Verify(m => m.UpdateCompanyQuotes(), Times.Never);

            Thread.Sleep(90);

            mockController.Verify(m => m.UpdateCompanyQuotes(), Times.Never);

            timer.Start();
            Thread.Sleep(90);

            mockController.Verify(m => m.UpdateCompanyQuotes(), Times.Once);
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.Backend.Controller;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.Backend.Tests.Controller
{
    [TestClass]
    public class BackendLockTest : AssertTestClass
    {
        private const int TestingPort = 52963;

        [TestMethod]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IBackendLock), typeof (BackendLock));
        }

        [TestMethod]
        public void TestGetsPortPassedIn()
        {
            var backendLock = new BackendLock(TestingPort);

            Assert.AreEqual(TestingPort, backendLock.Port);
        }

        [TestMethod]
        public void TestLockReturnsTrue_FirstLockOnPort()
        {
            var backendLock = new BackendLock(TestingPort);

            try
            {
                Assert.IsTrue(backendLock.Lock());
            }
            finally
            {
                backendLock.WrappedListener.Stop();
            }
        }

        [TestMethod]
        public void TestLockReturnsFalse_LockExistsOnPort()
        {
            var backendLock1 = new BackendLock(TestingPort);
            var backendLock2 = new BackendLock(TestingPort);

            try
            {
                Assert.IsTrue(backendLock1.Lock());
                Assert.IsFalse(backendLock2.Lock());
            }
            finally
            {
                backendLock1.WrappedListener.Stop();
            }
        }

        [TestMethod]
        public void TestCallingUnlockDoesNotThrowException_LockHasNotBeenCalled()
        {
            var backendLock = new BackendLock(TestingPort);

            try
            {
                backendLock.Unlock();
            }
            catch (Exception)
            {
                Assert.Fail("No exception should be thrown.");
            }
        }

        [TestMethod]
        public void TestUnlockAllowsOtherLocksToLockTheSocket()
        {
            var backendLock1 = new BackendLock(TestingPort);
            var backendLock2 = new BackendLock(TestingPort);

            try
            {
                Assert.IsTrue(backendLock1.Lock());

                backendLock1.Unlock();

                Assert.IsTrue(backendLock2.Lock());
            }
            finally
            {
                backendLock1.WrappedListener.Stop();
                backendLock2.WrappedListener.Stop();
            }
        }
    }
}
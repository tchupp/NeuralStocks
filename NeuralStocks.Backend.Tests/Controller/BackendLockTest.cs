using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.Backend.Controller;
using NeuralStocks.Backend.Tests.Testing;

namespace NeuralStocks.Backend.Tests.Controller
{
    [TestClass]
    public class BackendLockTest : AssertTestClass
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IBackendLock), typeof (BackendLock));
        }

        [TestMethod]
        public void TestGetsPortPassedIn()
        {
            const int expectedPort = 58525;
            var backendLock = new BackendLock(expectedPort);

            Assert.AreEqual(expectedPort, backendLock.Port);
        }

        [TestMethod]
        public void TestLockReturnsTrue_FirstLockOnPort()
        {
            const int expectedPort = 58525;
            var backendLock = new BackendLock(expectedPort);

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
            const int expectedPort = 58525;
            var backendLock1 = new BackendLock(expectedPort);
            var backendLock2 = new BackendLock(expectedPort);

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
            const int expectedPort = 58525;
            var backendLock = new BackendLock(expectedPort);

            try
            {
                backendLock.Unlock();
            }
            catch (Exception ex)
            {
                Assert.Fail("No exception should be thrown.");
            }
        }

        [TestMethod]
        public void TestUnlockAllowsOtherLocksToLockTheSocket()
        {
            const int expectedPort = 58525;
            var backendLock1 = new BackendLock(expectedPort);
            var backendLock2 = new BackendLock(expectedPort);

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
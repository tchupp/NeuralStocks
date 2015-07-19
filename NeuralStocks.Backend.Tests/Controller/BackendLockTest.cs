using System;
using NeuralStocks.Backend.Controller;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace NeuralStocks.Backend.Tests.Controller
{
    [TestFixture]
    public class BackendLockTest : AssertTestClass
    {
        private const int TestingPort = 52963;

        [Test]
        [Category("Backend")]
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

        [Test]
        [Category("Backend")]
        public void TestGetsPortPassedIn()
        {
            var backendLock = new BackendLock(TestingPort);

            Assert.AreEqual(TestingPort, backendLock.Port);
        }

        [Test]
        [Category("Backend")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IBackendLock), typeof (BackendLock));
        }

        [Test]
        [Category("Backend")]
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

        [Test]
        [Category("Backend")]
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

        [Test]
        [Category("Backend")]
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
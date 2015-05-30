using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeuralStocksTests.Testing
{
    public class MoreAssert
    {
        public static void ImplementsInterface(Type expectedInterface, Type expectedClass)
        {
            Assert.IsTrue(expectedClass.IsClass, "Class is not actually a class");

            var interfaces = expectedClass.GetInterfaces();
            Assert.AreEqual(1, interfaces.Count(), "There is not exactly one interface");

            Assert.IsTrue(interfaces.Contains(expectedInterface), "");
        }
    }
}
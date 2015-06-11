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
            Assert.IsTrue(expectedInterface.IsInterface, "Interface is not actually an Interface");

            var interfaces = expectedClass.GetInterfaces();
            Assert.AreEqual(1, interfaces.Count(), "There is not exactly one interface");

            Assert.IsTrue(interfaces.Contains(expectedInterface), "");
        }

        public static void PrivateContructor(Type expectedClass)
        {
            Assert.IsTrue(expectedClass.IsClass, "Class is not actually a class");

            var constructors = expectedClass.GetConstructors();
            Assert.AreEqual(0, constructors.Length, "Class has at least one public constructor");
        }

        public static T AssertIsOfTypeAndGet<T>(object obj)
        {
            Assert.IsNotNull(obj, "Object was null instead of type " + typeof (T));
            Assert.IsInstanceOfType(obj, typeof (T),
                "Object was type: " + obj.GetType() + ", not: " + typeof (T));

            return (T) obj;
        }
    }
}
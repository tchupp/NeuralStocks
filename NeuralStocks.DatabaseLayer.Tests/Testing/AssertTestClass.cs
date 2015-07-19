using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq.Language.Flow;

namespace NeuralStocks.DatabaseLayer.Tests.Testing
{
    public class AssertTestClass
    {
        protected static void AssertImplementsInterface(Type expectedInterface, Type expectedClass)
        {
            Assert.IsTrue(expectedClass.IsClass, "Class is not actually a class");
            Assert.IsTrue(expectedInterface.IsInterface, "Interface is not actually an Interface");

            var interfaces = expectedClass.GetInterfaces();
            Assert.AreEqual(1, interfaces.Count(), "There is not exactly one interface");

            Assert.IsTrue(interfaces.Contains(expectedInterface), "");
        }

        protected static void AssertPrivateContructor(Type expectedClass)
        {
            Assert.IsTrue(expectedClass.IsClass, "Class is not actually a class");

            var constructors = expectedClass.GetConstructors();
            Assert.AreEqual(0, constructors.Length, "Class has at least one public constructor");
        }

        protected static T AssertIsOfTypeAndGet<T>(object obj)
        {
            Assert.IsNotNull(obj, "Object was null instead of type " + typeof (T));
            Assert.IsInstanceOfType(obj, typeof (T),
                "Object was type: " + obj.GetType() + ", not: " + typeof (T));

            return (T) obj;
        }
    }

    public static class MoqExtentions
    {
        public static void ReturnsInOrder<T, TResult>(this ISetup<T, TResult> setup, params TResult[] results)
            where T : class
        {
            setup.Returns(new Queue<TResult>(results).Dequeue);
        }
    }
}
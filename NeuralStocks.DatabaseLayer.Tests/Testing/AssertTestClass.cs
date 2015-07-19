using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq.Language.Flow;

namespace NeuralStocks.DatabaseLayer.Tests.Testing
{
    public class AssertTestClass
    {
        protected static string TestDatabaseName = "TestStocksDatabase.sqlite";
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

        protected static void AssertFieldIsOfTypeAndSet<T>(object obj, string fieldName, T newValue)
        {
            Assert.IsNotNull(obj, "Object was null");

            var type = obj.GetType();
            var field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(field, string.Format("Field named {0} was null", fieldName));

            var expectedType = typeof (T);
            var fieldType = field.FieldType;
            Assert.AreSame(expectedType, fieldType,
                string.Format("Field was not of type: {0}, but was: {1}", expectedType, fieldType));

            field.SetValue(obj, newValue);
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
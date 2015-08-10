using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq.Language.Flow;
using NUnit.Framework;

namespace NeuralStocks.DatabaseLayer.Tests.Testing
{
    public class AssertTestClass
    {
        protected const string TestDatabaseName = "TestStocksDatabase.sqlite";

        protected static void AssertImplementsInterface(Type expected, Type actual)
        {
            Assert.IsTrue(actual.IsClass, string.Format("{0} is not actually a class", actual));
            Assert.IsTrue(expected.IsInterface, string.Format("{0} is not actually an Interface", expected));

            var interfaces = actual.GetInterfaces();
            Assert.AreEqual(1, interfaces.Count(), "There is not exactly one interface");

            Assert.IsTrue(interfaces.Contains(expected),
                string.Format("{0} did not contain interface {1}", actual, expected));
        }

        protected static void AssertMethodHasAttribute(MethodInfo method, Type attributeType)
        {
            Assert.IsTrue(method.GetCustomAttributes(attributeType, false).Any());
        }

        protected static void AssertExtendsClass(Type expected, Type actual)
        {
            Assert.IsTrue(actual.IsClass, "Sub-class is not actually a class");
            Assert.IsTrue(expected.IsClass, "Super-class is not actually a class");

            Assert.IsTrue(actual.IsSubclassOf(expected),
                string.Format("{0} was not a subclass of {1}", actual, expected));
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
            Assert.AreEqual(typeof (T), obj.GetType(), "Object was type: " + obj.GetType() + ", not: " + typeof (T));

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
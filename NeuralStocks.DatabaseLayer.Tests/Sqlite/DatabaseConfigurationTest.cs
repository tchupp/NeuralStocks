using System;
using System.IO;
using System.Linq;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;

namespace NeuralStocks.DatabaseLayer.Tests.Sqlite
{
    [TestFixture]
    public class DatabaseConfigurationTest : AssertTestClass
    {
        [Test]
        [Category("Database")]
        public void TestFullDatabaseFileName()
        {
            var solutionDirectory =
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))));
            const string fileName = "NeuralStocksDatabase.sqlite";
            var databaseDirectory = solutionDirectory + "\\Database\\" + fileName;

            var databaseFileName = DatabaseConfiguration.FullDatabaseFileName;
            Assert.AreEqual(databaseDirectory, databaseFileName);
        }

        [Test]
        [Category("Database")]
        public void TestIsStaticClass()
        {
            var databaseConfigurationType = typeof (DatabaseConfiguration);

            AssertPrivateContructor(databaseConfigurationType);
            Assert.IsTrue(databaseConfigurationType.IsAbstract);
            Assert.IsTrue(databaseConfigurationType.IsSealed);
            Assert.IsTrue(databaseConfigurationType.IsClass);
            Assert.IsTrue(!databaseConfigurationType.GetConstructors().Any());
        }
    }
}
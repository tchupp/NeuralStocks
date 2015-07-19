using Moq;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;

namespace NeuralStocks.DatabaseLayer.Tests.Database
{
    [TestFixture]
    public class DatabaseSetupManagerTest : AssertTestClass
    {
        [Test]
        [Category("Database")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IDatabaseSetupManager), typeof (DatabaseSetupManager));
        }

        [Test]
        [Category("Database")]
        public void TestInitializeDatabaseCreatesInitialDatabaseWithEmptyTable()
        {
            const string databaseFileName = "TestStocksDatabase.sqlite";

            var mockCommandRunner = new Mock<IDatabaseCommunicator>();
            var setupManager = new DatabaseSetupManager(mockCommandRunner.Object);

            mockCommandRunner.Verify(m => m.CreateCompanyTable(), Times.Never);

            setupManager.InitializeDatabase(databaseFileName);

            mockCommandRunner.Verify(m => m.CreateCompanyTable(), Times.Once);
        }
    }
}
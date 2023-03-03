using Microsoft.Data.SqlClient;
using System.Data;

namespace PetShelter.BusinessLayer.Tests
{
    public class DBConnectionTest
    {
        private readonly string _connectionString = "Server=localhost\\SQLEXPRESS;Database=PetShelterTema04;Trusted_Connection=True;TrustServerCertificate=True;";

        [Fact]
        public void TestDatabaseConnection()
        {
            //Arrange
            var connection = new SqlConnection(_connectionString);

            //Act
            connection.Open();

            //Assert
            Assert.Equal(ConnectionState.Open, connection.State);
        }
    }
}

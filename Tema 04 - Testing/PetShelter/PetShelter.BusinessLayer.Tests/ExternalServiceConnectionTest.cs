using PetShelter.BusinessLayer.ExternalServices;

namespace PetShelter.BusinessLayer.Tests
{
    public class ExternalServiceConnectionTest
    {
        [Fact]
        public async Task TestIdNumberValidator()
        {
            // Arrange
            var httpClient = new HttpClient();
            var idNumberValidator = new IdNumberValidator(httpClient);

            // Act
            var isValid = await idNumberValidator.Validate("1234567890123");

            // Assert
            Assert.True(isValid);
        }
    }
}

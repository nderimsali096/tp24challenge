using TP24.Models;
using TP24.Services;
using TP24.UnitTests.Utilities;

namespace TP24.UnitTests.Services
{
    public class ValidationServiceTests
    {
        [Fact]
        public void ValidateReceivablePayload_ValidPayload_ReturnsTrue()
        {
            // Arrange
            var service = new ValidationService();
            var validPayload = TestData.GenerateValidReceivablePayload();

            // Act
            var result = service.ValidateReceivablePayload(validPayload);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("", "USD", "2023-08-01", 100, 50, "2023-08-10")] // Empty Reference
        [InlineData("INV123", "", "2023-08-01", 100, 50, "2023-08-10")] // Empty CurrencyCode
        [InlineData("INV123", "USD", "", 100, 50, "2023-08-10")] // Empty IssueDate
        [InlineData("INV123", "USD", "2023-08-01", 0, 50, "2023-08-10")] // OpeningValue = 0
        [InlineData("INV123", "USD", "2023-08-01", 100, 0, "2023-08-10")] // PaidValue = 0
        [InlineData("INV123", "USD", "2023-08-01", 100, 50, "")] // Empty DueDate
        public void ValidateReceivablePayload_InvalidPayload_ReturnsFalse(
            string reference, string currencyCode, string issueDate, decimal openingValue, decimal paidValue, string dueDate)
        {
            // Arrange
            var service = new ValidationService();
            var invalidPayload = new ReceivablePayload
            {
                Reference = reference,
                CurrencyCode = currencyCode,
                IssueDate = issueDate,
                OpeningValue = openingValue,
                PaidValue = paidValue,
                DueDate = dueDate
            };

            // Act
            var result = service.ValidateReceivablePayload(invalidPayload);

            // Assert
            Assert.False(result);
        }
    }

}

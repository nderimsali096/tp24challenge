using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using TP24.Models;
using TP24.Repositories;
using TP24.Services;
using TP24.UnitTests.Utilities;
using Xunit;

namespace TP24.UnitTests.Services
{
    public class ReceivableServiceTests
    {
        [Fact]
        public async Task AddReceivable_ValidPayload_CallsRepositoryAdd()
        {
            // Arrange
            var mockRepository = new Mock<IReceivableRepository>();
            var mockValidationService = new Mock<ValidationService>();
            var service = new ReceivableService(mockRepository.Object, mockValidationService.Object);
            var validPayload = TestData.GenerateValidReceivablePayload();

            // Act
            await service.AddReceivable(validPayload);

            // Assert
            mockRepository.Verify(repo => repo.Add(It.IsAny<ReceivablePayload>()), Times.Once);
        }

        [Fact]
        public async Task AddReceivable_InvalidPayload_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IReceivableRepository>();
            var mockValidationService = new ValidationService();
            var service = new ReceivableService(mockRepository.Object, mockValidationService);
            var invalidPayload = TestData.GenerateInvalidReceivablePayload();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.AddReceivable(invalidPayload));
        }

        [Fact]
        public async Task GetReceivableSummary_CalculatesOpenAndClosedInvoicesValue()
        {
            // Arrange
            var mockRepository = new Mock<IReceivableRepository>();
            var mockValidationService = new Mock<ValidationService>();
            var service = new ReceivableService(mockRepository.Object, mockValidationService.Object);

            var allReceivables = new[]
            {
                new ReceivablePayload { OpeningValue = 100, PaidValue = 50, Cancelled = false, ClosedDate = null },
                new ReceivablePayload { OpeningValue = 200, PaidValue = 100, Cancelled = false, ClosedDate = "2023-08-01" },
                new ReceivablePayload { OpeningValue = 300, PaidValue = 150, Cancelled = true, ClosedDate = null },
            };

            mockRepository.Setup(repo => repo.GetAllReceivables()).ReturnsAsync(allReceivables.ToList());

            // Act
            var summary = await service.GetReceivableSummary();

            // Assert
            Assert.Equal(50, summary.OpenInvoicesValue);
            Assert.Equal(100, summary.ClosedInvoicesValue);
        }
    }
}

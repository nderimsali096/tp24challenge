using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP24.Models;
using TP24.Services;
using TP24.UnitTests.Utilities;
using Xunit;

namespace TP24.UnitTests.Controllers
{
    public class ReceivablesControllerUnitTests
    {
        [Fact]
        public async Task AddReceivable_ValidPayload_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IReceivableService>();
            var controller = new ReceivablesController(mockService.Object);
            var validPayload = TestData.GenerateValidReceivablePayload();

            // Act
            var result = await controller.AddReceivable(validPayload);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddReceivable_ServiceThrowsArgumentException_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<IReceivableService>();
            mockService
                .Setup(service => service.AddReceivable(It.IsAny<ReceivablePayload>()))
                .ThrowsAsync(new ArgumentException("Invalid argument."));
            var controller = new ReceivablesController(mockService.Object);
            var validPayload = TestData.GenerateValidReceivablePayload();

            // Act
            var result = await controller.AddReceivable(validPayload);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid argument.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetReceivableSummary_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IReceivableService>();
            var controller = new ReceivablesController(mockService.Object);
            var summary = new ReceivableSummary();
            mockService.Setup(service => service.GetReceivableSummary()).ReturnsAsync(summary);

            // Act
            var result = await controller.GetReceivableSummary();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}

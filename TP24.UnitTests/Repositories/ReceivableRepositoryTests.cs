using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TP24.Data;
using TP24.Models;
using TP24.Repositories;
using TP24.UnitTests.Utilities;
using Xunit;

namespace TP24.UnitTests.Repositories
{
    public class ReceivableRepositoryTests
    {
        [Fact]
        public async Task GetAllReceivables_ReturnsListOfReceivables()
        {
            // Arrange
            var receivables = TestData.GenerateListOfReceivablePayloads();
            var mockSet = new Mock<DbSet<ReceivablePayload>>();
            mockSet.As<IQueryable<ReceivablePayload>>().Setup(m => m.Provider).Returns(receivables.AsQueryable().Provider);
            mockSet.As<IQueryable<ReceivablePayload>>().Setup(m => m.Expression).Returns(receivables.AsQueryable().Expression);
            mockSet.As<IQueryable<ReceivablePayload>>().Setup(m => m.ElementType).Returns(receivables.AsQueryable().ElementType);
            mockSet.As<IQueryable<ReceivablePayload>>().Setup(m => m.GetEnumerator()).Returns(receivables.AsQueryable().GetEnumerator());

            // Create an asynchronous version of IEnumerable
            mockSet.As<IAsyncEnumerable<ReceivablePayload>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<ReceivablePayload>(receivables.GetEnumerator()));

            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(c => c.Receivables).Returns(mockSet.Object);

            // Act
            var repository = new ReceivableRepository(mockContext.Object);
            var result = await repository.GetAllReceivables();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public async Task Add_ValidReceivable_SavesToDatabase()
        {
            // Arrange
            var mockSet = new Mock<DbSet<ReceivablePayload>>();
            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(c => c.Receivables).Returns(mockSet.Object);

            var repository = new ReceivableRepository(mockContext.Object);
            var validReceivable = TestData.GenerateValidReceivablePayload();

            // Act
            await repository.Add(validReceivable);

            // Assert
            mockSet.Verify(m => m.AddAsync(validReceivable, It.IsAny<CancellationToken>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task Add_ThrowsExceptionDuringSaveChanges_ThrowsException()
        {
            // Arrange
            var mockSet = new Mock<DbSet<ReceivablePayload>>();
            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(c => c.Receivables).Returns(mockSet.Object);
            mockContext.Setup(c => c.SaveChangesAsync()).ThrowsAsync(new Exception("SaveChanges failed"));

            var repository = new ReceivableRepository(mockContext.Object);
            var validReceivable = TestData.GenerateValidReceivablePayload();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => repository.Add(validReceivable));
        }
}

internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public TestAsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }

        public ValueTask DisposeAsync()
        {
            _enumerator.Dispose();
            return new ValueTask();
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_enumerator.MoveNext());
        }

        public T Current => _enumerator.Current;
    }
}

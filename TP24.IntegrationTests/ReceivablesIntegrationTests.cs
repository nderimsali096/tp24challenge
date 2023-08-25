using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using TP24.Models;
using TP24.IntegrationTests;
using System.Net.Http.Json;
using TP24.UnitTests.Utilities;

namespace TP24.IntegrationTests
{
    public class ReceivablesIntegrationTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ReceivablesIntegrationTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddReceivable_IntegrationTest()
        {
            TestDataIntegration testDataIntegration = new TestDataIntegration();
            // Arrange
            var payload = testDataIntegration.GenerateValidReceivablePayload();

            // Act
            var response = await _client.PostAsJsonAsync("/receivables/add", payload);

            // Assert
            response.EnsureSuccessStatusCode();        
        }
    }
}

using System.Net;
using System.Net.Http;
using FluentAssertions;

namespace E2E;
    [TestFixture]
    public class WeatherForecastE2ETests
    {
        private HttpClient _client = null!;

        [SetUp]
        public void SetUp()
        {
            // Retrieve the shared factory instance
            _client = TestAssemblySetup.Factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [Test]
        public async Task GetWeatherForecast_ReturnsSuccessAndData()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert HTTP status
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Assert JSON content contains expected properties
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("date", "because the JSON must include a date field");
            content.Should().Contain("temperatureC", "because each forecast must include temperature");
        }

        // Additional tests for count, range, summaries...
    }

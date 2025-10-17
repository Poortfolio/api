using api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;


namespace Tests.IT.StepDefinitions
{
    [Binding]
    public class WeatherForecastSteps : IAsyncDisposable
    {
        private WebApplicationFactory<Program> _factory = null!;
        private HttpClient _client = null!;
        private HttpResponseMessage _response = null!;
        private WeatherForecast[]? _forecasts;

        [BeforeScenario]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
            });
            _client  = _factory.CreateClient();
        }

        [Given("the weather forecast API is available")]
        public async Task GivenTheWeatherForecastApiIsAvailable()
        {
            _response = await _client.GetAsync("/");
            Assert.That(_response.IsSuccessStatusCode, Is.False);
        }

        [When("I request the weather forecast")]
        public async Task WhenIRequestTheWeatherForecast()
        {
            _response = await _client.GetAsync("/WeatherForecast");
        }

        [Then("the response should be successful")]
        public void ThenTheResponseShouldBeSuccessful()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then("the response should contain forecast data")]
        public async Task ThenTheResponseShouldContainForecastData()
        {
            var content = await _response.Content.ReadAsStringAsync();
            _forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.That(_forecasts, Is.Not.Null);
            Assert.That(_forecasts!.Length, Is.GreaterThan(0));
        }

        // …and so on for other steps, always using:
        // Assert.That(actual, Is.Constraint)

        public async ValueTask DisposeAsync()
        {
            _response?.Dispose();
            _client?.Dispose();
            await _factory.DisposeAsync();
        }
    }
}

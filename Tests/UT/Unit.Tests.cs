using Moq;
using api.Controllers;
using Microsoft.Extensions.Logging;

namespace UT;

public class Tests
{
    private static Mock<ILogger<WeatherForecastController>> _mockedLogger;
    private static WeatherForecastController _controller;

    [SetUp]
    public void Setup()
    {
        _mockedLogger = new Mock<ILogger<WeatherForecastController>>();
        _controller = new(_mockedLogger.Object);
    }

    [Test]
    public void Test1()
    {
        var result = _controller.Get();
        Assert.That(result, Is.Not.Null);
    }
}

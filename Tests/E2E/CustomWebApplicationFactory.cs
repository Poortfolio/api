using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace E2E;

public class CustomWebApplicationFactory<TEntryPoint>
    : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Optionally override configuration or services here, e.g.:
        // builder.ConfigureServices(services => { ... });
        return base.CreateHost(builder);
    }
}

[SetUpFixture]
public class TestAssemblySetup
{
    public static CustomWebApplicationFactory<Program> Factory { get; private set; } = null!;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        Factory = new CustomWebApplicationFactory<Program>();
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        Factory.Dispose();
    }
}

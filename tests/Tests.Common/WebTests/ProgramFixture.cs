namespace NetAdvancedShop.Tests.Common.WebTests;

public abstract class ProgramFixture<TProgram> : WebApplicationFactory<TProgram>, IProgramFixture
    where TProgram : class
{
    private const string Environment = "Development";

    void IProgramFixture.SetUp() => SetUp();

    async Task IProgramFixture.TearDown() => await CleanUp();

    void IProgramFixture.Initialize() => Initialize();

    public virtual void SetUp() { }

    protected virtual Task CleanUp() => Task.CompletedTask;

    protected virtual void Initialize() { }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("UseInMemoryDatabase", "true")
            .UseEnvironment(Environment)
            .ConfigureServices(ConfigureServices);
    }

    protected virtual void ConfigureServices(IServiceCollection services) { }
}

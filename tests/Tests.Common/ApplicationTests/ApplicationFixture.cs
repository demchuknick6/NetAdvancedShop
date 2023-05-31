namespace NetAdvancedShop.Tests.Common.ApplicationTests;

public abstract class ApplicationFixture : IApplicationFixture
{
    public Guid RandomId => Guid.NewGuid();

    public string RandomString => RandomId.ToString();

    public int RandomInt => new Random().Next(1, 100);

    public uint RandomUint => (uint)new Random().Next(1, 10);

    public string RandomUrl => "https://www.google.com/";

    public IServiceProvider Services => ServiceScope.ServiceProvider;

    public virtual void SetUp()
    {
        ServiceScope = SingletonServiceProvider.CreateScope();
    }

    public async Task TearDown()
    {
        await CleanUp();

        ServiceScope.Dispose();
    }

    public THandler CreateHandler<THandler>() => ActivatorUtilities.CreateInstance<THandler>(Services);

    public ISender GetSender() => Services.GetRequiredService<ISender>();

    public virtual void ClearTrackedEntries() =>
        throw new NotSupportedException("Not supported since there is no DbContext");

    public void Initialize()
    {
        var services = new ServiceCollection();

        ConfigureCommonServices(services);
        ConfigureServiceSpecificServices(services);

        SingletonServiceProvider = services.BuildServiceProvider();
    }

    protected abstract void ConfigureCommonServices(ServiceCollection services);

    protected abstract void ConfigureServiceSpecificServices(ServiceCollection services);

    protected abstract Task CleanUp();

    private ServiceProvider SingletonServiceProvider { get; set; } = null!;

    private IServiceScope ServiceScope { get; set; } = null!;
}

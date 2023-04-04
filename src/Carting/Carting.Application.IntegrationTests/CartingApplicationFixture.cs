namespace Carting.Application.IntegrationTests;

public class CartingApplicationFixture : ApplicationFixture
{
    private readonly string _connectionString = @"Cart.db";

    public IOptions<CartingSettings> Settings => Services.GetRequiredService<IOptions<CartingSettings>>();

    protected override void ConfigureCommonServices(ServiceCollection services)
    { }

    protected override void ConfigureServiceSpecificServices(ServiceCollection services)
    {
        services.AddApplicationServices();

        var cartingSettings = new CartingSettings { ConnectionString = _connectionString };
        var cartingSettingsWrapper = new OptionsWrapper<CartingSettings>(cartingSettings);
        services.AddSingleton<IOptions<CartingSettings>>(cartingSettingsWrapper);
    }

    protected override Task CleanUp()
    {
        DeleteDatabase();
        return Task.CompletedTask;
    }

    protected virtual void DeleteDatabase()
    {
        var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var databasePath = Path.Combine(assemblyDirectory!, _connectionString);
        File.Delete(databasePath);
    }
}

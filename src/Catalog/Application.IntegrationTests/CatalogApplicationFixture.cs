namespace Catalog.Application.IntegrationTests;

public class CatalogApplicationFixture : ApplicationFixture<CatalogContext>
{
    protected override void ConfigureServiceSpecificServices(ServiceCollection services)
    {
        services.AddInfrastructureServices();
        services.AddApplicationServices();
    }
}

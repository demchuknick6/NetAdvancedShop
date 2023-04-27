namespace Catalog.WebUI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.IncludeXmlCommentsFromAssemblyOf<Program>();
        });

        return services;
    }
}

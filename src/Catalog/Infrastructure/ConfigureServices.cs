namespace Catalog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
                    builder =>
                    {
                        builder.MigrationsAssembly(typeof(CatalogContext).Assembly.FullName);
                        builder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    }));

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICatalogContext>(provider => provider.GetRequiredService<CatalogContext>());

        return services;
    }
}

namespace NetAdvancedShop.Catalog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var useInMemoryDatabase = bool.TryParse(configuration["UseInMemoryDatabase"], out var result) && result;

        services.AddDbContext<CatalogContext>(options =>
        {
            if (useInMemoryDatabase)
            {
                options.UseInMemoryDatabase("CatalogService");
            }
            else
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder =>
                    {
                        builder.MigrationsAssembly(typeof(CatalogContext).Assembly.FullName);
                        builder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            }
        });

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICatalogContext>(provider => provider.GetRequiredService<CatalogContext>());

        return services;
    }
}

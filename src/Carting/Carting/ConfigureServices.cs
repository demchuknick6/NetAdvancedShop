namespace Carting;

public static class ConfigureServices
{
    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions()
            .Configure<CartingSettings>(s => configuration.GetSection(CartingSettings.Carting).Bind(s));

        return services;
    }

    public static IServiceCollection AddOptions(this IServiceCollection services, string connectionString)
    {
        services.AddOptions()
            .Configure<CartingSettings>(o => o.ConnectionString = connectionString);

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IMapper<CartItemDto, CartItem>, CartItemMapper>();
        services.AddTransient<IMapper<CartItem, AddCartItemDto>, AddCartItemMapper>();

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}

namespace NetAdvancedShop.Identity.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var useInMemoryDatabase = bool.TryParse(configuration["UseInMemoryDatabase"], out var result) && result;

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (useInMemoryDatabase)
            {
                options.UseInMemoryDatabase("IdentityService");
            }
            else
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder =>
                    {
                        builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        builder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            }
        });

        return services;
    }

    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddApplicationIdentityServer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentityServer(options =>
            {
                options.IssuerUri = "null";
                options.Authentication.CookieLifetime = TimeSpan.FromHours(1);

                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryClients(Config.GetClients(configuration))
            .AddAspNetIdentity<ApplicationUser>()
            .AddDeveloperSigningCredential()
			.AddProfileService<ProfileService>();

        // Need to do this as it maps "role" to ClaimTypes.Role and causes issues
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");

        return services;
    }

    public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication();

        return services;
    }
}

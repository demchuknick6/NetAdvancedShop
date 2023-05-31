var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext(builder.Configuration)
    .AddInfrastructureServices()
    .AddApplicationServices()
    .AddWebUIServices()
    .AddSwagger(builder.Configuration)
    .AddAuthentication(builder.Configuration)
    .AddAuthorization(builder.Configuration)
    .AddEventBus(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    var pathBase = builder.Configuration["PATH_BASE"];
    if (!string.IsNullOrEmpty(pathBase))
    {
        app.UsePathBase(pathBase);
    }

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetAdvancedShop.Catalog.WebUI v1");
        c.OAuthClientId("catalogswaggerui");
        c.OAuthAppName("Catalog Swagger UI");
    });

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();
    if (dbContext.Database.IsSqlServer())
    {
        await dbContext.Database.MigrateAsync();
    }
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    public static string? Namespace = typeof(Program).Namespace;
    public static string? AppName = Namespace?.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}

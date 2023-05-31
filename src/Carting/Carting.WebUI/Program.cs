var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, TokenLoggingMiddleware>()
    .AddOptions(builder.Configuration)
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

    var pathBase = builder.Configuration["PATH_BASE"];

    if (!string.IsNullOrEmpty(pathBase))
    {
        app.UsePathBase(pathBase);
    }

    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetAdvancedShop.Carting.WebUI v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "NetAdvancedShop.Carting.WebUI v2");
        c.OAuthClientId("cartingswaggerui");
        c.OAuthAppName("Carting Swagger UI");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//ConfigureEventBus(app);

app.Run();

static void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.Subscribe<ItemChangedApplicationEvent, ItemChangedApplicationEventHandler>();
}

public partial class Program
{
    public static string? Namespace = typeof(Program).Namespace;
    public static string? AppName = Namespace?.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}

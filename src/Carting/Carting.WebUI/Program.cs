var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebUIServices();
builder.Services.AddEventBus(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carting.WebUI v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Carting.WebUI v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ConfigureEventBus(app);

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

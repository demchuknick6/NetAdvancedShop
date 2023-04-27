var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebUIServices();

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

app.Run();

public partial class Program { }

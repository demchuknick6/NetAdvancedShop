var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddApplicationDbContext(builder.Configuration)
    .AddApplicationIdentity()
    .AddApplicationIdentityServer(builder.Configuration)
    .AddApplicationAuthentication();

var app = builder.Build();

await SeedDatabaseAsync(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

var pathBase = builder.Configuration["PATH_BASE"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
}

app.UseHttpsRedirection();

app.UseStaticFiles();

// This cookie policy fixes login issues with Chrome 80+ using HHTP
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();


async Task SeedDatabaseAsync(IConfiguration configuration)
{
    var useInMemoryDatabase = bool.TryParse(configuration["UseInMemoryDatabase"], out var result) && result;

    if (useInMemoryDatabase)
        return;

    app.Logger.LogInformation("Seeding Database...");
    using var scope = app.Services.CreateScope();
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var dbContext = scopedProvider.GetRequiredService<ApplicationDbContext>();
        await ApplicationDbContextSeed.SeedAsync(dbContext, userManager, roleManager);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
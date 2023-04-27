namespace Catalog.Infrastructure;

public class CatalogContext : DbContext, ICatalogContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    { }

    public DbSet<Item> Items { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}

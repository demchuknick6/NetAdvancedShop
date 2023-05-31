namespace NetAdvancedShop.Catalog.Application.Interfaces;

public interface ICatalogContext
{
    DbSet<Category> Categories { get; }

    DbSet<Item> Items { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

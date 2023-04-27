namespace Tests.Common.ApplicationTests;

public abstract class ApplicationFixture<TDbContext> : ApplicationFixture where TDbContext : DbContext
{
    public TDbContext DbContext => Services.GetRequiredService<TDbContext>();

    public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class =>
        await DbContext.FindAsync<TEntity>(keyValues);

    public async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        DbContext.Add(entity);

        await DbContext.SaveChangesAsync();
    }

    public override void ClearTrackedEntries() =>
        DbContext.ChangeTracker.Entries()
            .ToList()
            .ForEach(entry => entry.State = EntityState.Detached);

    protected static DbContextOptionsBuilder ConfigureDbContext(DbContextOptionsBuilder b) =>
        b.UseInMemoryDatabase("in-memory")
            .ConfigureWarnings(c => c.Ignore(InMemoryEventId.TransactionIgnoredWarning));

    protected override void ConfigureCommonServices(ServiceCollection services) =>
        services.AddDbContext<TDbContext>(builder => ConfigureDbContext(builder));

    protected override async Task CleanUp() => await CleanUpDatabase();

    protected virtual async Task CleanUpDatabase()
    {
        // Clear locally tracked entries
        // NOTE: Re-creation of database does not remove tracked entities 
        DbContext.ChangeTracker.Entries()
            .ToList()
            .ForEach(entry => entry.State = EntityState.Detached);

        // Re-create database
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
    }
}

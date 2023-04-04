namespace Carting.Infrastructure;

public class CartingContext : IDisposable
{
    private readonly ILiteDatabase _database;

    public CartingContext(IOptions<CartingSettings> settings)
    {
        _database = new LiteDatabase(settings.Value.ConnectionString);
    }

    public ILiteCollection<Cart> Carts => _database.GetCollection<Cart>("carts");

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _database?.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

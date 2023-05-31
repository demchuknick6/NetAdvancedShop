namespace NetAdvancedShop.Carting.Domain.Entities;

public class Cart
{
    public required Guid Id { get; set; }

    public ICollection<CartItem> Items { get; set; } = Array.Empty<CartItem>();
}
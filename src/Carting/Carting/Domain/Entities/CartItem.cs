namespace Carting.Domain.Entities;

public class CartItem
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public CartItemImage? Image { get; set; }

    public required decimal Price { get; set; }

    public required uint Quantity { get; set; }
}

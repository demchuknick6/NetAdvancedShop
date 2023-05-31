namespace NetAdvancedShop.Carting.Application.Queries;

public class CartItemDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string? CartImageUrl { get; init; } = null!;

    public string? CartImageAltText { get; init; }

    public decimal Price { get; set; }

    public uint Quantity { get; set; }
}

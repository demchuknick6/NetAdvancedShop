namespace Carting.Application.Commands.AddCartItem;

public class AddCartItemDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public AddCartItemImageModel? Image { get; set; }

    public decimal Price { get; set; }

    public uint Quantity { get; set; }
}

public class AddCartItemImageModel
{
    public string Uri { get; set; } = null!;

    public string? AltText { get; set; }
}

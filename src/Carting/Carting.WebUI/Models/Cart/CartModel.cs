namespace Carting.WebUI.Models.Cart;

public class CartModel
{
    public Guid Id { get; set; }

    public List<CartItemDto> Items { get; set; } = new();
}

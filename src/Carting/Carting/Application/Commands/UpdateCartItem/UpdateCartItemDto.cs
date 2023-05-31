namespace NetAdvancedShop.Carting.Application.Commands.UpdateCartItem;

public class UpdateCartItemDto
{
    public string NewName { get; set; } = null!;

    public decimal NewPrice { get; set; }
}

namespace NetAdvancedShop.Carting.WebUI.Models.CartItem;

public class AddCartItemModel
{
    [JsonRequired]
    public int Id { get; set; }

    [JsonRequired]
    public string Name { get; set; } = null!;

    public CartItemImageModel? Image { get; set; }

    [JsonRequired]
    public  decimal Price { get; set; }

    [JsonRequired]
    public uint Quantity { get; set; }
}

public class CartItemImageModel
{
    [JsonRequired]
    public string Uri { get; set; } = null!;

    public string? AltText { get; set; }
}

namespace NetAdvancedShop.Carting.Application.Queries;

internal class CartItemMapper : IMapper<CartItemDto, CartItem>
{
    public CartItemDto Translate(CartItem entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name,
            CartImageUrl = entity.Image?.Uri,
            CartImageAltText = entity.Image?.AltText,
            Price = entity.Price,
            Quantity = entity.Quantity
        };
}

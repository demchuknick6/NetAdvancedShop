namespace Carting.Application.Commands.AddCartItem;

internal class AddCartItemMapper : IMapper<CartItem, AddCartItemDto>
{
    public CartItem Translate(AddCartItemDto entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Image = entity.Image == null
                ? null
                : new CartItemImage { Uri = entity.Image.Uri, AltText = entity.Image.AltText },
            Price = entity.Price,
            Quantity = entity.Quantity
        };
}

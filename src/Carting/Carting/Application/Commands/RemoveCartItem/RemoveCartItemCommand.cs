namespace NetAdvancedShop.Carting.Application.Commands.RemoveCartItem;

public record RemoveCartItemCommand(Guid CartId, int ItemId) : IRequest<Unit>;

public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, Unit>
{
    private readonly IOptions<CartingSettings> _settings;

    public RemoveCartItemCommandHandler(IOptions<CartingSettings> settings)
    {
        _settings = settings;
    }

    public async Task<Unit> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        using var context = new CartingContext(_settings);

        var cart = context.Carts.FindOne(c => c.Id == request.CartId);

        if (cart == null)
        {
            throw new NotFoundException(nameof(Cart), request.CartId);
        }

        var cartItem = cart.Items.FirstOrDefault(i => i.Id == request.ItemId);

        if (cartItem == null)
        {
            throw new NotFoundException(nameof(CartItem), request.ItemId);
        }

        cart.Items.Remove(cartItem);

        context.Carts.Update(cart);

        return await Task.FromResult(Unit.Value);
    }
}

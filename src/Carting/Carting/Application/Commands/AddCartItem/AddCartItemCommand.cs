namespace Carting.Application.Commands.AddCartItem;

public record AddCartItemCommand(Guid CartId, CartItem Item) : IRequest<Unit>;

public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, Unit>
{
    private readonly IOptions<CartingSettings> _settings;

    public AddCartItemCommandHandler(IOptions<CartingSettings> settings)
    {
        _settings = settings;
    }

    public Task<Unit> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        using var context = new CartingContext(_settings);

        var cart = context.Carts.FindOne(c => c.Id == request.CartId);

        if (cart == null)
        {
            cart = new Cart
            {
                Id = request.CartId,
                Items = new List<CartItem>()
            };

            context.Carts.Insert(cart);
        }

        var existingItem = cart.Items.FirstOrDefault(i => i.Id == request.Item.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Item.Quantity;
        }
        else
        {
            cart.Items.Add(request.Item);
        }

        context.Carts.Update(cart);

        return Task.FromResult(Unit.Value);
    }
}

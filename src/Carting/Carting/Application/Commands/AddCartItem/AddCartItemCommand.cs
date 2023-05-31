namespace NetAdvancedShop.Carting.Application.Commands.AddCartItem;

public record AddCartItemCommand(Guid CartId, AddCartItemDto Item) : IRequest<Unit>;

public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, Unit>
{
    private readonly IOptions<CartingSettings> _settings;
    private readonly IMapper<CartItem, AddCartItemDto> _mapper;

    public AddCartItemCommandHandler(IOptions<CartingSettings> settings, IMapper<CartItem, AddCartItemDto> mapper)
    {
        _settings = settings;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
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
            var entity = _mapper.Translate(request.Item);
            cart.Items.Add(entity);
        }

        context.Carts.Update(cart);

        return await Task.FromResult(Unit.Value);
    }
}

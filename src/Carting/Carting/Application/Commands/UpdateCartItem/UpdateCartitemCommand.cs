namespace NetAdvancedShop.Carting.Application.Commands.UpdateCartItem;

public record UpdateCartItemCommand(int ItemId, UpdateCartItemDto Item) : IRequest<Unit>;

public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, Unit>
{
    private readonly IOptions<CartingSettings> _settings;

    public UpdateCartItemCommandHandler(IOptions<CartingSettings> settings)
    {
        _settings = settings;
    }

    public async Task<Unit> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        using var context = new CartingContext(_settings);

        var carts = context.Carts.FindAll().Where(c => c != null).ToArray();

        if (!carts.Any())
        {
            return await Task.FromResult(Unit.Value);
        }

        foreach (var cart in carts)
        {
            var itemsToUpdate = cart.Items.Where(i => i.Id == request.ItemId);

            foreach (var item in itemsToUpdate)
            {
                item.Name = request.Item.NewName;
                item.Price = request.Item.NewPrice;
            }

            context.Carts.Update(cart);
        }

        return await Task.FromResult(Unit.Value);
    }
}


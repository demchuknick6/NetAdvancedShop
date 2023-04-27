namespace Carting.Application.Queries;

public record GetCartItemsQuery(Guid CartId) : IRequest<IReadOnlyCollection<CartItemDto>>;

public class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQuery, IReadOnlyCollection<CartItemDto>>
{
    private readonly IOptions<CartingSettings> _settings;
    private readonly IMapper<CartItemDto, CartItem> _mapper;

    public GetCartItemsQueryHandler(IOptions<CartingSettings> settings, IMapper<CartItemDto, CartItem> mapper)
    {
        _settings = settings;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<CartItemDto>> Handle(
        GetCartItemsQuery request,
        CancellationToken cancellationToken)
    {
        using var context = new CartingContext(_settings);

        var cart = context.Carts.FindOne(c => c.Id == request.CartId);

        if (cart == null)
        {
            throw new NotFoundException(nameof(Cart), request.CartId);
        }

        var items = cart.Items.Select(i => _mapper.Translate(i)).ToList();

        return await Task.FromResult(items);
    }
}

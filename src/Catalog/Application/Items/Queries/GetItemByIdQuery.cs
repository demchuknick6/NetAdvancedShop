namespace Catalog.Application.Items.Queries;

public record GetItemByIdQuery(int Id) : IRequest<ItemDto>;

public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDto>
{
    private readonly ICatalogContext _context;
    private readonly IMapper _mapper;

    public GetItemByIdQueryHandler(ICatalogContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ItemDto> Handle(
        GetItemByIdQuery request,
        CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (item == null)
        {
            throw new NotFoundException(nameof(Item), request.Id);
        }

        return _mapper.Map<ItemDto>(item);
    }
}

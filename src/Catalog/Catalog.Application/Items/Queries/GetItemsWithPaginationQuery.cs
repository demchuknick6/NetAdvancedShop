namespace NetAdvancedShop.Catalog.Application.Items.Queries;

public record GetItemsWithPaginationQuery(int CategoryId, int PageNumber = 1, int PageSize = 10)
    : IRequest<PaginatedList<ItemDto>>;

public class GetItemsWithPaginationQueryHandler : IRequestHandler<GetItemsWithPaginationQuery, PaginatedList<ItemDto>>
{
    private readonly ICatalogContext _context;
    private readonly IMapper _mapper;

    public GetItemsWithPaginationQueryHandler(ICatalogContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ItemDto>> Handle(
        GetItemsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        return await _context.Items
            .Where(x => x.CategoryId == request.CategoryId)
            .OrderBy(x => x.Name)
            .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}

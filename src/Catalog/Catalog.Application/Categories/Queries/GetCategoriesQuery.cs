namespace NetAdvancedShop.Catalog.Application.Categories.Queries;

public record GetCategoriesQuery : IRequest<IReadOnlyCollection<CategoryDto>>;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IReadOnlyCollection<CategoryDto>>
{
    private readonly ICatalogContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(ICatalogContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<CategoryDto>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken) =>
        await _context.Categories
            .AsNoTracking()
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
}

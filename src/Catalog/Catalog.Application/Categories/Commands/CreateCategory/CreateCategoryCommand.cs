namespace Catalog.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string? ImageUri, int? ParentCategoryId) : IRequest<int>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly ICatalogContext _context;

    public CreateCategoryCommandHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentCategoryId.HasValue)
        {
            var parentCategory = await _context.Categories
                .FindAsync(request.ParentCategoryId, cancellationToken);

            if (parentCategory == null)
            {
                throw new NotFoundException(nameof(Category), request.ParentCategoryId);
            }
        }

        var entity = new Category
        {
            Name = request.Name,
            ImageUri = request.ImageUri,
            ParentCategoryId = request.ParentCategoryId
        };

        _context.Categories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

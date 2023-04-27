namespace Catalog.Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(int Id, string Name, string? ImageUri, int? ParentCategoryId) : IRequest<Unit>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly ICatalogContext _context;

    public UpdateCategoryCommandHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .FindAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        if (request.ParentCategoryId.HasValue)
        {
            var parentCategory = await _context.Categories
                .FindAsync(request.ParentCategoryId, cancellationToken);

            if (parentCategory == null)
            {
                throw new NotFoundException(nameof(Category), request.ParentCategoryId);
            }
        }

        entity.Name = request.Name;
        entity.ImageUri = request.ImageUri;
        entity.ParentCategoryId = request.ParentCategoryId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

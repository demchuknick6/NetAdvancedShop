namespace Catalog.Application.Items.Commands.UpdateItem;

public record UpdateItemCommand(int Id, string Name, string? Description, string? ImageUri,
    int CategoryId, decimal Price, uint Amount) : IRequest<Unit>;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Unit>
{
    private readonly ICatalogContext _context;

    public UpdateItemCommandHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Items
            .FindAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Item), request.Id);
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.ImageUri = request.ImageUri;
        entity.CategoryId = request.CategoryId;
        entity.Price = request.Price;
        entity.Amount = request.Amount;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

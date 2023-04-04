namespace Catalog.Application.Items.Commands.CreateItem;

public record CreateItemCommand(string Name, string? Description, string? ImageUri,
    int CategoryId, decimal Price, uint Amount) : IRequest<int>;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
{
    private readonly ICatalogContext _context;

    public CreateItemCommandHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        var entity = new Item
        {
            Name = request.Name,
            Description = request.Description,
            ImageUri = request.ImageUri,
            CategoryId = request.CategoryId,
            Price = request.Price,
            Amount = request.Amount
        };

        _context.Items.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

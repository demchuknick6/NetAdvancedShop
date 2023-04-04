namespace Catalog.Application.Items.Commands.DeleteItem;

public record DeleteItemCommand(int Id) : IRequest<Unit>;

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Unit>
{
    private readonly ICatalogContext _context;

    public DeleteItemCommandHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Items
            .FindAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Item), request.Id);
        }

        _context.Items.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

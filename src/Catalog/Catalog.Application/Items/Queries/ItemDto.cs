namespace Catalog.Application.Items.Queries;

public class ItemDto : IMapFrom<Item>
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string? Description { get; init; }

    public string? ImageUri { get; init; }

    public int CategoryId { get; init; }

    public decimal Price { get; init; }

    public uint Amount { get; init; }
}

namespace Catalog.Application.Categories.Queries;

public class ItemBriefDto : IMapFrom<Item>
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}

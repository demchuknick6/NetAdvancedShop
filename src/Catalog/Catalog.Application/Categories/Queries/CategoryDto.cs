namespace NetAdvancedShop.Catalog.Application.Categories.Queries;

public class CategoryDto : IMapFrom<Category>
{
    public CategoryDto()
    {
        Items = Array.Empty<ItemBriefDto>();
    }

    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string? ImageUri { get; init; }

    public int? ParentCategoryId { get; init; }

    public IReadOnlyCollection<ItemBriefDto> Items { get; init; }
}

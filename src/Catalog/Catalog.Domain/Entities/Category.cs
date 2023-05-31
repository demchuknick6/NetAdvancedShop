namespace NetAdvancedShop.Catalog.Domain.Entities;

public class Category : BaseEntity
{
    private string _name = null!;

    public required string Name
    {
        get => _name;
        set
        {
            if (value.Length > 50)
            {
                throw new CatalogDomainException("Category Name cannot exceed 50 chars length.");
            }

            _name = value;
        }
    }

    public string? ImageUri { get; set; }

    public int? ParentCategoryId { get; set; }

    public Category? ParentCategory { get; set; }

    public IReadOnlyCollection<Item> Items { get; set; } = null!;

}

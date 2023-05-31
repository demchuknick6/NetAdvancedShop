namespace NetAdvancedShop.Catalog.Domain.Entities;

public class Item : BaseEntity
{
    private string _name = null!;

    public required string Name
    {
        get => _name;
        set
        {
            if (value.Length > 50)
            {
                throw new CatalogDomainException("Item Name cannot exceed 50 chars length.");
            }

            _name = value;
        }
    }

    public string? Description { get; set; }

    public string? ImageUri { get; set; }

    public required int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public required decimal Price { get; set; }

    public required uint Amount { get; set; }
}

using static NetAdvancedShop.Catalog.WebUI.Constants;

namespace NetAdvancedShop.Catalog.WebUI.Models.Item;

public class CreateItemModel
{
    [JsonRequired]
    [StringLength(NAME_PROPERTY_MAX_LENGTH, MinimumLength = NAME_PROPERTY_MIN_LENGTH)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUri { get; set; }

    [JsonRequired]
    public int CategoryId { get; set; }

    [JsonRequired]
    public decimal Price { get; set; }

    [JsonRequired]
    public uint Amount { get; set; }
}

using static NetAdvancedShop.Catalog.WebUI.Constants;

namespace NetAdvancedShop.Catalog.WebUI.Models.Category;

public class CreateCategoryModel
{
    [JsonRequired]
    [StringLength(NAME_PROPERTY_MAX_LENGTH, MinimumLength = NAME_PROPERTY_MIN_LENGTH)]
    public string Name { get; set; } = null!;

    public string? ImageUri { get; set; }

    public int? ParentCategoryId { get; set; }
}

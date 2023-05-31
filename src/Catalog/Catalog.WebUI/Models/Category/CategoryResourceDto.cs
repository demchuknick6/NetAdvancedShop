namespace NetAdvancedShop.Catalog.WebUI.Models.Category;

public class CategoryResourceDto : CategoryDto, ILinkResource
{
    public IEnumerable<LinkDto> Links { get; set; }

    public CategoryResourceDto(CategoryDto category, IEnumerable<LinkDto> links)
    {
        Id = category.Id;
        Name = category.Name;
        ImageUri = category.ImageUri;
        ParentCategoryId = category.ParentCategoryId;
        Items = category.Items;
        Links = links;
    }
}

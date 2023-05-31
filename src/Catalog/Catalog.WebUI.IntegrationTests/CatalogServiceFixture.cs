namespace NetAdvancedShop.Catalog.WebUI.IntegrationTests;

public class CatalogServiceFixture : RestApiFixture<Program>
{
    private HttpClient? _categories;
    private HttpClient? _items;

    public HttpClient Categories => _categories ??= CreateApiClient("categories");

    public HttpClient Items => _items ??= CreateApiClient("items");

    protected override Task CleanUp()
    {
        Categories.RemoveUserIdentifier();
        Items.RemoveUserIdentifier();
        return base.CleanUp();
    }
}

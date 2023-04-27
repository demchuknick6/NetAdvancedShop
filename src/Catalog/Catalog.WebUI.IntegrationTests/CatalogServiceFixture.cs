namespace Catalog.WebUI.IntegrationTests;

public class CatalogServiceFixture : RestApiFixture<Program>
{
    public HttpClient Categories => CreateApiClient("categories");

    public HttpClient Items => CreateApiClient("items");
}

namespace Catalog.WebUI.IntegrationTests.Endpoints;

public class SwaggerFileTests : WebTests<CatalogServiceFixture>
{
    [Test]
    public async Task Swagger_documentation_should_be_generated_successfully()
    {
        var response = await Fixture.CreateClient().GetAsync("swagger/v1/swagger.json");
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
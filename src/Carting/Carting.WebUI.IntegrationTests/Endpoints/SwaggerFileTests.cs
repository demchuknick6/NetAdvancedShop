namespace NetAdvancedShop.Carting.WebUI.IntegrationTests.Endpoints;

public class SwaggerFileTests : WebTests<CartingServiceFixture>
{
    [Test]
    public async Task Swagger_v1_documentation_should_be_generated_successfully()
    {
        var response = await Fixture.CreateClient().GetAsync("swagger/v2/swagger.json");
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Test]
    public async Task Swagger_v2_documentation_should_be_generated_successfully()
    {
        var response = await Fixture.CreateClient().GetAsync("swagger/v2/swagger.json");
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
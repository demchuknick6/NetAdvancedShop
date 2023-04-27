namespace Carting.WebUI.IntegrationTests;

public class CartingServiceFixture : RestApiFixture<Program>
{
    public HttpClient CartV1 => CreateApiClient("v1/cart");

    public HttpClient CartV2 => CreateApiClient("v2/cart");
}

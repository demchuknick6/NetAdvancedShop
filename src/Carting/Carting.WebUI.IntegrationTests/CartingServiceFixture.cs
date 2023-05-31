namespace NetAdvancedShop.Carting.WebUI.IntegrationTests;

public class CartingServiceFixture : RestApiFixture<Program>
{
    private HttpClient? _cartV1;
    private HttpClient? _cartV2;

    public HttpClient CartV1 => _cartV1 ??= CreateApiClient("v1/cart");

    public HttpClient CartV2 => _cartV2 ??= CreateApiClient("v2/cart");

    protected override Task CleanUp()
    {
        CartV1.RemoveUserIdentifier();
        CartV2.RemoveUserIdentifier();
        return base.CleanUp();
    }
}

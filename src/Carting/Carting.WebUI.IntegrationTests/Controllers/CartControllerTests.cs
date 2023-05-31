namespace NetAdvancedShop.Carting.WebUI.IntegrationTests.Controllers;

public class CartControllerTests : WebTests<CartingServiceFixture>
{
    [TestCase(TestUserIdentifiers.Manager)]
    [TestCase(TestUserIdentifiers.Buyer)]
    public async Task GetCartV1_UserIsAuthorized_ReturnsOk200(string userId)
    {
        Fixture.CartV1.AddUserIdentifier(userId);
        Fixture.MediatorMock.SetupSendReturns(Array.Empty<CartItemDto>() as IReadOnlyCollection<CartItemDto>);
        var cartId = Fixture.RandomId;

        var response = await Fixture.CartV1.GetAsync($"{cartId}");
        var responsecontent = await response.ReadContent<CartModel>();

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responsecontent.Should().NotBeNull().And.BeOfType<CartModel>();
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new GetCartItemsQuery(cartId));
    }

    [Test]
    public async Task GetCartV1_UserIsUnauthorized_ReturnsUnauthorized401()
    {
        var cartId = Fixture.RandomId;

        var response = await Fixture.CartV1.GetAsync($"{cartId}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
    }

    [TestCase(TestUserIdentifiers.Manager)]
    [TestCase(TestUserIdentifiers.Buyer)]
    public async Task GetCartV2_UserIsAuthorized_ReturnsOk200(string userId)
    {
        Fixture.CartV2.AddUserIdentifier(userId);
        Fixture.MediatorMock.SetupSendReturns(Array.Empty<CartItemDto>() as IReadOnlyCollection<CartItemDto>);
        var cartId = Fixture.RandomId;

        var response = await Fixture.CartV2.GetAsync($"{cartId}");
        var responsecontent = await response.ReadContent<List<CartItemDto>>();

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responsecontent.Should().NotBeNull().And.BeOfType<List<CartItemDto>>();
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new GetCartItemsQuery(cartId));
    }

    [Test]
    public async Task GetCartV2_UserIsUnauthorized_ReturnsUnauthorized401()
    {
        var cartId = Fixture.RandomId;

        var response = await Fixture.CartV2.GetAsync($"{cartId}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
    }

    [TestCase(TestUserIdentifiers.Manager)]
    [TestCase(TestUserIdentifiers.Buyer)]
    public async Task AddCartItemV1_UserIsAuthorized_ReturnsOk200(string userId)
    {
        Fixture.CartV1.AddUserIdentifier(userId);
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var cartId = Fixture.RandomId;
        var model = AddCartItemModel();

        var response = await Fixture.CartV1.PostAsJsonAsync($"{cartId}/items", model);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new AddCartItemCommand(cartId, new AddCartItemDto
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                Image = new AddCartItemImageModel { Uri = model.Image!.Uri, AltText = model.Image.AltText }
            }));
    }

    [Test]
    public async Task AddCartItemV1_UserIsUnauthorized_ReturnsUnauthorized401()
    {
        var cartId = Fixture.RandomId;
        var model = AddCartItemModel();

        var response = await Fixture.CartV1.PostAsJsonAsync($"{cartId}/items", model);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
    }

    [TestCase(TestUserIdentifiers.Manager)]
    [TestCase(TestUserIdentifiers.Buyer)]
    public async Task AddCartItemV2_UserIsAuthorized_ReturnsOk200(string userId)
    {
        Fixture.CartV2.AddUserIdentifier(userId);
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var cartId = Fixture.RandomId;
        var model = AddCartItemModel();

        var response = await Fixture.CartV2.PostAsJsonAsync($"{cartId}/items", model);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new AddCartItemCommand(cartId, new AddCartItemDto
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                Image = new AddCartItemImageModel { Uri = model.Image!.Uri, AltText = model.Image.AltText }
            }));
    }

    [Test]
    public async Task AddCartItemV2_UserIsUnauthorized_ReturnsUnauthorized401()
    {
        var cartId = Fixture.RandomId;
        var model = AddCartItemModel();

        var response = await Fixture.CartV2.PostAsJsonAsync($"{cartId}/items", model);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
    }

    [TestCase(TestUserIdentifiers.Manager)]
    [TestCase(TestUserIdentifiers.Buyer)]
    public async Task RemoveCartItemV1_UserIsAuthorized_ReturnsOk200(string userId)
    {
        Fixture.CartV1.AddUserIdentifier(userId);
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var cartId = Fixture.RandomId;
        var itemId = Fixture.RandomInt;

        var response = await Fixture.CartV1.DeleteAsync($"{cartId}/items/{itemId}");

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new RemoveCartItemCommand(cartId, itemId));
    }

    [Test]
    public async Task RemoveCartItemV1_UserIsUnauthorized_ReturnsUnauthorized401()
    {
        var cartId = Fixture.RandomId;
        var itemId = Fixture.RandomInt;

        var response = await Fixture.CartV1.DeleteAsync($"{cartId}/items/{itemId}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
    }

    [TestCase(TestUserIdentifiers.Manager)]
    [TestCase(TestUserIdentifiers.Buyer)]
    public async Task RemoveCartItemV2_UserIsAuthorized_ReturnsOk200(string userId)
    {
        Fixture.CartV2.AddUserIdentifier(userId);
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var cartId = Fixture.RandomId;
        var itemId = Fixture.RandomInt;

        var response = await Fixture.CartV2.DeleteAsync($"{cartId}/items/{itemId}");

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new RemoveCartItemCommand(cartId, itemId));
    }

    [Test]
    public async Task RemoveCartItemV2_UserIsUnauthorized_ReturnsUnauthorized401()
    {
        var cartId = Fixture.RandomId;
        var itemId = Fixture.RandomInt;

        var response = await Fixture.CartV2.DeleteAsync($"{cartId}/items/{itemId}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
    }

    private AddCartItemModel AddCartItemModel() =>
        new()
        {
            Id = Fixture.RandomInt,
            Name = Fixture.RandomString,
            Image = new CartItemImageModel
            {
                Uri = Fixture.RandomUrl,
                AltText = Fixture.RandomString
            },
            Price = Fixture.RandomInt,
            Quantity = Fixture.RandomUint
        };
}

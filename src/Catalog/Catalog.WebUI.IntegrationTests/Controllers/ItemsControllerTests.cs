namespace NetAdvancedShop.Catalog.WebUI.IntegrationTests.Controllers;

public class ItemsControllerTests : WebTests<CatalogServiceFixture>
{
    [Test]
    public async Task GetItem_UserIsManager_ReturnsOk200()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Manager);
        Fixture.MediatorMock.SetupSendReturns(new ItemDto());
        var itemId = Fixture.RandomInt;

        var response = await Fixture.Items.GetAsync($"{itemId}");

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new GetItemByIdQuery(itemId));
    }

    [Test]
    public async Task GetItem_UserIsBuyer_ReturnsOk200()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Buyer);
        Fixture.MediatorMock.SetupSendReturns(new ItemDto());
        var itemId = Fixture.RandomInt;

        var response = await Fixture.Items.GetAsync($"{itemId}");

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new GetItemByIdQuery(itemId));
    }

    [Test]
    public async Task GetItem_UserIsUnauthorized_ReturnsOk200()
    {
        Fixture.MediatorMock.SetupSendReturns(new ItemDto());
        var itemId = Fixture.RandomInt;

        var response = await Fixture.Items.GetAsync($"{itemId}");

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new GetItemByIdQuery(itemId));
    }

    [Test]
    public async Task GetItems_UserIsManager_ReturnsOk200()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Manager);
        Fixture.MediatorMock.SetupSendReturns(new PaginatedList<ItemDto>(
            new List<ItemDto>(), 0, 1, 10));
        var categoryId = Fixture.RandomInt;

        var response = await Fixture.Items.GetAsync($"?categoryId={categoryId}");

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new GetItemsWithPaginationQuery(categoryId));
    }

    [Test]
    public async Task GetItems_UserIsBuyer_ReturnsOk200()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Buyer);
        Fixture.MediatorMock.SetupSendReturns(new PaginatedList<ItemDto>(
            new List<ItemDto>(), 0, 1, 10));
        var categoryId = Fixture.RandomInt;

        var response = await Fixture.Items.GetAsync($"?categoryId={categoryId}");

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new GetItemsWithPaginationQuery(categoryId));
    }

    [Test]
    public async Task GetItems_UserIsUnauthorized_ReturnsOk200()
    {
        Fixture.MediatorMock.SetupSendReturns(new PaginatedList<ItemDto>(
            new List<ItemDto>(), 0, 1, 10));
        var categoryId = Fixture.RandomInt;

        var response = await Fixture.Items.GetAsync($"?categoryId={categoryId}");

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new GetItemsWithPaginationQuery(categoryId));
    }

    [Test]
    public async Task Create_UserIsManager_ReturnsOk200()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Manager);
        Fixture.MediatorMock.SetupSendReturns(Fixture.RandomInt);
        var model = CreateItemModel();

        var response = await Fixture.Items.PostAsJsonAsync("", model);

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new CreateItemCommand(
                model.Name, model.Description, model.ImageUri, model.CategoryId, model.Price, model.Amount));
    }

    [Test]
    public async Task Create_UserIsBuyer_ReturnsForbidden403()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Buyer);
        Fixture.MediatorMock.SetupSendReturns(Fixture.RandomInt);
        var model = CreateItemModel();

        var response = await Fixture.Items.PostAsJsonAsync("", model);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Forbidden);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
    }

    [Test]
    public async Task Create_UserIsUnauthorized_ReturnsUnauthorized401()
    {
        Fixture.MediatorMock.SetupSendReturns(Fixture.RandomInt);
        var model = CreateItemModel();

        var response = await Fixture.Items.PostAsJsonAsync("", model);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
    }

    [Test]
    public async Task Create_NameIsMissing_ReturnsBadRequest400()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Manager);
        var model = CreateItemModel();
        model.Name = null!;

        var response = await Fixture.Items.PostAsJsonAsync("", model);

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
    }

    [Test]
    public async Task Update_UserIsManager_PublishesEventAndReturnsOk200()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Manager);
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var itemId = Fixture.RandomInt;
        var model = UpdateItemModel();

        var response = await Fixture.Items.PutAsJsonAsync($"{itemId}", model);

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new UpdateItemCommand(
                itemId, model.Name, model.Description, model.ImageUri, model.CategoryId, model.Price, model.Amount));
        Fixture.PublishedEvents.SingleOrDefault()
            .Should()
            .NotBeNull()
            .And
            .BeOfType<ItemChangedApplicationEvent>()
            .And
            .BeEquivalentTo(new { ItemId = itemId, NewName = model.Name, NewPrice = model.Price });
    }

    [Test]
    public async Task Update_UserIsBuyer_ReturnsForbidden403()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Buyer);
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var itemId = Fixture.RandomInt;
        var model = UpdateItemModel();

        var response = await Fixture.Items.PutAsJsonAsync($"{itemId}", model);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Forbidden);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
        Fixture.PublishedEvents
            .Should()
            .BeEmpty();
    }

    [Test]
    public async Task Update_UserIsUnauthorized_ReturnsUnauthorized401()
    {
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var itemId = Fixture.RandomInt;
        var model = UpdateItemModel();

        var response = await Fixture.Items.PutAsJsonAsync($"{itemId}", model);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
        Fixture.PublishedEvents
            .Should()
            .BeEmpty();
    }

    [Test]
    public async Task Update_NameIsMissing_ReturnsBadRequest400()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Manager);
        var model = UpdateItemModel();
        model.Name = null!;

        var response = await Fixture.Items.PutAsJsonAsync($"{Fixture.RandomInt}", model);

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
        Fixture.PublishedEvents
            .Should()
            .BeEmpty();
    }

    [Test]
    public async Task Delete_UserIsManager_ReturnsOk200()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Manager);
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var itemId = Fixture.RandomInt;

        var response = await Fixture.Items.DeleteAsync($"{itemId}");

        response
            .Should()
            .NotBeNull();
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new DeleteItemCommand(itemId));
    }

    [Test]
    public async Task Delete_UserIsBuyer_ReturnsForbidden403()
    {
        Fixture.Items.AddUserIdentifier(TestUserIdentifiers.Buyer);
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var itemId = Fixture.RandomInt;

        var response = await Fixture.Items.DeleteAsync($"{itemId}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Forbidden);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
        Fixture.PublishedEvents
            .Should()
            .BeEmpty();
    }

    [Test]
    public async Task Delete_UserIsUnauthorized_ReturnsUnauthorized401()
    {
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var itemId = Fixture.RandomInt;

        var response = await Fixture.Items.DeleteAsync($"{itemId}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
        Fixture.SentRequests
            .Should()
            .BeEmpty();
        Fixture.PublishedEvents
            .Should()
            .BeEmpty();
    }

    private CreateItemModel CreateItemModel() =>
        new()
        {
            Name = Fixture.RandomString,
            Description = Fixture.RandomString,
            ImageUri = Fixture.RandomUrl,
            CategoryId = Fixture.RandomInt,
            Price = Fixture.RandomUint,
            Amount = Fixture.RandomUint
        };

    private UpdateItemModel UpdateItemModel() =>
        new()
        {
            Name = Fixture.RandomString,
            Description = Fixture.RandomString,
            ImageUri = Fixture.RandomUrl,
            CategoryId = Fixture.RandomInt,
            Price = Fixture.RandomUint,
            Amount = Fixture.RandomUint
        };
}

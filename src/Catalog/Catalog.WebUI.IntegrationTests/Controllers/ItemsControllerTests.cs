namespace Catalog.WebUI.IntegrationTests.Controllers;

public class ItemsControllerTests : WebTests<CatalogServiceFixture>
{
    [Test]
    public async Task GetItem_ReturnsOk200()
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
    public async Task GetItems_ReturnsOk200()
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
    public async Task Create_ReturnsOk200()
    {
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
    public async Task Create_NameIsMissing_ReturnsBadRequest400()
    {
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
    public async Task Update_PublishesEventAndReturnsOk200()
    {
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
    public async Task Update_NameIsMissing_ReturnsBadRequest400()
    {
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
    public async Task Delete_ReturnsOk200()
    {
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

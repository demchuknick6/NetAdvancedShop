namespace Catalog.WebUI.IntegrationTests.Controllers;

public class CategoriesControllerTests : WebTests<CatalogServiceFixture>
{
    [Test]
    public async Task GetCategory_ReturnsOk200()
    {
        Fixture.MediatorMock.SetupSendReturns(new CategoryDto());
        var categoryId = Fixture.RandomInt;

        var response = await Fixture.Categories.GetAsync($"{categoryId}");

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
            .ContainSameTo(new GetCategoryByIdQuery(categoryId));
    }

    [Test]
    public async Task GetCategories_ReturnsOk200()
    {
        Fixture.MediatorMock.SetupSendReturns(Array.Empty<CategoryDto>() as IReadOnlyCollection<CategoryDto>);

        var response = await Fixture.Categories.GetAsync("");

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
            .AllBeOfType<GetCategoriesQuery>();
    }

    [Test]
    public async Task Create_ReturnsOk200()
    {
        Fixture.MediatorMock.SetupSendReturns(Fixture.RandomInt);
        var model = CreateCategoryModel();

        var response = await Fixture.Categories.PostAsJsonAsync("", model);

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
            .ContainSameTo(new CreateCategoryCommand(
                model.Name, model.ImageUri, model.ParentCategoryId));
    }

    [Test]
    public async Task Create_NameIsMissing_ReturnsBadRequest400()
    {
        var model = CreateCategoryModel();
        model.Name = null!;

        var response = await Fixture.Categories.PostAsJsonAsync("", model);

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
    public async Task Update_ReturnsOk200()
    {
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var categoryId = Fixture.RandomInt;
        var model = UpdateCategoryModel();

        var response = await Fixture.Categories.PutAsJsonAsync($"{categoryId}", model);

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
            .ContainSameTo(new UpdateCategoryCommand(
                categoryId, model.Name, model.ImageUri, model.ParentCategoryId));
    }

    [Test]
    public async Task Update_NameIsMissing_ReturnsBadRequest400()
    {
        var model = UpdateCategoryModel();
        model.Name = null!;

        var response = await Fixture.Categories.PutAsJsonAsync($"{Fixture.RandomInt}", model);

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
    public async Task Delete_ReturnsOk200()
    {
        Fixture.MediatorMock.SetupSendReturns(Unit.Value);
        var categoryId = Fixture.RandomInt;

        var response = await Fixture.Categories.DeleteAsync($"{categoryId}");

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
            .ContainSameTo(new DeleteCategoryCommand(categoryId));
    }

    private CreateCategoryModel CreateCategoryModel() =>
        new()
        {
            Name = Fixture.RandomString,
            ImageUri = Fixture.RandomUrl,
            ParentCategoryId = Fixture.RandomInt
        };

    private UpdateCategoryModel UpdateCategoryModel() =>
        new()
        {
            Name = Fixture.RandomString,
            ImageUri = Fixture.RandomUrl,
            ParentCategoryId = Fixture.RandomInt
        };
}

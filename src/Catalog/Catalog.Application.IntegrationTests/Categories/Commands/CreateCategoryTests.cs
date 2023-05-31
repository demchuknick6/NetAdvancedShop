namespace NetAdvancedShop.Catalog.Application.IntegrationTests.Categories.Commands;

public class CreateCategoryTests : ApplicationTests<CatalogApplicationFixture>
{
    [Test]
    public async Task Send_NameIsNull_ThrowsValidationException()
    {
        var command = new CreateCategoryCommand(null!, null, null);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<ValidationException>()
            .Where(x => x.Errors.Values.Single().Contains("Name is required."));
    }

    [Test]
    public async Task Send_NameIsTooLong_ThrowsValidationException()
    {
        var command = new CreateCategoryCommand(new string('*', 51), null, null);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<ValidationException>()
            .Where(x => x.Errors.Values.Single().Contains("Name must not exceed 50 characters."));
    }

    [Test]
    public async Task Send_ImageUrlIsNotValid_ThrowsValidationException()
    {
        var command = new CreateCategoryCommand(Application.RandomString, Application.RandomString, null);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<ValidationException>()
            .Where(x => x.Errors.Values.Single().Contains("ImageUrl can be empty or must be a valid image URL."));
    }

    [Test]
    public async Task Send_ParentCategoryNotExists_ThrowsNotFoundException()
    {
        var command = new CreateCategoryCommand(Application.RandomString, null, Application.RandomInt);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Handle_CreatesCategory()
    {
        var name = Application.RandomString;
        var imageUrl = Application.RandomUrl;
        int? parentCategoryId = null;
        var command = new CreateCategoryCommand(name, imageUrl, parentCategoryId);

        var id = await SendAsync(command);

        var category = await Application.FindAsync<Category>(id);

        category
            .Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(
                new
                {
                    Id = id,
                    Name = name,
                    ImageUri = imageUrl,
                    ParentCategoryId = parentCategoryId,
                });
    }
}

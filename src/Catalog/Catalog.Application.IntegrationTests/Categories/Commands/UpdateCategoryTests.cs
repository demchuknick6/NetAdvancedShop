namespace NetAdvancedShop.Catalog.Application.IntegrationTests.Categories.Commands;

public class UpdateCategoryTests : ApplicationTests<CatalogApplicationFixture>
{
    [Test]
    public async Task Send_NameIsNull_ThrowsValidationException()
    {
        var command = new UpdateCategoryCommand(Application.RandomInt, null!, null, null);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<ValidationException>()
            .Where(x => x.Errors.Values.Single().Contains("Name is required."));
    }

    [Test]
    public async Task Send_NameIsTooLong_ThrowsValidationException()
    {
        var command = new UpdateCategoryCommand(Application.RandomInt, new string('*', 51), null, null);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<ValidationException>()
            .Where(x => x.Errors.Values.Single().Contains("Name must not exceed 50 characters."));
    }

    [Test]
    public async Task Send_ImageUrlIsNotValid_ThrowsValidationException()
    {
        var command = new UpdateCategoryCommand(Application.RandomInt, Application.RandomString,
            Application.RandomString, null);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<ValidationException>()
            .Where(x => x.Errors.Values.Single().Contains("ImageUrl can be empty or must be a valid image URL."));
    }

    [Test]
    public async Task Send_CategoryNotExists_ThrowsNotFoundException()
    {
        var command = new UpdateCategoryCommand(Application.RandomInt, Application.RandomString,
            null, null);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Send_ParentCategoryNotExists_ThrowsNotFoundException()
    {
        var category = new Category
        {
            Name = Application.RandomString,
            ImageUri = null,
            ParentCategoryId = null
        };
        await Application.AddAsync(category);

        var updatedName = Application.RandomString;
        var updatedImageUrl = Application.RandomUrl;
        var updatedParentCategoryId = Application.RandomInt;

        var command = new UpdateCategoryCommand(category.Id, updatedName,
            updatedImageUrl, updatedParentCategoryId);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Send_UpdatesCategory()
    {
        var category1 = new Category
        {
            Name = Application.RandomString,
            ImageUri = null,
            ParentCategoryId = null
        };
        var category2 = new Category
        {
            Name = Application.RandomString,
            ImageUri = null,
            ParentCategoryId = null
        };
        await Application.AddAsync(category1);
        await Application.AddAsync(category2);
        var updatedName = Application.RandomString;
        var updatedImageUrl = Application.RandomUrl;

        var command = new UpdateCategoryCommand(category1.Id, updatedName,
            updatedImageUrl, category2.Id);

        await SendAsync(command);

        var category = await Application.FindAsync<Category>(category1.Id);

        category
            .Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(
                new
                {
                    category1.Id,
                    Name = updatedName,
                    ImageUri = updatedImageUrl,
                    ParentCategoryId = category2.Id
                });
    }
}

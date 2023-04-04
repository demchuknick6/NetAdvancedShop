namespace Catalog.Application.IntegrationTests.Items.Commands;

public class CreateItemTests : ApplicationTests<CatalogApplicationFixture>
{
    [Test]
    public async Task Send_NameIsNull_ThrowsValidationException()
    {
        var command = new CreateItemCommand(
            null!, null, null, Application.RandomInt,
            Application.RandomInt, Application.RandomUint);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<ValidationException>()
            .Where(x => x.Errors.Values.Single().Contains("Name is required."));
    }

    [Test]
    public async Task Send_NameIsTooLong_ThrowsValidationException()
    {
        var command = new CreateItemCommand(new string('*', 51), null,
            null, Application.RandomInt, Application.RandomInt, Application.RandomUint);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<ValidationException>()
            .Where(x => x.Errors.Values.Single().Contains("Name must not exceed 50 characters."));
    }

    [Test]
    public async Task Send_ImageUrlIsNotValid_ThrowsValidationException()
    {
        var command = new CreateItemCommand(Application.RandomString, null,
            Application.RandomString, Application.RandomInt, Application.RandomInt, Application.RandomUint);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<ValidationException>()
            .Where(x => x.Errors.Values.Single().Contains("ImageUrl can be empty or must be a valid image URL."));
    }

    [Test]
    public async Task Send_DescriptionIsNotValid_ThrowsValidationException()
    {
        var xssDescription = "Hello <a href='javascript:alert(1)'>World</a>! <p>Valid HTML</p>";
        var command = new CreateItemCommand(Application.RandomString, xssDescription,
            Application.RandomUrl, Application.RandomInt, Application.RandomInt, Application.RandomUint);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<ValidationException>()
            .Where(x => x.Errors.Values.Single().Contains("Description can be empty or contain valid HTML or plain text, but cannot contain potentially dangerous input."));
    }

    [Test]
    public async Task Send_CategoryNotExists_ThrowsNotFoundException()
    {
        var command = new CreateItemCommand(
            Application.RandomString, null, null, Application.RandomInt,
            Application.RandomInt, Application.RandomUint);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Handle_CreatesItem()
    {
        var category = new Category
        {
            Name = Application.RandomString,
            ImageUri = null,
            ParentCategoryId = null,
        };

        await Application.AddAsync(category);
        var name = Application.RandomString;
        var description = Application.RandomString;
        var imageUrl = Application.RandomUrl;
        var price = Application.RandomInt;
        var amount = Application.RandomUint;

        var command = new CreateItemCommand(name, description, imageUrl, category.Id, price, amount);

        var id = await SendAsync(command);

        var item = await Application.FindAsync<Item>(id);

        item
            .Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(new
            {
                Name = name,
                Description = description,
                ImageUri = imageUrl,
                Price = price,
                Amount = amount,
                CategoryId = category.Id
            });
    }
}

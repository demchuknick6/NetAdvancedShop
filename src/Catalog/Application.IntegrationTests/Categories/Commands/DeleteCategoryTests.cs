namespace Catalog.Application.IntegrationTests.Categories.Commands;

public class DeleteCategoryTests : ApplicationTests<CatalogApplicationFixture>
{
    [Test]
    public async Task Send_CategoryNotExists_ThrowsNotFoundException()
    {
        var command = new DeleteCategoryCommand(Application.RandomInt);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Send_DeletesCategory()
    {
        var category = new Category
        {
            Name = Application.RandomString,
            ImageUri = null,
            ParentCategoryId = null
        };
        await Application.AddAsync(category);

        await SendAsync(new DeleteCategoryCommand(category.Id));

        var deletedCategory = await Application.FindAsync<Category>(category.Id);

        deletedCategory.Should().BeNull();
    }
}

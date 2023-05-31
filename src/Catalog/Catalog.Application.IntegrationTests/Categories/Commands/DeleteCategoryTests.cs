namespace NetAdvancedShop.Catalog.Application.IntegrationTests.Categories.Commands;

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
    public async Task Send_DeletesCategoryWithItem()
    {
        var category = new Category
        {
            Name = Application.RandomString,
            ImageUri = null,
            ParentCategoryId = null
        };
        await Application.AddAsync(category);
        var item = new Item
        {
            Name = Application.RandomString,
            Description = null,
            ImageUri = null,
            Price = Application.RandomInt,
            Amount = Application.RandomUint,
            CategoryId = category.Id
        };
        await Application.AddAsync(item);

        await SendAsync(new DeleteCategoryCommand(category.Id));

        var deletedCategory = await Application.FindAsync<Category>(category.Id);
        var deletedItem = await Application.FindAsync<Item>(item.Id);
        deletedCategory.Should().BeNull();
        deletedItem.Should().BeNull();
    }
}

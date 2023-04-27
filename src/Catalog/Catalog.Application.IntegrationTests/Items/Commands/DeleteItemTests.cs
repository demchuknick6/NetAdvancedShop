namespace Catalog.Application.IntegrationTests.Items.Commands;

public class DeleteItemTests : ApplicationTests<CatalogApplicationFixture>
{
    [Test]
    public async Task Send_ItemNotExists_ThrowsNotFoundException()
    {
        var command = new DeleteItemCommand(Application.RandomInt);

        await Application.Awaiting(_ => SendAsync(command))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Send_DeletesItem()
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

        await SendAsync(new DeleteItemCommand(item.Id));

        var deletedItem = await Application.FindAsync<Item>(item.Id);

        deletedItem.Should().BeNull();
    }
}

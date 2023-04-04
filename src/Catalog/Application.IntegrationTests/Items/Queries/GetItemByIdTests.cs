namespace Catalog.Application.IntegrationTests.Items.Queries;

public class GetItemByIdTests : ApplicationTests<CatalogApplicationFixture>
{
    [Test]
    public async Task Send_ItemNotExists_ThrowsNotFoundException()
    {
        var query = new GetItemByIdQuery(Application.RandomInt);

        await Application.Awaiting(_ => SendAsync(query))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Send_ReturnsItemById()
    {
        var item1 = RandomItem();
        var item2 = RandomItem();
        var category = new Category
        {
            Name = Application.RandomString,
            ImageUri = null,
            ParentCategoryId = null,
            Items = new[] { item1, item2 }
        };

        await Application.AddAsync(category);

        var result = await SendAsync(new GetItemByIdQuery(2));

        result
            .Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(new
            {
                item2.Id,
                item2.Name,
                item2.Description,
                item2.ImageUri,
                item2.Price,
                item2.Amount,
                item2.CategoryId
            });
    }

    private Item RandomItem() =>
        new()
        {
            Name = Application.RandomString,
            Description = Application.RandomString,
            ImageUri = Application.RandomUrl,
            Price = Application.RandomInt,
            Amount = Application.RandomUint,
            CategoryId = 1
        };
}

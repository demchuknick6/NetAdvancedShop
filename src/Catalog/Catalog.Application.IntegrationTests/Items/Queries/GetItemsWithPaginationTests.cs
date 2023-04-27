namespace Catalog.Application.IntegrationTests.Items.Queries;

public class GetItemsWithPaginationTests : ApplicationTests<CatalogApplicationFixture>
{
    [Test]
    public async Task Send_CategoryNotExists_ThrowsNotFoundException()
    {
        var query = new GetItemsWithPaginationQuery(Application.RandomInt);

        await Application.Awaiting(_ => SendAsync(query))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Send_ReturnsItemsWithPagination()
    {
        var item1 = RandomItem(1 + Application.RandomString, 1);
        var item2 = RandomItem(2 + Application.RandomString, 1);
        var item3 = RandomItem(3 + Application.RandomString, 1);
        var item4 = RandomItem(4 + Application.RandomString, 2);
        var category1 = RandomCategory(item1, item2, item3);
        var category2 = RandomCategory(item4);
        var category3 = RandomCategory(Array.Empty<Item>());

        await Application.AddAsync(category1);
        await Application.AddAsync(category2);
        await Application.AddAsync(category3);

        var pageNumber = 1;
        var pageSize = 2;

        var result = await SendAsync(new GetItemsWithPaginationQuery(category1.Id, pageNumber, pageSize));

        result
            .Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(new
            {
                Items = new[]
                {
                    new
                    {
                        item1.Id,
                        item1.Name,
                        item1.Description,
                        item1.ImageUri,
                        item1.CategoryId,
                        item1.Price,
                        item1.Amount
                    },
                    new
                    {
                        item2.Id,
                        item2.Name,
                        item2.Description,
                        item2.ImageUri,
                        item2.CategoryId,
                        item2.Price,
                        item2.Amount
                    }
                },
                PageNumber = pageNumber,
                TotalCount = 3,
                HasNextPage = true,
                HasPreviousPage = false
            });
    }

    private Item RandomItem(string name, int categoryId) =>
        new()
        {
            Name = name,
            Description = Application.RandomString,
            ImageUri = Application.RandomUrl,
            Price = Application.RandomInt,
            Amount = Application.RandomUint,
            CategoryId = categoryId
        };

    private Category RandomCategory(params Item[] items) =>
        new()
        {
            Name = Application.RandomString,
            ImageUri = null,
            ParentCategoryId = null,
            Items = items
        };
}

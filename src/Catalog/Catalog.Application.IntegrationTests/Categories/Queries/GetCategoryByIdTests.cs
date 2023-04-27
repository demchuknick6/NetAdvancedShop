namespace Catalog.Application.IntegrationTests.Categories.Queries;

public class GetCategoryByIdTests : ApplicationTests<CatalogApplicationFixture>
{
    [Test]
    public async Task Send_CategoryNotExists_ThrowsNotFoundException()
    {
        var query = new GetCategoryByIdQuery(Application.RandomInt);

        await Application.Awaiting(_ => SendAsync(query))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Send_ReturnsCategoryById()
    {
        var category1 = new Category
        {
            Name = Application.RandomString,
            ImageUri = Application.RandomUrl,
            ParentCategoryId = null
        };
        var category2 = new Category
        {
            Name = Application.RandomString,
            ImageUri = null,
            ParentCategoryId = null
        };
        var category3 = new Category
        {
            Name = Application.RandomString,
            ImageUri = null,
            ParentCategoryId = 1,
            Items = new[]
            {
                new Item
                {
                    Name = Application.RandomString,
                    Description = Application.RandomString,
                    ImageUri = Application.RandomUrl,
                    Price = Application.RandomInt,
                    Amount = Application.RandomUint,
                    CategoryId = 3
                }
            }
        };
        await Application.AddAsync(category1);
        await Application.AddAsync(category2);
        await Application.AddAsync(category3);

        var result = await SendAsync(new GetCategoryByIdQuery(3));

        result
            .Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(new
            {
                category3.Id,
                category3.Name,
                category3.ImageUri,
                category3.ParentCategoryId,
                Items = new[]
                {
                    new
                    {
                        category3.Items.Single().Id,
                        category3.Items.Single().Name
                    }
                }
            });
    }
}

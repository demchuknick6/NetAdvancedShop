namespace Catalog.Application.IntegrationTests.Categories.Queries;

public class GetCategoriesTests : ApplicationTests<CatalogApplicationFixture>
{
    [Test]
    public async Task Send_ReturnsCategories()
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
            ParentCategoryId = 1
        };

        await Application.AddAsync(category1);
        await Application.AddAsync(category2);
        await Application.AddAsync(category3);

        var result = await SendAsync(new GetCategoriesQuery());

        result
            .Should()
            .HaveCount(3)
            .And
            .BeEquivalentTo(new[]
            {
                new
                {
                    category1.Id,
                    category1.Name,
                    category1.ImageUri,
                    category1.ParentCategoryId
                },
                new
                {
                    category2.Id,
                    category2.Name,
                    category2.ImageUri,
                    category2.ParentCategoryId
                }!,
                new
                {
                    category3.Id,
                    category3.Name,
                    category3.ImageUri,
                    category3.ParentCategoryId
                }!
            });
    }
}

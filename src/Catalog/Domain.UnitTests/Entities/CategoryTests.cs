namespace Catalog.Domain.UnitTests.Entities;

public class CategoryTests
{
    [Test]
    public void Ctor_CreatesEntity()
    {
        var name = "test";

        var category = new Category { Name = name };

        category.Should().NotBeNull();
        category.Name.Should().Be(name);
    }

    [Test]
    public void Ctor_NameIsTooLong_ThrowsCatalogDomainException()
    {
        var name = new string('*', 51);

        this.Invoking(_ => new Category { Name = name })
            .Should()
            .ThrowExactly<CatalogDomainException>()
            .WithMessage("Category Name cannot exceed 50 chars length.");
    }
}

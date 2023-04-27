namespace Catalog.Domain.UnitTests.Entities;

public class ItemTests
{
    [Test]
    public void Ctor_CreatesEntity()
    {
        var name = "test";
        var categoryId = 3;
        var price = 5;
        uint amount = 1;

        var item = new Item { Name = name, CategoryId = categoryId, Price = price, Amount = amount };

        item.Should().NotBeNull();
        item.Name.Should().Be(name);
        item.CategoryId.Should().Be(categoryId);
        item.Price.Should().Be(price);
        item.Amount.Should().Be(amount);
    }

    [Test]
    public void Ctor_NameIsTooLong_ThrowsCatalogDomainException()
    {
        var name = new string('*', 51);

        this.Invoking(_ => new Item { Name = name, CategoryId = 0, Price = 0, Amount = 0 })
            .Should()
            .ThrowExactly<CatalogDomainException>()
            .WithMessage("Item Name cannot exceed 50 chars length.");
    }
}

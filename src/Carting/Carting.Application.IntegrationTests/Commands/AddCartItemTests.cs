namespace NetAdvancedShop.Carting.Application.IntegrationTests.Commands;

public class AddCartItemTests : ApplicationTests<CartingApplicationFixture>
{
    [Test]
    public async Task Send_CartNotExists_CreatesNewAndAddsItem()
    {
        var cartId = Application.RandomId;
        var cartItem = AddCartItemDto();

        var command = new AddCartItemCommand(cartId, cartItem);

        await SendAsync(command, clearEntries: false);

        using var context = new CartingContext(Application.Settings);

        var cart = context.Carts.FindOne(c => c.Id == cartId);

        cart.Should().NotBeNull();
        cart.Items
            .Should()
            .HaveCount(1)
            .And
            .BeEquivalentTo(new[]
            {
                new
                {
                    cartItem.Id,
                    cartItem.Name,
                    cartItem.Price,
                    cartItem.Quantity,
                    cartItem.Image
                }
            });
    }

    [Test]
    public async Task Send_CartWithItemExists_IncreasesItemQuantity()
    {
        var cartId = Application.RandomId;
        var cartItem = new CartItem
        {
            Id = Application.RandomInt,
            Name = Application.RandomString,
            Price = Application.RandomInt,
            Quantity = Application.RandomUint,
            Image = null
        };

        using (var ctx = new CartingContext(Application.Settings))
        {
            ctx.Carts.Insert(new Cart { Id = cartId, Items = new[] { cartItem } });
        }

        var addCartItem = AddCartItemDto(cartItem.Id);
        var command = new AddCartItemCommand(cartId, addCartItem);

        // since cart with item exists, increase item quantity
        await SendAsync(command, clearEntries: false);

        using var context = new CartingContext(Application.Settings);

        var cart = context.Carts.FindOne(c => c.Id == cartId);

        cart.Should().NotBeNull();
        cart.Items
            .Should()
            .HaveCount(1)
            .And
            .BeEquivalentTo(new[]
            {
                new
                {
                    cartItem.Id,
                    cartItem.Name,
                    cartItem.Price,
                    Quantity = cartItem.Quantity + addCartItem.Quantity,
                    cartItem.Image
                }
            });
    }

    private AddCartItemDto AddCartItemDto(int? id = null) =>
        new()
        {
            Id = id ?? Application.RandomInt,
            Name = Application.RandomString,
            Price = Application.RandomInt,
            Quantity = Application.RandomUint,
            Image = null
        };
}

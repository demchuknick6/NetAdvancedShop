namespace Carting.Application.IntegrationTests.Commands;

public class RemoveCartItemTests : ApplicationTests<CartingApplicationFixture>
{
    [Test]
    public async Task Send_CartNotExists_ThrowsNotFoundException()
    {
        var command = new RemoveCartItemCommand(Application.RandomId, Application.RandomInt);

        await Application.Awaiting(_ => SendAsync(command, clearEntries: false))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Send_CartItemNotExists_ThrowsNotFoundException()
    {
        var cartId = Application.RandomId;

        using (var ctx = new CartingContext(Application.Settings))
        {
            ctx.Carts.Insert(new Cart { Id = cartId });
        }

        var command = new RemoveCartItemCommand(cartId, Application.RandomInt);

        await Application.Awaiting(_ => SendAsync(command, clearEntries: false))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Send_RemovesCartItem()
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

        var command = new RemoveCartItemCommand(cartId, cartItem.Id);

        await SendAsync(command, clearEntries: false);

        using var context = new CartingContext(Application.Settings);
        var updatedCart = context.Carts.FindOne(c => c.Id == cartId);

        updatedCart.Items.Should().HaveCount(0);
    }
}

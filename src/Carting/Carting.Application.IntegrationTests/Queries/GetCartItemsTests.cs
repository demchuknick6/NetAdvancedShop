namespace Carting.Application.IntegrationTests.Queries;

public class GetCartItemsTests : ApplicationTests<CartingApplicationFixture>
{
    [Test]
    public async Task Send_CartNotExists_ThrowsNotFoundException()
    {
        var query = new GetCartItemsQuery(Application.RandomId);

        await Application.Awaiting(_ => SendAsync(query, clearEntries: false))
            .Should()
            .ThrowExactlyAsync<NotFoundException>();
    }

    [Test]
    public async Task Send_ReturnsCartWithItems()
    {
        var cartId = Application.RandomId;
        var cartItem1 = RandomCartItem();
        var cartItem2 = RandomCartItem();
        cartItem2.Image = new CartItemImage { Uri = Application.RandomUrl, AltText = Application.RandomString };

        using (var ctx = new CartingContext(Application.Settings))
        {
            ctx.Carts.Insert(new Cart { Id = cartId, Items = new[] { cartItem1, cartItem2 } });
        }

        var query = new GetCartItemsQuery(cartId);

        var result = await SendAsync(query, clearEntries: false);
        result.Should()
            .NotBeNull()
            .And
            .HaveCount(2)
            .And
            .BeEquivalentTo(
                new[]
                {
                    new
                    {
                        cartItem1.Id,
                        cartItem1.Name,
                        CartImageUrl = cartItem1.Image?.Uri,
                        CartImageAltText = cartItem1.Image?.AltText,
                        cartItem1.Price,
                        cartItem1.Quantity
                    },
                    new
                    {
                        cartItem2.Id,
                        cartItem2.Name,
                        CartImageUrl = cartItem2.Image?.Uri,
                        CartImageAltText = cartItem2.Image?.AltText,
                        cartItem2.Price,
                        cartItem2.Quantity
                    }
                });
    }

    private CartItem RandomCartItem() =>
        new()
        {
            Id = Application.RandomInt,
            Name = Application.RandomString,
            Price = Application.RandomInt,
            Quantity = Application.RandomUint,
            Image = null
        };
}

namespace NetAdvancedShop.Carting.Application.IntegrationTests.Commands;

public class UpdateCartitemTests : ApplicationTests<CartingApplicationFixture>
{
    [Test]
    public async Task Send_CartsWithItemExist_UpdatesCartItem()
    {
        var cartId1 = Application.RandomId;
        var cartId2 = Application.RandomId;
        var cartId3 = Application.RandomId;
        var cartItemId = Application.RandomInt;
        var newName = Application.RandomString;
        var newPrice = Application.RandomInt;

        using (var ctx = new CartingContext(Application.Settings))
        {
            ctx.Carts.Insert(
                new Cart { Id = cartId1, Items = new[]
                    { CartItem(cartItemId) } });
            ctx.Carts.Insert(
                new Cart { Id = cartId2, Items = new[]
                    { CartItem(cartItemId), CartItem(Application.RandomInt) } });
            ctx.Carts.Insert(
                new Cart { Id = cartId3, Items = new[]
                    { CartItem(Application.RandomInt) } });
        }

        var command = new UpdateCartItemCommand(cartItemId, new UpdateCartItemDto
            { NewName = newName, NewPrice = newPrice });

        await SendAsync(command, clearEntries: false);

        using var context = new CartingContext(Application.Settings);

        var cart1 = context.Carts.FindOne(c => c.Id == cartId1);
        var cart2 = context.Carts.FindOne(c => c.Id == cartId2);
        var cart3 = context.Carts.FindOne(c => c.Id == cartId3);

        cart1.Items
            .Should()
            .HaveCount(1)
            .And
            .BeEquivalentTo(new[]
            {
                new
                {
                    Id = cartItemId,
                    Name = newName,
                    Price = newPrice
                }
            });
        cart2.Items
            .Should()
            .HaveCount(2)
            .And
            .ContainEquivalentOf(
                new
                {
                    Id = cartItemId,
                    Name = newName,
                    Price = newPrice
                });
        cart3.Items
            .Should()
            .HaveCount(1)
            .And
            .NotContainEquivalentOf(
                new
                {
                    Id = cartItemId,
                    Name = newName,
                    Price = newPrice
                });
    }

    private CartItem CartItem(int id) => new()
    {
        Id = id,
        Name = Application.RandomString,
        Price = Application.RandomInt,
        Quantity = Application.RandomUint,
        Image = null
    };
}

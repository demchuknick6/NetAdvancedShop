namespace Carting.WebUI.IntegrationTests.ApplicationEvents.EventHandling;

public class ItemChangedApplicationEventHandlerTests : WebTests<CartingServiceFixture>
{
    [Test]
    public async Task Handle_SendsUpdateCartItemCommand()
    {
        var cartItemId = Fixture.RandomInt;
        var newName = Fixture.RandomString;
        var newPrice = Fixture.RandomInt;

        await Handle(new ItemChangedApplicationEvent(cartItemId, newName, newPrice));

        Fixture.SentRequests
            .Should()
            .ContainSingle()
            .And
            .ContainSameTo(new UpdateCartItemCommand(
                cartItemId, new UpdateCartItemDto
                {
                    NewName = newName,
                    NewPrice = newPrice
                }));
    }

    private Task Handle(ItemChangedApplicationEvent @event) =>
        Fixture.HandleApplicationEvent<ItemChangedApplicationEvent, ItemChangedApplicationEventHandler>(@event);
}

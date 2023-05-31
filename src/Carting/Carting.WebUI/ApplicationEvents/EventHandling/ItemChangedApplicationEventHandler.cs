namespace NetAdvancedShop.Carting.WebUI.ApplicationEvents.EventHandling;

public class ItemChangedApplicationEventHandler : IApplicationEventHandler<ItemChangedApplicationEvent>
{
    private readonly ISender _mediator;
    private readonly ILogger<ItemChangedApplicationEventHandler> _logger;

    public ItemChangedApplicationEventHandler(ISender mediator, ILogger<ItemChangedApplicationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(ItemChangedApplicationEvent @event)
    {
        _logger.LogInformation("----- Handling application event: {ApplicationEventId} at {AppName} - ({@ApplicationEvent})", @event.Id, Program.AppName, @event);

        // Ideally a domain event should be raised here instead of a command.
        await _mediator.Send(new UpdateCartItemCommand(
            @event.ItemId,
            new UpdateCartItemDto
            {
                NewName = @event.NewName,
                NewPrice = @event.NewPrice
            }));
    }
}

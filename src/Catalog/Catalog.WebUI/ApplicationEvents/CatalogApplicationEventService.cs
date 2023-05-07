namespace Catalog.WebUI.ApplicationEvents;

public class CatalogApplicationEventService : ICatalogApplicationEventService
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<CatalogApplicationEventService> _logger;

    public CatalogApplicationEventService(IEventBus eventBus, ILogger<CatalogApplicationEventService> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public void PublishThroughEventBus(ApplicationEvent @event)
    {
        try
        {
            _logger.LogInformation("----- Publishing application event: {ApplicationEventId_published} from {AppName} - ({@ApplicationEvent})", @event.Id, Program.AppName, @event);
            _eventBus.Publish(@event);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Publishing application event: {ApplicationEventId} from {AppName} - ({@ApplicationEvent})", @event.Id, Program.AppName, @event);
        }
    }
}

﻿namespace NetAdvancedShop.Catalog.WebUI.ApplicationEvents;

public interface ICatalogApplicationEventService
{
    void PublishThroughEventBus(ApplicationEvent @event);
}

using Common.Infrastructure.EventBus.Events;

namespace Catalog.Application.ApplicationEvents;

public record ItemChangedApplicationEvent : ApplicationEvent
{
    public int ItemId  { get; private init; }

    public string NewName { get; private init; }

    public decimal NewPrice { get; private init; }

    public ItemChangedApplicationEvent(int itemId, string newName, decimal newPrice)
    {
        ItemId = itemId;
        NewName = newName;
        NewPrice = newPrice;
    }
}

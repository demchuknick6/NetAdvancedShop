namespace NetAdvancedShop.Common.Infrastructure.EventBus;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;

    void AddSubscription<T, TH>()
        where T : ApplicationEvent
        where TH : IApplicationEventHandler<T>;

    void RemoveSubscription<T, TH>()
            where TH : IApplicationEventHandler<T>
            where T : ApplicationEvent;

    bool HasSubscriptionsForEvent(string eventName);
    Type GetEventTypeByName(string eventName);
    void Clear();
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
    string GetEventKey<T>();
}

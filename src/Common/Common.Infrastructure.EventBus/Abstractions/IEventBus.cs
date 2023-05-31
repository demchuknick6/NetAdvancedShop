namespace NetAdvancedShop.Common.Infrastructure.EventBus.Abstractions;

public interface IEventBus
{
    void Publish(ApplicationEvent @event);

    void Subscribe<T, TH>()
        where T : ApplicationEvent
        where TH : IApplicationEventHandler<T>;

    void Unsubscribe<T, TH>()
        where TH : IApplicationEventHandler<T>
        where T : ApplicationEvent;
}

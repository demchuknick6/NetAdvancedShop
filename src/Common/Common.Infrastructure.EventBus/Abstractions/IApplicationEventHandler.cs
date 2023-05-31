namespace NetAdvancedShop.Common.Infrastructure.EventBus.Abstractions;

public interface IApplicationEventHandler<in TApplicationEvent> : IApplicationEventHandler
    where TApplicationEvent : ApplicationEvent
{
    Task Handle(TApplicationEvent @event);
}

public interface IApplicationEventHandler
{
}

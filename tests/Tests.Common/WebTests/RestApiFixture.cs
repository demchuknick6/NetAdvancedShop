namespace NetAdvancedShop.Tests.Common.WebTests;

public class RestApiFixture<TProgram> : ProgramFixture<TProgram> where TProgram : class
{
    private static readonly Uri HttpsBaseUri = new("https://localhost");

    public Guid RandomId => Guid.NewGuid();

    public string RandomString => RandomId.ToString();

    public int RandomInt => new Random().Next(1, 100);

    public uint RandomUint => (uint)new Random().Next(1, 10);

    public string RandomUrl => "https://www.google.com/";

    public RestApiFixture()
    {
        MediatorMock = new Mock<IMediator>();
        EventBusMock = new Mock<IEventBus>();
        RabbitMQPersistentConnectionMock = new Mock<IRabbitMQPersistentConnection>();
    }

    public Mock<IMediator> MediatorMock;

    public Mock<IEventBus> EventBusMock;

    public Mock<IRabbitMQPersistentConnection> RabbitMQPersistentConnectionMock;

    public IReadOnlyCollection<object> SentRequests =>
        MediatorMock.Invocations
            .Where(i => i.Method.Name == nameof(IMediator.Send))
            .Select(i => i.Arguments[index: 0])
            .ToArray();

    public IReadOnlyCollection<object> PublishedEvents =>
        EventBusMock.Invocations
            .Where(i => i.Method.Name == nameof(IEventBus.Publish))
            .Select(i => i.Arguments[index: 0])
            .ToArray();

    public HttpClient CreateApiClient(string basePath)
    {
        var client = CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(HttpsBaseUri, $"api/{basePath}/"),
                AllowAutoRedirect = false
            });

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(scheme: TestAuthHandler.AuthenticationScheme);

        return client;
    }

    public async Task HandleApplicationEvent<TApplicationEvent, TApplicationEventHandler>(TApplicationEvent applicationEvent)
        where TApplicationEventHandler : IApplicationEventHandler<TApplicationEvent>
        where TApplicationEvent : ApplicationEvent
    {
        var handler = ActivatorUtilities.CreateInstance<TApplicationEventHandler>(Services);
        await handler.Handle(applicationEvent);
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Singleton(MediatorMock.Object));
        services.Replace(ServiceDescriptor.Singleton(RabbitMQPersistentConnectionMock.Object));
        services.Replace(ServiceDescriptor.Singleton(EventBusMock.Object));

        services.AddAuthentication(defaultScheme: TestAuthHandler.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                TestAuthHandler.AuthenticationScheme, _ => { });
    }

    protected override Task CleanUp()
    {
        MediatorMock.Reset();
        EventBusMock.Reset();
        return base.CleanUp();
    }
}

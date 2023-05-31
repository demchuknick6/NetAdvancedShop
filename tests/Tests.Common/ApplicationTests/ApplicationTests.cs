namespace NetAdvancedShop.Tests.Common.ApplicationTests;

public abstract class ApplicationTests<TFixture> where TFixture : IApplicationFixture, new()
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Application.Initialize();
    }

    [SetUp]
    public void SetUp()
    {
        Application.SetUp();
    }

    [TearDown]
    public async Task TearDown()
    {
        await Application.TearDown();
    }

    protected TFixture Application { get; } = new();

    protected async Task<TResponse> HandleCommand<TMessage, TMessageHandler, TResponse>(TMessage message)
        where TMessageHandler : IRequestHandler<TMessage, TResponse>
        where TMessage : IRequest<TResponse>
    {
        var response = await Application.CreateHandler<TMessageHandler>().Handle(message, CancellationToken.None);

        Application.ClearTrackedEntries();

        return response;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, bool clearEntries = true)
    {
        var response = await Application.GetSender().Send(request);

        if (clearEntries)
            Application.ClearTrackedEntries();

        return response;
    }

    public async Task SendAsync(IBaseRequest request, bool clearEntries = true)
    {
        await Application.GetSender().Send(request);

        if (clearEntries)
            Application.ClearTrackedEntries();
    }
}

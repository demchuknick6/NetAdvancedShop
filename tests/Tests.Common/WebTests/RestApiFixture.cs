namespace Tests.Common.WebTests;

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
    }

    public Mock<IMediator> MediatorMock;

    public IReadOnlyCollection<object> SentRequests =>
        MediatorMock.Invocations
            .Where(i => i.Method.Name == nameof(IMediator.Send))
            .Select(i => i.Arguments[index: 0])
            .ToArray();

    public HttpClient CreateApiClient(string basePath)
    {
        var client = CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(HttpsBaseUri, $"api/{basePath}/")
            });

        return client;
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Singleton(MediatorMock.Object));
    }

    protected override Task CleanUp()
    {
        MediatorMock.Reset();
        return base.CleanUp();
    }
}

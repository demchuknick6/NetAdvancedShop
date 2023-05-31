namespace NetAdvancedShop.Tests.Common.WebTests;

[TestFixture]
public abstract class WebTests<TProgramFixture> where TProgramFixture : IProgramFixture
{
    [OneTimeSetUp]
    public void Setup()
    {
        Fixture.Initialize();
    }

    [OneTimeTearDown]
    public virtual void OneTimeTearDown()
    {
        if (Fixture is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    [SetUp]
    public virtual void SetUp()
    {
        Fixture.SetUp();
    }

    [TearDown]
    public virtual async Task TearDown()
    {
        await Fixture.TearDown();
    }

    protected TProgramFixture Fixture { get; }

    protected WebTests()
    {
        Fixture = Activator.CreateInstance<TProgramFixture>();
    }

    protected WebTests(TProgramFixture fixture)
    {
        Fixture = fixture;
    }
}
namespace Tests.Common.WebTests;

public interface IProgramFixture
{
    void SetUp();
    Task TearDown();
    void Initialize();
}

namespace Tests.Common.ApplicationTests;

public interface IApplicationFixture
{
    void SetUp();
    Task TearDown();
    THandler CreateHandler<THandler>();
    ISender GetSender();
    void ClearTrackedEntries();
    void Initialize();
}

namespace NetAdvancedShop.Tests.Common.Extensions;

public static class HttpClientExtensions
{
    public static void AddUserIdentifier(this HttpClient httpClient, string userId) =>
        httpClient.DefaultRequestHeaders.Add(TestAuthHandler.UserIdHeader, userId);

    public static void RemoveUserIdentifier(this HttpClient httpClient) =>
        httpClient.DefaultRequestHeaders.Remove(TestAuthHandler.UserIdHeader);
}

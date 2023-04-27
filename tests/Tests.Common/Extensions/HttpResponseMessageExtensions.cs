namespace Tests.Common.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<T?> ReadContent<T>(this HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseContent);
    }
}

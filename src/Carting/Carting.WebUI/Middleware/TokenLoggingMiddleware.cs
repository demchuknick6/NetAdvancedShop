namespace NetAdvancedShop.Carting.WebUI.Middleware;

public class TokenLoggingMiddleware : IAuthorizationMiddlewareResultHandler
{
    // reference - https://learn.microsoft.com/en-us/aspnet/core/security/authorization/customizingauthorizationmiddlewareresponse
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Succeeded && context.Request.Headers.TryGetValue("Authorization", out var token))
        {
            Console.WriteLine($"Access token received: {token}");
        }

        // Fall back to the default implementation.
        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}

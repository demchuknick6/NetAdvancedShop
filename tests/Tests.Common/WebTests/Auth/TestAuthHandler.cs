namespace NetAdvancedShop.Tests.Common.WebTests.Auth;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string AuthenticationScheme = "TestScheme";
    public const string UserIdHeader = "UserId";

    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authenticate = false;
        var claims = new List<Claim> { new(ClaimTypes.Name, "Test user") };

        // Extract User ID from the request headers if it exists and add corresponding role
        if (Context.Request.Headers.TryGetValue(UserIdHeader, out var userIds))
        {
            var userId = userIds[0]!;
            switch (userId)
            {
                case TestUserIdentifiers.Unauthorized:
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, TestUserIdentifiers.Unauthorized));
                    break;
                case TestUserIdentifiers.Buyer:
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, TestUserIdentifiers.Buyer));
                    claims.Add(new Claim(ClaimTypes.Role, Roles.Buyer));
                    authenticate = true;
                    break;
                case TestUserIdentifiers.Manager:
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, TestUserIdentifiers.Manager));
                    claims.Add(new Claim(ClaimTypes.Role, Roles.Manager));
                    authenticate = true;
                    break;
            }
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, TestUserIdentifiers.Unauthorized));
        }

        var identity = new ClaimsIdentity(claims, AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

        return Task.FromResult(authenticate
            ? AuthenticateResult.Success(ticket)
            : AuthenticateResult.Fail("Fail to authenticate"));
    }
}

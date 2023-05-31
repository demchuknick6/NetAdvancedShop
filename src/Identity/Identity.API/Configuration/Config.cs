namespace NetAdvancedShop.Identity.API.Configuration;

public class Config
{
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new("catalog", "Catalog Service"),
            new("carting", "Carting Service")
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new("catalog", "Catalog Service"),
            new("carting", "Carting Service")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "Your role(s)", new List<string> { "role" })
        };

    public static IEnumerable<Client> GetClients(IConfiguration configuration)
    {
        var catalogUrl = configuration["CatalogUrl"];
        var cartingUrl = configuration["CartingUrl"];

        return new List<Client>
        {
            new()
            {
                ClientId = "catalog",
                ClientName = "Catalog Service",
                ClientSecrets = new List<Secret>
                {
                    new("secret".Sha256())
                },
                ClientUri = catalogUrl,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                AlwaysIncludeUserClaimsInIdToken = true,
                RequirePkce = false,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "catalog"
                },
                AccessTokenLifetime = 60 * 60 * 2, // 2 hours
                IdentityTokenLifetime = 60 * 60 * 2 // 2 hours
            },
            new()
            {
                ClientId = "carting",
                ClientName = "Carting Service",
                ClientSecrets = new List<Secret>
                {
                    new("secret".Sha256())
                },
                ClientUri = cartingUrl,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                AlwaysIncludeUserClaimsInIdToken = true,
                RequirePkce = false,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "carting"
                },
                AccessTokenLifetime = 60 * 60 * 2, // 2 hours
                IdentityTokenLifetime = 60 * 60 * 2 // 2 hours
            },
            new()
            {
                ClientId = "catalogswaggerui",
                ClientName = "Catalog Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { $"{catalogUrl}/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { $"{catalogUrl}/swagger/" },

                AllowedScopes =
                {
                    "catalog"
                }
            },
            new()
            {
                ClientId = "cartingswaggerui",
                ClientName = "Carting Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { $"{cartingUrl}/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { $"{cartingUrl}/swagger/" },

                AllowedScopes =
                {
                    "carting"
                }
            }
        };
    }
}

namespace NetAdvancedShop.Identity.API.Services;

public class ProfileService : ProfileService<ApplicationUser>
{
    public ProfileService(UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        : base(userManager, claimsFactory)
    {
    }

    public ProfileService(UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        ILogger<ProfileService<ApplicationUser>> logger) :
        base(userManager, claimsFactory, logger)
    {
    }

    public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        await base.GetProfileDataAsync(context);

        var user = await FindUserAsync(context.Subject.GetSubjectId());

        var roles = await UserManager.GetRolesAsync(user);

        if (roles.Any())
        {
            foreach (var role in roles)
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, role));
        }
    }
}

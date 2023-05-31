namespace NetAdvancedShop.Identity.API.Domain.Entries;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser(string userName) : base(userName)
    {
    }
}

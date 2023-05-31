namespace NetAdvancedShop.Identity.API.Infrastructure.DataSeeding;

public class ApplicationDbContextSeed
{
	static class Roles
	{
		internal const string Manager = "Manager";
		internal const string Buyer = "Buyer";
	}

	static class UserNames
	{
        internal const string Manager = "manager";
		internal const string Buyer = "Buyer";
    }

	const string DefaultPassword = "Pass@word1";

	public static async Task SeedAsync(ApplicationDbContext dbContext,
		UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
	{
		if (dbContext.Database.IsSqlServer())
		{
			await dbContext.Database.MigrateAsync();
		}

		await CreateRoleIfNotExists(roleManager, Roles.Manager);
		await CreateRoleIfNotExists(roleManager, Roles.Buyer);

		await CreateUserIfNotExists(userManager, UserNames.Manager, DefaultPassword, Roles.Manager);
		await CreateUserIfNotExists(userManager, UserNames.Buyer, DefaultPassword, Roles.Buyer);
	}

	private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string role)
	{
		if (!await roleManager.RoleExistsAsync(role))
			await roleManager.CreateAsync(new IdentityRole(role));
	}

	private static async Task CreateUserIfNotExists(UserManager<ApplicationUser> userManager,
		string userName, string password, string? role = null)
	{
		if (await userManager.FindByNameAsync(userName) == null)
		{
			var user = new ApplicationUser(userName);
			await userManager.CreateAsync(user, password);
			if (!string.IsNullOrEmpty(role))
			{
				user = await userManager.FindByNameAsync(userName);
				await userManager.AddToRoleAsync(user!, role);
			}
		}
	}
}

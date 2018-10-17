using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.eShopWeb.Infrastructure.Identity
{
	public class AppIdentityDbContextSeed
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
		{
			var defaultUser = new ApplicationUser { UserName = "gcloutier@nwaretech.com", Email = "gcloutier@nwaretech.com" };
			await userManager.CreateAsync(defaultUser, "Gui123#");
		}
	}
}
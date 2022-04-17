using System.Linq;
using System.Threading.Tasks;
using core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "MTK",
                    Email = "MTK@jet.com",
                    UserName = "MTK@jet.com",
                    Address = new Address
                    {
                        FirstName = "Mtk",
                        LastName = "Khedr",
                        Street = "Chornich",
                        City = "RAK",
                        State = "RAK",
                        ZipCode = "4",

                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace RiluCrocidilu.Areas.Identity.Services
{
    public static class RoleInitializer
    {
        public static async Task Initialize(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Tutor"))
            {
                IdentityRole role = new IdentityRole("Tutor");
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Student"))
            {
                IdentityRole role = new IdentityRole("Student");
                await roleManager.CreateAsync(role);
            }
        }
    }
}

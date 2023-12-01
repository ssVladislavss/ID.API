using ID.Core.Users.Default;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ID.Core.Users
{
    public partial class UserService
    {
        public static async Task StartInitializerAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserID>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var user = DefaultUserID.RootAdmin;
            var role = DefaultUserID.RootAdminRole;

            var nowUser = await userManager.FindByIdAsync(user.Id);
            if (nowUser == null)
                await userManager.CreateAsync(user, DefaultUserID.DefaultPassword);
            else
                user = nowUser;

            var nowRole = await roleManager.FindByIdAsync(role.Id);
            if (nowRole == null)
                await roleManager.CreateAsync(role);
            else
                role = nowRole;

            if (!await userManager.IsInRoleAsync(user, role.Name))
                await userManager.AddToRoleAsync(user, role.Name);
        }
    }
}

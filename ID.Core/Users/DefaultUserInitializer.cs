using ID.Core.Roles.Default;
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
            var role = DefaultRole.RootAdminRole;
            var rootAdminRoleClaims = DefaultRole.RootAdminClaims;

            var nowUser = await userManager.FindByIdAsync(user.Id);
            if (nowUser == null)
                await userManager.CreateAsync(user, DefaultUserID.DefaultPassword);
            else
                user = nowUser;

            var nowRole = await roleManager.FindByIdAsync(role.Id);
            if (nowRole == null)
            {
                await roleManager.CreateAsync(role);

                foreach (var rootRoleClaim in rootAdminRoleClaims)
                    await roleManager.AddClaimAsync(role, rootRoleClaim);
            }
            else
            {
                role = nowRole;

                var existsRootRoleClaims = await roleManager.GetClaimsAsync(role);
                foreach(var rootClaim in existsRootRoleClaims)
                    await roleManager.RemoveClaimAsync(role, rootClaim);

                foreach (var rootRoleClaim in rootAdminRoleClaims)
                    await roleManager.AddClaimAsync(role, rootRoleClaim);
            }

            if (!await userManager.IsInRoleAsync(user, role.Name))
                await userManager.AddToRoleAsync(user, role.Name);
        }
    }
}

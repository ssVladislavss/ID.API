using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ID.Core.Roles.Default
{
    public class DefaultRole
    {
        public static IdentityRole[] Roles
            => new[]
            {
                RootAdminRole
            };

        public static IdentityRole RootAdminRole
            => new()
            {
                Id = "a0c64f39-d0f7-461b-9c05-c52bcfb3cb3a",
                Name = IDConstants.Roles.RootAdmin,
                NormalizedName = IDConstants.Roles.RootAdmin.ToUpper(),
            };
        public static Claim[] RootAdminClaims
            => new[]
            {
                new Claim(IDConstants.Roles.Claims.Types.RoleType, IDConstants.Roles.Claims.Values.Base)
            };
    }
}

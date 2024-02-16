using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ID.Core.Roles.Default
{
    public class DefaultRole
    {
        public static IdentityRole[] Roles
            => new[]
            {
                RootAdminRole,
                AdminRole
            };

        public static IdentityRole RootAdminRole
            => new()
            {
                Id = "a0c64f39-d0f7-461b-9c05-c52bcfb3cb3a",
                Name = IDConstants.Roles.RootAdmin,
                NormalizedName = IDConstants.Roles.RootAdmin.ToUpper(),
            };
        public static IdentityRole AdminRole
            => new()
            {
                Id = "7619cbdc-892a-4cb3-8970-ab7dbf86d34e",
                Name = IDConstants.Roles.Admin,
                NormalizedName = IDConstants.Roles.Admin.ToUpper(),
            };
        public static Claim[] RootAdminClaims
            => new[]
            {
                new Claim(IDConstants.Roles.Claims.Types.RoleType, IDConstants.Roles.Claims.Values.Base)
            };
        public static Claim[] AdminClaims
            => new[]
            {
                new Claim(IDConstants.Roles.Claims.Types.RoleType, IDConstants.Roles.Claims.Values.Base)
            };
    }
}

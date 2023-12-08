using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ID.Core.Roles
{
    public class EditingRoleData
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<Claim> Claims { get; set; }

        public EditingRoleData(IdentityRole role, IEnumerable<Claim> claims)
        {
            Role = role;
            Claims = claims ?? Enumerable.Empty<Claim>();
        }
    }
}

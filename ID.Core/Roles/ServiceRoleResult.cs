using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ID.Core.Roles
{
    public class ServiceRoleResult
    {
        public IdentityRole Role { get; }
        public IEnumerable<Claim> Claims { get; }

        public ServiceRoleResult(IdentityRole role, IEnumerable<Claim>? claims = null)
        {
            Role = role;
            Claims = claims ?? Enumerable.Empty<Claim>();
        }
    }
}

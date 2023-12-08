using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ID.Core.Users
{
    public class UserInfo
    {
        public UserID User { get; }
        public IEnumerable<IdentityRole> Roles { get; }
        public IEnumerable<Claim> Claims { get; }

        internal UserInfo(UserID user, IEnumerable<IdentityRole> roles, IEnumerable<Claim> claims)
        {
            this.User = user;
            this.Roles = roles;
            this.Claims = claims;
        }
    }
}

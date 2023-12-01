using Microsoft.AspNetCore.Identity;

namespace ID.Core.Users
{
    public class UserInfo
    {
        public UserID User { get; }
        public IEnumerable<IdentityRole> Roles { get; }

        internal UserInfo(UserID user, IEnumerable<IdentityRole> roles)
        {
            this.User = user;
            this.Roles = roles;
        }
    }
}

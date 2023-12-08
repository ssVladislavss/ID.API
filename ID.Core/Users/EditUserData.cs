using System.Security.Claims;

namespace ID.Core.Users
{
    public class EditUserData
    {
        public UserID User { get; }
        public IEnumerable<string> RoleNames { get; }
        public IEnumerable<Claim> Claims { get; }

        public EditUserData(UserID user, IEnumerable<Claim> claims, IEnumerable<string> roleNames)
        {
            User = user;
            Claims = claims ?? Enumerable.Empty<Claim>();
            RoleNames = roleNames ?? Enumerable.Empty<string>();
        }
    }
}

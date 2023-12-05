using Microsoft.AspNetCore.Identity;

namespace ID.Core.Users
{
    public class CreateUserResult
    {
        public UserID CreatedUser { get; }
        public IEnumerable<IdentityRole> UserRoles { get; }
        public string Password { get; }

        public CreateUserResult(UserID createdUser, IEnumerable<IdentityRole> userRoles, string password)
        {
            this.CreatedUser = createdUser;
            this.UserRoles = userRoles ?? Enumerable.Empty<IdentityRole>();
            this.Password = password;
        }
    }
}

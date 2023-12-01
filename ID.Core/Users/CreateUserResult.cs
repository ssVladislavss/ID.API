using Microsoft.AspNetCore.Identity;

namespace ID.Core.Users
{
    public class CreateUserResult
    {
        public UserID CreatedUser { get; }
        public IdentityRole UserRole { get; }
        public string Password { get; }

        public CreateUserResult(UserID createdUser, IdentityRole userRole, string password)
        {
            this.CreatedUser = createdUser;
            this.UserRole = userRole;
            this.Password = password;
        }
    }
}

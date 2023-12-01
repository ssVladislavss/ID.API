namespace ID.Core.Users
{
    public class CreateUserData
    {
        public UserID User { get; internal set; }
        public string RoleName { get; internal set; }
        public string? Password { get; internal set; }

        public CreateUserData(UserID user, string roleName, string? password = null)
        {
            this.User = user;
            this.RoleName = roleName;
            this.Password = password;
        }
    }
}

namespace ID.Core.Users
{
    public class CreateUserData
    {
        public UserID User { get; internal set; }
        public IEnumerable<string> RoleNames { get; internal set; }
        public string? Password { get; internal set; }

        public CreateUserData(UserID user, IEnumerable<string> roleNames, string? password = null)
        {
            this.User = user;
            this.RoleNames = roleNames;
            this.Password = password;
        }
    }
}

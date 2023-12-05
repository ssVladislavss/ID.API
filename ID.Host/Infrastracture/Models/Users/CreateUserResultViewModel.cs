using ID.Core.Users;

namespace ID.Host.Infrastracture.Models.Users
{
    public class CreateUserResultViewModel
    {
        public UserViewModel User { get; set; }
        public string Password { get; set; }

        public CreateUserResultViewModel(CreateUserResult createdResult)
        {
            User = new UserViewModel(createdResult);
            Password = createdResult.Password;
        }
    }
}

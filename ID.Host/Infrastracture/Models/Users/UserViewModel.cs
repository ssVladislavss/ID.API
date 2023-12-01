using ID.Core.Users;

namespace ID.Host.Infrastracture.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Phone { get; set; }
        public bool PhoneConfirmed { get; set; }
        public string UserName { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? BirthDate { get; set; }
        public IEnumerable<UserRoleViewModel> Roles { get; set; }

        public UserViewModel(UserID user)
        {
            Id = user.Id;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            Phone = user.PhoneNumber;
            PhoneConfirmed = user.PhoneNumberConfirmed;
            UserName = user.UserName;
            LastName = user.LastName;
            FirstName = user.FirstName;
            SecondName = user.SecondName;
            BirthDate = user.BirthDate;
            Roles = Enumerable.Empty<UserRoleViewModel>();
        }

        public UserViewModel(UserInfo userInfo)
        {
            Id = userInfo.User.Id;
            Email = userInfo.User.Email;
            EmailConfirmed = userInfo.User.EmailConfirmed;
            Phone = userInfo.User.PhoneNumber;
            PhoneConfirmed = userInfo.User.PhoneNumberConfirmed;
            UserName = userInfo.User.UserName;
            LastName = userInfo.User.LastName;
            FirstName = userInfo.User.FirstName;
            SecondName = userInfo.User.SecondName;
            BirthDate = userInfo.User.BirthDate;
            Roles = userInfo.Roles.Select(x => new UserRoleViewModel(x));
        }
    }
}

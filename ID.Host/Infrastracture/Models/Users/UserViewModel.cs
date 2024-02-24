using ID.Core.Users;
using ID.Host.Infrastracture.Models.Claims;
using ID.Host.Infrastracture.Models.Roles;
using OnlineSales.Access.Data;

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
        public bool IsLocked { get; set; }
        public DateTimeOffset? LockedEndDate { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
        public IEnumerable<Functional> AvailableFunctionality { get; set; }

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
            IsLocked = user.LockoutEnabled;
            LockedEndDate = user.LockoutEnd;
            Roles = Enumerable.Empty<RoleViewModel>();
            Claims = Enumerable.Empty<ClaimViewModel>();
            AvailableFunctionality = Enumerable.Empty<Functional>();
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
            IsLocked = userInfo.User.LockoutEnabled;
            LockedEndDate = userInfo.User.LockoutEnd;
            Roles = userInfo.Roles.Select(x => new RoleViewModel(x));
            Claims = userInfo.Claims.Select(x => new ClaimViewModel() { Type = x.Type, Value = x.Value });
            AvailableFunctionality = userInfo.User.AvailableFunctionality?.Select(x => (Functional)x) ?? Enumerable.Empty<Functional>();
        }

        public UserViewModel(CreateUserResult createResult)
        {
            Id = createResult.CreatedUser.Id;
            Email = createResult.CreatedUser.Email;
            EmailConfirmed = createResult.CreatedUser.EmailConfirmed;
            Phone = createResult.CreatedUser.PhoneNumber;
            PhoneConfirmed = createResult.CreatedUser.PhoneNumberConfirmed;
            UserName = createResult.CreatedUser.UserName;
            LastName = createResult.CreatedUser.LastName;
            FirstName = createResult.CreatedUser.FirstName;
            SecondName = createResult.CreatedUser.SecondName;
            BirthDate = createResult.CreatedUser.BirthDate;
            IsLocked = createResult.CreatedUser.LockoutEnabled;
            LockedEndDate = createResult.CreatedUser.LockoutEnd;
            Roles = createResult.UserRoles.Select(x => new RoleViewModel(x));
            Claims = Enumerable.Empty<ClaimViewModel>();
            AvailableFunctionality = createResult.CreatedUser.AvailableFunctionality?.Select(x => (Functional)x) ?? Enumerable.Empty<Functional>();
        }
    }
}

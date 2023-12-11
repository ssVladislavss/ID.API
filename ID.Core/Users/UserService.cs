using ID.Core.Users.Abstractions;
using ID.Core.Users.Default;
using ID.Core.Users.Exceptions;
using ISDS.ServiceExtender.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;

namespace ID.Core.Users
{
    public partial class UserService : IUserService
    {
        protected readonly IDUserManager _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly IdentityOptions _identityOptions;

        public UserService
            (IDUserManager userManager,
             RoleManager<IdentityRole> roleManager,
             IOptions<IdentityOptions>? identityDescriptor = null)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _identityOptions = identityDescriptor?.Value ?? new IdentityOptions();
        }

        public virtual async Task<CreateUserResult> AddAsync(CreateUserData data, ISrvUser iniciator, CancellationToken token = default)
        {
            var nowUser = await _userManager.FindByEmailAsync(data.User.Email);
            if (nowUser != null)
                throw new UserAddExistException($"AddAsync: user (Email - {data.User.Email}) was found");

            List<IdentityRole> roles = new List<IdentityRole>();

            foreach (var roleName in data.RoleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName)
                      ?? throw new UserRoleNotFoundException($"AddAsync: role (RoleName - {roleName}) was not found");

                roles.Add(role);
            }

            if (string.IsNullOrEmpty(data.Password))
                data.Password = UserID.GeneratePassword(_identityOptions.Password.RequiredLength);

            var addingResult = await _userManager.CreateAsync(data.User, data.Password);
            if (!addingResult.Succeeded)
                throw new UserAddException
                    ($"AddAsync: user (Email - {data.User.Email})," +
                    $" {string.Concat(addingResult.Errors.Select(x => $"{x.Code} - {x.Description}, ")).TrimEnd(' ', ',')}");

            var addingToRoleResult = await _userManager.AddToRolesAsync(data.User, roles.Select(x => x.Name));
            if (!addingResult.Succeeded)
            {
                await _userManager.DeleteAsync(data.User);

                throw new UserAddException($"AddAsync: error adding user to role" +
                    $" (Email - {data.User.Email}, Roles - {string.Join(',', data.RoleNames)})," +
                    $" {string.Concat(addingToRoleResult.Errors.Select(x => $"{x.Code} - {x.Description}, ")).TrimEnd(' ', ',')}");
            }

            return new CreateUserResult(data.User, roles, data.Password);
        }
        public virtual async Task ChangeEmailAsync(string userId, string newEmail, ISrvUser iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserChangeEmailException($"ChangeEmailAsync: user (UserId - {userId}, NewEmail - {newEmail}) was not found");

            var setNewEmailResult = await _userManager.SetEmailAsync(currentUser, newEmail);
            if (!setNewEmailResult.Succeeded)
                throw new UserChangeEmailException($"ChangeEmailAsync: user (UserId - {currentUser.Id}, NewEmail - {newEmail}) the email address could not be changed. " +
                    $"{string.Join(';', setNewEmailResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
        public virtual async Task ConfirmEmailAsync(string userId, string newEmail, string base64ConfirmToken, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserNotFoundException($"ConfirmEmailAsync: user (UserId - {userId}) was not found");

            var verificationResult = await _userManager.ChangeEmailAsync(currentUser, newEmail, base64ConfirmToken);
            if (!verificationResult.Succeeded)
                throw new UserEmailConfirmException($"ConfirmEmailAsync: user (UserId - {currentUser.Id}, NewEmail - {newEmail}) the email address could not be verified. " +
                    $"{string.Join(';', verificationResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
        public virtual async Task DeleteAsync(string userId, ISrvUser iniciator, CancellationToken token = default)
        {
            if (DefaultUserID.Users.Any(x => x.Id == userId))
                throw new UserDefaultException($"DeleteAsync: user (UserId - {userId}) it is forbidden to delete the user");

            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new UserDeleteException($"DeleteAsync: user (UserId - {userId}) was not found");

            if (user.Id == DefaultUserID.RootAdmin.Id)
                throw new UserRemoveAccessException($"DeleteAsync: user (UserId - {user.Id}) cannot be deleted");

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
                throw new UserDeleteException($"DeleteAsync: user (UserId - {userId}) error deleting. " +
                    $"{string.Join(',', deleteResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
        public virtual async Task<UserInfo> FindByEmailAsync(string email, ISrvUser iniciator, CancellationToken token = default)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException($"FindByEmailAsync: user (Email - {email}) was not found");

            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = new List<IdentityRole>();

            if(userRoleNames.Any())
            {
                foreach (var roleName in userRoleNames)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if(role != null)
                        userRoles.Add(role);
                }
            }

            var currentUserClaims = await _userManager.GetClaimsAsync(user);

            return new UserInfo(user, userRoles, currentUserClaims);
        }
        public virtual async Task<UserInfo> FindByIdAsync(string userId, ISrvUser iniciator, CancellationToken token = default)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new UserNotFoundException($"FindByEmailAsync: user (UserId - {userId}) was not found");

            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = new List<IdentityRole>();

            if (userRoleNames.Any())
            {
                foreach (var roleName in userRoleNames)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                        userRoles.Add(role);
                }
            }

            var currentUserClaims = await _userManager.GetClaimsAsync(user);

            return new UserInfo(user, userRoles, currentUserClaims);
        }
        public virtual async Task<UserInfo> FindByNameAsync(string userName, ISrvUser iniciator, CancellationToken token = default)
        {
            var user = await _userManager.FindByNameAsync(userName)
                ?? throw new UserNotFoundException($"FindByEmailAsync: user (UserName - {userName}) was not found");

            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = new List<IdentityRole>();

            if (userRoleNames.Any())
            {
                foreach (var roleName in userRoleNames)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                        userRoles.Add(role);
                }
            }

            var currentUserClaims = await _userManager.GetClaimsAsync(user);

            return new UserInfo(user, userRoles, currentUserClaims);
        }
        public virtual async Task<IEnumerable<UserInfo>> GetAsync(ISrvUser iniciator, CancellationToken token = default)
        {
            var usersInfo = new List<UserInfo>();

            var users = await _userManager.Users.ToListAsync(token);

            if (!users.Any())
                throw new UserNoContentException($"GetAsync: the users table does not contain any records");

            foreach (var user in users)
            {
                var userRoleNames = await _userManager.GetRolesAsync(user);
                var userRoles = new List<IdentityRole>();

                if (userRoleNames.Any())
                {
                    foreach (var roleName in userRoleNames)
                    {
                        var role = await _roleManager.FindByNameAsync(roleName);
                        if (role != null)
                            userRoles.Add(role);
                        else
                            throw new UserRoleNotFoundException($"GetAsync: user role (RoleName - {roleName}) was not found");
                    }
                }

                var currentUserClaims = await _userManager.GetClaimsAsync(user);

                usersInfo.Add(new UserInfo(user, userRoles, currentUserClaims));
            }

            return usersInfo;
        }
        public virtual async Task<IEnumerable<UserInfo>> GetAsync(UserSearchFilter filter, ISrvUser iniciator, CancellationToken token = default)
        {
            var usersInfo = new List<UserInfo>();

            var query = _userManager.Users;

            filter.Apply(ref query);

            var users = await query.ToListAsync(token);
            
            if (!string.IsNullOrEmpty(filter.Role))
            {
                var userInRole = new List<UserID>();

                foreach (var user in users)
                    if (await _userManager.IsInRoleAsync(user, filter.Role))
                        userInRole.Add(user);

                users = userInRole;
            }

            if (!users.Any())
                throw new UserNoContentException($"GetAsync: the users table does not contain any records by filter (Filter - {filter})");

            foreach (var user in users)
            {
                var userRoleNames = await _userManager.GetRolesAsync(user);
                var userRoles = new List<IdentityRole>();

                if (userRoleNames.Any())
                {
                    foreach (var roleName in userRoleNames)
                    {
                        var role = await _roleManager.FindByNameAsync(roleName);
                        if (role != null)
                            userRoles.Add(role);
                        else
                            throw new UserRoleNotFoundException($"GetAsync: user role (RoleName - {roleName}) was not found");
                    }
                }

                var currentUserClaims = await _userManager.GetClaimsAsync(user);

                usersInfo.Add(new UserInfo(user, userRoles, currentUserClaims));
            }

            return usersInfo;
        }
        public virtual async Task UpdateAsync(EditUserData data, ISrvUser iniciator, CancellationToken token = default)
        {
            if (DefaultUserID.Users.Any(x => x.Id == data.User.Id))
                throw new UserDefaultException($"AddAsync: user (UserId - {data.User.Id}) it is forbidden to change the user");

            var currentUser = await _userManager.FindByIdAsync(data.User.Id)
                ?? await _userManager.FindByEmailAsync(data.User.Email)
                ?? throw new UserEditException($"UpdateAsync: user (Id - {data.User.Id}, Email - {data.User.Email}) was not found");

            currentUser.LastName = data.User.LastName;
            currentUser.FirstName = data.User.FirstName;
            currentUser.SecondName = data.User.SecondName;
            currentUser.BirthDate = data.User.BirthDate;
            currentUser.AvailableFunctionality = data.User.AvailableFunctionality;

            var updateResult = await _userManager.UpdateAsync(currentUser);
            if (!updateResult.Succeeded)
                throw new UserEditException($"UpdateAsync: user (Id - {data.User.Id}, Email - {data.User.Email}) error updating. " +
                    $"{string.Join(',', updateResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            var currentUserClaims = await _userManager.GetClaimsAsync(currentUser);

            if(currentUserClaims.Count > 0)
                await _userManager.RemoveClaimsAsync(currentUser, currentUserClaims);

            await _userManager.AddClaimsAsync(currentUser, data.Claims);

            foreach (var roleName in data.RoleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName)
                    ?? throw new UserRoleNotFoundException($"UpdateAsync: role (RoleName - {roleName}) was not found");
            }

            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
            await _userManager.RemoveFromRolesAsync(currentUser, currentUserRoles);
            await _userManager.AddToRolesAsync(currentUser, data.RoleNames);
        }
    }
}

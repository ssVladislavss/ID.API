using ID.Core.Users.Abstractions;
using ID.Core.Users.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static IdentityServer4.Models.IdentityResources;

namespace ID.Core.Users
{
    public partial class UserService : IUserService
    {
        protected readonly UserManager<UserID> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly IdentityOptions _identityOptions;

        public UserService
            (UserManager<UserID> userManager,
             RoleManager<IdentityRole> roleManager,
             IOptions<IdentityOptions>? identityDescriptor = null)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _identityOptions = identityDescriptor?.Value ?? new IdentityOptions();
        }

        public async Task<CreateUserResult> AddAsync(CreateUserData data, Iniciator iniciator, CancellationToken token = default)
        {
            var nowUser = await _userManager.FindByEmailAsync(data.User.Email);
            if (nowUser != null)
                throw new UserAddException($"AddAsync: user (Email - {data.User.Email}) was found");

            List<IdentityRole> roles = new List<IdentityRole>();

            foreach (var roleName in data.RoleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName)
                      ?? throw new UserAddException($"AddAsync: role (RoleName - {roleName}) was not found");

                roles.Add(role);
            }

            if (string.IsNullOrEmpty(data.Password))
                data.Password = UserID.CreatePassword(_identityOptions.Password.RequiredLength);

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

        public async Task DeleteAsync(string userId, Iniciator iniciator, CancellationToken token = default)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new UserDeleteException($"DeleteAsync: user (UserId - {userId}) was not found");

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
                throw new UserDeleteException($"DeleteAsync: user (UserId - {userId}) error deleting. " +
                    $"{string.Join(',', deleteResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }

        public async Task<UserInfo> FindByEmailAsync(string email, Iniciator iniciator, CancellationToken token = default)
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

            return new UserInfo(user, userRoles);
        }

        public async Task<UserInfo> FindByIdAsync(string userId, Iniciator iniciator, CancellationToken token = default)
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

            return new UserInfo(user, userRoles);
        }

        public async Task<UserInfo> FindByNameAsync(string userName, Iniciator iniciator, CancellationToken token = default)
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

            return new UserInfo(user, userRoles);
        }

        public async Task<IEnumerable<UserInfo>> GetAsync(Iniciator iniciator, CancellationToken token = default)
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

                usersInfo.Add(new UserInfo(user, userRoles));
            }

            return usersInfo;
        }

        public async Task<IEnumerable<UserInfo>> GetAsync(UserSearchFilter filter, Iniciator iniciator, CancellationToken token = default)
        {
            var usersInfo = new List<UserInfo>();

            var query = _userManager.Users;

            if (filter.BirthDate.HasValue)
                query = query.Where(x => x.BirthDate!.Value.Date == filter.BirthDate.Value.Date);
            if (!string.IsNullOrEmpty(filter.Phone))
                query = query.Where(x => x.PhoneNumber == filter.Phone);
            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(x => x.Email == filter.Email);
            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(x => x.FirstName == filter.FirstName);
            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(x => x.LastName == filter.LastName);
            if (!string.IsNullOrEmpty(filter.SecondName))
                query = query.Where(x => x.SecondName == filter.SecondName);

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

                usersInfo.Add(new UserInfo(user, userRoles));
            }

            return usersInfo;
        }

        public async Task UpdateAsync(UserID user, Iniciator iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(user.Id)
                ?? await _userManager.FindByEmailAsync(user.Email)
                ?? throw new UserEditException($"UpdateAsync: user (Id - {user.Id}, Email - {user.Email}) was not found");

            currentUser.LastName = user.LastName;
            currentUser.FirstName = user.FirstName;
            currentUser.SecondName = user.SecondName;
            currentUser.BirthDate = user.BirthDate;
            currentUser.AvailableFunctionality = user.AvailableFunctionality;

            var updateResult = await _userManager.UpdateAsync(currentUser);
            if (!updateResult.Succeeded)
                throw new UserEditException($"UpdateAsync: user (Id - {user.Id}, Email - {user.Email}) error updating. " +
                    $"{string.Join(',', updateResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
    }
}

using ID.Core.Users.Abstractions;
using ID.Core.Users.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
             IUserStore<UserID> userRoleStore,
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

            var role = await _roleManager.FindByNameAsync(data.RoleName)
                ?? throw new UserAddException($"AddAsync: role (RoleName - {data.RoleName}) was not found");

            if (string.IsNullOrEmpty(data.Password))
                data.Password = UserID.CreatePassword(_identityOptions.Password.RequiredLength);

            var addingResult = await _userManager.CreateAsync(data.User, data.Password);
            if (!addingResult.Succeeded)
                throw new UserAddException
                    ($"AddAsync: user (Email - {data.User.Email})," +
                    $" {string.Concat(addingResult.Errors.Select(x => $"{x.Code} - {x.Description}, ")).TrimEnd(' ', ',')}");

            var addingToRoleResult = await _userManager.AddToRoleAsync(data.User, role.Name);
            if (!addingResult.Succeeded)
            {
                await _userManager.DeleteAsync(data.User);

                throw new UserAddException($"AddAsync: error adding user to role (Email - {data.User.Email}, Role - {role})," +
                    $" {string.Concat(addingToRoleResult.Errors.Select(x => $"{x.Code} - {x.Description}, ")).TrimEnd(' ', ',')}");
            }

            return new CreateUserResult(data.User, role, data.Password);
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
    }
}

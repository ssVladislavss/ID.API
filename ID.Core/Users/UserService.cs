using ID.Core.Clients.Abstractions;
using ID.Core.Users.Abstractions;
using ID.Core.Users.Default;
using ID.Core.Users.Exceptions;
using IdentityServer4.Models;
using ISDS.ServiceExtender.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ID.Core.Users
{
    public partial class UserService : IUserService
    {
        protected readonly IDUserManager _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly IdentityOptions _identityOptions;
        protected readonly IClientRepository _clientRepository;

        public UserService
            (IDUserManager userManager,
             RoleManager<IdentityRole> roleManager,
             IClientRepository clientRepository,
             IOptions<IdentityOptions>? identityDescriptor = null)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _identityOptions = identityDescriptor?.Value ?? new IdentityOptions();
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public virtual async Task<CreateUserResult> AddAsync(CreateUserData data, ISrvUser iniciator, CancellationToken token = default)
        {
            if (data.RoleNames.Any(x => x == IDConstants.Roles.RootAdmin) && !iniciator.IsInRole(IDConstants.Roles.RootAdmin))
                throw new UserAddAccessException($"AddAsync: user (Email - {data.User.Email}, Roles - {string.Join(';', data.RoleNames)}) " +
                    $"the user could not be registered because the initiator does not have rights to register system administrators " +
                    $"(Iniciator - {iniciator.Email}, ClientId - {iniciator.ClientId}, Roles - {string.Join(';', iniciator.Role ?? Enumerable.Empty<string>())})");

            var nowUser = await _userManager.FindByEmailAsync(data.User.Email);
            if (nowUser != null)
                throw new UserAddExistException($"AddAsync: user (Email - {data.User.Email}) was found");

            List<IdentityRole> roles = new();

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
        public virtual async Task SetEmailAsync(string userId, string newEmail, ISrvUser iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserChangeEmailException($"ChangeEmailAsync: user (UserId - {userId}, NewEmail - {newEmail}) was not found");

            var setNewEmailResult = await _userManager.SetEmailAsync(currentUser, newEmail);
            if (!setNewEmailResult.Succeeded)
                throw new UserChangeEmailException($"ChangeEmailAsync: user (UserId - {currentUser.Id}, NewEmail - {newEmail}) the email address could not be changed. " +
                    $"{string.Join(';', setNewEmailResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
        public virtual async Task<string> ResetPasswordAsync(string email, string? clientId = null, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByEmailAsync(email)
                ?? throw new UserChangePasswordException($"ResetPasswordAsync: user (Email - {email}) was not found");

            var newPassword = UserID.GeneratePassword(_identityOptions.Password.RequiredLength);

            var removeCurrentPasswordResult = await _userManager.RemovePasswordAsync(currentUser);
            if (!removeCurrentPasswordResult.Succeeded)
                throw new UserChangePasswordException($"ResetPasswordAsync: user (Email - {email}) the current password could not be deleted. " +
                    $"{string.Join(';', removeCurrentPasswordResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            var setNewPasswordResult = await _userManager.AddPasswordAsync(currentUser, newPassword);
            if (!setNewPasswordResult.Succeeded)
                throw new UserChangePasswordException($"ResetPasswordAsync: user (Email -  {email}) the new password could not be added. " +
                    $"{string.Join(';', setNewPasswordResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            return newPassword;
        }
        public virtual async Task SetPhoneNumberAsync(string userId, string newPhoneNumber, ISrvUser iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserChangePhoneNumberException($"SetPhoneNumberAsync: user (UserId - {userId}, NewPhoneNumber - {newPhoneNumber}) was not found");

            var changePhoneNumberResult = await _userManager.SetPhoneNumberAsync(currentUser, newPhoneNumber);
            if (!changePhoneNumberResult.Succeeded)
                throw new UserChangePhoneNumberException($"SetPhoneNumberAsync: user (UserId - {userId}) couldn't save a new phone number. " +
                    $"{string.Join(';', changePhoneNumberResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
        public virtual async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword, ISrvUser iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserChangePasswordException($"ChangePasswordAsync: user (UserId - {userId}) was not found");

            var changePasswordResult = await _userManager.ChangePasswordAsync(currentUser, currentPassword, newPassword);
            if (!changePasswordResult.Succeeded)
                throw new UserChangePasswordException($"ChangePasswordAsync: user " +
                    $"(UserId - {userId}, OldPassword - {currentPassword}, NewPassword - {newPassword}) the new password could not be added. " +
                    $"{string.Join(';', changePasswordResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
        public virtual async Task ConfirmEmailAsync(string userId, string newEmail, string base64ConfirmToken, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserEmailConfirmException($"ConfirmEmailAsync: user (UserId - {userId}) was not found");

            var verificationResult = await _userManager.ChangeEmailAsync(currentUser, newEmail, base64ConfirmToken);
            if (!verificationResult.Succeeded)
                throw new UserEmailConfirmException($"ConfirmEmailAsync: user (UserId - {currentUser.Id}, NewEmail - {newEmail}) the email address could not be verified. " +
                    $"{string.Join(';', verificationResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            if (currentUser.LockoutEnabled && !currentUser.LockoutEnd.HasValue)
                await _userManager.SetLockoutEnabledAsync(currentUser, false);
        }
        public virtual async Task ConfirmPhoneNumberAsync(string userId, string newPhoneNumber, string confirmationCode, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserConfirmPhoneNumberException($"ConfirmPhoneNumberAsync: user (UserId - {userId}) was not found");

            var verificationResult = await _userManager.ChangePhoneNumberAsync(currentUser, newPhoneNumber, confirmationCode);
            if (!verificationResult.Succeeded)
                throw new UserConfirmPhoneNumberException($"ConfirmPhoneNumberAsync: user (UserId - {currentUser.Id}, NewPhoneNumber - {newPhoneNumber}) the specified phone number and token are not valid. " +
                    $"{string.Join(';', verificationResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
        public virtual async Task<ResetPasswordConfirmResult> ConfirmResetPasswordAsync(string userId, string base64ConfirmToken, string? clientId = null, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserResetConfirmPasswordException($"ConfirmPasswordAsync: user (UserId - {userId}) was not found");

            var newPassword = UserID.GeneratePassword(_identityOptions.Password.RequiredLength);

            var resetPasswordResult = await _userManager.ResetPasswordAsync(currentUser, base64ConfirmToken, newPassword);
            if (!resetPasswordResult.Succeeded)
                throw new UserResetConfirmPasswordException($"ConfirmPasswordAsync: user (UserId - {currentUser.Id}) the password could not be reset. " +
                    $"{string.Join(';', resetPasswordResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            Client? client = null;
            if (!string.IsNullOrEmpty(clientId))
                client = await _clientRepository.FindAsync(clientId, token);

            return new ResetPasswordConfirmResult(newPassword, client);
        }
        public virtual async Task DeleteAsync(string userId, ISrvUser iniciator, CancellationToken token = default)
        {
            if (DefaultUserID.Users.Any(x => x.Id == userId))
                throw new UserDefaultException($"DeleteAsync: user (UserId - {userId}) it is forbidden to delete the user");

            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new UserDeleteException($"DeleteAsync: user (UserId - {userId}) was not found");

            if (user.Id == DefaultUserID.RootAdmin.Id)
                throw new UserRemoveAccessException($"DeleteAsync: user (UserId - {user.Id}) cannot be deleted");

            if (await _userManager.IsInRoleAsync(user, IDConstants.Roles.RootAdmin) && !iniciator.IsInRole(IDConstants.Roles.RootAdmin))
                throw new UserRemoveAccessException($"DeleteAsync: user (UserId - {user.Id}) the initiator (Email - {iniciator.Email}) does not have access to delete user data");

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
                throw new UserDeleteException($"DeleteAsync: user (UserId - {userId}) error deleting. " +
                    $"{string.Join(',', deleteResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
        public virtual async Task<UserInfo> FindByEmailAsync(string email, ISrvUser iniciator, CancellationToken token = default)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException($"FindByEmailAsync: user (Email - {email}) was not found");

            if (await _userManager.IsInRoleAsync(user, IDConstants.Roles.RootAdmin) && !iniciator.IsInRole(IDConstants.Roles.RootAdmin))
                throw new UserFindAccessException($"FindByEmailAsync: user (UserId - {user.Id}) the initiator (Email - {iniciator.Email}) does not have access to receive user data");

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
        public virtual async Task<UserInfo> FindByIdAsync(string userId, ISrvUser iniciator, CancellationToken token = default)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new UserNotFoundException($"FindByEmailAsync: user (UserId - {userId}) was not found");

            if (await _userManager.IsInRoleAsync(user, IDConstants.Roles.RootAdmin) && !iniciator.IsInRole(IDConstants.Roles.RootAdmin))
                throw new UserFindAccessException($"FindByEmailAsync: user (UserId - {user.Id}) the initiator (Email - {iniciator.Email}) does not have access to receive user data");

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

            if (await _userManager.IsInRoleAsync(user, IDConstants.Roles.RootAdmin) && !iniciator.IsInRole(IDConstants.Roles.RootAdmin))
                throw new UserFindAccessException($"FindByEmailAsync: user (UserId - {user.Id}) the initiator (Email - {iniciator.Email}) does not have access to receive user data");

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

                if ((userRoles.Any(x => x.Name == IDConstants.Roles.RootAdmin) && iniciator.IsInRole(IDConstants.Roles.RootAdmin)) ||
                    !userRoles.Any(x => x.Name == IDConstants.Roles.RootAdmin))
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

                if ((userRoles.Any(x => x.Name == IDConstants.Roles.RootAdmin) && iniciator.IsInRole(IDConstants.Roles.RootAdmin)) ||
                    !userRoles.Any(x => x.Name == IDConstants.Roles.RootAdmin))
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

            if (currentUserClaims.Count > 0)
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
        public virtual async Task SetLockStatusByVerifyCodeAsync(string userId, bool enabled, string confirmationCode, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserSetLockoutEnabledException($"SetLockoutEnabledAsync: user (UserId - {userId}) was not found");

            var savedLockVerificationCode = await _userManager.GetAuthenticationTokenAsync
                (currentUser,
                 IDConstants.Users.ConfirmationCodeProviders.IDProvider,
                 IDConstants.Users.ConfirmationCodeNames.CodeBySetLockoutEnabled);

            if (savedLockVerificationCode == confirmationCode)
            {
                var setLockoutEnabledResult = await _userManager.SetLockoutEnabledAsync(currentUser, enabled);
                if (!setLockoutEnabledResult.Succeeded)
                    throw new UserSetLockoutEnabledException($"SetLockoutEnabledAsync: user (UserId - {currentUser.Id}) cannot be blocked. " +
                        $"{string.Join(';', setLockoutEnabledResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

                var setLockedEndDateResult = await _userManager.SetLockoutEndDateAsync(currentUser, DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(1)));
                if (!setLockedEndDateResult.Succeeded)
                    throw new UserSetLockoutEnabledException($"SetLockoutEnabledAsync: user (UserId - {currentUser.Id}) cannot be set locked end date. " +
                        $"{string.Join(';', setLockedEndDateResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
            }
        }
        public virtual async Task<DateTimeOffset?> SetLockoutEnabledAsync(string userId, bool enabled, ISrvUser iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserSetLockoutEnabledException($"SetLockoutEnabledAsync: user (UserId - {userId}) was not found");

            if(DefaultUserID.Users.Any(x => x.Id == currentUser.Id))
                throw new UserDefaultException($"SetLockoutEnabledAsync: user (UserId - {currentUser.Id}) it is forbidden to set lockout enabled the user");

            var setLockoutEnabledResult = await _userManager.SetLockoutEnabledAsync(currentUser, enabled);
            if (!setLockoutEnabledResult.Succeeded)
                throw new UserSetLockoutEnabledException($"SetLockoutEnabledAsync: user (UserId - {currentUser.Id}) cannot be blocked. " +
                    $"{string.Join(';', setLockoutEnabledResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            if (enabled)
            {
                var lockoutEndTime = DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(1));

                var setLockedEndDateResult = await _userManager.SetLockoutEndDateAsync(currentUser, lockoutEndTime);
                if (!setLockedEndDateResult.Succeeded)
                    throw new UserSetLockoutEnabledException($"SetLockoutEnabledAsync: user (UserId - {currentUser.Id}) cannot be set locked end date. " +
                        $"{string.Join(';', setLockedEndDateResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

                return lockoutEndTime;
            }

            return null;
        }
    }
}

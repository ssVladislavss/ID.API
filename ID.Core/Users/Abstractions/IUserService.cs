using ISDS.ServiceExtender.Http;

namespace ID.Core.Users.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfo>> GetAsync(ISrvUser iniciator, CancellationToken token = default);
        Task<IEnumerable<UserInfo>> GetAsync(UserSearchFilter filter, ISrvUser iniciator, CancellationToken token = default);
        Task<UserInfo> FindByIdAsync(string userId, ISrvUser iniciator, CancellationToken token = default);
        Task<UserInfo> FindByNameAsync(string userName, ISrvUser iniciator, CancellationToken token = default);
        Task<UserInfo> FindByEmailAsync(string email, ISrvUser iniciator, CancellationToken token = default);
        Task<CreateUserResult> AddAsync(CreateUserData data, ISrvUser iniciator, CancellationToken token = default);
        Task ConfirmEmailAsync(string userId, string newEmail, string base64ConfirmToken, CancellationToken token = default);
        Task ConfirmPhoneNumberAsync(string userId, string newPhoneNumber, string confirmationCode, CancellationToken token = default);
        Task<ResetPasswordConfirmResult> ConfirmResetPasswordAsync(string userId, string base64ConfirmToken, string? clientId = null, CancellationToken token = default);
        Task SetEmailAsync(string userId, string newEmail, ISrvUser iniciator, CancellationToken token = default);
        Task<string> ResetPasswordAsync(string email, string? clientId = null, CancellationToken token = default);
        Task ChangePasswordAsync(string userId, string currentPassword, string newPassword, ISrvUser iniciator, CancellationToken token = default);
        Task SetPhoneNumberAsync(string userId, string newPhoneNumber, ISrvUser iniciator, CancellationToken token = default);
        Task UpdateAsync(EditUserData data, ISrvUser iniciator, CancellationToken token = default);
        Task DeleteAsync(string userId, ISrvUser iniciator, CancellationToken token = default);
        Task SetLockStatusByVerifyCodeAsync(string userId, bool enabled, string confirmationCode, CancellationToken token = default);
        Task<DateTimeOffset?> SetLockoutEnabledAsync(string userId, bool enabled, ISrvUser iniciator, CancellationToken token = default);
    }
}

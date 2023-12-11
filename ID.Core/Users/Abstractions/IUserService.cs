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
        Task ChangeEmailAsync(string userId, string newEmail, ISrvUser iniciator, CancellationToken token = default);
        Task UpdateAsync(EditUserData data, ISrvUser iniciator, CancellationToken token = default);
        Task DeleteAsync(string userId, ISrvUser iniciator, CancellationToken token = default);
    }
}

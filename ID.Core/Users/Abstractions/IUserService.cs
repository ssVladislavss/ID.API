namespace ID.Core.Users.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfo>> GetAsync(Iniciator iniciator, CancellationToken token = default);
        Task<IEnumerable<UserInfo>> GetAsync(UserSearchFilter filter, Iniciator iniciator, CancellationToken token = default);
        Task<UserInfo> FindByIdAsync(string userId, Iniciator iniciator, CancellationToken token = default);
        Task<UserInfo> FindByNameAsync(string userName, Iniciator iniciator, CancellationToken token = default);
        Task<UserInfo> FindByEmailAsync(string email, Iniciator iniciator, CancellationToken token = default);
        Task<CreateUserResult> AddAsync(CreateUserData data, Iniciator iniciator, CancellationToken token = default);
        Task UpdateAsync(EditUserData data, Iniciator iniciator, CancellationToken token = default);
        Task DeleteAsync(string userId, Iniciator iniciator, CancellationToken token = default);
    }
}

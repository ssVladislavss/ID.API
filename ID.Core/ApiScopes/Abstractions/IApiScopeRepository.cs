namespace ID.Core.ApiScopes.Abstractions
{
    public interface IApiScopeRepository
    {
        Task<IEnumerable<IDApiScope>> GetAsync(CancellationToken token = default);
        Task<IEnumerable<IDApiScope>> GetAsync(ApiScopeSearchFilter filter, CancellationToken token = default);
        Task<IDApiScope?> FindAsync(int id, CancellationToken token = default);
        Task<IDApiScope?> FindByNameAsync(string name, CancellationToken token = default);
        Task<int> AddAsync(IDApiScope apiScope, CancellationToken token = default);
        Task EditAsync(IDApiScope apiScope, CancellationToken token = default);
        Task RemoveAsync(int id, CancellationToken token = default);
    }
}

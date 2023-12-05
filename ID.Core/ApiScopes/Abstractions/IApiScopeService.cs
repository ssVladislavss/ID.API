namespace ID.Core.ApiScopes.Abstractions
{
    public interface IApiScopeService
    {
        Task<IEnumerable<IDApiScope>> GetAsync(Iniciator iniciator, bool includeStandartScopes = false, CancellationToken token = default);
        Task<IEnumerable<IDApiScope>> GetAsync(ApiScopeSearchFilter filter, Iniciator iniciator, bool includeStandartScopes = false, CancellationToken token = default);
        Task<IDApiScope> FindAsync(int id, Iniciator iniciator, CancellationToken token = default);
        Task<IDApiScope> FindByNameAsync(string name, Iniciator iniciator, CancellationToken token = default);
        Task<IDApiScope> AddAsync(IDApiScope apiScope, Iniciator iniciator, CancellationToken token = default);
        Task EditAsync(IDApiScope apiScope, Iniciator iniciator, CancellationToken token = default);
        Task EditStateAsync(int scopeId, bool status, Iniciator iniciator, CancellationToken token = default);
        Task RemoveAsync(int id, Iniciator iniciator, CancellationToken token = default);
    }
}

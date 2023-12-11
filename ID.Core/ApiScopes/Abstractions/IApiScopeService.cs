using ISDS.ServiceExtender.Http;

namespace ID.Core.ApiScopes.Abstractions
{
    public interface IApiScopeService
    {
        Task<IEnumerable<IDApiScope>> GetAsync(ISrvUser iniciator, bool includeStandartScopes = false, CancellationToken token = default);
        Task<IEnumerable<IDApiScope>> GetAsync(ApiScopeSearchFilter filter, ISrvUser iniciator, bool includeStandartScopes = false, CancellationToken token = default);
        Task<IDApiScope> FindAsync(int id, ISrvUser iniciator, CancellationToken token = default);
        Task<IDApiScope> FindByNameAsync(string name, ISrvUser iniciator, CancellationToken token = default);
        Task<IDApiScope> AddAsync(IDApiScope apiScope, ISrvUser iniciator, CancellationToken token = default);
        Task EditAsync(IDApiScope apiScope, ISrvUser iniciator, CancellationToken token = default);
        Task EditStateAsync(int scopeId, bool status, ISrvUser iniciator, CancellationToken token = default);
        Task RemoveAsync(int id, ISrvUser iniciator, CancellationToken token = default);
    }
}

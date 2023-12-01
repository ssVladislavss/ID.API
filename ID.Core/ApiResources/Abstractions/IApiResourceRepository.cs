using IdentityServer4.Models;

namespace ID.Core.ApiResources.Abstractions
{
    public interface IApiResourceRepository
    {
        Task<IEnumerable<IDApiResource>> GetAsync(CancellationToken token = default);
        Task<IEnumerable<IDApiResource>> GetAsync(ApiResourceSearchFilter filter, CancellationToken token = default);
        Task<IDApiResource?> FindAsync(int id, CancellationToken token = default);
        Task<IDApiResource?> FindByNameAsync(string name, CancellationToken token = default);
        Task<int> AddAsync(IDApiResource resource, CancellationToken token = default);
        Task EditAsync(IDApiResource resource, CancellationToken token = default);
        Task RemoveAsync(int id, CancellationToken token = default);
    }
}

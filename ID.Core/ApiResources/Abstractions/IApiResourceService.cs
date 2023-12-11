using ISDS.ServiceExtender.Http;

namespace ID.Core.ApiResources.Abstractions
{
    public interface IApiResourceService
    {
        Task<IEnumerable<IDApiResource>> GetAsync(ISrvUser iniciator, CancellationToken token = default);
        Task<IEnumerable<IDApiResource>> GetAsync(ApiResourceSearchFilter filter, ISrvUser iniciator, CancellationToken token = default);
        Task<IDApiResource> FindAsync(int resourceId, ISrvUser iniciator, CancellationToken token = default);
        Task<IDApiResource> FindByNameAsync(string name, ISrvUser iniciator, CancellationToken token = default);
        Task<IDApiResource> AddAsync(IDApiResource resource, ISrvUser iniciator, CancellationToken token = default);
        Task EditAsync(IDApiResource resource, ISrvUser iniciator, CancellationToken token = default);
        Task EditStateAsync(int resourceId, bool status, ISrvUser iniciator, CancellationToken token = default);
        Task RemoveAsync(int id, ISrvUser iniciator, CancellationToken token = default);
    }
}

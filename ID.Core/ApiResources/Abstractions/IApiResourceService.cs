namespace ID.Core.ApiResources.Abstractions
{
    public interface IApiResourceService
    {
        Task<IEnumerable<IDApiResource>> GetAsync(Iniciator iniciator, CancellationToken token = default);
        Task<IEnumerable<IDApiResource>> GetAsync(ApiResourceSearchFilter filter, Iniciator iniciator, CancellationToken token = default);
        Task<IDApiResource> FindAsync(int resourceId, Iniciator iniciator, CancellationToken token = default);
        Task<IDApiResource> FindByNameAsync(string name, Iniciator iniciator, CancellationToken token = default);
        Task<IDApiResource> AddAsync(IDApiResource resource, Iniciator iniciator, CancellationToken token = default);
        Task EditAsync(IDApiResource resource, Iniciator iniciator, CancellationToken token = default);
        Task EditStateAsync(int resourceId, bool status, Iniciator iniciator, CancellationToken token = default);
        Task RemoveAsync(int id, Iniciator iniciator, CancellationToken token = default);
    }
}

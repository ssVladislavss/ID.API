using ID.Core.ApiResources.Abstractions;
using ID.Core.ApiResources.Exceptions;
using ID.Core.ApiResources.Extensions;

namespace ID.Core.ApiResources
{
    public class ApiResourceService : IApiResourceService
    {
        protected readonly IApiResourceRepository _apiResourceRepository;

        public ApiResourceService(IApiResourceRepository apiResourceRepository)
        {
            _apiResourceRepository = apiResourceRepository ?? throw new ArgumentNullException(nameof(apiResourceRepository));
        }

        public async Task<IDApiResource> AddAsync(IDApiResource resource, Iniciator iniciator, CancellationToken token = default)
        {
            var model = await _apiResourceRepository.FindByNameAsync(resource.Name, token);

            if (model != null)
                throw new ApiResourceAddException($"AddAsync: api resource (ResourceName - {resource.Name}) was found");

            resource.Id = await _apiResourceRepository.AddAsync(resource, token);

            return resource;
        }

        public async Task EditAsync(IDApiResource resource, Iniciator iniciator, CancellationToken token = default)
        {
            var model = await _apiResourceRepository.FindAsync(resource.Id, token)
                ?? throw new ApiResourceEditException($"EditAsync: api resource (ResourceId - {resource.Id}) was not found");

            model.Set(resource);

            await _apiResourceRepository.EditAsync(model, token);
        }

        public async Task EditStateAsync(int resourceId, bool status, Iniciator iniciator, CancellationToken token = default)
        {
            var model = await _apiResourceRepository.FindAsync(resourceId, token)
                ?? throw new ApiResourceEditException($"EditStateAsync: api resource (ResourceId - {resourceId}) was not found");

            if(model.Enabled != status)
            {
                model.Enabled = status;

                await _apiResourceRepository.EditAsync(model, token);
            }
        }

        public async Task<IDApiResource> FindAsync(int resourceId, Iniciator iniciator, CancellationToken token = default)
        {
            var resource = await _apiResourceRepository.FindAsync(resourceId, token);

            return resource ?? throw new ApiResourceNotFoundException($"FindAsync: api resource (ResourceId - {resourceId}) was not found");
        }

        public async Task<IDApiResource> FindByNameAsync(string name, Iniciator iniciator, CancellationToken token = default)
        {
            var resource = await _apiResourceRepository.FindByNameAsync(name, token);

            return resource ?? throw new ApiResourceNotFoundException($"FindByNameAsync: api resource (ResourceName - {name}) was not found");
        }

        public async Task<IEnumerable<IDApiResource>> GetAsync(Iniciator iniciator, CancellationToken token = default)
        {
            var resources = await _apiResourceRepository.GetAsync(token);

            if (!resources.Any())
                throw new ApiResourceNoContentException($"GetAsync: the api resource table does not contain any records");

            return resources;
        }

        public async Task<IEnumerable<IDApiResource>> GetAsync(ApiResourceSearchFilter filter, Iniciator iniciator, CancellationToken token = default)
        {
            var resources = await _apiResourceRepository.GetAsync(filter, token);

            if (!resources.Any())
                throw new ApiResourceNoContentException($"GetAsync: the api resource table does not contain any records by filter (Filter - {filter})");

            return resources;
        }

        public async Task RemoveAsync(int id, Iniciator iniciator, CancellationToken token = default)
        {
            var apiResource = await _apiResourceRepository.FindAsync(id, token)
                ?? throw new ApiResourceRemoveException($"RemoveAsync: api resource (ResourceId - {id}) was not found");

            await _apiResourceRepository.RemoveAsync(apiResource.Id, token);
        }
    }
}

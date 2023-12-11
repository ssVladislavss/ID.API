using ID.Core.ApiResources.Abstractions;
using ID.Core.ApiResources.Default;
using ID.Core.ApiResources.Exceptions;
using ID.Core.ApiResources.Extensions;
using ISDS.ServiceExtender.Http;

namespace ID.Core.ApiResources
{
    public class ApiResourceService : IApiResourceService
    {
        protected readonly IApiResourceRepository _apiResourceRepository;

        public ApiResourceService(IApiResourceRepository apiResourceRepository)
        {
            _apiResourceRepository = apiResourceRepository ?? throw new ArgumentNullException(nameof(apiResourceRepository));
        }

        public virtual async Task<IDApiResource> AddAsync(IDApiResource resource, ISrvUser iniciator, CancellationToken token = default)
        {
            if (DefaultApiResource.Resources.Any(x => x.Name == resource.Name))
                throw new ApiResourceDefaultException($"AddAsync: resource (ResourceName - {resource.Name}) it is forbidden to add a resource");

            var model = await _apiResourceRepository.FindByNameAsync(resource.Name, token);

            if (model != null)
                throw new ApiResourceAddException($"AddAsync: api resource (ResourceName - {resource.Name}) was found");

            resource.Id = await _apiResourceRepository.AddAsync(resource, token);

            return resource;
        }
        public virtual async Task EditAsync(IDApiResource resource, ISrvUser iniciator, CancellationToken token = default)
        {
            var model = await _apiResourceRepository.FindAsync(resource.Id, token)
                ?? throw new ApiResourceEditException($"EditAsync: api resource (ResourceId - {resource.Id}) was not found");

            if (DefaultApiResource.Resources.Any(x => x.Name == model.Name))
                throw new ApiResourceDefaultException($"EditAsync: resource (ResourceName - {model.Name}) it is forbidden to edit a resource");

            model.Set(resource);

            await _apiResourceRepository.EditAsync(model, token);
        }
        public virtual async Task EditStateAsync(int resourceId, bool status, ISrvUser iniciator, CancellationToken token = default)
        {
            var model = await _apiResourceRepository.FindAsync(resourceId, token)
                ?? throw new ApiResourceEditException($"EditStateAsync: api resource (ResourceId - {resourceId}) was not found");

            if (DefaultApiResource.Resources.Any(x => x.Name == model.Name))
                throw new ApiResourceDefaultException($"EditStateAsync: resource (ResourceName - {model.Name}) it is forbidden to edit a resource");

            if (model.Enabled != status)
            {
                model.Enabled = status;

                await _apiResourceRepository.EditAsync(model, token);
            }
        }
        public virtual async Task<IDApiResource> FindAsync(int resourceId, ISrvUser iniciator, CancellationToken token = default)
        {
            var resource = await _apiResourceRepository.FindAsync(resourceId, token);

            return resource ?? throw new ApiResourceNotFoundException($"FindAsync: api resource (ResourceId - {resourceId}) was not found");
        }
        public virtual async Task<IDApiResource> FindByNameAsync(string name, ISrvUser iniciator, CancellationToken token = default)
        {
            var resource = await _apiResourceRepository.FindByNameAsync(name, token);

            return resource ?? throw new ApiResourceNotFoundException($"FindByNameAsync: api resource (ResourceName - {name}) was not found");
        }
        public virtual async Task<IEnumerable<IDApiResource>> GetAsync(ISrvUser iniciator, CancellationToken token = default)
        {
            var resources = await _apiResourceRepository.GetAsync(token);

            if (!resources.Any())
                throw new ApiResourceNoContentException($"GetAsync: the api resource table does not contain any records");

            return resources;
        }
        public virtual async Task<IEnumerable<IDApiResource>> GetAsync(ApiResourceSearchFilter filter, ISrvUser iniciator, CancellationToken token = default)
        {
            var resources = await _apiResourceRepository.GetAsync(filter, token);

            if (!resources.Any())
                throw new ApiResourceNoContentException($"GetAsync: the api resource table does not contain any records by filter (Filter - {filter})");

            return resources;
        }
        public virtual async Task RemoveAsync(int id, ISrvUser iniciator, CancellationToken token = default)
        {
            var apiResource = await _apiResourceRepository.FindAsync(id, token)
                ?? throw new ApiResourceRemoveException($"RemoveAsync: api resource (ResourceId - {id}) was not found");

            if (DefaultApiResource.Resources.Any(x => x.Name == apiResource.Name))
                throw new ApiResourceDefaultException($"EditStateAsync: resource (ResourceName - {apiResource.Name}) it is forbidden to delete a resource");

            await _apiResourceRepository.RemoveAsync(apiResource.Id, token);
        }
    }
}

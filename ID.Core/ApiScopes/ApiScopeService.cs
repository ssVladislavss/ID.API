using ID.Core.ApiScopes.Abstractions;
using ID.Core.ApiScopes.Exceptions;
using ID.Core.ApiScopes.Extensions;

namespace ID.Core.ApiScopes
{
    public class ApiScopeService : IApiScopeService
    {
        protected readonly IApiScopeRepository _apiScopeRepository;

        public ApiScopeService(IApiScopeRepository apiScopeRepository)
        {
            _apiScopeRepository = apiScopeRepository ?? throw new ArgumentNullException(nameof(apiScopeRepository));
        }

        public async Task<IDApiScope> AddAsync(IDApiScope apiScope, Iniciator iniciator, CancellationToken token = default)
        {
            var model = await _apiScopeRepository.FindByNameAsync(apiScope.Name, token);

            if (model != null)
                throw new ApiScopeAddException($"AddAsync: api scope (ScopeName - {apiScope.Name}) was found");

            apiScope.Id = await _apiScopeRepository.AddAsync(apiScope, token);

            return apiScope;
        }

        public async Task EditAsync(IDApiScope apiScope, Iniciator iniciator, CancellationToken token = default)
        {
            var model = await _apiScopeRepository.FindAsync(apiScope.Id, token)
                ?? throw new ApiScopeEditException($"EditAsync: api scope (ScopeId - {apiScope.Id}) was not found");

            model.Set(apiScope);

            await _apiScopeRepository.EditAsync(model, token);
        }

        public async Task EditStateAsync(int scopeId, bool status, Iniciator iniciator, CancellationToken token = default)
        {
            var model = await _apiScopeRepository.FindAsync(scopeId, token)
                ?? throw new ApiScopeEditException($"EditAsync: api scope (ScopeId - {scopeId}) was not found");

            if(model.Enabled != status)
            {
                model.Enabled = status;

                await _apiScopeRepository.EditAsync(model, token);
            }
        }

        public async Task<IDApiScope> FindAsync(int id, Iniciator iniciator, CancellationToken token = default)
        {
            var scope = await _apiScopeRepository.FindAsync(id, token);

            return scope ?? throw new ApiScopeNotFoundException($"FindAsync: api scope (ScopeId - {id}) was not found");
        }

        public async Task<IDApiScope> FindByNameAsync(string name, Iniciator iniciator, CancellationToken token = default)
        {
            var scope = await _apiScopeRepository.FindByNameAsync(name, token);

            return scope ?? throw new ApiScopeNotFoundException($"FindByNameAsync: api scope (ScopeName - {name}) was not found");
        }

        public async Task<IEnumerable<IDApiScope>> GetAsync(Iniciator iniciator, CancellationToken token = default)
        {
            var scopes = await _apiScopeRepository.GetAsync(token);

            if(!scopes.Any())
                throw new ApiScopeNoContentException($"GetAsync: the api scope table does not contain any records");

            return scopes;
        }

        public async Task<IEnumerable<IDApiScope>> GetAsync(ApiScopeSearchFilter filter, Iniciator iniciator, CancellationToken token = default)
        {
            var scopes = await _apiScopeRepository.GetAsync(filter, token);

            if (!scopes.Any())
                throw new ApiScopeNoContentException($"GetAsync: the api scope table does not contain any records by filter (Filter - {filter})");

            return scopes;
        }

        public async Task RemoveAsync(int id, Iniciator iniciator, CancellationToken token = default)
        {
            var scope = await _apiScopeRepository.FindAsync(id, token);
            if(scope == null)
            {
                throw new ApiScopeRemoveException($"RemoveAsync: api scope (ScopeId - {id}) was not found");
            }

            await _apiScopeRepository.RemoveAsync(scope.Id, token);
        }
    }
}

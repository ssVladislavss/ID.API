using ID.Core.ApiResources;
using ID.Core.ApiResources.Abstractions;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ID.Data.EF.Repositories
{
    public class ApiResourceRepository : IApiResourceRepository
    {
        protected readonly ConfigurationDbContext _context;
        protected readonly Expression<Func<IdentityServer4.EntityFramework.Entities.ApiResource, object>>[] _includeProps;

        public ApiResourceRepository
            (ConfigurationDbContext context,
             Expression<Func<IdentityServer4.EntityFramework.Entities.ApiResource, object>>[] includeProps = null!)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _includeProps = includeProps;
        }

        public async Task<int> AddAsync(IDApiResource resource, CancellationToken token = default)
        {
            var entity = resource.ToEntity();

            _context.ApiResources.Add(entity);

            await _context.SaveChangesAsync(token);

            return entity.Id;
        }

        public async Task EditAsync(IDApiResource resource, CancellationToken token = default)
        {
            var entity = resource.ToEntity();

            var query = _context.ApiResources.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var nowEntity = await query.FirstOrDefaultAsync(x => x.Id == resource.Id);
            if (nowEntity != null)
            {
                entity.Id = nowEntity.Id;

                _context.Entry(nowEntity).CurrentValues.SetValues(entity);

                foreach (var resourceScope in nowEntity.Scopes)
                    _context.Entry(resourceScope).State = EntityState.Deleted;

                if (entity.Scopes.Any())
                {
                    var resourceScopes = entity.Scopes.ToList();
                    resourceScopes.ForEach(x => x.ApiResourceId = entity.Id);

                    _context.AddRange(resourceScopes);
                }

                foreach (var resourceClaim in nowEntity.UserClaims)
                    _context.Entry(resourceClaim).State = EntityState.Deleted;

                if (entity.UserClaims.Any())
                {
                    var resourceClaims = entity.UserClaims.ToList();
                    resourceClaims.ForEach(x => x.ApiResourceId = entity.Id);

                    _context.AddRange(resourceClaims);
                }

                foreach(var resourceProperty in nowEntity.Properties)
                    _context.Entry(resourceProperty).State = EntityState.Deleted;

                if (entity.Properties.Any())
                {
                    var resourceProperties = entity.Properties.ToList();
                    resourceProperties.ForEach(x => x.ApiResourceId = entity.Id);

                    _context.AddRange(resourceProperties);
                }

                foreach (var resourceSecret in nowEntity.Secrets)
                    _context.Entry(resourceSecret).State = EntityState.Deleted;

                if (entity.Secrets.Any())
                {
                    var resourceSecrets = entity.Secrets.ToList();
                    resourceSecrets.ForEach(x => x.ApiResourceId = entity.Id);

                    _context.AddRange(resourceSecrets);
                }

                await _context.SaveChangesAsync(token);
            }
        }

        public async Task<IDApiResource?> FindAsync(int id, CancellationToken token = default)
        {
            var query = _context.ApiResources.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var resource = await query.FirstOrDefaultAsync(x => x.Id == id, token);
            if (resource == null)
                return null;

            var model = resource.ToModel();

            return new IDApiResource(resource.Id, model);
        }

        public async Task<IDApiResource?> FindByNameAsync(string name, CancellationToken token = default)
        {
            var query = _context.ApiResources.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var resource = await query.FirstOrDefaultAsync(x => x.Name == name, token);

            if (resource == null)
                return null;

            var model = resource.ToModel();
            
            return new IDApiResource(resource.Id, model);
        }

        public async Task<IEnumerable<IDApiResource>> GetAsync(CancellationToken token = default)
        {
            var query = _context.ApiResources.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var resources = await query.ToListAsync(token);
            var models = resources.Select(x => x.ToModel());

            return resources.Join(models, e => e.Name, m => m.Name, (e, m) => new IDApiResource(e.Id, m));
        }

        public async Task<IEnumerable<IDApiResource>> GetAsync(ApiResourceSearchFilter filter, CancellationToken token = default)
        {
            var query = _context.ApiResources.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            if (filter.Id.HasValue)
                query = query.Where(x => x.Id == filter.Id.Value);
            if(!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name == filter.Name);

            var resources = await query.ToListAsync(token);
            var models = resources.Select(x => x.ToModel());

            return resources.Join(models, e => e.Name, m => m.Name, (e, m) => new IDApiResource(e.Id, m));
        }

        public async Task RemoveAsync(int id, CancellationToken token = default)
        {
            var resource = await _context.ApiResources.FindAsync(new object?[] { id }, cancellationToken: token);
            if(resource != null)
            {
                _context.Entry(resource).State = EntityState.Deleted;

                await _context.SaveChangesAsync(token);
            }
        }
    }
}

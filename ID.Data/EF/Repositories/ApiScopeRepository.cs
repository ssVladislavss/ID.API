using ID.Core.ApiResources;
using ID.Core.ApiScopes;
using ID.Core.ApiScopes.Abstractions;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ID.Data.EF.Repositories
{
    public class ApiScopeRepository : IApiScopeRepository
    {
        protected readonly ConfigurationDbContext _context;
        protected readonly Expression<Func<IdentityServer4.EntityFramework.Entities.ApiScope, object>>[] _includeProps;

        public ApiScopeRepository
            (ConfigurationDbContext context,
             Expression<Func<IdentityServer4.EntityFramework.Entities.ApiScope, object>>[] includeProps = null!)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _includeProps = includeProps;
        }

        public async Task<int> AddAsync(IDApiScope apiScope, CancellationToken token = default)
        {
            var entity = apiScope.ToEntity();

            _context.ApiScopes.Add(entity);

            await _context.SaveChangesAsync(token);

            return entity.Id;
        }

        public async Task EditAsync(IDApiScope apiScope, CancellationToken token = default)
        {
            var entity = apiScope.ToEntity();

            var query = _context.ApiScopes.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var nowScope = await query.FirstOrDefaultAsync(x => x.Id == apiScope.Id, cancellationToken: token);

            if(nowScope != null)
            {
                entity.Id = nowScope.Id;

                _context.Entry(nowScope).CurrentValues.SetValues(entity);

                foreach (var scopeUserClaim in nowScope.UserClaims)
                    _context.Entry(scopeUserClaim).State = EntityState.Deleted;

                if(entity.UserClaims.Any())
                {
                    var userClaims = entity.UserClaims.ToList();
                    userClaims.ForEach(x => x.ScopeId = entity.Id);

                    _context.AddRange(userClaims);
                }

                foreach (var property in nowScope.Properties)
                    _context.Entry(property).State = EntityState.Deleted;

                if(entity.Properties.Any())
                {
                    var scopeProperties = entity.Properties.ToList();
                    scopeProperties.ForEach(x => x.ScopeId = entity.Id);

                    _context.AddRange(scopeProperties);
                }

                await _context.SaveChangesAsync(token);
            }
        }

        public async Task<IDApiScope?> FindAsync(int id, CancellationToken token = default)
        {
            var query = _context.ApiScopes.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var scope = await query.FirstOrDefaultAsync(x => x.Id == id, token);
            if (scope == null)
                return null;

            var model = scope.ToModel();

            return new IDApiScope(scope.Id, model);
        }

        public async Task<IDApiScope?> FindByNameAsync(string name, CancellationToken token = default)
        {
            var query = _context.ApiScopes.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var scope = await query.FirstOrDefaultAsync(x => x.Name == name, token);
            if (scope == null)
                return null;

            var model = scope.ToModel();

            return new IDApiScope(scope.Id, model);
        }

        public async Task<IEnumerable<IDApiScope>> GetAsync(CancellationToken token = default)
        {
            var query = _context.ApiScopes.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var scopes = await query.ToListAsync(token);
            var models = scopes.Select(x => x.ToModel());

            return scopes.Join(models, e => e.Name, m => m.Name, (e, m) => new IDApiScope(e.Id, m));
        }

        public async Task<IEnumerable<IDApiScope>> GetAsync(ApiScopeSearchFilter filter, CancellationToken token = default)
        {
            var query = _context.ApiScopes.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            if (filter.Id.HasValue)
                query = query.Where(x => x.Id == filter.Id.Value);
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name == filter.Name);

            var scopes = await query.ToListAsync(token);
            var models = scopes.Select(x => x.ToModel());

            return scopes.Join(models, e => e.Name, m => m.Name, (e, m) => new IDApiScope(e.Id, m));
        }

        public async Task RemoveAsync(int id, CancellationToken token = default)
        {
            var scope = await _context.ApiScopes.FindAsync(new object?[] { id }, cancellationToken: token);
            if(scope != null)
            {
                _context.Entry(scope).State = EntityState.Deleted;

                await _context.SaveChangesAsync(token);
            }
        }
    }
}

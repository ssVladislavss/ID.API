using ID.Core.Clients;
using ID.Core.Clients.Abstractions;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ID.Data.EF.Repositories
{
    public class ClientRepository : IClientRepository
    {
        protected readonly ConfigurationDbContext _context;
        protected readonly Expression<Func<IdentityServer4.EntityFramework.Entities.Client, object>>[] _includeProps;

        public ClientRepository
            (ConfigurationDbContext context,
             Expression<Func<IdentityServer4.EntityFramework.Entities.Client, object>>[] includeProps = null!)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _includeProps = includeProps;
        }

        public async Task<int> AddAsync(Client client, CancellationToken token = default)
        {
            var entity = client.ToEntity();

            _context.Clients.Add(entity);

            await _context.SaveChangesAsync(token);

            return entity.Id;
        }

        public async Task EditAsync(Client client, CancellationToken token = default)
        {
            var entity = client.ToEntity();

            var query = _context.Clients.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var nowEntity = await query.FirstOrDefaultAsync(x => x.ClientId == client.ClientId, token);
            if(nowEntity != null)
            {
                entity.Id = nowEntity.Id;

                _context.Entry(nowEntity).CurrentValues.SetValues(entity);

                foreach (var clientCors in nowEntity.AllowedCorsOrigins)
                    _context.Entry(clientCors).State = EntityState.Deleted;

                foreach (var clientGrantType in nowEntity.AllowedGrantTypes)
                    _context.Entry(clientGrantType).State = EntityState.Deleted;

                foreach (var clientScopes in nowEntity.AllowedScopes)
                    _context.Entry(clientScopes).State = EntityState.Deleted;

                foreach (var clientClaim in nowEntity.Claims)
                    _context.Entry(clientClaim).State = EntityState.Deleted;

                foreach (var clientSecret in nowEntity.ClientSecrets)
                    _context.Entry(clientSecret).State = EntityState.Deleted;

                foreach (var clientRestriction in nowEntity.IdentityProviderRestrictions)
                    _context.Entry(clientRestriction).State = EntityState.Deleted;

                foreach (var clientPostLogoutRedirectUrl in nowEntity.PostLogoutRedirectUris)
                    _context.Entry(clientPostLogoutRedirectUrl).State = EntityState.Deleted;

                foreach (var clientProperty in nowEntity.Properties)
                    _context.Entry(clientProperty).State = EntityState.Deleted;

                foreach (var clientRedirectUrl in nowEntity.RedirectUris)
                    _context.Entry(clientRedirectUrl).State = EntityState.Deleted;

                if (entity.AllowedCorsOrigins.Any())
                {
                    var clientCors = entity.AllowedCorsOrigins.ToList();
                    clientCors.ForEach(x => x.ClientId = entity.Id);

                    _context.AddRange(clientCors);
                }

                if (entity.AllowedGrantTypes.Any())
                {
                    var clientGrantTypes = entity.AllowedGrantTypes.ToList();
                    clientGrantTypes.ForEach(x => x.ClientId = entity.Id);

                    _context.AddRange(clientGrantTypes);
                }

                if (entity.AllowedScopes.Any())
                {
                    var clientScopes = entity.AllowedScopes.ToList();
                    clientScopes.ForEach(x => x.ClientId = entity.Id);

                    _context.AddRange(clientScopes);
                }

                if (entity.Claims.Any())
                {
                    var clientClaims = entity.Claims.ToList();
                    clientClaims.ForEach(x => x.ClientId = entity.Id);

                    _context.AddRange(clientClaims);
                }

                if (entity.ClientSecrets.Any())
                {
                    var clientSecrets = entity.ClientSecrets.ToList();
                    clientSecrets.ForEach(x => x.ClientId = entity.Id);

                    _context.AddRange(clientSecrets);
                }

                if (entity.IdentityProviderRestrictions.Any())
                {
                    var clientProviderRestrictions = entity.IdentityProviderRestrictions.ToList();
                    clientProviderRestrictions.ForEach(x => x.ClientId = entity.Id);

                    _context.AddRange(clientProviderRestrictions);
                }

                if (entity.PostLogoutRedirectUris.Any())
                {
                    var clientPostLogoutRedirectUris = entity.PostLogoutRedirectUris.ToList();
                    clientPostLogoutRedirectUris.ForEach(x => x.ClientId = entity.Id);

                    _context.AddRange(clientPostLogoutRedirectUris);
                }

                if (entity.Properties.Any())
                {
                    var clientProperties = entity.Properties.ToList();
                    clientProperties.ForEach(x => x.ClientId = entity.Id);

                    _context.AddRange(clientProperties);
                }

                if (entity.RedirectUris.Any())
                {
                    var clientRedirectUris = entity.RedirectUris.ToList();
                    clientRedirectUris.ForEach(x => x.ClientId = entity.Id);

                    _context.AddRange(clientRedirectUris);
                }

                await _context.SaveChangesAsync(token);
            }
        }

        public async Task<Client?> FindAsync(string clientId, CancellationToken token = default)
        {
            var query = _context.Clients.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var client = await query.FirstOrDefaultAsync(x => x.ClientId == clientId, token);

            if (client == null)
                return null;

            return client.ToModel();
        }

        public async Task<IEnumerable<Client>> GetAsync(CancellationToken token = default)
        {
            var query = _context.Clients.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            var clients = await query.ToListAsync(cancellationToken: token);

            return clients.Select(x => x.ToModel());
        }

        public async Task<IEnumerable<Client>> GetAsync(ClientSearchFilter filter, CancellationToken token = default)
        {
            var query = _context.Clients.AsQueryable();

            if (_includeProps != null)
                query = _includeProps.Aggregate(query, (current, incProp) => current.Include(incProp));

            if (filter.Id.HasValue)
                query = query.Where(x => x.Id == filter.Id.Value);
            if(!string.IsNullOrEmpty(filter.ClientId))
                query = query.Where(x => x.ClientId == filter.ClientId);
            if(filter.Status.HasValue)
                query = query.Where(x => x.Enabled == filter.Status.Value);
            if(!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.ClientName == filter.Name);

            var clients = await query.ToListAsync(token);

            return clients.Select(x => x.ToModel());
        }

        public async Task RemoveAsync(string clientId, CancellationToken token = default)
        {
            var nowEntity = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId, token);
            if(nowEntity != null)
            {
                _context.Entry(nowEntity).State = EntityState.Deleted;

                await _context.SaveChangesAsync(token);
            }
        }
    }
}

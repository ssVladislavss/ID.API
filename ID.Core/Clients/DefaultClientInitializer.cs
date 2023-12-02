using ID.Core.ApiResources.Default;
using ID.Core.ApiScopes.Default;
using ID.Core.Clients.Default;
using ID.Core.Default;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ID.Core.Clients
{
    public partial class ClientService
    {
        public static async Task StartInitializerAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            ConfigurationDbContext configurationContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            configurationContext.Database.Migrate();

            var defaultClients = DefaultClient.Clients;

            foreach (var client in defaultClients)
            {
                var existDefaultClient = configurationContext.Clients.FirstOrDefault(x => x.ClientId == client.ClientId);
                if (existDefaultClient == null)
                {
                    configurationContext.Clients.Add(client.ToEntity());
                }
            }

            if (!configurationContext.IdentityResources.Any())
            {
                configurationContext.IdentityResources.Add(DefaultIdentityResource.EmailResource.ToEntity());
                configurationContext.IdentityResources.Add(DefaultIdentityResource.AddressResource.ToEntity());
                configurationContext.IdentityResources.Add(DefaultIdentityResource.OpenIdResource.ToEntity());
                configurationContext.IdentityResources.Add(DefaultIdentityResource.PhoneResource.ToEntity());
                configurationContext.IdentityResources.Add(DefaultIdentityResource.ProfileResource.ToEntity());
            }

            var defaultApiScopes = DefaultApiScope.Scopes;

            foreach (var apiScope in defaultApiScopes)
            {
                if (!configurationContext.ApiScopes.Any(x => x.Name == apiScope.Name))
                    configurationContext.Add(apiScope.ToEntity());
            }

            var defaultApiResources = DefaultApiResource.Resources;

            foreach (var recource in defaultApiResources)
            {
                if (!configurationContext.ApiResources.Any(x => x.Name == recource.Name))
                    configurationContext.Add(recource.ToEntity());
            }

            await configurationContext.SaveChangesAsync();
        }
    }
}

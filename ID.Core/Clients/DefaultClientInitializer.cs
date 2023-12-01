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

            var defaultClient = DefaultClient.ServiceID;

            var existDefaultClient = configurationContext.Clients.FirstOrDefault(x => x.ClientId == defaultClient.ClientId);
            if (existDefaultClient == null)
            {
                configurationContext.Clients.Add(defaultClient.ToEntity());
            }

            if (!configurationContext.IdentityResources.Any())
            {
                configurationContext.IdentityResources.Add(DefaultIdentityResource.EmailResource.ToEntity());
                configurationContext.IdentityResources.Add(DefaultIdentityResource.AddressResource.ToEntity());
                configurationContext.IdentityResources.Add(DefaultIdentityResource.OpenIdResource.ToEntity());
                configurationContext.IdentityResources.Add(DefaultIdentityResource.PhoneResource.ToEntity());
                configurationContext.IdentityResources.Add(DefaultIdentityResource.ProfileResource.ToEntity());
            }

            var defaultApiScope = DefaultApiScope.ServiceID;

            if (!configurationContext.ApiScopes.Any(x => x.Name == defaultApiScope.Name))
                configurationContext.Add(defaultApiScope.ToEntity());

            var defaultApiResource = DefaultApiResource.ServiceID;

            if (!configurationContext.ApiResources.Any(x => x.Name == defaultApiResource.Name))
                configurationContext.Add(defaultApiResource.ToEntity());

            await configurationContext.SaveChangesAsync();
        }
    }
}

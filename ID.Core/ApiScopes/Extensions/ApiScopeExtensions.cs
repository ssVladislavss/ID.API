using IdentityServer4.Models;

namespace ID.Core.ApiScopes.Extensions
{
    public static class ApiScopeExtensions
    {
        public static void Set(this ApiScope inner, ApiScope source)
        {
            inner.Required = source.Required;
            inner.Name = source.Name;
            inner.Description = source.Description;
            inner.ShowInDiscoveryDocument = source.ShowInDiscoveryDocument;
            inner.UserClaims = source.UserClaims;
            inner.DisplayName = source.DisplayName;
            inner.Emphasize = source.Emphasize;
            inner.Enabled = source.Enabled;
            inner.Properties = source.Properties;
        }
    }
}

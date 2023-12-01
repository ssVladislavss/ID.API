using IdentityServer4.Models;

namespace ID.Core.ApiResources.Extensions
{
    public static class ApiResourceExtensions
    {
        public static void Set(this ApiResource inner, ApiResource source)
        {
            inner.ApiSecrets = source.ApiSecrets;
            inner.ShowInDiscoveryDocument = source.ShowInDiscoveryDocument;
            inner.UserClaims = source.UserClaims;
            inner.AllowedAccessTokenSigningAlgorithms = source.AllowedAccessTokenSigningAlgorithms;
            inner.Description = source.Description;
            inner.DisplayName = source.DisplayName;
            inner.Name = source.Name;
            inner.Enabled = source.Enabled;
            inner.Properties = source.Properties;
            inner.Scopes = source.Scopes;
        }
    }
}

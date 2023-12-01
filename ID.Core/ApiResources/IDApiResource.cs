using IdentityServer4.Models;

namespace ID.Core.ApiResources
{
    public class IDApiResource : ApiResource
    {
        public int Id { get; set; }

        public IDApiResource() { }
        public IDApiResource(int id, ApiResource resource)
        {
            this.Id = id;
            this.AllowedAccessTokenSigningAlgorithms = resource.AllowedAccessTokenSigningAlgorithms;
            this.ApiSecrets = resource.ApiSecrets;
            this.ShowInDiscoveryDocument = resource.ShowInDiscoveryDocument;
            this.UserClaims = resource.UserClaims;
            this.Enabled = resource.Enabled;
            this.Description = resource.Description;
            this.DisplayName = resource.DisplayName;
            this.Name = resource.Name;
            this.Properties = resource.Properties;
            this.Scopes = resource.Scopes;
        }
    }
}

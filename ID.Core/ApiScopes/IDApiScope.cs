using IdentityServer4.Models;

namespace ID.Core.ApiScopes
{
    public class IDApiScope : ApiScope
    {
        public int Id { get; set; }

        public IDApiScope() { }
        public IDApiScope(int id, ApiScope apiScope)
        {
            this.Id = id;
            this.Name = apiScope.Name;
            this.Description = apiScope.Description;
            this.DisplayName = apiScope.DisplayName;
            this.Enabled = apiScope.Enabled;
            this.Required = apiScope.Required;
            this.Properties = apiScope.Properties;
            this.Emphasize = apiScope.Emphasize;
            this.ShowInDiscoveryDocument = apiScope.ShowInDiscoveryDocument;
            this.UserClaims = apiScope.UserClaims;
        }
    }
}

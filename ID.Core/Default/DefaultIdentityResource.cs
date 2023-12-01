using IdentityServer4.Models;

namespace ID.Core.Default
{
    public class DefaultIdentityResource
    {
        public static IdentityResource OpenIdResource
            => new IdentityResources.OpenId();
        public static IdentityResource EmailResource
            => new IdentityResources.Email();
        public static IdentityResource ProfileResource
            => new IdentityResources.Profile();
        public static IdentityResource AddressResource
            => new IdentityResources.Address();
        public static IdentityResource PhoneResource
            => new IdentityResources.Phone();
    }
}

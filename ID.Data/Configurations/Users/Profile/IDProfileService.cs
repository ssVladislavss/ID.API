using ID.Core.Users;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ID.Data.Configurations.Users.Profile
{
    public class IDProfileService : ProfileService<UserID>
    {
        public IDProfileService(UserManager<UserID> userManager, IUserClaimsPrincipalFactory<UserID> claimsFactory) : base(userManager, claimsFactory)
        {
        }

        public IDProfileService(UserManager<UserID> userManager, IUserClaimsPrincipalFactory<UserID> claimsFactory, ILogger<ProfileService<UserID>> logger) : base(userManager, claimsFactory, logger)
        {
        }
    }
}

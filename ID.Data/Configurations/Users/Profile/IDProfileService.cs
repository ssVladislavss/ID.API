using ID.Core.Users;
using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ID.Data.Configurations.Users.Profile
{
    public class IDProfileService : ProfileService<UserID>
    {
        protected readonly RoleManager<IdentityRole> RoleManager;
        public IDProfileService
            (UserManager<UserID> userManager,
             IUserClaimsPrincipalFactory<UserID> claimsFactory,
             RoleManager<IdentityRole> roleManager)
             : base(userManager, claimsFactory)
        {
            RoleManager = roleManager;
        }

        public IDProfileService
            (UserManager<UserID> userManager,
             IUserClaimsPrincipalFactory<UserID> claimsFactory,
             ILogger<ProfileService<UserID>> logger,
             RoleManager<IdentityRole> roleManager)
             : base(userManager, claimsFactory, logger)
        {
            RoleManager = roleManager;
        }

        protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, UserID user)
        {
            var responseClaims = await GetIDUserClaimsAsync(user);

            var requestedClaims = context.RequestedClaimTypes.ToList();

            requestedClaims.AddRange(responseClaims.Claims.Select(x => x.Type));

            context.RequestedClaimTypes = requestedClaims;

            await base.GetProfileDataAsync(context, user);
        }

        protected override async Task<ClaimsPrincipal> GetUserClaimsAsync(UserID user)
        {
            return await GetIDUserClaimsAsync(user);
        }

        protected async Task<ClaimsPrincipal> GetIDUserClaimsAsync(UserID user)
        {
            var currentPrincipal = await base.GetUserClaimsAsync(user);

            var userRoleNames = await UserManager.GetRolesAsync(user);
            var userClaimsSpecified = await UserManager.GetClaimsAsync(user);

            var userRoleClaims = new Dictionary<string, Claim>();
            var userSpecifiedClaims = new Dictionary<string, Claim>();

            var claims = new List<Claim>(currentPrincipal.Claims);

            if (!string.IsNullOrEmpty(user.LastName))
                claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            if (!string.IsNullOrEmpty(user.FirstName))
                claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            if (!string.IsNullOrEmpty(user.SecondName))
                claims.Add(new Claim(JwtClaimTypes.MiddleName, user.SecondName));
            if (user.BirthDate.HasValue)
                claims.Add(new Claim(JwtClaimTypes.BirthDate, user.BirthDate.Value.ToString("d")));
            if (user.AvailableFunctionality?.Count > 0)
                claims.Add(new Claim("allowed_functional", JsonConvert.SerializeObject(user.AvailableFunctionality)));
            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
            if (!string.IsNullOrEmpty(user.PhoneNumber))
                claims.Add(new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber));

            claims.AddRange(userRoleNames.Select(x => new Claim(JwtClaimTypes.Role, x)));

            claims = claims.Distinct().ToList();

            var identity = new ClaimsIdentity(claims);

            return new ClaimsPrincipal(identity);
        }
    }
}

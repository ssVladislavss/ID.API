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
        public IDProfileService
            (UserManager<UserID> userManager,
             IUserClaimsPrincipalFactory<UserID> claimsFactory)
             : base(userManager, claimsFactory)
        {
        }

        public IDProfileService
            (UserManager<UserID> userManager,
             IUserClaimsPrincipalFactory<UserID> claimsFactory,
             ILogger<ProfileService<UserID>> logger)
             : base(userManager, claimsFactory, logger)
        {
        }

        protected override Task GetProfileDataAsync(ProfileDataRequestContext context, UserID user)
        {
            var requestedClaims = context.RequestedClaimTypes.ToList();

            requestedClaims.AddRange(new[]
            {
                JwtClaimTypes.FamilyName,
                JwtClaimTypes.GivenName,
                JwtClaimTypes.MiddleName,
                JwtClaimTypes.BirthDate,
                "allowed_functional",
                JwtClaimTypes.PhoneNumber,
                JwtClaimTypes.EmailVerified,
                JwtClaimTypes.PhoneNumberVerified
            });

            context.RequestedClaimTypes = requestedClaims;

            return base.GetProfileDataAsync(context, user);
        }

        protected override async Task<ClaimsPrincipal> GetUserClaimsAsync(UserID user)
        {
            var userRoleNames = await UserManager.GetRolesAsync(user);

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.LastName))
                claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            if (!string.IsNullOrEmpty(user.FirstName))
                claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            if (!string.IsNullOrEmpty(user.SecondName))
                claims.Add(new Claim(JwtClaimTypes.MiddleName, user.SecondName));
            if(user.BirthDate.HasValue)
                claims.Add(new Claim(JwtClaimTypes.BirthDate, user.BirthDate.Value.ToString("d")));
            if (user.AvailableFunctionality?.Count > 0)
                claims.Add(new Claim("allowed_functional", JsonConvert.SerializeObject(user.AvailableFunctionality)));
            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
            if (!string.IsNullOrEmpty(user.PhoneNumber))
                claims.Add(new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber));

            claims.AddRange(userRoleNames.Select(x => new Claim(JwtClaimTypes.Role, x)));
            claims.Add(new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed.ToString()));
            claims.Add(new Claim(JwtClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed.ToString()));

            var identity = new ClaimsIdentity(claims);

            return new ClaimsPrincipal(identity);
        }
    }
}

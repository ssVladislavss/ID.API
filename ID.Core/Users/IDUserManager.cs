using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace ID.Core.Users
{
    public class IDUserManager : UserManager<UserID>
    {
        public IDUserManager
            (IUserStore<UserID> store,
             IOptions<IdentityOptions> optionsAccessor,
             IPasswordHasher<UserID> passwordHasher,
             IEnumerable<IUserValidator<UserID>> userValidators,
             IEnumerable<IPasswordValidator<UserID>> passwordValidators,
             ILookupNormalizer keyNormalizer,
             IdentityErrorDescriber errors,
             IServiceProvider services,
             ILogger<UserManager<UserID>> logger)
             : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public override async Task<string> GenerateChangeEmailTokenAsync(UserID user, string newEmail)
        {
            var baseManagerTokenResult = await base.GenerateChangeEmailTokenAsync(user, newEmail);

            var bytesInToken = Encoding.UTF8.GetBytes(baseManagerTokenResult);

            baseManagerTokenResult = Convert.ToBase64String(bytesInToken).Replace('+', '-').Replace('/', '_');

            return baseManagerTokenResult;
        }

        public override async Task<IdentityResult> ChangeEmailAsync(UserID user, string newEmail, string token)
        {
            token = token.Replace('-', '+').Replace('_', '/');

            var bytesTokenFromBase64 = Convert.FromBase64String(token);
            token = Encoding.UTF8.GetString(bytesTokenFromBase64);

            return await base.ChangeEmailAsync(user, newEmail, token);
        }
    }
}

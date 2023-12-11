using Builder.Messages.Html.Abstractions;
using EmailSending.Abstractions;
using ID.Core;
using ID.Core.Users;
using ID.Core.Users.Abstractions;
using ID.Core.Users.Exceptions;
using ID.Host.Infrastracture.Services.Users.Exceptions;
using ISDS.ServiceExtender.Http;

namespace ID.Host.Infrastracture.Services.Users
{
    public class IDUserVerificationCodeService : IVerificationService
    {
        protected readonly IDUserManager _userManager;
        protected readonly IHtmlBuilder _htmlBuilder;
        protected readonly IEmailProvider _emailProvider;

        public IDUserVerificationCodeService
            (IDUserManager userManager,
             IHtmlBuilder htmlBuilder,
             IEmailProvider emailProvider)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _htmlBuilder = htmlBuilder ?? throw new ArgumentNullException(nameof(htmlBuilder));
            _emailProvider = emailProvider ?? throw new ArgumentNullException(nameof(emailProvider));
        }

        public virtual async Task SendCodeOnEmailAsync(string userId, ISrvUser iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserNotFoundException($"SendCodeOnEmailAsync: user (UserId - {userId}) was not found");

            var currentCode = currentUser.GenerateCode(4);

            var saveCodeResult = await _userManager.SetAuthenticationTokenAsync(currentUser, "identity_server", IDConstants.Users.VerifyCodeTypes.CodeOnEmail, currentCode);
            if (!saveCodeResult.Succeeded)
                throw new UserCodeAddException($"SendCodeOnEmailAsync: user (UserId - {currentUser.Id}, Code - {currentCode}) the generated confirmation code could not be saved. " +
                    $"{string.Join(';', saveCodeResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
        }
    }
}

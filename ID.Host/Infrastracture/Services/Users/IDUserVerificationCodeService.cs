using Builder.Messages.Html.Abstractions;
using EmailSending.Abstractions;
using EmailSending.Models;
using ID.Core;
using ID.Core.Clients.Abstractions;
using ID.Core.Clients.Default;
using ID.Core.Users;
using ID.Core.Users.Abstractions;
using ID.Core.Users.Exceptions;
using ID.Host.App_Data.Notify.Email.Models;
using ID.Host.Infrastracture.Services.Users.Exceptions;
using IdentityServer4.Models;
using ISDS.ServiceExtender.Http;
using ServiceExtender.Sms.Abstractions;
using ServiceExtender.Sms.Models;

namespace ID.Host.Infrastracture.Services.Users
{
    public class IDUserVerificationCodeService : IVerificationService
    {
        protected readonly IDUserManager _userManager;
        protected readonly IHtmlBuilder _htmlBuilder;
        protected readonly IEmailProvider _emailProvider;
        protected readonly IClientRepository _clientRepository;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        protected readonly ISmsProviderFactory _smsProviderFactory;

        public IDUserVerificationCodeService
            (IDUserManager userManager,
             IHtmlBuilder htmlBuilder,
             IEmailProvider emailProvider,
             IClientRepository clientRepository,
             IWebHostEnvironment webHostEnvironment,
             ISmsProviderFactory smsProviderFactory)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _htmlBuilder = htmlBuilder ?? throw new ArgumentNullException(nameof(htmlBuilder));
            _emailProvider = emailProvider ?? throw new ArgumentNullException(nameof(emailProvider));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));

            _emailProvider.OnError += EmailProvider_OnError;
            _smsProviderFactory = smsProviderFactory ?? throw new ArgumentNullException(nameof(smsProviderFactory));
        }

        private void EmailProvider_OnError(object sender, EmailSending.Events.SendEmailEventArgs args)
        {
            if (args.Exception != null)
                throw new UserEmailNotifyDeliveredException($"Email notify: (Data - {args.SendingMessage}) the message could not be delivered", args.Exception);
            else
                throw new UserEmailNotifyDeliveredException($"Email notify: (Data - {args.SendingMessage}) the message could not be delivered");
        }

        public virtual async Task SendCodeOnEmailAsync(string userId, ISrvUser iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserNotFoundException($"SendCodeOnEmailAsync: user (UserId - {userId}) was not found");

            var existSavedVerificationCode = await _userManager.GetAuthenticationTokenAsync
                (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.CodeNames.ConfirmationCode);

            if(!string.IsNullOrEmpty(existSavedVerificationCode))
                await _userManager.RemoveAuthenticationTokenAsync
                      (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.CodeNames.ConfirmationCode);

            var currentCode = currentUser.GenerateCode(4);
            var validTo = DateTimeOffset.UtcNow.AddMinutes(30).ToString();

            var saveCodeResult = await _userManager.SetAuthenticationTokenAsync
                (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.CodeNames.ConfirmationCode, $"{currentCode}|{validTo}");
            if (!saveCodeResult.Succeeded)
                throw new UserCodeAddException($"SendCodeOnEmailAsync: user (UserId - {currentUser.Id}, Code - {currentCode}) the generated confirmation code could not be saved. " +
                    $"{string.Join(';', saveCodeResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            Client? client = !string.IsNullOrEmpty(iniciator.ClientId) && !string.IsNullOrWhiteSpace(iniciator.ClientId)
                ? await _clientRepository.FindAsync(iniciator.ClientId, token)
                : DefaultClient.ServiceID;

            var body = await _htmlBuilder.SetHtmlPath(Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "Notify", "Email", "BaseVerificationCode.cshtml"))
                                         .SetHtmlObject(new UserVerificationCodeHtmlData(currentUser.Email, currentCode, client))
                                         .SetHtmlTemplateName("verification_code:" + currentUser.Email)
                                         .BuildAsync();

            await _emailProvider.SendAsync(new EmailMessage(currentUser.Email, client?.ClientName ?? "Сервис идинтификации", body, "Ваш код подтверждения"), token);
        }

        public virtual async Task SendCodeOnSmsAsync(string userId, ISrvUser iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserNotFoundException($"SendCodeOnSmsAsync: user (UserId - {userId}) was not found");

            var existSavedVerificationCode = await _userManager.GetAuthenticationTokenAsync
                (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.CodeNames.ConfirmationCode);

            if (!string.IsNullOrEmpty(existSavedVerificationCode))
                await _userManager.RemoveAuthenticationTokenAsync
                      (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.CodeNames.ConfirmationCode);

            var currentCode = currentUser.GenerateCode(4);
            var validTo = DateTimeOffset.UtcNow.AddMinutes(30).ToString();

            var saveCodeResult = await _userManager.SetAuthenticationTokenAsync
                (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.CodeNames.ConfirmationCode, $"{currentCode}|{validTo}");
            if (!saveCodeResult.Succeeded)
                throw new UserCodeAddException
                    ($"SendCodeOnEmailAsync: user (UserId - {currentUser.Id}, Code - {currentCode}) the generated confirmation code could not be saved. " +
                    $"{string.Join(';', saveCodeResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            Client? client = !string.IsNullOrEmpty(iniciator.ClientId) && !string.IsNullOrWhiteSpace(iniciator.ClientId)
                ? await _clientRepository.FindAsync(iniciator.ClientId, token)
                : DefaultClient.ServiceID;

            var smsProvider = _smsProviderFactory.Create(SmsProviderType.RedSms);

            await smsProvider.SendAsync
                (new SmsSendingMessage($"Ваш код подтверждения: {currentCode}", "79251066154"),
                 new SmsRequestOptions("test", "12Qwaszx", "default", true));
        }

        public virtual async Task VerifyCodeAsync(string userId, string currentCode, ISrvUser iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserVerifyCodeException($"VerifyCodeAsync: user (UserId - {userId}) was not found");

            var userSavedCode = await _userManager.GetAuthenticationTokenAsync
                (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.CodeNames.ConfirmationCode);

            if (string.IsNullOrEmpty(userSavedCode))
                throw new UserVerifyCodeException($"VerifyCodeAsync: user (UserId - {currentUser.Id}) the user does not have any active confirmation codes");

            var checkingCode = userSavedCode.Split('|')[0];
            var validDate = DateTimeOffset.Parse(userSavedCode.Split('|')[1]);

            if(DateTimeOffset.UtcNow >= validDate)
            {
                await _userManager.RemoveAuthenticationTokenAsync
                      (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.CodeNames.ConfirmationCode);

                throw new UserVerifyCodeExpireException($"VerifyCodeAsync: user (UserId - {currentUser.Id}, ConfirmationCode - {currentCode}, ValidTo - {validDate}) " +
                    $"the lifetime of the received confirmation code has expired");
            }

            if (checkingCode != currentCode)
                throw new UserVerifyCodeException($"VerifyCodeAsync: user (UserId - {currentUser.Id}, ConfirmationCode - {currentCode})" +
                    $" the received confirmation code from the user is not valid");

            await _userManager.RemoveAuthenticationTokenAsync
                (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.CodeNames.ConfirmationCode);
        }
    }
}

﻿using Builder.Messages.Html.Abstractions;
using EmailSending.Abstractions;
using EmailSending.Models;
using ID.Core;
using ID.Core.Clients.Abstractions;
using ID.Core.Users;
using ID.Core.Users.Abstractions;
using ID.Core.Users.Exceptions;
using ID.Host.App_Data.Notify.Email.Models;
using ID.Host.Infrastracture.Services.Users.Exceptions;
using IdentityServer4.Models;
using ISDS.ServiceExtender.Http;

namespace ID.Host.Infrastracture.Services.Users
{
    public class IDUserVerificationCodeService : IVerificationService
    {
        protected readonly IDUserManager _userManager;
        protected readonly IHtmlBuilder _htmlBuilder;
        protected readonly IEmailProvider _emailProvider;
        protected readonly IClientRepository _clientRepository;
        protected readonly IWebHostEnvironment _webHostEnvironment;

        public IDUserVerificationCodeService
            (IDUserManager userManager,
             IHtmlBuilder htmlBuilder,
             IEmailProvider emailProvider,
             IClientRepository clientRepository,
             IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _htmlBuilder = htmlBuilder ?? throw new ArgumentNullException(nameof(htmlBuilder));
            _emailProvider = emailProvider ?? throw new ArgumentNullException(nameof(emailProvider));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));

            _emailProvider.OnError += _emailProvider_OnError;
        }

        private void _emailProvider_OnError(EmailMessage errorSendingMessage, Exception? exception = null)
        {
            if (exception != null)
                throw new UserEmailNotifyDeliveredException($"Email notify: (Data - {errorSendingMessage}) the message could not be delivered", exception);
        }

        public virtual async Task SendCodeOnEmailAsync(string userId, ISrvUser iniciator, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserNotFoundException($"SendCodeOnEmailAsync: user (UserId - {userId}) was not found");

            var existSavedVerificationCode = await _userManager.GetAuthenticationTokenAsync
                (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.ConfirmationCodeNames.BaseCode);

            if(!string.IsNullOrEmpty(existSavedVerificationCode))
                await _userManager.RemoveAuthenticationTokenAsync
                      (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.ConfirmationCodeNames.BaseCode);

            var currentCode = currentUser.GenerateCode(4);
            var validTo = DateTimeOffset.UtcNow.AddMinutes(30).ToString();

            var saveCodeResult = await _userManager.SetAuthenticationTokenAsync
                (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.ConfirmationCodeNames.BaseCode, $"{currentCode}|{validTo}");
            if (!saveCodeResult.Succeeded)
                throw new UserCodeAddException($"SendCodeOnEmailAsync: user (UserId - {currentUser.Id}, Code - {currentCode}) the generated confirmation code could not be saved. " +
                    $"{string.Join(';', saveCodeResult.Errors.Select(x => $"{x.Code} - {x.Description}"))}");

            Client? client = null;
            if (!string.IsNullOrEmpty(iniciator.ClientId))
                client = await _clientRepository.FindAsync(iniciator.ClientId, token);

            var body = await _htmlBuilder.SetHtmlPath(Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "Notify", "Email", "BaseVerificationCode.cshtml"))
                                         .SetHtmlObject(new UserVerificationCodeHtmlData(currentUser.Email, currentCode, client))
                                         .SetHtmlTemplateName("verification_code:" + currentUser.Email)
                                         .BuildAsync();

            await _emailProvider.SendAsync(new EmailMessage(currentUser.Email, client?.ClientName ?? "Сервис идинтификации", body, "Ваш код подтверждения"), token);
        }

        public Task SendCodeOnSmsAsync(string userId, ISrvUser iniciator, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public virtual async Task VerifyCodeAsync(string userId, string currentCode, CancellationToken token = default)
        {
            var currentUser = await _userManager.FindByIdAsync(userId)
                ?? throw new UserVerifyCodeException($"VerifyCodeAsync: user (UserId - {userId}) was not found");

            var userSavedCode = await _userManager.GetAuthenticationTokenAsync
                (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.ConfirmationCodeNames.BaseCode);

            if (string.IsNullOrEmpty(userSavedCode))
                throw new UserVerifyCodeException($"VerifyCodeAsync: user (UserId - {currentUser.Id}) the user does not have any active confirmation codes");

            var checkingCode = userSavedCode.Split('|')[0];
            var validDate = DateTimeOffset.Parse(userSavedCode.Split('|')[1]);

            if(DateTimeOffset.UtcNow >= validDate)
            {
                await _userManager.RemoveAuthenticationTokenAsync
                      (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.ConfirmationCodeNames.BaseCode);

                throw new UserVerifyCodeExpireException($"VerifyCodeAsync: user (UserId - {currentUser.Id}, ConfirmationCode - {currentCode}, ValidTo - {validDate}) " +
                    $"the lifetime of the received confirmation code has expired");
            }

            if (checkingCode != currentCode)
                throw new UserVerifyCodeException($"VerifyCodeAsync: user (UserId - {currentUser.Id}, ConfirmationCode - {currentCode})" +
                    $" the received confirmation code from the user is not valid");

            await _userManager.RemoveAuthenticationTokenAsync
                (currentUser, IDConstants.Users.ConfirmationCodeProviders.IDProvider, IDConstants.Users.ConfirmationCodeNames.BaseCode);
        }
    }
}

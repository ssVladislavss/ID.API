using Builder.Messages.Html.Abstractions;
using EmailSending.Abstractions;
using EmailSending.Models;
using ID.Core;
using ID.Core.Clients.Abstractions;
using ID.Core.Clients.Default;
using ID.Core.Users;
using ID.Core.Users.Exceptions;
using ID.Host.App_Data.Notify.Email.Models;
using ID.Host.Infrastracture.Services.Users.Exceptions;
using IdentityServer4.Models;
using ISDS.ServiceExtender.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ID.Host.Infrastracture.Services.Users
{
    public class IDUserService : UserService
    {
        protected readonly IEmailProvider _emailProvider;
        protected readonly IHtmlBuilder _htmlBuilder;
        protected readonly IWebHostEnvironment _webHostEnvironment;

        public IDUserService
            (IDUserManager userManager,
             RoleManager<IdentityRole> roleManager,
             IEmailProvider emailProvider,
             IHtmlBuilder htmlBuilder,
             IWebHostEnvironment webHostEnvironment,
             IClientRepository clientRepository,
             IOptions<IdentityOptions>? identityDescriptor = null)
             : base(userManager, roleManager, clientRepository, identityDescriptor)
        {
            _emailProvider = emailProvider ?? throw new ArgumentNullException(nameof(emailProvider));
            _htmlBuilder = htmlBuilder ?? throw new ArgumentNullException(nameof(htmlBuilder));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));

            _emailProvider.OnError += _emailProvider_OnError;
        }

        private void _emailProvider_OnError(EmailMessage errorSendingMessage, Exception? exception = null)
        {
            if (exception != null)
                throw new UserEmailNotifyDeliveredException($"Email notify: (Data - {errorSendingMessage}) the message could not be delivered", exception);
        }

        public override async Task<CreateUserResult> AddAsync(CreateUserData data, ISrvUser iniciator, CancellationToken token = default)
        {
            var addedResult = await base.AddAsync(data, iniciator, token);

            if (!string.IsNullOrEmpty(iniciator.ClientId) || !string.IsNullOrEmpty(data.ClientId))
            {
                await Task.Run(async () =>
                {
                    var client = await _clientRepository.FindAsync(string.IsNullOrEmpty(data.ClientId) ? iniciator.ClientId! : data.ClientId, token);
                    if (client != null)
                    {
                        var verificationToken = await _userManager.GenerateChangeEmailTokenAsync(addedResult.CreatedUser, data.User.Email);

                        var body = await _htmlBuilder.SetHtmlPath(Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "Notify", "Email", "BaseIDUserRegistration.cshtml"))
                                                     .SetHtmlObject(new CreatedUserHtmlData(addedResult.Password, AppSettings.ServiceAddresses?.IdentityUrl?.AbsoluteUri
                                                            + $"api/account/confirmation/email" +
                                                            $"?userId={addedResult.CreatedUser.Id}&newEmail={data.User.Email}&token={verificationToken}",
                                                            client))
                                                     .SetHtmlTemplateName($"verify_email:" + client.ClientName)
                                                     .BuildAsync();

                        await _emailProvider.SendAsync(new EmailMessage(addedResult.CreatedUser.Email, client.ClientName, body, $"Данные о зарегистрированном аккаунте {client.ClientName}"), token);
                    }
                }, token).ConfigureAwait(false);
            }

            return addedResult;
        }
        public override async Task SetEmailAsync(string userId, string newEmail, ISrvUser iniciator, CancellationToken token = default)
        {
            Client? client = !string.IsNullOrEmpty(iniciator.ClientId) && !string.IsNullOrWhiteSpace(iniciator.ClientId)
                ? await _clientRepository.FindAsync(iniciator.ClientId, token)
                : DefaultClient.ServiceID;

            if (client != null)
            {
                var currentUser = await _userManager.FindByIdAsync(userId);
                if (currentUser != null)
                {
                    var verificationToken = await _userManager.GenerateChangeEmailTokenAsync(currentUser, newEmail);

                    var body = await _htmlBuilder.SetHtmlPath(Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "Notify", "Email", "BaseEmailConfirm.cshtml"))
                                                 .SetHtmlObject(new UserConfirmEmailHtmlData(currentUser.Email, AppSettings.ServiceAddresses?.IdentityUrl?.AbsoluteUri
                                                    + $"api/account/confirmation/email" +
                                                    $"?userId={userId}&newEmail={newEmail}&token={verificationToken}", client))
                                                 .SetHtmlTemplateName("verify_email:" + client.ClientName)
                                                 .BuildAsync();

                    await _emailProvider.SendAsync(new EmailMessage(newEmail, client.ClientName, body, $"Смена адреса электронной почты"), token);
                }
            }
        }
        public override async Task SetPhoneNumberAsync(string userId, string newPhoneNumber, ISrvUser iniciator, CancellationToken token = default)
        {
            Client? client = !string.IsNullOrWhiteSpace(iniciator.ClientId) && !string.IsNullOrEmpty(iniciator.ClientId)
                ? await _clientRepository.FindAsync(iniciator.ClientId, token)
                : DefaultClient.ServiceID;

            if(client != null)
            {
                var currentUser = await _userManager.FindByIdAsync(userId)
                    ?? throw new UserNotFoundException($"SetPhoneNumberAsync: user (UserId - {userId}) was not found");

                var confirmationToken = await _userManager.GenerateChangePhoneNumberTokenAsync(currentUser, newPhoneNumber);

                // скоро будет отправка на sms
                await Task.Delay(2000, token);
            }
        }
        public override async Task<string> ResetPasswordAsync(string email, string? clientId = null, CancellationToken token = default)
        {
            var client = !string.IsNullOrEmpty(clientId) && !string.IsNullOrWhiteSpace(clientId)
                ? await _clientRepository.FindAsync(clientId, token)
                : DefaultClient.ServiceID;

            if (client != null)
            {
                var currentUser = await _userManager.FindByEmailAsync(email)
                        ?? throw new UserChangePasswordException($"ResetPasswordAsync: user (Email -  {email}) was not found");

                var currentPasswordConfirmToken = await _userManager.GeneratePasswordResetTokenAsync(currentUser);

                var body = await _htmlBuilder.SetHtmlPath(Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "Notify", "Email", "BaseConfirmResetPassword.cshtml"))
                                             .SetHtmlObject(new UserConfirmResetPasswordHtmlData(currentUser.Email, AppSettings.ServiceAddresses?.IdentityUrl?.AbsoluteUri
                                                + $"api/account/confirmation/password/reset" +
                                                $"?userId={currentUser.Id}&clientId={client.ClientId}&token={currentPasswordConfirmToken}", client))
                                             .SetHtmlTemplateName("verify_reset_password:" + client.ClientName)
                                             .BuildAsync();

                await _emailProvider.SendAsync(new EmailMessage(currentUser.Email, client.ClientName, body, $"Подтверждение сброса пароля"), token);
            }

            return string.Empty;
        }
        public override async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword, ISrvUser iniciator, CancellationToken token = default)
        {
            await base.ChangePasswordAsync(userId, currentPassword, newPassword, iniciator, token);

            if (!string.IsNullOrEmpty(iniciator.ClientId))
            {
                var client = await _clientRepository.FindAsync(iniciator.ClientId, token);
                if(client != null)
                {
                    var currentUser = await _userManager.FindByIdAsync(userId);
                    if (currentUser != null)
                    {
                        var lockVerificationCode = currentUser.GenerateCode(10);

                        await _userManager.SetAuthenticationTokenAsync
                            (currentUser,
                             IDConstants.Users.ConfirmationCodeProviders.IDProvider,
                             IDConstants.Users.CodeNames.CodeBySetLockoutEnabled,
                             lockVerificationCode);

                        var body = await _htmlBuilder.SetHtmlPath(Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "Notify", "Email", "BaseUserChangedPassword.cshtml"))
                                                     .SetHtmlObject(new UserChangedPasswordHtmlData(currentUser.Email, AppSettings.ServiceAddresses?.IdentityUrl?.AbsoluteUri
                                                        + $"api/account/confirmation/email/lock" +
                                                        $"?userId={userId}&code={lockVerificationCode}", client))
                                                     .SetHtmlTemplateName("changed_password:" + client.ClientName)
                                                     .BuildAsync();

                        await _emailProvider.SendAsync(new EmailMessage(currentUser.Email, client.ClientName, body, $"Пароль от Вашей учётной записи был изменён"), token);
                    }
                }
            }
        }
        public override async Task<ResetPasswordConfirmResult> ConfirmResetPasswordAsync(string userId, string base64ConfirmToken, string? clientId = null, CancellationToken token = default)
        {
            var confirmResult = await base.ConfirmResetPasswordAsync(userId, base64ConfirmToken, clientId, token);

            Client? client = confirmResult.Client;

            if(client == null && !string.IsNullOrEmpty(clientId))
                client = await _clientRepository.FindAsync(clientId, token);

            var currentUser = await _userManager.FindByIdAsync(userId);
            if(currentUser != null)
            {
                var body = await _htmlBuilder.SetHtmlPath(Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "Notify", "Email", "BasePasswordResetComplete.cshtml"))
                                             .SetHtmlObject(new UserPasswordResetCompleteHtmlData(currentUser.Email, confirmResult.NewPassword, client))
                                             .SetHtmlTemplateName("verify_reset_password_complete:" + client?.ClientName)
                                             .BuildAsync();

                await _emailProvider.SendAsync(new EmailMessage(currentUser.Email, client?.ClientName ?? "ID", body, $"Пароль от учетной записи сброшен"), token);
            }

            return confirmResult;
        }
    }
}

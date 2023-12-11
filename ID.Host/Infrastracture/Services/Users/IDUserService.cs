using Builder.Messages.Html.Abstractions;
using EmailSending.Abstractions;
using EmailSending.Models;
using ID.Core.Clients.Abstractions;
using ID.Core.Users;
using ID.Host.App_Data.Notify.Email.Models;
using ID.Host.Infrastracture.Services.Users.Exceptions;
using ISDS.ServiceExtender.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text;

namespace ID.Host.Infrastracture.Services.Users
{
    public class IDUserService : UserService
    {
        protected readonly IEmailProvider _emailProvider;
        protected readonly IHtmlBuilder _htmlBuilder;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        protected readonly IClientRepository _clientRepository;

        public IDUserService
            (IDUserManager userManager,
             RoleManager<IdentityRole> roleManager,
             IEmailProvider emailProvider,
             IHtmlBuilder htmlBuilder,
             IWebHostEnvironment webHostEnvironment,
             IClientRepository clientRepository,
             IOptions<IdentityOptions>? identityDescriptor = null)
             : base(userManager, roleManager, identityDescriptor)
        {
            _emailProvider = emailProvider ?? throw new ArgumentNullException(nameof(emailProvider));
            _htmlBuilder = htmlBuilder ?? throw new ArgumentNullException(nameof(htmlBuilder));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));

            _emailProvider.OnError += _emailProvider_OnError;
        }

        private void _emailProvider_OnError(EmailMessage errorSendingMessage, Exception? exception = null)
        {
            if (exception != null)
                throw new EmailNotifyOnRegistredUserException($"Email notify: (Data - {errorSendingMessage}) the message could not be delivered", exception);
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
                                                            + $"api/user/email/confirm" +
                                                            $"?userId={addedResult.CreatedUser.Id}&newEmail={data.User.Email}&token={verificationToken}",
                                                            client))
                                                     .SetHtmlTemplateName(client.ClientName)
                                                     .BuildAsync();

                        await _emailProvider.SendAsync(new EmailMessage(addedResult.CreatedUser.Email, client.ClientName, body, client.ClientName), token);
                    }
                }, token).ConfigureAwait(false);
            }

            return addedResult;
        }
        public override async Task ChangeEmailAsync(string userId, string newEmail, ISrvUser iniciator, CancellationToken token = default)
        {
            await base.ChangeEmailAsync(userId, newEmail, iniciator, token);

            if (!string.IsNullOrEmpty(iniciator.ClientId))
            {
                await Task.Run(async () =>
                {
                    var client = await _clientRepository.FindAsync(iniciator.ClientId);

                    if (client != null)
                    {
                        var currentUser = await _userManager.FindByIdAsync(userId);
                        if(currentUser != null)
                        {
                            var verificationToken = await _userManager.GenerateChangeEmailTokenAsync(currentUser, newEmail);

                            var body = await _htmlBuilder.SetHtmlPath(Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "Notify", "Email", "BaseEmailConfirm.cshtml"))
                                                         .SetHtmlObject(new UserConfirmEmailHtmlData(AppSettings.ServiceAddresses?.IdentityUrl?.AbsoluteUri
                                                            + $"api/user/email/confirm" +
                                                            $"?userId={userId}&newEmail={newEmail}&token={verificationToken}", client))
                                                         .SetHtmlTemplateName("verify_email:" + client.ClientName)
                                                         .BuildAsync();

                            await _emailProvider.SendAsync(new EmailMessage(newEmail, client.ClientName, body, client.ClientName), token);
                        }
                    }

                }, token).ConfigureAwait(false);
            }
        }
    }
}

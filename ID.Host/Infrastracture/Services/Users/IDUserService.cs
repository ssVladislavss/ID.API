using Builder.Messages.Html.Abstractions;
using EmailSending.Abstractions;
using EmailSending.Models;
using ID.Core;
using ID.Core.Clients.Abstractions;
using ID.Core.Users;
using ID.Host.Infrastracture.Services.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ID.Host.Infrastracture.Services.Users
{
    public class IDUserService : UserService
    {
        protected readonly IEmailProvider _emailProvider;
        protected readonly IHtmlBuilder _htmlBuilder;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        protected readonly IClientRepository _clientRepository;

        public IDUserService
            (UserManager<UserID> userManager,
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
        }

        public override async Task<CreateUserResult> AddAsync(CreateUserData data, Iniciator iniciator, CancellationToken token = default)
        {
            var addedResult = await base.AddAsync(data, iniciator, token);

            var client = await _clientRepository.FindAsync(iniciator.ClientId!, token);
            if(client != null)
            {
                var body = await _htmlBuilder.SetHtmlPath(Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "Notify", "Email", "BaseIDUserRegistration.cshtml"))
                                             .SetHtmlObject(new CreatedUserHtmlData(addedResult.CreatedUser.Email, addedResult.Password, client))
                                             .SetHtmlTemplateName(client.ClientName)
                                             .BuildAsync();

                await _emailProvider.SendAsync(new EmailMessage(addedResult.CreatedUser.Email, iniciator.ClientName ?? "Сервер идентификации", body), token);
            }

            return addedResult;
        }
    }
}

using ID.Core;
using ID.Core.Clients;
using ID.Core.Clients.Abstractions;
using ID.Host.Infrastracture;
using ID.Host.Infrastracture.Mapping;
using ID.Host.Infrastracture.Models.Clients;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using ISDS.ServiceExtender.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ID.Host.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = IDConstants.Roles.RootAdmin)]
    public class ClientController : BaseController<RequestIniciator>
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        [HttpGet("")]
        public async Task<ActionResult<AjaxResult<IEnumerable<Client>>>> GetAsync([FromQuery] ClientSearhFilterViewModel? filter)
        {
            var clientFilter = new ClientSearchFilter();

            if(filter != null)
            {
                if(filter.Id.HasValue)
                    clientFilter = clientFilter.WithId(filter.Id.Value);
                if(!string.IsNullOrWhiteSpace(filter.ClientId))
                    clientFilter = clientFilter.WithClientId(filter.ClientId);
                if(!string.IsNullOrEmpty(filter.Name))
                    clientFilter = clientFilter.WithName(filter.Name);
                if(filter.Status.HasValue)
                    clientFilter = clientFilter.WithStatus(filter.Status.Value);
            }

            var clients = await _clientService.GetAsync(clientFilter, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<IEnumerable<Client>>.Success(clients));
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<AjaxResult<Client>>> FindAsync(string clientId)
        {
            var client = await _clientService.FindAsync(clientId, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<Client>.Success(client));
        }

        [HttpPost("create")]
        public async Task<ActionResult<AjaxResult<Client>>> CreateAsync(CreateClientViewModel model)
        {
            var addedClient = await _clientService.AddAsync(model.ToModel(), SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<Client>.Success(addedClient));
        }

        [HttpPut("edit")]
        public async Task<ActionResult<AjaxResult>> EditAsync(EditClientViewModel model)
        {
            await _clientService.EditAsync(model.ToModel(), SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }

        [HttpPut("edit/status")]
        public async Task<ActionResult> EditStatusAsync(EditClientStatusViewModel model)
        {
            await _clientService.EditStatusAsync(model.ClientId, model.Status, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }

        [HttpDelete("{clientId}/remove")]
        public async Task<ActionResult<AjaxResult>> RemoveAsync(string clientId)
        {
            await _clientService.RemoveAsync(clientId, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }
    }
}

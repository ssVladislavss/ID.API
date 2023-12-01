using ID.Core.ApiScopes;
using ID.Core.ApiScopes.Abstractions;
using ID.Host.Infrastracture;
using ID.Host.Infrastracture.Mapping;
using ID.Host.Infrastracture.Models.ApiScopes;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace ID.Host.Controllers
{
    [Route("api/apiscope")]
    [ApiController]
    public class ApiScopeController : ControllerBase
    {
        private readonly IApiScopeService _apiScopeService;

        public ApiScopeController(IApiScopeService apiScopeService)
        {
            _apiScopeService = apiScopeService ?? throw new ArgumentNullException(nameof(apiScopeService));
        }

        [HttpGet("")]
        public async Task<ActionResult<AjaxResult<IEnumerable<IDApiScope>>>> GetAsync([FromQuery] ApiScopesSearchFilterViewModel? filter)
        {
            var scopesFilter = new ApiScopeSearchFilter();
            if(filter != null)
            {
                if (filter.Id.HasValue)
                    scopesFilter = scopesFilter.WithId(filter.Id.Value);
                if(!string.IsNullOrEmpty(filter.Name))
                    scopesFilter = scopesFilter.WithName(filter.Name);
            }

            var apiScopes = await _apiScopeService.GetAsync(scopesFilter, new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult<IEnumerable<IDApiScope>>.Success(apiScopes));
        }

        [HttpGet("{scopeId:int}")]
        public async Task<ActionResult<AjaxResult<IDApiScope>>> FindAsync(int scopeId)
        {
            var apiScope = await _apiScopeService.FindAsync(scopeId, new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult<ApiScope>.Success(apiScope));
        }

        [HttpPost("create")]
        public async Task<ActionResult<AjaxResult<IDApiScope>>> CreateAsync(CreateApiScopeViewModel model)
        {
            var addedScope = await _apiScopeService.AddAsync(model.ToModel(), new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult<IDApiScope>.Success(addedScope));
        }

        [HttpPut("edit")]
        public async Task<ActionResult<AjaxResult>> EditAsync(EditApiScopeViewModel model)
        {
            await _apiScopeService.EditAsync(model.ToModel(), new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }

        [HttpPut("edit/status")]
        public async Task<ActionResult<AjaxResult>> EditStatusAsync(EditApiScopeStatusViewModel model)
        {
            await _apiScopeService.EditStateAsync(model.Id, model.Status, new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }

        [HttpDelete("{scopeId:int}/remove")]
        public async Task<ActionResult<AjaxResult>> RemoveAsync(int scopeId)
        {
            await _apiScopeService.RemoveAsync(scopeId, new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }
    }
}

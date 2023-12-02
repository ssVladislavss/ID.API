using ID.Core.ApiResources.Abstractions;
using ID.Host.Infrastracture.Models.ApiScopes;
using ID.Host.Infrastracture;
using Microsoft.AspNetCore.Mvc;
using ID.Core.ApiResources;
using ID.Host.Infrastracture.Models.ApiResources;
using ID.Host.Infrastracture.Mapping;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;

namespace ID.Host.Controllers
{
    [Route("api/apiresource")]
    [ApiController]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class ApiResourceController : ControllerBase
    {
        private readonly IApiResourceService _apiResourceService;

        public ApiResourceController(IApiResourceService apiResourceService)
        {
            _apiResourceService = apiResourceService ?? throw new ArgumentNullException(nameof(apiResourceService));
        }

        [HttpGet("")]
        public async Task<ActionResult<AjaxResult<IEnumerable<IDApiResource>>>> GetAsync([FromQuery] ApiResourceSearchFilterViewModel? filter)
        {
            var scopesFilter = new ApiResourceSearchFilter();
            if (filter != null)
            {
                if (filter.Id.HasValue)
                    scopesFilter = scopesFilter.WithId(filter.Id.Value);
                if (!string.IsNullOrEmpty(filter.Name))
                    scopesFilter = scopesFilter.WithName(filter.Name);
            }

            var apiResources = await _apiResourceService.GetAsync(scopesFilter, new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult<IEnumerable<IDApiResource>>.Success(apiResources));
        }

        [HttpGet("{resourceId:int}")]
        public async Task<ActionResult<AjaxResult<IDApiResource>>> FindAsync(int resourceId)
        {
            var apiResource = await _apiResourceService.FindAsync(resourceId, new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult<IDApiResource>.Success(apiResource));
        }

        [HttpPost("create")]
        public async Task<ActionResult<AjaxResult<IDApiResource>>> CreateAsync(CreateApiResourceViewModel model)
        {
            var addedResource = await _apiResourceService.AddAsync(model.ToModel(), new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult<IDApiResource>.Success(addedResource));
        }

        [HttpPut("edit")]
        public async Task<ActionResult<AjaxResult>> EditAsync(EditApiResourceViewModel model)
        {
            await _apiResourceService.EditAsync(model.ToModel(), new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }

        [HttpPut("edit/status")]
        public async Task<ActionResult<AjaxResult>> EditStatusAsync(EditApiScopeStatusViewModel model)
        {
            await _apiResourceService.EditStateAsync(model.Id, model.Status, new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }

        [HttpDelete("{resourceId:int}/remove")]
        public async Task<ActionResult<AjaxResult>> RemoveAsync(int resourceId)
        {
            await _apiResourceService.RemoveAsync(resourceId, new Core.Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }
    }
}

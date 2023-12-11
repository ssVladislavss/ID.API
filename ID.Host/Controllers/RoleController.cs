using ID.Core;
using ID.Core.Roles;
using ID.Core.Roles.Abstractions;
using ID.Host.Infrastracture;
using ID.Host.Infrastracture.Mapping;
using ID.Host.Infrastracture.Models.Roles;
using IdentityServer4.AccessTokenValidation;
using ISDS.ServiceExtender.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ID.Host.Controllers
{
    [Route("api/role")]
    [ApiController]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = IDConstants.Roles.RootAdmin)]
    public class RoleController : BaseController<RequestIniciator>
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        }

        [HttpGet("")]
        public async Task<ActionResult<AjaxResult<IEnumerable<RoleViewModel>>>> GetAsync([FromQuery] RoleSearchFilterViewModel? filter)
        {
            var roleFilter = new RoleSearchFilter();

            if (filter != null)
            {
                if(!string.IsNullOrEmpty(filter.Id))
                    roleFilter = roleFilter.WithId(filter.Id);
                if (!string.IsNullOrEmpty(filter.Name))
                    roleFilter = roleFilter.WithId(filter.Name);
            }

            var roles = await _roleService.GetAsync(roleFilter, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<IEnumerable<RoleViewModel>>.Success(roles.Select(x => new RoleViewModel(x))));
        }

        [HttpGet("{roleId}")]
        public async Task<ActionResult<AjaxResult<RoleViewModel>>> FindByIdAsync(string roleId)
        {
            var role = await _roleService.FindByIdAsync(roleId, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<RoleViewModel>.Success(new RoleViewModel(role)));
        }

        [HttpPost("create")]
        public async Task<ActionResult<AjaxResult<RoleViewModel>>> CreateAsync(CreateRoleViewModel data)
        {
            var createdResult = await _roleService.CreateAsync(data.ToModel(), SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<RoleViewModel>.Success(new RoleViewModel(createdResult)));
        }

        [HttpPut("edit")]
        public async Task<ActionResult<AjaxResult>> EditAsync(EditRoleViewModel data)
        {
            await _roleService.EditAsync(data.ToModel(), SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }

        [HttpDelete("{roleId}/remove")]
        public async Task<ActionResult<AjaxResult>> RemoveAsync(string roleId)
        {
            await _roleService.RemoveAsync(roleId, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }
    }
}

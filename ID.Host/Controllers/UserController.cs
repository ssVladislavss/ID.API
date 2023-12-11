using ID.Core;
using ID.Core.Users;
using ID.Core.Users.Abstractions;
using ID.Host.Infrastracture;
using ID.Host.Infrastracture.Mapping;
using ID.Host.Infrastracture.Models.Users;
using IdentityServer4.AccessTokenValidation;
using ISDS.ServiceExtender.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ID.Host.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController<RequestIniciator>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = IDConstants.Roles.RootAdmin)]
        public async Task<ActionResult<AjaxResult<IEnumerable<UserViewModel>>>> GetAsync([FromQuery] UserSearchFilterViewModel filter)
        {
            var userFilter = new UserSearchFilter();

            if(filter != null)
            {
                if(!string.IsNullOrEmpty(filter.Email))
                    userFilter = userFilter.WithEmail(filter.Email);
                if(!string.IsNullOrEmpty(filter.FirstName))
                    userFilter = userFilter.WithFirstName(filter.FirstName);
                if (!string.IsNullOrEmpty(filter.LastName))
                    userFilter = userFilter.WithLastName(filter.LastName);
                if (!string.IsNullOrEmpty(filter.SecondName))
                    userFilter = userFilter.WithSecondName(filter.SecondName);
                if (!string.IsNullOrEmpty(filter.Phone))
                    userFilter = userFilter.WithPhone(filter.Phone);
                if (filter.BirthDate.HasValue)
                    userFilter = userFilter.WithBirthDate(filter.BirthDate.Value);
                if(!string.IsNullOrEmpty(filter.Role))
                    userFilter = userFilter.WithRole(filter.Role);
            }

            var users = await _userService.GetAsync(userFilter, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<IEnumerable<UserViewModel>>.Success(users.Select(x => new UserViewModel(x))));
        }

        [HttpGet("{userId}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = IDConstants.Roles.RootAdmin)]
        public async Task<ActionResult<AjaxResult<UserViewModel>>> FindByIdAsync(string userId)
        {
            var userInfo = await _userService.FindByIdAsync(userId, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<UserViewModel>.Success(new UserViewModel(userInfo)));
        }
        [HttpGet("by/email/{email}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = IDConstants.Roles.RootAdmin)]
        public async Task<ActionResult<AjaxResult<UserViewModel>>> FindByEmailAsync(string email)
        {
            var userInfo = await _userService.FindByEmailAsync(email, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<UserViewModel>.Success(new UserViewModel(userInfo)));
        }
        [HttpGet("by/name/{userName}")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = IDConstants.Roles.RootAdmin)]
        public async Task<ActionResult<AjaxResult<UserViewModel>>> FindByNameAsync(string userName)
        {
            var userInfo = await _userService.FindByNameAsync(userName, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<UserViewModel>.Success(new UserViewModel(userInfo)));
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = IDConstants.Roles.RootAdmin)]
        public async Task<ActionResult<AjaxResult<CreateUserResultViewModel>>> CreateAsync(CreateUserViewModel model)
        {
            var createdResult = await _userService.AddAsync(model.ToModel(), SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult<CreateUserResultViewModel>.Success(new CreateUserResultViewModel(createdResult)));
        }

        [HttpPut("edit")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = IDConstants.Roles.RootAdmin)]
        public async Task<ActionResult<AjaxResult>> UpdateAsync(EditUserViewModel model)
        {
            await _userService.UpdateAsync(model.ToModel(), SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }

        [HttpDelete("{userId}/remove")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Roles = IDConstants.Roles.RootAdmin)]
        public async Task<ActionResult<AjaxResult>> DeleteAsync(string userId)
        {
            await _userService.DeleteAsync(userId, SrvUser, HttpContext.RequestAborted);

            return Ok(AjaxResult.Success());
        }

        [HttpGet("email/confirm")]
        public async Task<ActionResult<AjaxResult>> ConfirmEmailAsync(string userId, string newEmail, string token)
        {
            await _userService.ConfirmEmailAsync(userId, newEmail, token, CancellationToken);

            return Ok(AjaxResult.Success());
        }
    }
}

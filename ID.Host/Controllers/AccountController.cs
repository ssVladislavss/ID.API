using ID.Core.Users;
using ID.Core.Users.Abstractions;
using ID.Host.Infrastracture;
using ID.Host.Infrastracture.Models.Account;
using IdentityServer4;
using IdentityServer4.Services;
using ISDS.ServiceExtender.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ID.Host.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController<RequestIniciator>
    {
        private readonly SignInManager<UserID> _signInManager;
        private readonly IDUserManager _manager;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IUserService _userService;

        public AccountController
            (SignInManager<UserID> signInManager,
             IDUserManager manager,
             IIdentityServerInteractionService interactionService,
             IUserService userService)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _interactionService = interactionService ?? throw new ArgumentNullException(nameof(interactionService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogoutAsync(string logoutId)
        {
            var logout = await _interactionService.GetLogoutContextAsync(logoutId);

            await HttpContext.SignOutAsync();

            return Redirect(logout.PostLogoutRedirectUri);
        }

        [HttpGet("confirmation/email")]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string newEmail, string token)
        {
            await _userService.ConfirmEmailAsync(userId, newEmail, token, CancellationToken);

            return Redirect("/Account/Email/Confirmed");
        }

        [HttpGet("confirmation/email/lock")]
        public async Task<IActionResult> LockByClickInEmailMessageAsync(string userId, string code)
        {
            await _userService.SetLockStatusByVerifyCodeAsync(userId, true, code, CancellationToken);

            return Redirect("/Account/Locked");
        }

        [HttpGet("confirmation/password/reset")]
        public async Task<ActionResult<AjaxResult>> ConfirmResetPasswordAsync(string userId, string? clientId, string token)
        {
            await _userService.ConfirmResetPasswordAsync(userId, token, clientId, CancellationToken);

            return Redirect("/Account/Password/Reset/Confirmed");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return StatusCode((int)HttpStatusCode.BadRequest, "Заполните обязательные поля");

            var user = await _manager.FindByNameAsync(model.UserName);

            if (user == null)
                return StatusCode((int)HttpStatusCode.BadRequest, "Неверный email адрес");

            if (user.LockoutEnabled)
                return StatusCode((int)HttpStatusCode.BadRequest, "Пользователь заблокирован");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!signInResult.Succeeded)
                return StatusCode((int)HttpStatusCode.BadRequest, "Неверный пароль");

            await HttpContext.SignInAsync(new IdentityServerUser(user.Id));

            return Ok(model.ReturnUrl);
        }

        [HttpPut("{email}/password/reset")]
        public async Task<ActionResult<AjaxResult>> ResetPasswordAsync(string email, string? clientId)
        {
            await _userService.ResetPasswordAsync(email, clientId, CancellationToken);

            return Ok(AjaxResult.Success());
        }
    }
}

﻿using ID.Core.Users;
using ID.Core.Users.Abstractions;
using ID.Host.Infrastracture;
using ID.Host.Infrastracture.Models.Account;
using ID.Host.Infrastracture.Models.Users;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using ISDS.ServiceExtender.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IVerificationService _verificationService;

        public AccountController
            (SignInManager<UserID> signInManager,
             IDUserManager manager,
             IIdentityServerInteractionService interactionService,
             IUserService userService,
             IVerificationService verificationService)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _interactionService = interactionService ?? throw new ArgumentNullException(nameof(interactionService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _verificationService = verificationService ?? throw new ArgumentNullException(nameof(verificationService));
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

        [HttpGet("confirmation/phone")]
        public async Task<IActionResult> ConfirmPhoneAsync(string userId, string newPhoneNumber, string token)
        {
            await _userService.ConfirmPhoneNumberAsync(userId, newPhoneNumber, token, CancellationToken);

            return Redirect("/Account/Phone/Confirmed");
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

        [HttpGet("{userId}/code/send/email")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AjaxResult>> SendVerifyCodeOnEmailAsync(string userId)
        {
            await _verificationService.SendCodeOnEmailAsync(userId, SrvUser, CancellationToken);

            return Ok(AjaxResult.Success());
        }

        [HttpGet("{userId}/code/send/sms")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AjaxResult>> SendVerifyCodeOnSmsAsync(string userId)
        {
            await _verificationService.SendCodeOnSmsAsync(userId, SrvUser, CancellationToken);

            return Ok(AjaxResult.Success());
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

        [HttpPost("code/verify")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AjaxResult>> VerifyCodeAsync(VerifyUserCodeViewModel model)
        {
            await _verificationService.VerifyCodeAsync(model.UserId, model.Code, SrvUser, CancellationToken);

            return Ok(AjaxResult.Success());
        }

        [HttpPut("{email}/password/reset")]
        public async Task<ActionResult<AjaxResult>> ResetPasswordAsync(string email, string? clientId)
        {
            await _userService.ResetPasswordAsync(email, clientId, CancellationToken);

            return Ok(AjaxResult.Success());
        }

        [HttpPut("change/password")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AjaxResult>> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            await _userService.ChangePasswordAsync(model.UserId, model.CurrentPassword, model.NewPassword, SrvUser, CancellationToken);

            return Ok(AjaxResult.Success());
        }

        [HttpPut("change/email")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AjaxResult>> ChangeEmailAsync(ChangeEmailViewModel model)
        {
            await _userService.SetEmailAsync(model.UserId, model.NewEmail, SrvUser, CancellationToken);

            return Ok(AjaxResult.Success());
        }

        [HttpPut("change/phone")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AjaxResult>> ChangePhoneAsync(ChangePhoneNumberViewModel model)
        {
            await _userService.SetPhoneNumberAsync(model.UserId, model.PhoneNumber, SrvUser, CancellationToken);

            return Ok(AjaxResult.Success());
        }
    }
}

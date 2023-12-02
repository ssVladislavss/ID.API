﻿using ID.Core;
using ID.Core.Users;
using ID.Core.Users.Abstractions;
using ID.Host.Infrastracture;
using ID.Host.Infrastracture.Models.Users;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ID.Host.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("")]
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

            var users = await _userService.GetAsync(userFilter, new Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult<IEnumerable<UserViewModel>>.Success(users.Select(x => new UserViewModel(x))));
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<AjaxResult<UserViewModel>>> FindByIdAsync(string userId)
        {
            var userInfo = await _userService.FindByIdAsync(userId, new Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult<UserViewModel>.Success(new UserViewModel(userInfo)));
        }
        [HttpGet("by/email/{email}")]
        public async Task<ActionResult<AjaxResult<UserViewModel>>> FindByEmailAsync(string email)
        {
            var userInfo = await _userService.FindByEmailAsync(email, new Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult<UserViewModel>.Success(new UserViewModel(userInfo)));
        }
        [HttpGet("by/name/{userName}")]
        public async Task<ActionResult<AjaxResult<UserViewModel>>> FindByNameAsync(string userName)
        {
            var userInfo = await _userService.FindByNameAsync(userName, new Iniciator(), HttpContext.RequestAborted);

            return Ok(AjaxResult<UserViewModel>.Success(new UserViewModel(userInfo)));
        }
    }
}
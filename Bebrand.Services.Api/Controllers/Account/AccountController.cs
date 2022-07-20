using AutoMapper.Configuration;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Bebrand.Infra.CrossCutting.Identity.Models;
using Bebrand.Infra.CrossCutting.Identity.Models.AccountViewModels;
using Bebrand.Services.Api.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.Model;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Services.Api.Controllers.Account
{
    [Authorize]
    public class AccountController : ApiController
    {
        #region Variables
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly Appsettings _config;
        private readonly IUser _user;
        #endregion

        #region Cons
        public AccountController(
           IOptions<Appsettings> config,
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           RoleManager<IdentityRole> roleManager,

           IMediatorHandler mediator, IUser user
            , IWebHostEnvironment env) : base(env)
        {
            _config = config.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            this._roleManager = roleManager;
            this._user = user;
        }
        #endregion


        [HttpPost]
        [AllowAnonymous]
        [Route("account-login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            if (result.Succeeded)
            {

                var userToVerify = await _userManager.FindByEmailAsync(model.Email);
                if (userToVerify.Status == Status.Deactivate)
                {
                    AddError("Username has been deleted");
                }
                var token = await GenerateToken(userToVerify);
                return CustomResponse(new { token });
            }

            AddError("Incorrect user or password");
            return CustomResponse();
        }

        [HttpPost]
        [Route("register")]
        //[Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState.Values)
                {
                    string Error = item.Errors.ToString();
                    AddError(Error);
                }
            }
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                var Roles = _roleManager.Roles.FirstOrDefault(x => x.Id == model.RoleId);

                if (Roles != null)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Name);
                }
                await _signInManager.SignInAsync(user, false);

                return CustomResponse(await GenerateToken(user));
            }
            foreach (var item in result.Errors)
            {
                AddError(item.Description);
            }

            return CustomResponse();
        }

        [HttpPost]
        [Route("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {

                AddError("Sorry, you can't reset your password");
                return CustomResponse();
            }
            else
            {
                return CustomResponse("Reset password has been finished");
            }
        }

        [HttpPost]
        [Route("change-password")]

        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            //if (!ModelState.IsValid) return CustomResponse(ModelState);
            //var UserId = _userManager.GetUserId(User);
            //ApplicationUser user = await _userManager.FindByIdAsync(UserId);
            //if (user == null)
            //{
            //    return NotFound();
            //}
            //user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            //var result = await _userManager.UpdateAsync(user);
            //if (!result.Succeeded)
            //{

            //    AddError("Sorry, you can't change your password");
            //    return CustomResponse();
            //}
            //else
            //{
            //    return CustomResponse("Password changed successfully");

            //}




            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            var user = await _userManager.FindByNameAsync(_user.Name);
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

            if (result.Succeeded)
            {
                return CustomResponse("Password changed successfully");
            }

            AddError("Sorry, you can't change your password");
            return CustomResponse();

        }


        #region Methods
        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.token));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var userRoles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim("ParentUserId",user.ParentUserId.ToString()),
                    new Claim(ClaimTypes.Role,userRoles.FirstOrDefault()),
                    new Claim("Role",userRoles.FirstOrDefault())
                };

            var token = new JwtSecurityToken(
              //issuer: _config.domain,
              //audience: _config.domain,
              claims: claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

    }
}

using Bebrand.Infra.CrossCutting.Identity.Models;
using Bebrand.Infra.CrossCutting.Identity.Models.RoleManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;

namespace Bebrand.Services.Api.Controllers
{

    public class RoleManagementController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> roleManager;
        public RoleManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment env) : base(env)
        {
            this._userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        [Route("RoleToPolicy-management")]
        public async Task<IActionResult> RoleToPolicy(PostRoleToPolicy Model)
        {

            //Added Roles     
            //var roleResult = await roleManager.FindByNameAsync("Administrator");
            //if (roleResult == null)
            //{
            //    roleResult = new IdentityRole("Administrator");
            //    await roleManager.CreateAsync(roleResult);
            //}

            //var roleClaimList = (await roleManager.GetClaimsAsync(roleResult)).Select(p => p.Type);
            //if (!roleClaimList.Contains("ManagerPermissions"))
            //{
            //    await roleManager.AddClaimAsync(roleResult, new Claim("ManagerPermissions", "true"));
            //}

            //var user = await _userManager.FindByEmailAsync("jignesh@gmail.com");

            //if (user == null)
            //{
            //    user = new ApplicationUser()
            //    {
            //        UserName = "jignesh@gmail.com",
            //        Email = "jignesh@gmail.com",
            //    };
            //    await _userManager.CreateAsync(user, "Test@123");
            //}
            //await _userManager.AddToRoleAsync(user, "Administrator");
            var Role = await roleManager.FindByIdAsync(Model.RoleId);
            try
            {
                var Claim = await roleManager.AddClaimAsync(Role, new Claim("ManagerPermissions", "true"));
                return CustomResponse(Claim.Succeeded);

            }
            catch (Exception ex)
            {
                AddError(ex.InnerException.Message);
                return CustomResponse();

            }

        }

    }
}

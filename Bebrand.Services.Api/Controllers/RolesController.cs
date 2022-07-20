using Bebrand.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bebrand.Services.Api.Controllers
{
    [Authorize]
    public class RolesController : ApiController
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        public RolesController(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userManager, IWebHostEnvironment env) : base(env)
        {
            roleManager = roleMgr;
            this.userManager = userManager;
        }


        [HttpGet("Role-management")]
        public IActionResult Get()
        {
            return CustomResponse(roleManager.Roles);
        }
        [HttpPost("Role-management/{name}")]
        public async Task<IActionResult> Post(string name)
        {
            IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
            {
                return CustomResponse(name);
            }
            AddError("Faild");
            return CustomResponse();
        }

        [HttpDelete("Role-management/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return CustomResponse("Deleted");
            }
            AddError("Role Id Not found");
            return CustomResponse();
        }

        [HttpPut("Role-management")]
        public async Task<IActionResult> Update([FromBody] RoleModification model)
        {

            if (ModelState.IsValid)
            {

                IdentityRole role = await roleManager.FindByIdAsync(model.RoleId);
                if (role != null)
                {
                    role.Name = model.RoleName;
                    IdentityResult result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                        return CustomResponse(model);

                }
                return CustomResponse("RoleId not found");
            }

            return CustomResponse("Faild");
        }


        [HttpGet("Role-management/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                return CustomResponse(role);
            }
            AddError("Id Not Found");
            return CustomResponse();
        }

    }
}

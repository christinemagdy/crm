using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.TeamLeaderView;
using Bebrand.Application.ViewModels.TeamMemberView;
using Bebrand.Domain.Enums;
using Bebrand.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bebrand.Services.Api.Controllers
{
    [Authorize]
    public class TeamMemberController : ApiController
    {
        private readonly ITeamMemberAppService _TeamMemberAppService;
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<TeamMemberController> _logger;
        private readonly ITeamLeaderAppService _teamLeaderApp;
        public TeamMemberController(ITeamMemberAppService TeamMemberAppService,
            UserManager<ApplicationUser> userManager
          , RoleManager<IdentityRole> roleManager, ILogger<TeamMemberController> logger, ITeamLeaderAppService teamLeaderApp, IWebHostEnvironment env) : base(env)
        {
            _TeamMemberAppService = TeamMemberAppService;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._logger = logger;
            _teamLeaderApp = teamLeaderApp;
        }

        [HttpGet("TeamMember-management")]
        [Authorize(Roles = "Teamleader,SuperAdmin")]
        public async Task<QueryResultResource<TeamMemberViewModel>> Get(Status? status)
        {
            return await _TeamMemberAppService.GetAll(status);
        }

        [HttpGet("AllTeamMember-management")]
        public async Task<QueryResultResource<TeamMemberViewModel>> GetAll(Status? status)
        {
            return await _TeamMemberAppService.GetAll(status);
        }

        [HttpGet("AllTeamLeader-management")]
        public async Task<QueryResultResource<TeamLeaderViewModel>> GetTeamLeader(Status? status)
        {
            return await _teamLeaderApp.GetAll(status);
        }

        [HttpGet("TeamMember-management/{id:guid}")]
        [Authorize(Roles = "Teamleader,SuperAdmin")]
        public async Task<TeamMemberViewModel> Get(Guid id)
        {
            return await _TeamMemberAppService.GetById(id);
        }

        [HttpPut("TeamMember-management")]
        [Authorize(Roles = "Teamleader,SuperAdmin")]
        public async Task<IActionResult> Put([FromBody] UpdateTeamMemberViewModel TeamMemberViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _TeamMemberAppService.Update(TeamMemberViewModel));
        }

        [HttpDelete("TeamMember-management")]
        [Authorize(Roles = "Teamleader,SuperAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {

            return CustomResponse(await _TeamMemberAppService.UserStatus(id, Status.Deactivate));
        }
        [HttpGet("TeamMember-management-Restore/{id:guid}")]
        [Authorize(Roles = "Teamleader,SuperAdmin")]
        public async Task<IActionResult> Restore(Guid id)
        {
            await _TeamMemberAppService.UserStatus(id, Status.Updated);
            return CustomResponse(id);
        }

        [HttpPost("TeamMember-management")]
        [Authorize(Roles = "Teamleader,SuperAdmin")]
        public async Task<IActionResult> Post([FromBody] CreateTeamMemberViewModel TeamMemberViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            return CustomResponse(await _TeamMemberAppService.Register(TeamMemberViewModel));
        }
    }
}

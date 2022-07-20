using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Application.ViewModels.TeamLeaderView;
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
    public class TeamLeaderController : ApiController
    {
        private readonly ITeamLeaderAppService _TeamLeaderAppService;
        private readonly ICustomerAppService _customerAppService;
        public TeamLeaderController(ITeamLeaderAppService TeamLeaderAppService, ICustomerAppService customerAppService, IWebHostEnvironment env) : base(env)
        {
            _TeamLeaderAppService = TeamLeaderAppService;
            _customerAppService = customerAppService;
        }

        [HttpGet("TeamLeader-management")]
        [Authorize(Roles = "Salesdirector,SuperAdmin")]
        public async Task<QueryResultResource<TeamLeaderViewModel>> Get(Status? status)
        {
            return await _TeamLeaderAppService.GetAll(status);
        }

        [HttpGet("AllSalesDirector-management")]
        public async Task<QueryResultResource<CustomerViewModel>> Get_SalesDirector(Status? status)
        {
            return await _customerAppService.GetAll(status);
        }

        [HttpGet("TeamLeader-management/{id:guid}")]
        [Authorize(Roles = "Salesdirector,SuperAdmin")]
        public async Task<TeamLeaderViewModel> Get(Guid id)
        {
            return await _TeamLeaderAppService.GetById(id);
        }


        [HttpPut("TeamLeader-management")]
        [Authorize(Roles = "Salesdirector,SuperAdmin")]
        public async Task<IActionResult> Put([FromBody] UpdateTeamLeaderViewModel TeamLeaderViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) :
                CustomResponse(await _TeamLeaderAppService.Update(TeamLeaderViewModel));
        }

        [HttpDelete("TeamLeader-management")]
        [Authorize(Roles = "Salesdirector,SuperAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _TeamLeaderAppService.UserStatus(id, Status.Deactivate));
        }

        [HttpGet("TeamLeader-management-Restore/{id:guid}")]
        [Authorize(Roles = "Salesdirector,SuperAdmin")]
        public async Task<IActionResult> Restore(Guid id)
        {
            //var c = _TeamLeaderAppService.Remove(id).Result;
            await _TeamLeaderAppService.UserStatus(id, Status.Updated);
            return CustomResponse(id);
        }

        [HttpPost("TeamLeader-management")]
        [Authorize(Roles = "Salesdirector,SuperAdmin")]
        public async Task<IActionResult> Post([FromBody] CreateTeamLeaderViewModel TeamLeaderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            return CustomResponse(await _TeamLeaderAppService.Register(TeamLeaderViewModel));
        }
    }
}

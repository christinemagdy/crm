using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.ClientView;
using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bebrand.Services.Api.Controllers
{
    [Authorize()]
    public class ClientController : ApiController
    {
        private readonly IClientAppService _ClientAppService;
        private readonly IUser _user;

        public ClientController(IClientAppService clientAppService, IUser user, IWebHostEnvironment env) : base(env)
        {
            _ClientAppService = clientAppService;
            _user = user;
        }

        [HttpGet("Client-management")]
        public QueryResultResource<ClientViewModel> Get(UserStatus? status, [FromQuery] OwnerParameters ownerParameters, string key)
        {
            return _ClientAppService.GetAll(status, ownerParameters, key);
        }

        [HttpGet("User-Client-management")]
        public QueryResultResource<ClientViewModel> Get_User(UserStatus? status, [FromQuery] OwnerParameters ownerParameters, string key)
        {
            return _ClientAppService.GetByUser(status, key, ownerParameters);
        }

        [HttpGet("Team-Client-management")]
        public async Task<QueryMultipleResult<IEnumerable<ClientViewModel>>> GetperTeam([FromQuery] OwnerParameters ownerParameters, string key)
        {
            return await _ClientAppService.GetForEachTeam(ownerParameters, key);
        }

        [HttpGet("Client-management/{id:guid}")]
        public async Task<ClientViewModel> Get(Guid id)
        {
            return await _ClientAppService.GetById(id);
        }

        [HttpGet("Client-management-Restore/{id:guid}")]
        public async Task<IActionResult> Restore(Guid id)
        {
            await _ClientAppService.UserStatus(id, UserStatus.Updated);
            return CustomResponse(id);
        }

        [HttpPut("Client-management")]
        public async Task<IActionResult> Put([FromBody] UpdateClientViewModel ClientViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _ClientAppService.Update(ClientViewModel));
        }

        [HttpDelete("Client-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _ClientAppService.UserStatus(id, UserStatus.Deactivate));
        }
        [HttpDelete("Client-management/hardDelete")]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            return CustomResponse(await _ClientAppService.Remove(id));
        }

        [HttpPost("Client-management")]
        public async Task<IActionResult> Post([FromBody] CreateClientViewModel ClientViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            var Data = await _ClientAppService.Register(ClientViewModel);
            return CustomResponse(Data);
        }
    }
}

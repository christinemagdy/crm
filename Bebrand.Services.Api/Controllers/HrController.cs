using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.HrView;
using Bebrand.Domain.Enums;
using Bebrand.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bebrand.Services.Api.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class HrController : ApiController
    {
        private readonly IHrAppService _hrAppService;
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HrController> _logger;
        private readonly IMapper _mapper;
        public HrController(IHrAppService hrAppService,
            UserManager<ApplicationUser> userManager
          , RoleManager<IdentityRole> roleManager, ILogger<HrController> logger, IMapper mapper, IWebHostEnvironment env) : base(env)
        {
            _hrAppService = hrAppService;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._logger = logger;
            _mapper = mapper;
        }


        [HttpGet("Hr-management")]
        public async Task<QueryResultResource<HrViewModel>> Get(Status? status)
        {
            return await _hrAppService.GetAll(status);
        }

        [HttpGet("Hr-management/{id:guid}")]
        public async Task<HrViewModel> Get(Guid id)
        {
            return await _hrAppService.GetById(id);
        }


        [HttpPost("Hr-management")]
        public async Task<IActionResult> Post([FromBody] CreateHrViewModel hrViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            var Register = await _hrAppService.Register(hrViewModel);
            if (!Register.IsValid)
            {
                foreach (var error in Register.Errors)
                {
                    AddError(error.ErrorMessage);
                }

            }
            else
            {
                return CustomResponse(hrViewModel);
            }



            return CustomResponse();
        }

        [HttpPut("Hr-management")]
        public async Task<IActionResult> Put([FromBody] UpdateHrViewModel hrViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _hrAppService.Update(hrViewModel));
        }

        [HttpDelete("Hr-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _hrAppService.Remove(id, Status.Deactivate));
        }

        [HttpGet("Hr-management-Restore/{id:guid}")]
        public async Task<IActionResult> Restore(Guid id)
        {

            await _hrAppService.UserStatus(id, Status.Updated);
            return CustomResponse(id);
        }

    }
}
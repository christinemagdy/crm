using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Domain.Enums;
using Bebrand.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Bebrand.Services.Api.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SalesdirectorController : ApiController
    {
        private readonly ICustomerAppService _customerAppService;
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SalesdirectorController> _logger;
        private readonly IMapper _mapper;
        public SalesdirectorController(ICustomerAppService customerAppService,
            UserManager<ApplicationUser> userManager
          , RoleManager<IdentityRole> roleManager, ILogger<SalesdirectorController> logger, IMapper mapper, IWebHostEnvironment env) : base(env)
        {
            _customerAppService = customerAppService;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._logger = logger;
            _mapper = mapper;
        }


        [HttpGet("Salesdirector-management")]
        public async Task<QueryResultResource<CustomerViewModel>> Get(Status? status)
        {
            return await _customerAppService.GetAll(status);
        }

        [HttpGet("Salesdirector-management/{id:guid}")]
        public async Task<CustomerViewModel> Get(Guid id)
        {
            return await _customerAppService.GetById(id);
        }


        [HttpPost("Salesdirector-management")]
        public async Task<IActionResult> Post([FromBody] CreateCustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            var Register = await _customerAppService.Register(customerViewModel);
            if (!Register.IsValid)
            {
                foreach (var error in Register.Errors)
                {
                    AddError(error.ErrorMessage);
                }
 
            }
            else
            {
                return CustomResponse(customerViewModel);
            }



            return CustomResponse();
        }

        [HttpPut("Salesdirector-management")]
        public async Task<IActionResult> Put([FromBody] UpdateCustomerViewModel customerViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _customerAppService.Update(customerViewModel));
        }

        [HttpDelete("Salesdirector-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _customerAppService.Remove(id, Status.Deactivate));
        }
       
        [HttpGet("Salesdirector-management-Restore/{id:guid}")]
        public async Task<IActionResult> Restore(Guid id)
        {

            await _customerAppService.UserStatus(id, Status.Updated);
            return CustomResponse(id);
        }

    }
}

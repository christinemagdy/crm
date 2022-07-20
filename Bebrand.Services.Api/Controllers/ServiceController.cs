using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.Services;
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
    [Authorize]
    public class ServiceController : ApiController
    {
        private readonly IServicesAppService _servicesAppService;


        public ServiceController(IServicesAppService servicesAppService, IWebHostEnvironment env) : base(env)
        {
            _servicesAppService = servicesAppService;
        }

        [HttpGet("Service-management")]
        public async Task<QueryMultipleResult<IEnumerable<ServiceViewModel>>> Get()
        {
            return await _servicesAppService.GetAll();
        }

        [HttpGet("Service-management/{id:guid}")]
        public async Task<QueryMultipleResult<ServiceViewModel>> Get(Guid id)
        {
            return await _servicesAppService.GetById(id);
        }

        [HttpPut("Service-management")]
        public async Task<IActionResult> Put([FromBody] UpdateServiceViewModel ServiceViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) :

            CustomResponse(await _servicesAppService.Update(ServiceViewModel));
        }

        [HttpDelete("Service-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _servicesAppService.Remove(id));
        }

        [HttpPost("Service-management")]
        public async Task<IActionResult> Post([FromBody] CreateServiceViewModel ServiceViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            var Register = await _servicesAppService.Register(ServiceViewModel);
            return CustomResponse(Register);
        }
    }
}

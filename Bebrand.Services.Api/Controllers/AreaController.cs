using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.AreaView;
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
    public class AreaController : ApiController
    {
        private readonly IAreaAppService _AreaAppService;
        public AreaController(IAreaAppService AreaAppService, IWebHostEnvironment env) : base(env)
        {
            _AreaAppService = AreaAppService;
        }
        [HttpGet("Area-management")]
        public async Task<QueryResultResource<AreaViewModel>> Get()
        {
            return await _AreaAppService.GetAll();
        }
      
        [HttpGet("Area-management/{id:guid}")]
        public async Task<AreaViewModel> Get(Guid id)
        {
            return await _AreaAppService.GetById(id);
        }

        [HttpPut("Area-management")]
        public async Task<IActionResult> Put([FromBody] UpdateAreaViewModel AreaViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) :

            CustomResponse(await _AreaAppService.Update(AreaViewModel));
        }

        [HttpDelete("Area-management")]
        public async Task<IActionResult> Delete(Guid id)
        {

            return CustomResponse(await _AreaAppService.Remove(id));
        }

        [HttpPost("Area-management")]
        public async Task<IActionResult> Post([FromBody] CreateAreaViewModel AreaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var Register = await _AreaAppService.Register(AreaViewModel);
            return CustomResponse(Register);
        }


    }
}

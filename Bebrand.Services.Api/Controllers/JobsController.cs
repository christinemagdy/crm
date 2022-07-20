using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels.JobsView;
using Bebrand.Domain.Core;
using Bebrand.Domain.Models;
using Bebrand.Services.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bebrand.Jobss.Api.Controllers
{
    [Authorize]
    public class JobsController : ApiController
    {
        private readonly IJobAppservice _JobAppservice;

        public JobsController(IWebHostEnvironment hostingEnvironment, IJobAppservice jobAppservice) : base(hostingEnvironment)
        {
            this._JobAppservice = jobAppservice;
        }

        [HttpGet("Jobs-management")]
        public async Task<QueryMultipleResult<IEnumerable<JobsViewModel>>> Get([FromQuery]OwnerParameters parameters , [FromQuery]UserStatus? Status)
        {
            return await _JobAppservice.GetAll(parameters,Status);
        }

        [HttpGet("Jobs-management/{id:guid}")]
        public async Task<QueryMultipleResult<JobsViewModel>> Get(Guid id)
        {
            return await _JobAppservice.GetById(id);
        }

        [HttpPut("Jobs-management")]
        public async Task<IActionResult> Put([FromBody] JobsViewModel JobsViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) :

            CustomResponse(await _JobAppservice.Update(JobsViewModel));
        }

        [HttpDelete("Jobs-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _JobAppservice.Remove(id, UserStatus.Deactivate));
        }

        [HttpGet("Jobs-management/restore{id:guid}")]
        public async Task<IActionResult> restore(Guid id)
        {
            return CustomResponse(await _JobAppservice.Restore(id));
        }

        [HttpPost("Jobs-management")]
        public async Task<IActionResult> Post([FromBody] CreateJobsViewModel JobsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            var Register = await _JobAppservice.Register(JobsViewModel);
            return CustomResponse(Register);
        }
    }
}

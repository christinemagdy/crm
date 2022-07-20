using Bebrand.Application.Interfaces;
using Bebrand.Infra.CrossCutting.Identity.Models;
using Bebrand.Services.Api.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bebrand.Services.Api.Controllers.Account
{
    [Authorize]
    public class HomeController : ApiController
    {
        #region Variables
        private readonly IHomeAppService _homeApp;
        #endregion
        public HomeController(IHomeAppService homeApp, IWebHostEnvironment env) : base(env)
        {
            _homeApp = homeApp;
        }

        [HttpGet]
        [Route("home-personal")]
        public async Task<IActionResult> personal()
        {
            return CustomResponse(await _homeApp.Home());
        }
    }
}

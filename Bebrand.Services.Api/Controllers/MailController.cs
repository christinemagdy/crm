//using Bebrand.Application.Interfaces;
//using Bebrand.Application.Services;
//using Bebrand.Application.ViewModels.VacanciesMail;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Bebrand.Services.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class MailController : ApiController
//    {
//        private readonly ImailAppService _imailAppService;
//        private readonly IVacanciesMailAppService _vacanciesMailAppService;
//        public MailController(ImailAppService imailAppService, IVacanciesMailAppService vacanciesMailAppService, IWebHostEnvironment env) : base(env)
//        {
//            _imailAppService = imailAppService;
//            _vacanciesMailAppService = vacanciesMailAppService;
//        }
//        [HttpGet]
//        public IActionResult GetAllEmails()
//        {
//             _imailAppService.GetAllMails();
           
//            return Ok();
//        }

//        [HttpPost("Mail-management")]
//        public async Task<IActionResult> Post([FromBody] CreateVacanciesMailViewModel mailViewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return CustomResponse(ModelState);
//            }
//            var Register = await _vacanciesMailAppService.Register(mailViewModel);
//            return CustomResponse(Register);
//        }

//    }
//}

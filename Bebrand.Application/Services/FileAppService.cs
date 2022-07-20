//using Bebrand.Application.Interfaces;
//using Bebrand.Application.ViewModels.VacanciesMail;
//using Bebrand.Domain.Models;
//using Microsoft.AspNetCore.Hosting;
//using MimeKit;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bebrand.Application.Services
//{
//    public class FileAppService : IFileAppService
//    {
//        private readonly Appsettings _config;
//        protected readonly IWebHostEnvironment _hostingEnvironment;

//        public FileAppService(Appsettings config, IWebHostEnvironment hostingEnvironment)
//        {
//            _config = config;
//            _hostingEnvironment = hostingEnvironment;
//        }

//        public void Dispose()
//        {
//            GC.SuppressFinalize(this);
//        }

//        public List<CreateVacanciesMailViewModel> Save(List<MimeEntity> attachements , List<Jobs> jobs)
//        {
//            var VacanciesList = new List<CreateVacanciesMailViewModel>();
//            foreach (var job in jobs)
//            {

//            }

//            foreach (var attachment in attachements)
//            {
                
//                var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
//                FileInfo fi = new FileInfo(fileName);
//                var ex = fi.Extension.ToLower();
//                var valid = _config.Extensions.Contains(ex);
//                if (valid)
//                {
//                    string webRootPath = _hostingEnvironment.ContentRootPath;
//                    var fullPath = Path.Combine(webRootPath, "Bebrand_Uploads");
//                    var file = Path.Combine(fullPath, fileName);
//                    using (var stream = File.Create(file))
//                    {
//                        if (attachment is MessagePart)
//                        {
//                            var rfc822 = (MessagePart)attachment;
//                            rfc822.Message.WriteTo(stream);
//                        }
//                        else
//                        {
//                            var part = (MimePart)attachment;
//                            part.Content.DecodeTo(stream);
//                        }
//                    }

//                    VacanciesList.Add(new CreateVacanciesMailViewModel()
//                    {
//                        Attachement = file,
//                        JobId = job.Id,
//                        Subject = message.Subject,
//                        TextBody = message.TextBody,
//                        UniqueIds = uniqueId.Id.ToString()
//                    });


//                }
//            }
//        }
//    }
//}

using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.ClientView;
using Bebrand.Domain.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bebrand.Services.Api.Controllers
{
    [Route("/[controller]")]
    public class FileController : ApiController
    {
        private readonly IFromFileAppService _fromFileAppService;
        private readonly IWebHostEnvironment _host;
        public FileController(IFromFileAppService fileApp, IWebHostEnvironment env) : base(env)
        {
            _fromFileAppService = fileApp;
            _host = env;
        }
        [HttpPost]
        [Authorize]
        public async Task<QueryResultResource<List<ClientViewModel>>> ImportFile(IFormFile importFile)
        {
            return await _fromFileAppService.GetDataFromCSVFile(importFile);
        }


        [HttpPost("FileUpload")]
        [DisableRequestSizeLimit]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> FileUpload()
        {
            try
            {
                string file_name = Path.GetRandomFileName();
                string root_path = _host.ContentRootPath;
                var upload_path = this.GetFilesUploadPath(true);
                var fileUploadForm = await FileUploadHelper.ParseRequestForm(this, async (section, formFileInfo) =>
                {
                    // TODO: This function will be called every time parser got a file
                    // but this controller only supports one file per request.
                    // Therefore the last file will be the one to be saved if the client
                    // passed up more than one file.

                    file_name += Path.GetExtension(formFileInfo.FileName);
                    var upload_file_path = Path.Combine(upload_path, file_name);
                    if (!CheckFileType(file_name))
                    {
                        throw new Exception("Invalid Extension");
                    }
                    using (var fileStream = System.IO.File.Create(upload_file_path))
                    {
                        await section.Body.CopyToAsync(fileStream);
                    }

                }, new FileUploadForm());
                return Content(file_name);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public void FileDelete()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var filename = reader.ReadToEnd();
                string root_path = _hostingEnvironment.ContentRootPath;
                var upload_path = Path.Combine(root_path, UPLOAD_FOLDER_TEMP);
                var upload_file_path = Path.Combine(upload_path, filename);

                if (System.IO.File.Exists(upload_file_path))
                {
                    System.IO.File.Delete(upload_file_path);
                }
            }
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    return Content("filename not present");

                var filePath = Path.Combine(this.GetFilesUploadPath(), fileName);

                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, this.GetContentType(filePath), Path.GetFileName(filePath));

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }

        #region Methods

        bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                case ".txt":
                    return true;

                case ".doc":
                    return true;
                case ".docx":
                    return true;
                case ".xls":
                    return true;
                case ".csv":
                    return true;
                case ".webp":
                    return true;

                case ".tif":
                    return true;
                case ".tiff":
                    return true;

                case ".pdf":
                    return true;
                case ".xlsx":
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }
    public class FileUploadForm
    {
        /// <summary>
        /// A unique identifier that identifies an upload. Used for chunking.
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// The total size of the file.
        /// </summary>
        public long totalFileSize { get; set; }
    }
}

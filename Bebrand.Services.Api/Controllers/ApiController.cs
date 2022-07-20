using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Bebrand.Services.Api.Controllers
{
    [ApiController]
    public abstract class ApiController : Controller
    {
        private readonly ICollection<string> _errors = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result,
                });
            }

            return Ok(new
            {
                success = false,
                Messages = _errors.ToArray(),
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool IsOperationValid()
        {
            return !_errors.Any();
        }

        protected void AddError(string erro)
        {
            _errors.Add(erro);
        }

        protected void ClearErrors()
        {
            _errors.Clear();
        }


        #region [vars]
        protected readonly IWebHostEnvironment _hostingEnvironment;
        #endregion

        #region [consts]
        protected const string UPLOAD_FOLDER = "Bebrand_Uploads";
        protected const string UPLOAD_FOLDER_TEMP = "temp";
        protected const int THUMB_WIDTH = 180;
        protected const int THUMB_HEIGHT = 180;
        protected const string THUMB_PREFIX = "thumb_";
        protected const int MED_WIDTH = 540;
        protected const int MED_HEIGHT = 320;
        protected const string MED_PREFIX = "medium_";
        protected const int LRG_WIDTH = 1200;
        protected const int LRG_HEIGHT = 900;
        protected const string LRG_PREFIX = "large_";
        #endregion

        #region [ctor]
        public ApiController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        protected void DeleteFile(string fileName, bool deleteVersions = false)
        {
            string webRootPath = _hostingEnvironment.ContentRootPath;
            string newPath = Path.Combine(webRootPath, UPLOAD_FOLDER);

            List<string> paths = new List<string>() { Path.Combine(newPath, fileName) };
            if (deleteVersions)
            {
                paths.Add(Path.Combine(newPath, $"{THUMB_PREFIX}{fileName}"));
                paths.Add(Path.Combine(newPath, $"{MED_PREFIX}{fileName}"));
                paths.Add(Path.Combine(newPath, $"{LRG_PREFIX}{fileName}"));
            }

            paths.ForEach(path =>
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            });
        }
        protected QueryMultipleResult<Boolean> DeleteTempFile(string fileName, bool deleteVersions = false)
        {
            var result = new QueryMultipleResult<Boolean>(true);

            string webRootPath = _hostingEnvironment.ContentRootPath;
            string newPath = Path.Combine(webRootPath, UPLOAD_FOLDER_TEMP);
            List<string> paths = new List<string>() { Path.Combine(newPath, fileName) };
            if (deleteVersions)
            {
                paths.Add(Path.Combine(newPath, $"{THUMB_PREFIX}{fileName}"));
                paths.Add(Path.Combine(newPath, $"{MED_PREFIX}{fileName}"));
                paths.Add(Path.Combine(newPath, $"{LRG_PREFIX}{fileName}"));
            }
            paths.ForEach(path =>
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            });

            return result;
        }
        protected string GetFilesUploadPath(bool isTemp = false)
        {
            string webRootPath = _hostingEnvironment.ContentRootPath;
            var uploadFolder = isTemp ? Path.Combine(UPLOAD_FOLDER, UPLOAD_FOLDER_TEMP) : UPLOAD_FOLDER;
            return Path.Combine(webRootPath, uploadFolder);
        }


        protected string GetwebsiteFilesUploadPath(string Folder, string SubFolder)
        {
            string webRootPath = _hostingEnvironment.ContentRootPath;
            var uploadFolder = Path.Combine(UPLOAD_FOLDER, path2: Folder, path3: SubFolder); // Files will save here
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            return Path.Combine(webRootPath, uploadFolder);
        }

        public static void SaveStreamAsFile(string filePath, Stream inputStream, string fileName)
        {
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }

            string path = Path.Combine(filePath, fileName);
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                inputStream.CopyTo(outputFileStream);
            }
        }


        protected void Resize(string sorcePath, string targetPath, string sourceImageName, string targetImagePrefix, int targetWidth, int targetHeight)
        {
            var sourceImagePath = Path.Combine(sorcePath, sourceImageName);
            using (var sourceImageObj = Image.Load(sourceImagePath))
            {
                sourceImageObj.Mutate(img => img.Resize(targetWidth, targetHeight));
                var targetImagePath = Path.Combine(targetPath, $"{targetImagePrefix}{sourceImageName}");
                sourceImageObj.Save(targetImagePath); // Automatic encoder selected based on extension.
            }
        }
        protected void ConfirmImageAdded(string image)
        {
            var uploadTempPath = GetFilesUploadPath(isTemp: true);
            var uploadPath = GetFilesUploadPath(isTemp: false);


            this.GenerateDiffSizes(image, uploadTempPath, uploadPath);
        }
        protected void GenerateDiffSizes(string image, string sourcePath, string targetPath)
        {
            string imagePath = Path.Combine(sourcePath, image);

            if (System.IO.File.Exists(imagePath))
            {
                //// [2.1] Generate Sizes                                              
                //this.Resize(sourcePath, targetPath, image, THUMB_PREFIX, THUMB_WIDTH, THUMB_HEIGHT);
                //this.Resize(sourcePath, targetPath, image, MED_PREFIX, MED_WIDTH, MED_HEIGHT);
                //this.Resize(sourcePath, targetPath, image, LRG_PREFIX, LRG_WIDTH, LRG_HEIGHT);
            }
        }
        protected void ConfirmFileAdded(string file)
        {
            var uploadTempPath = GetFilesUploadPath(isTemp: true);

            var uploadPath = GetFilesUploadPath(isTemp: false);

            string tempFilePath = Path.Combine(uploadTempPath, file);

            if (System.IO.File.Exists(tempFilePath))
            {
                var originalFinalPath = Path.Combine(uploadPath, file);
                System.IO.File.Move(tempFilePath, originalFinalPath);
            }
        }

        protected string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats/officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".webp", "image/webp"},
                {".tif", "image/tiff" },
                { "tiff", "image/tiff"}
            };
        }
    }
}

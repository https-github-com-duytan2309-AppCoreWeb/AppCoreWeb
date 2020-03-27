using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

//using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

//using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

//using Microsoft.Extensions.Configuration;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Services;

//using TeduCoreApp.Extensions;
using Microsoft.Extensions.FileProviders;
using TeduCoreApp.Models.FileViewModels;
using TeduCoreApp.Data.EF;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Syncfusion.MVC.EJ;

//using Syncfusion.JavaScript;

namespace TeduCoreApp.Controllers
{
    public class FilesController : Controller
    {
        private readonly IFileProvider fileProvider;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly AppDbContext _context;
        private readonly IFunctional _functionalService;
        private readonly UserManager<AppUser> _userManager;

        public FilesController(IFileProvider fileProvider, IHostingEnvironment hostingEnvironment, AppDbContext context, IFunctional functionalService, UserManager<AppUser> userManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _functionalService = functionalService;
            _userManager = userManager;
            this.fileProvider = fileProvider;
        }

        public IActionResult UploadFiles(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                filename = _hostingEnvironment.WebRootPath + $@"\uploaded\" + $@"\{ filename}
                ";
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            return Content("");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(5000000)]
        public async Task<IActionResult> PostUploadProfilePictures(List<IFormFile> files)
        {
            try
            {
                var IdCurent = User.FindFirst("UserId").Value.ToString();
                var appUser = await _context.AppUsers.Where(x => x.Id.ToString() == IdCurent).SingleOrDefaultAsync();
                var folderUpload = "uploaded";

                var fileName = await _functionalService.UploadFiles(files, _hostingEnvironment, folderUpload, IdCurent);
                if (appUser != null)
                {
                    appUser.Avatar = "/" + folderUpload + "/" + fileName;
                    _context.AppUsers.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                return Ok(fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        [RequestSizeLimit(5000000)]
        public async Task<IActionResult> PostUploadProfilePicture(IFormFile file)
        {
            try
            {
                var IdCurent = User.FindFirst("UserId").Value.ToString();
                var appUser = await _context.AppUsers.Where(x => x.Id.ToString() == IdCurent).SingleOrDefaultAsync();
                var folderUpload = "uploaded";
                var fileName = await _functionalService.UploadFile(file, _hostingEnvironment, folderUpload, IdCurent);
                var webRoot = _hostingEnvironment.ContentRootPath;
                if (appUser != null)
                {
                    appUser.Avatar = "/" + folderUpload + "/" + fileName;
                    _context.AppUsers.Update(appUser);
                    await _context.SaveChangesAsync();
                }

                return Ok(fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploaded", file.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("Files");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Content("files not selected");

            foreach (var file in files)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploaded", file.GetFilename());

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok("Files");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileViaModel(FileInputModel model)
        {
            if (model == null ||
                model.FileToUpload == null || model.FileToUpload.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploaded/Client",
                        model.FileToUpload.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.FileToUpload.CopyToAsync(stream);
            }

            return RedirectToAction("Files");
        }

        public IActionResult Files()
        {
            var model = new FilesViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents(""))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath });
            }
            return View(model);
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/files", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
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
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
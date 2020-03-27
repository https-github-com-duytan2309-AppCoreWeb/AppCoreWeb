//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Hosting;
//using System.Net.Http.Headers;
//using System.IO;
//using Microsoft.AspNetCore.Http;
//using TeduCoreApp.Data.EF;
//using TeduCoreApp.Services;
//using Microsoft.AspNetCore.Identity;
//using TeduCoreApp.Data.Entities;
//using TeduCoreApp.Application.ViewModels.System;
//using Microsoft.EntityFrameworkCore;

//// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace TeduCoreApp.Areas.Admin.Controllers
//{
//    public class UploadController : BaseController
//    {
//        private readonly IHostingEnvironment _hostingEnvironment;
//        private readonly AppDbContext _context;
//        private readonly IFunctional _functionalService;
//        private readonly UserManager<AppUser> _userManager;

// public UploadController(IHostingEnvironment hostingEnvironment, AppDbContext context, IFunctional
// functionalService, UserManager<AppUser> userManager) { _hostingEnvironment = hostingEnvironment;
// _context = context; _functionalService = functionalService; _userManager = userManager; }

// [HttpPost] public async Task UploadImageForCKEditor(IList<IFormFile> upload, string
// CKEditorFuncNum, string CKEditor, string langCode) { DateTime now = DateTime.Now; if
// (upload.Count == 0) { await HttpContext.Response.WriteAsync("Yêu cầu nhập ảnh"); } else { var
// file = upload[0]; var filename = ContentDispositionHeaderValue .Parse(file.ContentDisposition)
// .FileName .Trim('"');

// var imageFolder = $@"\uploaded\images\{now.ToString("yyyyMMdd")}";

// string folder = _hostingEnvironment.WebRootPath + imageFolder;

// if (!Directory.Exists(folder)) { Directory.CreateDirectory(folder); } string filePath =
// Path.Combine(folder, filename); using (FileStream fs = System.IO.File.Create(filePath)) {
// file.CopyTo(fs); fs.Flush(); } await HttpContext.Response.WriteAsync("
// <script>
// window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + Path.Combine(imageFolder,
// filename).Replace(@"\", @"/") + "');
// </script>
// "); } }

// ///
// <summary>
// /// Upload image for form ///
// </summary>
// ///
// <returns>
// ///
// </returns>
// [HttpPost] public IActionResult UploadImage() { DateTime now = DateTime.Now; var files =
// Request.Form.Files; if (files.Count == 0) { return new BadRequestObjectResult(files); } else {
// var file = files[0]; var filename = ContentDispositionHeaderValue .Parse(file.ContentDisposition)
// .FileName .Trim('"');

// var imageFolder = $@"\uploaded\images\{now.ToString("yyyyMMdd")}";

// string folder = _hostingEnvironment.WebRootPath + imageFolder;

// if (!Directory.Exists(folder)) { Directory.CreateDirectory(folder); } string filePath =
// Path.Combine(folder, filename); using (FileStream fs = System.IO.File.Create(filePath)) {
// file.CopyTo(fs); fs.Flush(); } return new OkObjectResult(Path.Combine(imageFolder,
// filename).Replace(@"\", @"/")); } }

// [HttpPost] [RequestSizeLimit(5000000)] public async Task<IActionResult>
// PostUploadProfilePicture(IFormFile UploadDefault) { try { var folderUpload = "uploaded"; var
// fileName = await _functionalService.UploadFile(UploadDefault, _hostingEnvironment, folderUpload);

//                var IdCurent = User.FindFirst("UserId").Value.ToString();
//                var appUser = await _context.AppUsers.Where(x => x.Id.ToString() == IdCurent).SingleOrDefaultAsync();
//                if (appUser != null)
//                {
//                    appUser.Avatar = "/" + folderUpload + "/" + fileName;
//                    _context.AppUsers.Update(appUser);
//                    await _context.SaveChangesAsync();
//                }
//                return Ok(fileName);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = ex.Message });
//            }
//        }
//    }
//}
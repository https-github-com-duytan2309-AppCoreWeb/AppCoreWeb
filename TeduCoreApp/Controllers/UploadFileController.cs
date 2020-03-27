using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Controllers
{
    public class UploadFileController : Controller
    {
        private IHostingEnvironment _env;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public UploadFileController(IHostingEnvironment env, AppDbContext context, UserManager<AppUser> userManager)
        {
            _env = env;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> UploadFiles(List<IFormFile> files, string filelocation)
        {
            var result = "";
            filelocation = "uploaded";
            var IdCurent = User.FindFirst("UserId").Value.ToString();
            var appUser = await _context.AppUsers.Where(x => x.Id.ToString() == IdCurent).SingleOrDefaultAsync();

            var webRoot = _env.WebRootPath;
            var uploads = Path.Combine(webRoot, filelocation);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    //Get .txt, .jpg , ....
                    var extension = Path.GetExtension(formFile.FileName);
                    //Create Name
                    var fileName = IdCurent + extension;

                    var filePath = Path.Combine(uploads, fileName);

                    if (appUser != null)
                    {
                        appUser.Avatar = filePath;
                        _context.AppUsers.Update(appUser);
                        await _context.SaveChangesAsync();
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;
                }
            }

            return result;
        }

        [HttpPost]
        public async Task<string> UploadFile(IFormFile files)
        {
            var result = "null";
            var filelocation = "uploaded";

            //var IdCurent = User.FindFirst("UserId").Value.ToString();
            //var appUser = await _context.AppUsers.Where(x => x.Id.ToString() == IdCurent).SingleOrDefaultAsync();

            var webRoot = _env.WebRootPath;
            var uploads = Path.Combine(webRoot, filelocation);

            //Get .txt, .jpg , ...
            var extension = Path.GetExtension(files.FileName);
            //Create Name
            var fileName = "images" + extension;
            var filePath = Path.Combine(uploads, fileName);
            //if (appUser != null)
            //{
            //    appUser.Avatar = filePath;
            //    _context.AppUsers.Update(appUser);
            //    await _context.SaveChangesAsync();
            //}

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await files.CopyToAsync(stream);
            }

            //result = fileName;

            return result;
        }
    }
}
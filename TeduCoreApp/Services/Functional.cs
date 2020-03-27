using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

namespace TeduCoreApp.Services
{
    public class Functional : IFunctional
    {
        public async Task<string> UploadFiles(List<IFormFile> files, IHostingEnvironment env, string uploadFolder, string user)
        {
            var result = "";
            var webRoot = env.WebRootPath;
            var uploads = System.IO.Path.Combine(webRoot, uploadFolder);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    //Get .txt, .jpg , ....
                    var extension = System.IO.Path.GetExtension(formFile.FileName);
                    //Create Name
                    var fileName = user + extension;
                    var filePath = System.IO.Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;
                }
            }

            return result;
        }

        public async Task<string> UploadFile(IFormFile files, IHostingEnvironment env, string uploadFolder, string user)
        {
            var webRoot = env.WebRootPath;
            var uploads = System.IO.Path.Combine(webRoot, uploadFolder);
            //Get .txt, .jpg , ....
            var extension = System.IO.Path.GetExtension(files.FileName);
            //Create Name
            var fileName = user + extension;
            var filePath = System.IO.Path.Combine(uploads, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await files.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
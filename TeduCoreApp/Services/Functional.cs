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
        public async Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env, string uploadFolder)
        {
            var result = "";

            var webRoot = env.WebRootPath;
            var uploads = System.IO.Path.Combine(webRoot, uploadFolder);
            var extension = "";
            var filePath = "";
            var fileName = "";

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    //Get .txt, .jpg , ....
                    extension = System.IO.Path.GetExtension(formFile.FileName);
                    //Create Name
                    fileName = Guid.NewGuid().ToString() + extension;
                    filePath = System.IO.Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;
                }
            }

            return result;
        }
    }
}
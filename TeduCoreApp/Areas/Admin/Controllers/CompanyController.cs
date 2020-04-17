using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class CompanyController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Introduction()
        {
            return View();
        }

        public IActionResult Recruitment()
        {
            return View();
        }

        public IActionResult Partner()
        {
            return View();
        }
    }
}
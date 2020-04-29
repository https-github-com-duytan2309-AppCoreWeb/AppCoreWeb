using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SystemController : Controller
    {
        [Route("quan-ly-he-thong.html")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UtilitiesController : Controller
    {
        [Route("tien-ich.html")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
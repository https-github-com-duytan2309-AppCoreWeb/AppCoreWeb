using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductBrandAndOriginController : Controller
    {
        [Route("/admin-quan-ly-product-xuat-xuat-nhan-hieu.html")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
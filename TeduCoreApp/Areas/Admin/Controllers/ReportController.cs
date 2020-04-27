using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        [Route("bao-cao.html")]
        public IActionResult Index()
        {
            return View();
        }

        //Kho chứa
        public IActionResult Warehouse()
        {
            return View();
        }

        //Hóa đơn
        public IActionResult Bills()
        {
            return View();
        }

        //Quảng Cáo
        public IActionResult Marketing()
        {
            return View();
        }

        //Doanh thu
        public IActionResult Revenues()
        {
            return View();
        }

        //Lợi nhuận
        public IActionResult Profit()
        {
            return View();
        }

        //Khách truy câp
        public IActionResult visitor()
        {
            return View();
        }
    }
}
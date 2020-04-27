using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Extensions;
using TeduCoreApp.Application.Dapper.Interfaces;
using TeduCoreApp.Admin.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    //[ServiceFilter(typeof(FilterActionAttribute))]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IReportService reportService, IAuthorizationService authorizationService)
        {
            _reportService = reportService;
            _authorizationService = authorizationService;
        }

        [Authorize(Roles = "Admin,Customer,Staff")]
        [Route("admin-trang-chu.html")]
        public IActionResult Index()
        {
            var email = User.GetSpecificClaim("Email");

            return View();
        }

        //public async Task<IActionResult> GetRevenue(string fromDate, string toDate)
        //{
        //    return new OkObjectResult(await _reportService.GetReportAsync(fromDate, toDate));
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Extensions;
using TeduCoreApp.Application.Dapper.Interfaces;
using TeduCoreApp.Admin.Filter;
using Microsoft.AspNetCore.Authorization;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    //[ServiceFilter(typeof(FilterActionAttribute))]
    [Area("Admin")]
    [Authorize(Roles = "Admin, Staff, Customer")]
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IReportService reportService, IAuthorizationService authorizationService)
        {
            _reportService = reportService;
            _authorizationService = authorizationService;
        }

        public IActionResult Index()
        {
            var email = User.GetSpecificClaim("Email");

            return View();
        }

        public async Task<IActionResult> GetRevenue(string fromDate, string toDate)
        {
            return new OkObjectResult(await _reportService.GetReportAsync(fromDate, toDate));
        }
    }
}
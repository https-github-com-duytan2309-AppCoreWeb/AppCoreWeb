using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using TeduCoreApp.Extensions;
using TeduCoreApp.Application.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace TeduCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        private IDichVuCategoryService _dichVuCategoryService;
        private IBlogService _blogService;
        private IDichVuService _dichVuService;
        private ICommonService _commonService;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IProductService productService,
        IBlogService blogService, ICommonService commonService,
        IDichVuService dichVuService, IDichVuCategoryService dichVuCategoryService,
       IProductCategoryService productCategoryService, IStringLocalizer<HomeController> localizer)
        {
            _blogService = blogService;
            _dichVuService = dichVuService;
            _commonService = commonService;
            _productService = productService;
            _productCategoryService = productCategoryService;
            _dichVuCategoryService = dichVuCategoryService;
            _localizer = localizer;
        }

        public IActionResult Test()
        {
            ViewBag.Test = Guid.NewGuid();
            return View();
        }

        //[ResponseCache(CacheProfileName = "Default")]
        public IActionResult Index()
        {
            var title = _localizer["Title"];
            var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            ViewData["BodyClass"] = "cms-index-index cms-home-page";
            var homeVm = new HomeViewModel();
            homeVm.HomeCategories = _productCategoryService.GetHomeCategories(10);
            homeVm.HomeDichVuCategories = _dichVuCategoryService.GetHomeCategories(4);
            homeVm.HotProducts = _productService.GetHotProduct(12);
            homeVm.TopSellProducts = _productService.GetLastest(12);
            homeVm.LastestBlogs = _blogService.GetLastest(10);
            homeVm.LastestDichVu = _dichVuService.GetLastest(10);
            homeVm.HomeSlides = _commonService.GetSlides("top");
            return View(homeVm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
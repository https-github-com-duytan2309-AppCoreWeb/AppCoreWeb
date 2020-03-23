using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Models.DichVuViewModels;

namespace TeduCoreApp.Controllers
{
    public class DichVuController : Controller
    {
        private IDichVuService _dichvuService;
        private IBillService _billService;
        private IDichVuCategoryService _dichvuCategoryService;
        private IConfiguration _configuration;

        public DichVuController(IDichVuService dichvuService, IConfiguration configuration,
            IBillService billService,
            IDichVuCategoryService dichvuCategoryService)
        {
            _dichvuService = dichvuService;
            _dichvuCategoryService = dichvuCategoryService;
            _configuration = configuration;
            _billService = billService;
        }

        [Route("dich-vu.html")]
        public IActionResult Index()
        {
            var categories = _dichvuCategoryService.GetAll();
            return View(categories);
        }

        [Route("/danh-muc-dich-vu/{alias}-cd.{id}.html")]
        public IActionResult DichVuCatalog(int id, int? pageSize, string sortBy, int page = 1)
        {
            var catalog = new DichVuCatalogViewModel();
            ViewData["BodyClass"] = "";
            //if (pageSize == null)
            //    pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            catalog.Data = _dichvuService.GetAllPaging(id, string.Empty, page, pageSize.Value);
            catalog.DichVuCategory = _dichvuCategoryService.GetById(id);

            return View(catalog);
        }

        [Route("tim-kiem-dich-vu.html")]
        public IActionResult Search(string keyword, int? pageSize, string sortBy, int page = 1)
        {
            var catalog = new SearchResultViewModel();
            ViewData["BodyClass"] = "";
            //if (pageSize == null)
            //    pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            catalog.Data = _dichvuService.GetAllPaging(null, keyword, page, pageSize.Value);
            catalog.Keyword = keyword;

            return View(catalog);
        }

        [Route("/dich-vu/{alias}-dv.{id}.html", Name = "DichVuDetail")]
        public IActionResult Details(int id)
        {
            ViewData["BodyClass"] = "dichvu-page";
            var model = new DetailDichVuViewModel();
            model.DichVu = _dichvuService.GetById(id);
            model.DichVuCategory = _dichvuCategoryService.GetById(model.DichVu.CategoryId);
            model.RelatedDichVus = _dichvuService.GetRelatedDichVus(id, 9);
            model.Tags = _dichvuService.GetListTagById(id);
            return View(model);
        }
    }
}
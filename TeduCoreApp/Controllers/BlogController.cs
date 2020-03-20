using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Models.BlogViewModels;


namespace TeduCoreApp.Controllers
{
    public class BlogController : Controller
    {
        IBlogService _blogService;
        IBillService _billService;
        IBlogCategoryService _blogCategoryService;
        IConfiguration _configuration;
        public BlogController(IBlogService blogService, IConfiguration configuration,
            IBillService billService,
            IBlogCategoryService blogCategoryService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
            _configuration = configuration;
            _billService = billService;
        }
        [Route("tin-tuc.html")]
        public IActionResult Index()
        {
            var categories = _blogCategoryService.GetAll();
            return View(categories);
        }

        [Route("/danh-muc-tin-tuc/{alias}-cb.{id}.html")]
        public IActionResult BlogCatalog(int id, int? pageSize, string sortBy, int page = 1)
        {
            var catalog = new BlogCatalogViewModel();
            ViewData["BodyClass"] = "";
            //if (pageSize == null)
            //    pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            catalog.Data = _blogService.GetAllPaging(id, string.Empty, page, pageSize.Value);
            catalog.BlogCategory = _blogCategoryService.GetById(id);

            return View(catalog);
        }


        [Route("tim-kiem-tin-tuc.html")]
        public IActionResult Search(string keyword, int? pageSize, string sortBy, int page = 1)
        {
            var catalog = new SearchResultViewModel();
            ViewData["BodyClass"] = "";
            //if (pageSize == null)
            //    pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            catalog.Data = _blogService.GetAllPaging(null, keyword, page, pageSize.Value);
            catalog.Keyword = keyword;

            return View(catalog);
        }

        [Route("/tin-tuc/{alias}-bl.{id}.html", Name = "BlogDetail")]
        public IActionResult Details(int id)
        {
            ViewData["BodyClass"] = "blog-page";
            var model = new DetailBlogViewModel();
            model.Blog = _blogService.GetById(id);
            model.BlogCategory = _blogCategoryService.GetById(model.Blog.CategoryId);
            model.GetReatedBlogs = _blogService.GetReatedBlogs(id, 9);
            model.Tags = _blogService.GetListTagById(id);

            return View(model);
        }

    }
}
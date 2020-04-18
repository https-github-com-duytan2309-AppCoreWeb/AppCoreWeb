using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Models.ProductViewModels;
using TeduCoreApp.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeduCoreApp.Data.EF;

namespace TeduCoreApp.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _context;
        private IProductService _productService;
        private IBillService _billService;
        private IProductCategoryService _productCategoryService;
        private IProductTrademarkService _productTrademarkService;
        private IConfiguration _configuration;

        public ProductController(IProductService productService, IConfiguration configuration,
            IBillService billService,
            IProductCategoryService productCategoryService,
            IProductTrademarkService productTrademarkService,
            AppDbContext context
            )
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _productTrademarkService = productTrademarkService;
            _configuration = configuration;
            _billService = billService;
            _context = context;
        }

        public IActionResult GetAll()
        {
            var model = _productCategoryService.GetAll();
            return new OkObjectResult(model);
        }

        public IActionResult GetAllFilter(int filter)
        {
            if (string.IsNullOrEmpty(filter.ToString()) || filter.ToString() == "0")
            {
                var model = _productCategoryService.GetAll();
                return new OkObjectResult(model);
            }
            else
            {
                var model = _productCategoryService.GetAllFiter(filter);
                return new OkObjectResult(model);
            }
        }

        [Route("san-pham.html")]
        public IActionResult Index()
        {
            var categories = _productCategoryService.GetAll();
            return View(categories);
        }

        [Route("/danh-muc/{alias}-c.{id}.html")]
        public IActionResult Catalog(int id, int? pageSize, string sortBy, int page = 1)
        {
            var catalog = new CatalogViewModel();
            ViewData["BodyClass"] = "";
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            catalog.Data = _productService.GetAllPaging(id, string.Empty, page, pageSize.Value);
            catalog.Category = _productCategoryService.GetById(id);
            //catalog.RelatedProductCate = _productCategoryService.GetRelatedProductCategory(id);
            return View(catalog);
        }

        [Route("{alias}-t.{id}.html")]
        public IActionResult Trademark(int id, int? pageSize, string sortBy, int page = 1)
        {
            var trademark = new TrademarkViewModel();
            ViewData["BodyClass"] = "";
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");

            trademark.PageSize = pageSize;
            trademark.SortType = sortBy;
            trademark.Data = _productService.GetAllPaging(id, string.Empty, page, pageSize.Value);
            trademark.Trademark = _productTrademarkService.GetById(id);

            return View(trademark);
        }

        [Route("tim-kiem.html")]
        public IActionResult Search(string keyword, int? pageSize, string sortBy, int page = 1)
        {
            var catalog = new SearchResultViewModel();
            ViewData["BodyClass"] = "";
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            catalog.Data = _productService.GetAllPaging(null, keyword, page, pageSize.Value);
            catalog.Keyword = keyword;

            return View(catalog);
        }

        [Route("/san-pham/{alias}-p.{id}.html", Name = "ProductDetail")]
        public IActionResult Details(int id)
        {
            ViewData["BodyClass"] = "product-page";
            var model = new DetailViewModel();
            model.Product = _productService.GetById(id);
            model.Category = _productCategoryService.GetById(model.Product.CategoryId);
            model.Trademark = _productTrademarkService.GetById(model.Product.Id);
            //model.RelatedProducts = _productService.GetRelatedProducts(id, 9);
            //model.UpsellProducts = _productService.GetUpsellProducts(6);
            model.ProductImages = _productService.GetImages(id);
            //model.Tags = _productService.GetProductTags(id);
            model.Colors = _billService.GetColors().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            model.Sizes = _billService.GetSizes().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductSizeController : Controller
    {
        private readonly ISizeService _sizeService;
        private readonly IAuthorizationService _authorizationService;

        public ProductSizeController(ISizeService sizeService,
            IAuthorizationService authorizationService
            )
        {
            _sizeService = sizeService;
            _authorizationService = authorizationService;
        }

        [Route("/admin-quan-ly-product-sizes.html")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _sizeService.GetById(id);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            _sizeService.Delete(id);
            _sizeService.Save();
            return new OkObjectResult("Success");
        }

        // GET: Admin/ShipCodes
        public IActionResult GetAllPaging(int page, int pageSize)
        {
            var model = _sizeService.GetAllPaging(page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(SizeViewModel sizeVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (sizeVm.Id == 0)
            {
                _sizeService.Create(sizeVm);
            }
            else
            {
                _sizeService.Update(sizeVm);
            }
            _sizeService.Save();
            return new OkObjectResult(sizeVm);
        }
    }
}
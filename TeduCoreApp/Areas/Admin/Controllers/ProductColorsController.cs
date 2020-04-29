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
    public class ProductColorsController : Controller
    {
        private readonly IColorService _colorService;
        private readonly IAuthorizationService _authorizationService;

        public ProductColorsController(IColorService colorService,
            IAuthorizationService authorizationService
            )
        {
            _colorService = colorService;
            _authorizationService = authorizationService;
        }

        [Route("/admin-quan-ly-product-colors.html")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _colorService.GetById(id);
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
            _colorService.Delete(id);
            _colorService.Save();
            return new OkObjectResult("Success");
        }

        // GET: Admin/ShipCodes
        public IActionResult GetAllPaging(int page, int pageSize)
        {
            var model = _colorService.GetAllPaging(page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(ColorViewModel colorVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (colorVm.Id == 0)
            {
                _colorService.Create(colorVm);
            }
            else
            {
                _colorService.Update(colorVm);
            }
            _colorService.Save();
            return new OkObjectResult(colorVm);
        }
    }
}
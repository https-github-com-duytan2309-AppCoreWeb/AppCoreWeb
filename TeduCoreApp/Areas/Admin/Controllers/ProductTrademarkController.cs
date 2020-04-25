using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using TeduCoreApp.Authorization;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ProductTrademarkController : BaseController
    {
        private IProductTrademarkService _productTrademarkService;
        private readonly IAuthorizationService _authorizationService;

        public ProductTrademarkController(IProductTrademarkService productTrademarkService, IAuthorizationService authorizationService)
        {
            _productTrademarkService = productTrademarkService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "PRODUCT_TRADEMARK", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Notify/AccessDenied");
            return View();
        }

        #region Get Data API

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _productTrademarkService.GetById(id);

            return new ObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductTrademarkViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                productVm.SeoAlias = TextHelper.ToUnsignString(productVm.Name);
                if (productVm.Id == 0)
                {
                    _productTrademarkService.Add(productVm);
                }
                else
                {
                    _productTrademarkService.Update(productVm);
                }
                _productTrademarkService.Save();
                return new OkObjectResult(productVm);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new BadRequestResult();
            }
            else
            {
                _productTrademarkService.Delete(id);
                _productTrademarkService.Save();
                return new OkObjectResult(id);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _productTrademarkService.GetAll();
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _productTrademarkService.Save();
                    return new OkResult();
                }
            }
        }

        [HttpPost]
        public IActionResult ReOrder(int sourceId, int targetId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _productTrademarkService.ReOrder(sourceId, targetId);
                    _productTrademarkService.Save();
                    return new OkResult();
                }
            }
        }

        #endregion Get Data API
    }
}
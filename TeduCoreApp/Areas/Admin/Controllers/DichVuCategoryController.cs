using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Utilities.Helpers;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class DichVuCategoryController : BaseController
    {
        private IProductCategoryService _dichvuCategoryService;

        public DichVuCategoryController(IProductCategoryService dichvuCategoryService)
        {
            _dichvuCategoryService = dichvuCategoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Get Data API

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _dichvuCategoryService.GetById(id);

            return new ObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductCategoryViewModel dichvuVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                dichvuVm.SeoAlias = TextHelper.ToUnsignString(dichvuVm.Name);
                if (dichvuVm.Id == 0)
                {
                    _dichvuCategoryService.Add(dichvuVm);
                }
                else
                {
                    _dichvuCategoryService.Update(dichvuVm);
                }
                _dichvuCategoryService.Save();
                return new OkObjectResult(dichvuVm);
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
                _dichvuCategoryService.Delete(id);
                _dichvuCategoryService.Save();
                return new OkObjectResult(id);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _dichvuCategoryService.GetAll();
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
                    _dichvuCategoryService.UpdateParentId(sourceId, targetId, items);
                    _dichvuCategoryService.Save();
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
                    _dichvuCategoryService.ReOrder(sourceId, targetId);
                    _dichvuCategoryService.Save();
                    return new OkResult();
                }
            }
        }

        #endregion Get Data API
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Utilities.Helpers;
using TeduCoreApp.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class BlogCategoryController : BaseController
    {
        private IProductCategoryService _blogCategoryService;
        private readonly IAuthorizationService _authorizationService;

        public BlogCategoryController(IProductCategoryService blogCategoryService, IAuthorizationService authorizationService)
        {
            _blogCategoryService = blogCategoryService;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "BLOG", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Notify/AccessDenied");
            return View();
        }

        #region Get Data API

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _blogCategoryService.GetById(id);

            return new ObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductCategoryViewModel blogVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                blogVm.SeoAlias = TextHelper.ToUnsignString(blogVm.Name);
                if (blogVm.Id == 0)
                {
                    _blogCategoryService.Add(blogVm);
                }
                else
                {
                    _blogCategoryService.Update(blogVm);
                }
                _blogCategoryService.Save();
                return new OkObjectResult(blogVm);
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
                _blogCategoryService.Delete(id);
                _blogCategoryService.Save();
                return new OkObjectResult(id);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _blogCategoryService.GetAll();
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
                    _blogCategoryService.UpdateParentId(sourceId, targetId, items);
                    _blogCategoryService.Save();
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
                    _blogCategoryService.ReOrder(sourceId, targetId);
                    _blogCategoryService.Save();
                    return new OkResult();
                }
            }
        }

        #endregion Get Data API
    }
}
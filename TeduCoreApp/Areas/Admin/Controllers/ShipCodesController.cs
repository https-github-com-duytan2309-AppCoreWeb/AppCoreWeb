using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Authorization;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Extensions;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ShipCodesController : BaseController
    {
        private readonly IShipCodeService _shipcodeService;
        private readonly IAuthorizationService _authorizationService;

        public ShipCodesController(IShipCodeService shipcodeService,
            IAuthorizationService authorizationService
            )
        {
            _shipcodeService = shipcodeService;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index()
        {
            //var email = User.GetSpecificClaim("Email");
            var result = await _authorizationService.AuthorizeAsync(User, "SHIPER", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Login/Index");
            return View();
        }

        [HttpGet]
        public IActionResult GetById(long id)
        {
            var model = _shipcodeService.GetById(id);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            _shipcodeService.Delete(id);
            _shipcodeService.Save();
            return new OkObjectResult("Success");
        }

        // GET: Admin/ShipCodes
        public IActionResult GetAllPaging(int page, int pageSize)
        {
            var model = _shipcodeService.GetAllPaging(page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(ShipCodeViewModel shipVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (shipVm.Id == 0)
            {
                _shipcodeService.Create(shipVm);
            }
            else
            {
                _shipcodeService.Update(shipVm);
            }
            _shipcodeService.Save();
            return new OkObjectResult(shipVm);
        }
    }
}
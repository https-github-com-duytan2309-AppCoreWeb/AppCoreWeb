using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using TeduCoreApp.Authorization;
using Microsoft.AspNetCore.Identity;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;

        public UserController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IUserService userService
            ,
            IAuthorizationService authorizationService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> UserProfile()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Notify/AccessDenied");

            return View("Index");
        }

        #region Ajax User

        public IActionResult GetAll()
        {
            var model = _userService.GetAllAsync();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var model = await _userService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _userService.GetAllPagingAsync(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(AppUserViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (userVm.Id == null)
                {
                    await _userService.AddAsync(userVm);
                }
                else
                {
                    await _userService.UpdateAsync(userVm);
                }
                return new OkObjectResult(userVm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                await _userService.DeleteAsync(id);

                return new OkObjectResult(id);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CheckPassOld(string id, string pass)
        {
            var model = await _userService.GetById(id);

            var result = await _signInManager.PasswordSignInAsync(model.Email, pass, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return new OkObjectResult(true);
            }
            return new OkObjectResult(false);
        }

        #endregion Ajax User
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Authorization;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : BaseController
    {
        private readonly IContactService _contactService;
        //private readonly IFeedbackService _feedbackService;
        //private readonly IRecruitmentService _recruitmentService;

        private readonly IAuthorizationService _authorizationService;

        public CompanyController(IContactService contactService, IAuthorizationService authorizationService)
        {
            _contactService = contactService;
            _authorizationService = authorizationService;
        }

        [Route("quan-ly-dich-vu-cong-ty.html")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("admin-lien-he.html")]
        public async Task<IActionResult> Contact()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "CONTACT", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/notify-denied.html");
            return View(_contactService.GetById("default"));
        }

        public async Task<IActionResult> SaveEntityContact(ContactViewModel ctVM)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }

            if (ctVM.Id.ToString() != "Default")
            {
                _contactService.Add(ctVM);
            }
            else
            {
                _contactService.Update(ctVM);
            }
            _contactService.SaveChanges();

            return new OkObjectResult(ctVM);
        }

        [Route("gioi-thieu.html")]
        public async Task<IActionResult> Introduction()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "ABOUT", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Notify/AccessDenied");
            return View();
        }

        [Route("tuyen-dung.html")]
        public async Task<IActionResult> Recruitment()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "RECRUITMENT", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Notify/AccessDenied");
            return View();
        }

        [Route("doi-tac.html")]
        public async Task<IActionResult> Partner()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "CLIENT", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Notify/AccessDenied");
            return View();
        }

        [Route("nhat-ky.html")]
        public async Task<IActionResult> Diary()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "DIARY", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Notify/AccessDenied");
            return View();
        }

        [Route("phan-hoi.html")]
        public async Task<IActionResult> FeedBack()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "FEEDBACK", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Notify/AccessDenied");
            return View();
        }

        [Route("hoi-dap.html")]
        public async Task<IActionResult> FAQ()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "FAQ", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Notify/AccessDenied");
            return View();
        }
    }
}
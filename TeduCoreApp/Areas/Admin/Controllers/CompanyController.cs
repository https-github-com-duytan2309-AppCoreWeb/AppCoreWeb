using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly IFeedbackService _feedbackService;
        private readonly IRecruitmentService _recruitmentService;

        private readonly IAuthorizationService _authorizationService;

        public CompanyController(IContactService contactService, IAuthorizationService authorizationService)
        {
            _contactService = contactService;
            _authorizationService = authorizationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View(_contactService.GetById("default"));
        }

        public IActionResult Introduction()
        {
            return View();
        }

        public IActionResult Recruitment()
        {
            return View();
        }

        public IActionResult Partner()
        {
            return View();
        }

        public IActionResult Diary()
        {
            return View();
        }

        public IActionResult FeedBack()
        {
            return View();
        }

        public IActionResult Helper()
        {
            return View();
        }
    }
}
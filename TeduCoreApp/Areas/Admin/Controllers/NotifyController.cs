using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotifyController : Controller
    {
        private readonly ILogger<NotifyController> logger;

        public NotifyController(ILogger<NotifyController> logger)
        {
            this.logger = logger;
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            logger.LogError($"The path {exceptionHandlerPathFeature.Path} " +
                $"threw an exception {exceptionHandlerPathFeature.Error}");

            return View("Error");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/notify-denied.html")]
        public IActionResult AccessDenied()
        {
            ViewData["Title"] = "Access Denied";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult NotUserInAction()
        {
            ViewData["Title"] = "User was  Denied";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult NotPermission()
        {
            ViewData["Title"] = "Not Permission";
            return View();
        }
    }
}
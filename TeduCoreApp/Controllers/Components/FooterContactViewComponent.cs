using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Models;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Controllers.Components
{
    public class FooterContactViewComponent : ViewComponent
    {

        private IContactService _contactService;

        public FooterContactViewComponent(IContactService contactService)
        {
            _contactService = contactService;
        }
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            var contact = _contactService.GetById(CommonConstants.DefaultContactId);
            var model = new ContactPageViewModel { Contact = contact };
            //return View(model);
            return Task.FromResult((IViewComponentResult)View(model));
        }


    }
}

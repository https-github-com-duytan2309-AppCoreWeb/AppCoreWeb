using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Controllers.Components
{
    public class FooterMenuViewComponent : ViewComponent
    {

        private IPageService _pageService;

        public FooterMenuViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_pageService.GetList("vethanhvuong"));
        }
    }
}

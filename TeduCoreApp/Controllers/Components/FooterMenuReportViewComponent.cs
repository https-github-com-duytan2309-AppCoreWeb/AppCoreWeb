using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;

namespace TeduCoreApp.Controllers.Components
{
    public class FooterMenuReportViewComponent : ViewComponent
    {

        private IPageService _pageService;

        public FooterMenuReportViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_pageService.GetList("hotrokhachhang"));
        }
    }
}

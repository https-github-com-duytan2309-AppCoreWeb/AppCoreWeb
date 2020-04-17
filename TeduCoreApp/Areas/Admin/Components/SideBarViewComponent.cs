using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.System;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Extensions;
using TeduCoreApp.Utilities.Constants;

namespace TeduCoreApp.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private IFunctionService _functionService;
        private RoleManager<AppRole> _roleManager;

        public SideBarViewComponent(IFunctionService functionService, RoleManager<AppRole> roleManager)
        {
            _functionService = functionService;
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsPrincipal)User).GetSpecificClaim("Roles");
            List<FunctionViewModel> functions = new List<FunctionViewModel>();
            if (roles.Split(";").Contains(CommonConstants.AppRole.AdminRole))
            {
                functions = await _functionService.GetAll(string.Empty);
            }
            else
            {
                string[] ArrayRoles = roles.Split(";");
                var ListAllRoles = _roleManager.Roles.ToList();
                foreach (var item in ArrayRoles)
                {
                    //TODO: Get by permission
                    var GetIdRole = ListAllRoles.Where(x => x.Name == item).Select(x => x.Id).FirstOrDefault();
                    var listFunction = await _functionService.GetAllAllowPermission(GetIdRole.ToString());
                    functions.AddRange(listFunction);
                }
            }
            return View(functions);
        }
    }
}
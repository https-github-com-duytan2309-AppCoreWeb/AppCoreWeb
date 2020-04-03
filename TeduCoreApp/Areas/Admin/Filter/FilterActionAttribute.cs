using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Identity;

//using System.Web.Security;
//using System.Runtime.Remoting.Contexts;
using TeduCoreApp.Areas.Admin.Models.BusinessModel;

//using coderush.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

//using System.Web.Routing;
//using Microsoft.Owin.Security;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeduCoreApp.Data.EF;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Data.Entities;
using System.Security.Claims;
using TeduCoreApp.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;

namespace TeduCoreApp.Admin.Filter
{
    //this page will delization for Action allow Roles of User Current
    public class FilterActionAttribute : ActionFilterAttribute
    {
        public readonly AppDbContext db;
        //public readonly ControllerActionDescriptor Descriptor;

        public FilterActionAttribute(AppDbContext db/*, ControllerActionDescriptor Descriptor*/)
        {
            this.db = db;
            //this.Descriptor = Descriptor;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Get Name and Acion current
            var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName;
            var controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName;

            var IdUserCurrent = filterContext.HttpContext.User.GetSpecificClaim("UserId").ToString();
            //var IdUserCurrent = filterContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //Check Id User has login
            if (IdUserCurrent == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Notify/AccessDenied");
            }
            else
            {
                //filterContext.Result = new RedirectResult("/Admin/Notify/NotPermission");
                //Get Id User with Action has

                var FindIdUser = db.AppUserActions.Where(x => x.IdUser.ToString() == IdUserCurrent).ToList();

                if (FindIdUser == null)
                {
                    filterContext.Result = new RedirectResult("/Admin/Notify/NotUserInAction");
                }
                else
                {
                    var nameaction = "Admin" + "-" + controllerName + "Controller" + "-" + actionName;
                    var listpermission = db.AppUserActions.Where(x => x.IdUser.ToString() == IdUserCurrent && x.IsAllowed == true && x.ListAction.ActionName == nameaction).SingleOrDefault();

                    if (listpermission == null)
                    {
                        filterContext.Result = new RedirectResult("/Admin/Notify/NotPermission");
                        return;
                    }
                }
            }
        }
    }
}
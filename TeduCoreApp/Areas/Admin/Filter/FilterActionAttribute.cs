//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

////using System.Web.Mvc;
//using Microsoft.AspNetCore.Identity;

////using System.Web.Security;
////using System.Runtime.Remoting.Contexts;
//using TeduCoreApp.Areas.Admin.Models.BusinessModel;

////using coderush.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

////using System.Web.Routing;
////using Microsoft.Owin.Security;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace TeduCoreApp.Admin.Filter
//{
//    //this page will delization for Action allow Roles of User Current
//    public class FilterActionAttribute : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            //Get Name and Acion current

// string actionName = filterContext.ActionDescriptor.ActionName; string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

//            //Check Id User has login
//            if (IdUserCurrent == null)
//            {
//                filterContext.Result = new RedirectResult("/Admin/NotInPage/NotLogin");
//            }
//            else
//            {
//                PermissionDBContext db = new PermissionDBContext();
//                //Get Id User with Action has
//                var FindIdUser = db.AspNetUserActions.Where(x => x.IdUser == IdUserCurrent).ToList();
//                if (FindIdUser == null)
//                {
//                    filterContext.Result = new RedirectResult("/Admin/NotInPage/NotRoles");
//                }
//                else
//                {
//                    var nameaction = actionName + "Of" + controllerName + "Controller";
//                    var listpermission = from p in FindIdUser
//                                         where p.IsAllowed == true
//                                         select p.ListAction.ActionName;
//                    if (!listpermission.Contains(nameaction))
//                    {
//                        filterContext.Result = new RedirectResult("/Admin/NotInPage/Index");
//                        return;
//                    }
//                }
//            }
//        }
//    }
//}
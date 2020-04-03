using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Admin.Filter;
using TeduCoreApp.Areas.Admin.Models.BusinessModel;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(FilterActionAttribute))]
    public class GetControllerAndActionController : Controller
    {
        private AppDbContext _context;
        private Roles_Action_Controller _db = new Roles_Action_Controller();

        public GetControllerAndActionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Action()
        {
            return new OkObjectResult(_context.ListActions.ToList());
        }

        public ActionResult Controller()
        {
            return new OkObjectResult(_context.ListControllers.ToList());
        }

        [HttpPost]
        //Updates Controller and Action
        public ActionResult UpdateBusinessAdmin(string returnUrl = "Index")
        {
            //LAy danh sach Contorller va Action
            ReflectionController rc = new ReflectionController();
            List<Type> listControllerType = rc.GetControllers("TeduCoreApp.Areas.Admin.Controllers");
            List<string> listControllerOld = _context.ListControllers.Select(c => c.ControllerName).ToList();
            List<string> listActionOld = _context.ListActions.Select(p => p.ActionName).ToList();
            foreach (var c in listControllerType)
            {
                if (!listControllerOld.Contains(c.Name))
                {
                    string namecontroller = c.Name.ToString();
                    ListController b = new ListController() { ControllerName = namecontroller, Discription = "Chưa có mô tả" };
                    _context.ListControllers.Add(b);
                    _context.SaveChanges();
                }

                List<string> listAction = rc.GetActions(c);

                foreach (var p in listAction)
                {
                    if (!listActionOld.Contains(c.Name + "-" + p))
                    {
                        var controllerOld = _context.ListControllers.SingleOrDefault(x => x.ControllerName == c.Name);
                        ListAction action = new ListAction() { IdController = controllerOld.Id, ActionName = "Admin-" + c.Name + "-" + p, Discription = "Chưa có mô tả" };
                        _context.ListActions.Add(action);
                        _context.SaveChanges();
                    }
                }
            }
            _context.SaveChanges();
            TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'></span><span class='sr-only'></span>Cập Nhật thành công</div>";
            return RedirectToAction(returnUrl);
        }

        [HttpPost]
        public ActionResult UpdateBusinessClient(string returnUrl = "Index")
        {
            //LAy danh sach Contorller va Action
            ReflectionController rc = new ReflectionController();
            List<Type> listControllerType = rc.GetControllers("TeduCoreApp.Controllers");
            List<string> listControllerOld = _context.ListControllers.Select(c => c.ControllerName).ToList();
            List<string> listActionOld = _context.ListActions.Select(p => p.ActionName).ToList();
            foreach (var c in listControllerType)
            {
                if (!listControllerOld.Contains(c.Name))
                {
                    string namecontroller = c.Name.ToString();
                    ListController b = new ListController() { ControllerName = namecontroller, Discription = "Chưa có mô tả" };
                    _context.ListControllers.Add(b);
                    _context.SaveChanges();
                }

                List<string> listAction = rc.GetActions(c);

                foreach (var p in listAction)
                {
                    if (!listActionOld.Contains(c.Name + "-" + p))
                    {
                        var controllerOld = _context.ListControllers.SingleOrDefault(x => x.ControllerName == c.Name);
                        ListAction action = new ListAction() { IdController = controllerOld.Id, ActionName = "Client-" + c.Name + "-" + p, Discription = "Chưa có mô tả" };
                        _context.ListActions.Add(action);
                        _context.SaveChanges();
                    }
                }
            }
            _context.SaveChanges();
            TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'></span><span class='sr-only'></span>Cập Nhật thành công</div>";
            return RedirectToAction(returnUrl);
        }

        public ActionResult ActionOfController(int id)
        {
            var listAction = _context.ListActions.Where(x => x.IdController == id).ToList();
            return View(listAction);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserActionsController : Controller
    {
        private AppDbContext _context;

        public UserActionsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.AppUserActions.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIsAlowed(string UserId, string ActionId)
        {
            var userActon = await _context.AppUserActions.Where(x => x.IdAction.ToString() == ActionId && x.IdUser.ToString() == UserId).SingleOrDefaultAsync();
            var active = true;
            userActon.IsAllowed = active;
            _context.Update(userActon);
            _context.SaveChanges();
            return new ObjectResult(userActon);
        }
    }
}
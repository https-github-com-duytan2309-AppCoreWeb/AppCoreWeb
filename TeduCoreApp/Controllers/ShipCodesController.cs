using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Controllers
{
    public class ShipCodesController : Controller
    {
        private readonly AppDbContext _context;

        public IActionResult Index()
        {
            return View();
        }
    }
}
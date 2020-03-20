using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Controllers
{
    public class Streets1Controller : Controller
    {
        private readonly AppDbContext _context;

        public Streets1Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: Streets1
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Streets.Where(x => x.ProvinceId == 1);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Streets1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var street = await _context.Streets
                .Include(s => s.District)
                .Include(s => s.Province)
                .Include(s => s.Ward)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (street == null)
            {
                return NotFound();
            }

            return View(street);
        }

        // GET: Streets1/Create
        public IActionResult Create()
        {
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Code");
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "Code");
            ViewData["WardId"] = new SelectList(_context.Wards, "Id", "Code");
            return View();
        }

        // POST: Streets1/Create To protect from overposting attacks, please enable the specific
        // properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,Rank,ProvinceId,DistrictId,WardId,Status,Id")] Street street)
        {
            if (ModelState.IsValid)
            {
                _context.Add(street);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Code", street.DistrictId);
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "Code", street.ProvinceId);
            ViewData["WardId"] = new SelectList(_context.Wards, "Id", "Code", street.WardId);
            return View(street);
        }

        // GET: Streets1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var street = await _context.Streets.FindAsync(id);
            if (street == null)
            {
                return NotFound();
            }
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Code", street.DistrictId);
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "Code", street.ProvinceId);
            ViewData["WardId"] = new SelectList(_context.Wards, "Id", "Code", street.WardId);
            return View(street);
        }

        // POST: Streets1/Edit/5 To protect from overposting attacks, please enable the specific
        // properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Code,Name,Rank,ProvinceId,DistrictId,WardId,Status,Id")] Street street)
        {
            if (id != street.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(street);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreetExists(street.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Code", street.DistrictId);
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "Code", street.ProvinceId);
            ViewData["WardId"] = new SelectList(_context.Wards, "Id", "Code", street.WardId);
            return View(street);
        }

        // GET: Streets1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var street = await _context.Streets
                .Include(s => s.District)
                .Include(s => s.Province)
                .Include(s => s.Ward)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (street == null)
            {
                return NotFound();
            }

            return View(street);
        }

        // POST: Streets1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var street = await _context.Streets.FindAsync(id);
            _context.Streets.Remove(street);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreetExists(int id)
        {
            return _context.Streets.Any(e => e.Id == id);
        }
    }
}
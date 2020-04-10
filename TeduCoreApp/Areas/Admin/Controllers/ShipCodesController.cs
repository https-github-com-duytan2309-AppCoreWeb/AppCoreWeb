using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ShipCodesController : BaseController
    {
        private readonly AppDbContext _context;

        public ShipCodesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ShipCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShipCodes.ToListAsync());
        }

        // GET: Admin/ShipCodes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipCode = await _context.ShipCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipCode == null)
            {
                return NotFound();
            }

            return View(shipCode);
        }

        // GET: Admin/ShipCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ShipCodes/Create To protect from overposting attacks, please enable the
        // specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Carriers,DeliveryTime,CollectionFee,ZipCode,Total,IdAddress,Id")] ShipCode shipCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipCode);
        }

        // GET: Admin/ShipCodes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipCode = await _context.ShipCodes.FindAsync(id);
            if (shipCode == null)
            {
                return NotFound();
            }
            return View(shipCode);
        }

        // POST: Admin/ShipCodes/Edit/5 To protect from overposting attacks, please enable the
        // specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Carriers,DeliveryTime,CollectionFee,ZipCode,Total,IdAddress,Id")] ShipCode shipCode)
        {
            if (id != shipCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipCodeExists(shipCode.Id))
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
            return View(shipCode);
        }

        // GET: Admin/ShipCodes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipCode = await _context.ShipCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipCode == null)
            {
                return NotFound();
            }

            return View(shipCode);
        }

        // POST: Admin/ShipCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var shipCode = await _context.ShipCodes.FindAsync(id);
            _context.ShipCodes.Remove(shipCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipCodeExists(long id)
        {
            return _context.ShipCodes.Any(e => e.Id == id);
        }
    }
}
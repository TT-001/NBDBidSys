using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBD_TractionFive.Data;
using NBD_TractionFive.Models;

namespace NBD_TractionFive.Controllers
{
    public class inventoryVMsController : Controller
    {
        private readonly NBDContext _context;

        public inventoryVMsController(NBDContext context)
        {
            _context = context;
        }

        // GET: inventoryVMs
        public async Task<IActionResult> Index()
        {
            return View(await _context.inventoryVMs.ToListAsync());
        }

        // GET: inventoryVMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryVM = await _context.inventoryVMs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inventoryVM == null)
            {
                return NotFound();
            }

            return View(inventoryVM);
        }

        // GET: inventoryVMs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: inventoryVMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,quantityOnHands,name,price,type")] inventoryVM inventoryVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventoryVM);
        }

        // GET: inventoryVMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryVM = await _context.inventoryVMs.FindAsync(id);
            if (inventoryVM == null)
            {
                return NotFound();
            }
            return View(inventoryVM);
        }

        // POST: inventoryVMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,quantityOnHands,name,price,type")] inventoryVM inventoryVM)
        {
            if (id != inventoryVM.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!inventoryVMExists(inventoryVM.ID))
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
            return View(inventoryVM);
        }

        // GET: inventoryVMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryVM = await _context.inventoryVMs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inventoryVM == null)
            {
                return NotFound();
            }

            return View(inventoryVM);
        }

        // POST: inventoryVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryVM = await _context.inventoryVMs.FindAsync(id);
            _context.inventoryVMs.Remove(inventoryVM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool inventoryVMExists(int id)
        {
            return _context.inventoryVMs.Any(e => e.ID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AoS_Web_Application.Data;
using AoS_Web_Application.Models;
using System.Security.Claims;

namespace AoS_Web_Application.Controllers
{
    public class ArmiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArmiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var model = await _context.Army
                                .Where(a => a.UserId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                                .ToListAsync();
            return View(model);
        }

        //// GET: Armies
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Army.Include(a => a.IdentityUser);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: Armies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var army = await _context.Army
                .Include(a => a.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (army == null)
            {
                return NotFound();
            }

            return View(army);
        }

        // GET: Armies/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Armies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GrandAlliance,Name,Description,UserId")] Army army)
        {
            if (ModelState.IsValid)
            {
                _context.Add(army);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", army.UserId);
            return View(army);
        }

        // GET: Armies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var army = await _context.Army.FindAsync(id);
            if (army == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", army.UserId);
            return View(army);
        }

        // POST: Armies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GrandAlliance,Name,Description,UserId")] Army army)
        {
            if (id != army.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(army);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmyExists(army.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", army.UserId);
            return View(army);
        }

        // GET: Armies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var army = await _context.Army
                .Include(a => a.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (army == null)
            {
                return NotFound();
            }

            return View(army);
        }

        // POST: Armies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var army = await _context.Army.FindAsync(id);
            _context.Army.Remove(army);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmyExists(int id)
        {
            return _context.Army.Any(e => e.Id == id);
        }
    }
}

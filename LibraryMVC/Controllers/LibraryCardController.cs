using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMVC.Areas.Identity.Data;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    public class LibraryCardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibraryCardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LibraryCard
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LibraryCard.Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LibraryCard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LibraryCard == null)
            {
                return NotFound();
            }

            var libraryCard = await _context.LibraryCard
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LibraryCardId == id);
            if (libraryCard == null)
            {
                return NotFound();
            }

            return View(libraryCard);
        }

        // GET: LibraryCard/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email");
            return View();
        }

        // POST: LibraryCard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // TODO Zamien Bind na dtoCreate
        public async Task<IActionResult> Create([Bind("LibraryCardId,CardNumber,IssuedAt,ExpiryDate,UserId")] LibraryCard libraryCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libraryCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", libraryCard.UserId);
            return View(libraryCard);
        }

        // GET: LibraryCard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LibraryCard == null)
            {
                return NotFound();
            }

            var libraryCard = await _context.LibraryCard.FindAsync(id);
            if (libraryCard == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", libraryCard.UserId);
            return View(libraryCard);
        }

        // POST: LibraryCard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // TODO Zamien Bind na dtoEdit
        public async Task<IActionResult> Edit(int id, [Bind("LibraryCardId,CardNumber,IssuedAt,ExpiryDate,UserId")] LibraryCard libraryCard)
        {
            if (id != libraryCard.LibraryCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libraryCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryCardExists(libraryCard.LibraryCardId))
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
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", libraryCard.UserId);
            return View(libraryCard);
        }

        // GET: LibraryCard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LibraryCard == null)
            {
                return NotFound();
            }

            var libraryCard = await _context.LibraryCard
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LibraryCardId == id);
            if (libraryCard == null)
            {
                return NotFound();
            }

            return View(libraryCard);
        }

        // POST: LibraryCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LibraryCard == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LibraryCard'  is null.");
            }
            var libraryCard = await _context.LibraryCard.FindAsync(id);
            if (libraryCard != null)
            {
                _context.LibraryCard.Remove(libraryCard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryCardExists(int id)
        {
          return (_context.LibraryCard?.Any(e => e.LibraryCardId == id)).GetValueOrDefault();
        }
    }
}

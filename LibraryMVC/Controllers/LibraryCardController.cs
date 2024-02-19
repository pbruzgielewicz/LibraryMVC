using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMVC.Areas.Identity.Data;
using LibraryMVC.Dto;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddLibraryCardDto addLibraryCard)
        {
            if (ModelState.IsValid)
            {
                var libraryCardToAdd = new LibraryCard()
                {
                    CardNumber = addLibraryCard.CardNumber,
                    IssuedAt = addLibraryCard.IssuedAt,
                    ExpiryDate = addLibraryCard.ExpiryDate,
                    UserId = addLibraryCard.UserId
                };
                
                _context.Add(libraryCardToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email",addLibraryCard.UserId);
            return View();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditLibraryCardDto editLibraryCard)
        {
            if (ModelState.IsValid)
            {
                var currentLibraryCard = await _context.LibraryCard!.FirstOrDefaultAsync(x => x.LibraryCardId == editLibraryCard.LibraryCardId);
                currentLibraryCard!.CardNumber = editLibraryCard.CardNumber;
                currentLibraryCard.IssuedAt = editLibraryCard.IssuedAt;
                currentLibraryCard.ExpiryDate = editLibraryCard.ExpiryDate;
                currentLibraryCard.UserId = editLibraryCard.UserId;
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", editLibraryCard.UserId);
            return View();
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

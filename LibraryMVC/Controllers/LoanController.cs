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
    public class LoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Loan
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Loan.Include(l => l.Book).Include(l => l.LibraryCard);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Loan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loan == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Book)
                .Include(l => l.LibraryCard)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loan/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Title");
            ViewData["LibraryCardId"] = new SelectList(_context.LibraryCard, "LibraryCardId", "CardNumber");
            return View();
        }

        // POST: Loan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // TODO Zamien Bind na dtoCreate
        public async Task<IActionResult> Create([Bind("LoanId,BookId,LibraryCardId,LoanDate,DueDate,ReturnDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Title", loan.BookId);
            ViewData["LibraryCardId"] = new SelectList(_context.LibraryCard, "LibraryCardId", "CardNumber", loan.LibraryCardId);
            return View(loan);
        }

        // GET: Loan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loan == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Title", loan.BookId);
            ViewData["LibraryCardId"] = new SelectList(_context.LibraryCard, "LibraryCardId", "CardNumber", loan.LibraryCardId);
            return View(loan);
        }

        // POST: Loan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // TODO Zamien Bind na dtoEdit
        public async Task<IActionResult> Edit(int id, [Bind("LoanId,BookId,LibraryCardId,LoanDate,DueDate,ReturnDate")] Loan loan)
        {
            if (id != loan.LoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.LoanId))
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
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Title", loan.BookId);
            ViewData["LibraryCardId"] = new SelectList(_context.LibraryCard, "LibraryCardId", "CardNumber", loan.LibraryCardId);
            return View(loan);
        }

        // GET: Loan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loan == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Book)
                .Include(l => l.LibraryCard)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loan == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Loan'  is null.");
            }
            var loan = await _context.Loan.FindAsync(id);
            if (loan != null)
            {
                _context.Loan.Remove(loan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
          return (_context.Loan?.Any(e => e.LoanId == id)).GetValueOrDefault();
        }
    }
}
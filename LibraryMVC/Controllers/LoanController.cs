using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMVC.Areas.Identity.Data;
using LibraryMVC.Dto;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Authorization;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddLoanDto addLoan)
        {
            if (ModelState.IsValid)
            {
                var loanToAdd = new Loan()
                {
                    LoanDate = addLoan.LoanDate,
                    DueDate = addLoan.DueDate,
                    ReturnDate = addLoan.ReturnDate,
                    BookId = addLoan.BookId,
                    LibraryCardId = addLoan.LibraryCardId
                };
                
                _context.Add(loanToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Title", addLoan.BookId);
            ViewData["LibraryCardId"] = new SelectList(_context.LibraryCard, "LibraryCardId", "CardNumber", addLoan.LibraryCardId);
            return View();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditLoanDto editLoan)
        {
            if (ModelState.IsValid)
            {
                var currentLoan = await _context.Loan!.FirstOrDefaultAsync(x => x.LoanId == editLoan.LoanId);
                currentLoan!.LoanDate = editLoan.LoanDate;
                currentLoan.DueDate = editLoan.DueDate;
                currentLoan.ReturnDate = editLoan.ReturnDate;
                currentLoan.LibraryCardId = editLoan.LibraryCardId;
                currentLoan.BookId = editLoan.BookId;
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Title", editLoan.BookId);
            ViewData["LibraryCardId"] = new SelectList(_context.LibraryCard, "LibraryCardId", "CardNumber", editLoan.LibraryCardId);
            return View();
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

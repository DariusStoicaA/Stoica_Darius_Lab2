using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stoica_Darius_Lab2.Models;

namespace Stoica_Darius_Lab2.Pages.BorrowingPages;

public class EditModel : PageModel
{
    private readonly Stoica_Darius_Lab2Context _context;

    public EditModel(Stoica_Darius_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Borrowing == null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(b => b.Author)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (borrowing == null)
        {
            return NotFound();
        }
        Borrowing = borrowing;
        PopulateSelectLists();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            PopulateSelectLists();
            return Page();
        }

        _context.Attach(Borrowing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BorrowingExists(Borrowing.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private void PopulateSelectLists()
    {
        var bookList = _context.Book
            .Include(b => b.Author)
            .Select(b => new
            {
                b.ID,
                BookFullName = b.Title + " - " +
                    (b.Author == null ? "Unknown author" : b.Author.LastName + " " + b.Author.FirstName)
            });

        ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName", Borrowing.BookID);
        ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName", Borrowing.MemberID);
    }

    private bool BorrowingExists(int id)
    {
        return (_context.Borrowing?.Any(e => e.ID == id)).GetValueOrDefault();
    }
}
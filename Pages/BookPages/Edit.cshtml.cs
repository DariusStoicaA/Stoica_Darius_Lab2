using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stoica_Darius_Lab2.Models;

namespace Stoica_Darius_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly Stoica_Darius_Lab2Context _context;

    public EditModel(Stoica_Darius_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        Book = await _context.Book
 .Include(b => b.Publisher)
 .Include(b => b.BookCategories).ThenInclude(b => b.Category)
 .AsNoTracking()
 .FirstOrDefaultAsync(m => m.ID == id);

        var book = await _context.Book.FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        Book = book;
        PopulateAssignedCategoryData(_context, Book);

        ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "LastName");

        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
        "PublisherName");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int? id, string[]? selectedCategories)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookToUpdate = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            b => b.Title, b => b.AuthorID, b => b.Price, b => b.PublishingDate, b => b.PublisherID))
        {
            UpdateBookCategories(_context, selectedCategories ?? Array.Empty<string>(), bookToUpdate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(bookToUpdate.ID))
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

        // If we got here something failed; re-populate categories and dropdowns
        PopulateAssignedCategoryData(_context, bookToUpdate);
        ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "LastName");
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
        return Page();
    }

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}
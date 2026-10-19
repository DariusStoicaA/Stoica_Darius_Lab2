using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stoica_Darius_Lab2.Models;

namespace Stoica_Darius_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Stoica_Darius_Lab2Context _context;

    public CreateModel(Stoica_Darius_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
        "PublisherName");
        ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "LastName");
        var book = new Book();
        book.BookCategories = new List<BookCategory>();
        PopulateAssignedCategoryData(_context, book);
        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        if (!ModelState.IsValid)
        {
            // re-populate lists and assigned categories when returning to the page
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "LastName");
            var bookForPopulate = new Book();
            bookForPopulate.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, bookForPopulate);
            return Page();
        }

        var newBook = new Book();
        newBook.BookCategories = new List<BookCategory>();
        if (selectedCategories != null)
        {
            foreach (var cat in selectedCategories)
            {
                var catToAdd = new BookCategory
                {
                    CategoryID = int.Parse(cat)
                };
                newBook.BookCategories.Add(catToAdd);
            }
        }

        // attach the created category links to the bound Book and save
        Book.BookCategories = newBook.BookCategories;
        _context.Book.Add(Book);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");

    }
}

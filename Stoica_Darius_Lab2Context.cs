using Microsoft.EntityFrameworkCore;
using Stoica_Darius_Lab2.Models;

public class Stoica_Darius_Lab2Context : DbContext
{
    public Stoica_Darius_Lab2Context(DbContextOptions<Stoica_Darius_Lab2Context> options)
        : base(options)
    {
    }

    public DbSet<Book> Book { get; set; } = default!;
    public DbSet<Publisher> Publisher { get; set; } = default!;
    public DbSet<Author> Author { get; set; } = default!;
    public DbSet<Category> Category { get; set; } = default!;
    public DbSet<BookCategory> BookCategory { get; set; } = default!;
}

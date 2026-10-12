using Microsoft.EntityFrameworkCore;
using Stoica_Darius_Lab2.Models; 

public class Stoica_Darius_Lab2Context(DbContextOptions<Stoica_Darius_Lab2Context> options) : DbContext(options)
{
    public DbSet<Stoica_Darius_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Stoica_Darius_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Stoica_Darius_Lab2.Models.Author> Author { get; set; } = default!;
}
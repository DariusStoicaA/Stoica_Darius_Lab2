using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class Stoica_Darius_Lab2IdentityContext(DbContextOptions<Stoica_Darius_Lab2IdentityContext> options) : IdentityDbContext<Stoica_Darius_Lab2.Data.ApplicationUser>(options)
{
}

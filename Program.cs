using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stoica_Darius_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Stoica_Darius_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Stoica_Darius_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Stoica_Darius_Lab2Context' not found.")));

builder.Services.AddDbContext<Stoica_Darius_Lab2IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Stoica_Darius_Lab2IdentityContext") ?? throw new InvalidOperationException("Connection string 'Stoica_Darius_Lab2IdentityContext' not found.")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<Stoica_Darius_Lab2IdentityContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();